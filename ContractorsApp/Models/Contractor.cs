using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractorsApp.Models
{
    public class Contractor : IEquatable<Contractor>
    {
        public int id { get; set; }
        public string name { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string settlement_account { get; set; }
        public string bank { get; set; }
        public string city { get; set; }
        public string corr_account { get; set; }
        public string full_name { get; set; }
        public string BIK { get; set; }

        public bool Equals(Contractor other)
        {
            if (this.name == other.name
             && this.INN == other.INN
             && this.KPP == other.KPP
             && this.settlement_account == other.settlement_account
             && this.bank == other.bank
             && this.city == other.city
             && this.corr_account == other.corr_account
             && this.full_name == other.full_name
             && this.BIK == other.BIK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}