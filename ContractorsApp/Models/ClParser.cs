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
            string[] lines = File.ReadAllLines(filepath);
            Regex encod_rx = new Regex(@"=(?<prop>[\s\S]*)$");
            Match m = encod_rx.Match(lines[2]);
            Encoding encoding = ClEncoding.EndcodingByProp(m.Groups["prop"].Value);
            return encoding;
        }

        private string[] attribs_to_find =
        {
                  "Счет"
                , ""
                , "ИНН"
                , "КПП"
                , "1"
                , "2"
                , "3"
                , "4"
                , "РасчСчет"
                , "Банк1"
                , "Банк2"
                , "БИК"
                , "КоррСчет"
        };
        private string _filepath;
        private Encoding _encoding;

        public ClParser(string filepath)
        {
            _filepath = filepath;
            _encoding = GetEncoding(filepath);
        }

        public List<Contractor> GetContractors()
        {
            string data = File.ReadAllText(_filepath, _encoding);
            List<string> attribs = new List<string>();
            foreach (string atr_to_find in attribs_to_find)
            {
                attribs.Add("Плательщик" + atr_to_find);
                attribs.Add("Получатель" + atr_to_find);
            };
            string attribs_rx = "";
            foreach (string atr in attribs)
            {
                attribs_rx += "(" + atr + "=(?<" + atr + ">.*?)\r\n)|";
            };
            Regex rx = new Regex(@"(?<doc>СекцияДокумент="
                                + @"("
                                  + attribs_rx
                                  + @"((.*?)\n)"
                                + @")*?"
                                + @"КонецДокумента)");
            MatchCollection matches = rx.Matches(data);
            List<CrudeContractor> crudes = new List<CrudeContractor>();
            foreach (Match match in matches)
            {
                CrudeContractor payer = new CrudeContractor()
                              , payee = new CrudeContractor();

                payer.account = match.Groups["ПлательщикСчет"].Value;
                payer.name = match.Groups["Плательщик"].Value;
                payer.INN = match.Groups["ПлательщикИНН"].Value;
                payer.KPP = match.Groups["ПлательщикКПП"].Value;
                payer.string1 = match.Groups["Плательщик1"].Value;
                payer.string2 = match.Groups["Плательщик2"].Value;
                payer.string3 = match.Groups["Плательщик3"].Value;
                payer.string4 = match.Groups["Плательщик4"].Value;
                payer.settlement_account = match.Groups["ПлательщикРасчСчет"].Value;
                payer.bank1 = match.Groups["ПлательщикБанк1"].Value;
                payer.bank2 = match.Groups["ПлательщикБанк2"].Value;
                payer.BIK = match.Groups["ПлательщикБИК"].Value;
                payer.corr_account = match.Groups["ПлательщикКоррСчет"].Value;

                payee.account = match.Groups["ПолучательСчет"].Value;
                payee.name = match.Groups["Получатель"].Value;
                payee.INN = match.Groups["ПолучательИНН"].Value;
                payee.KPP = match.Groups["ПолучательКПП"].Value;
                payee.string1 = match.Groups["Получатель1"].Value;
                payee.string2 = match.Groups["Получатель2"].Value;
                payee.string3 = match.Groups["Получатель3"].Value;
                payee.string4 = match.Groups["Получатель4"].Value;
                payee.settlement_account = match.Groups["ПолучательРасчСчет"].Value;
                payee.bank1 = match.Groups["ПолучательБанк1"].Value;
                payee.bank2 = match.Groups["ПолучательБанк2"].Value;
                payee.BIK = match.Groups["ПолучательБИК"].Value;
                payee.corr_account = match.Groups["ПолучательКоррСчет"].Value;

                if (!crudes.Contains(payer))
                {
                    crudes.Add(payer);
                }
                if (!crudes.Contains(payee))
                {
                    crudes.Add(payee);
                }
            }

            List<Contractor> contactors = new List<Contractor>();

            foreach (CrudeContractor crude in crudes)
            {
                Contractor contractor = crude.MakeContractor();
                if (!contactors.Contains(contractor))
                {
                    contactors.Add(contractor);
                }
            }

            for (int i = 0; i < contactors.Count; i++)
            {
                contactors[i].id = i + 1;
            }

            return contactors;
        }
    }
}