using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.Repository.Dtos.Accounts
{
    public class Revenue
    {
        public double TypeA { get; set; }
        public double TypeB { get; set; }
        public double TypeC { get; set; }
        public double TypeD { get; set; }
        public double TypeE { get; set; }
        public double TypeF { get; set; }
        public double Undefined { get; set; }
        public double AverageDailyRate { get; set; }
        public double RevenuePerAvailableRoom { get; set; }
        public double SpentPerAvailableRoom { get; set; }
        public double TotalRevenue { get; set; }
    }
}
