using System;
using System.ComponentModel.DataAnnotations.Schema;
using VDVI.Repository.Models;

namespace VDVI.Repository.DB
{
    [Table("[hce].[RatePlanStatistics_FutureAudit]")]
    public class DbRatePlanStatisticFutureAudit : Audit
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
