using System;

namespace VDVI.Repository.Models
{
    public class Audit
    {
        public long CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
