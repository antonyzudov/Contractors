using System;

namespace ContractorsApp.Models
{
    class CrudeContractor : IEquatable<CrudeContractor>
    {
        public string account { get; set; }
        public string name { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string string1 { get; set; }
        public string string2 { get; set; }
        public string string3 { get; set; }
        public string string4 { get; set; }
        public string settlement_account { get; set; }
        public string bank1 { get; set; }
        public string bank2 { get; set; }
        public string BIK { get; set; }
        public string corr_account { get; set; }

        public bool Equals(CrudeContractor other)
        {
            if (this.account == other.account
             && this.name == other.name
             && this.INN == other.INN
             && this.KPP == other.KPP
             && this.string1 == other.string1
             && this.string2 == other.string2
             && this.string3 == other.string3
             && this.string4 == other.string4
             && this.settlement_account == other.settlement_account
             && this.bank1 == other.bank1
             && this.bank2 == other.bank2
             && this.BIK == other.BIK
             && this.corr_account == other.corr_account)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Contractor MakeContractor()
        {
            Contractor contractor = new Contractor();
            contractor.name = this.string1;
            if (contractor.name == "")
                contractor.name = this.name;
            contractor.INN = this.INN;
            contractor.KPP = this.KPP;
            contractor.settlement_account = this.settlement_account;
            contractor.bank = this.string3;
            if (contractor.bank == "")
                contractor.bank = this.bank1;
            contractor.city = this.string4;
            if (contractor.city == "")
                contractor.city = this.bank2;
            contractor.corr_account = this.corr_account;
            contractor.full_name = this.name;
            contractor.BIK = this.BIK;
            return contractor;
        }
    }
}