using System;
using VDVI.Repository.Models.Common;

namespace VDVI.Repository.Dtos.Accounts
{
    public class RatePlanStatisticFutureDto : Audit
    {
        public string PropertyCode { get; set; }
        public DateTime? BusinessDate { get; set; }
        public string RatePlan { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalRevenueExcl { get; set; }
        public decimal RevenueStatCodeA { get; set; }
        public decimal RevenueStatCodeAExcl { get; set; }
        public decimal RevenueStatCodeB { get; set; }
        public decimal RevenueStatCodeBExcl { get; set; }
        public decimal RevenueStatCodeC { get; set; }
        public decimal RevenueStatCodeCExcl { get; set; }
        public decimal RevenueStatCodeD { get; set; }
        public decimal RevenueStatCodeDExcl { get; set; }
        public decimal RevenueStatCodeE { get; set; }
        public decimal RevenueStatCodeEExcl { get; set; }
        public decimal RevenueStatCodeF { get; set; }
        public decimal RevenueStatCodeFExcl { get; set; }
        public decimal RevenueStatCodeUndefined { get; set; }
        public decimal RevenueStatCodeUndefinedExcl { get; set; }
    }
}
