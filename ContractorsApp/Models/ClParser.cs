using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ContractorsApp.Models
{
    public class ClParser
    {
        private Encoding GetEncoding(string filepath)
        {
            string line = "";
            using (StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default))
            {
                for(int i = 0; i < 3; i++)
                {
                    line = sr.ReadLine();
                }
            }
            Regex encod_rx = new Regex(@"=(?<prop>[\s\S]*)$");
            Match m = encod_rx.Match(line);
            Encoding encoding = ClEncoding.EndcodingByProp(m.Groups["prop"].Value);
            return encoding;
        }

        private string _filepath;
        private Encoding _encoding;

        public ClParser(string filepath)
        {
            _filepath = filepath;
            _encoding = GetEncoding(filepath);
        }

        private string GetCrudeAtr(string atr, string line)
        {
            StringBuilder rx_str = new StringBuilder(
                "(" + atr + "=(?<" + atr + ">.*?)$)"
              , 100
            );
            Regex rx = new Regex(rx_str.ToString());
            Match m = rx.Match(line);
            return m.Groups[atr].Value;
        }

        private void UpdateCrudeAtribs(string prefix, string line, ref CrudeContractor crude)
        {
            string s;
            if (((s = GetCrudeAtr(prefix + "Счет", line)) != "")) crude.account = s;
            if (((s = GetCrudeAtr(prefix, line)) != "")) crude.name = s;
            if (((s = GetCrudeAtr(prefix + "ИНН", line)) != "")) crude.INN = s;
            if (((s = GetCrudeAtr(prefix + "КПП", line)) != "")) crude.KPP = s;
            if (((s = GetCrudeAtr(prefix + "1", line)) != "")) crude.string1 = s;
            if (((s = GetCrudeAtr(prefix + "2", line)) != "")) crude.string2 = s;
            if (((s = GetCrudeAtr(prefix + "3", line)) != "")) crude.string3 = s;
            if (((s = GetCrudeAtr(prefix + "4", line)) != "")) crude.string4 = s;
            if (((s = GetCrudeAtr(prefix + "РасчСчет", line)) != "")) crude.settlement_account = s;
            if (((s = GetCrudeAtr(prefix + "Банк1", line)) != "")) crude.bank1 = s;
            if (((s = GetCrudeAtr(prefix + "Банк2", line)) != "")) crude.bank2 = s;
            if (((s = GetCrudeAtr(prefix + "БИК", line)) != "")) crude.BIK = s;
            if (((s = GetCrudeAtr(prefix + "КоррСчет", line)) != "")) crude.corr_account = s;
        }

        private PaymentDoc ReadDoc(StreamReader sr)
        {
            PaymentDoc doc = new PaymentDoc();
            doc.payer = new CrudeContractor();
            doc.payee = new CrudeContractor();

            string s;
            Regex end_of_doc = new Regex(@"КонецДокумента");
            string line;
            while (true)
            {
                line = sr.ReadLine();
                if (line == null) break;
                if ((end_of_doc.Match(line)).Success) break;
                UpdateCrudeAtribs("Плательщик", line, ref doc.payer);
                UpdateCrudeAtribs("Получатель", line, ref doc.payee);
            }
            return doc;
        }

        public List<Contractor> GetContractors()
        {
            List<Contractor> list = new List<Contractor>();
            using (StreamReader sr = new StreamReader(_filepath, _encoding))
            {
                string line;
                Regex doc_rx = new Regex(@"СекцияДокумент=(.*?)");
                while ((line = sr.ReadLine()) != null)
                {
                    Match match = doc_rx.Match(line);
                    if (match.Success)
                    {
                        PaymentDoc doc = ReadDoc(sr);
                        Contractor payer = doc.payer.MakeContractor();
                        Contractor payee = doc.payee.MakeContractor();
                        if (!list.Contains(payer))
                            list.Add(payer);
                        if (!list.Contains(payee))
                            list.Add(payee);
                    }
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                list[i].id = i;
            }
            return list;
        }
    }
}