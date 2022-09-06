using System.ComponentModel;

namespace Framework.Core.Enums
{

    public enum DataStatusEnum
    {
        [Description("Approved")]
        Approved = 1,

        [Description("Pending")]
        Pending = 2,

        [Description("Invoiced")]
        Invoiced = 3,

        [Description("Posting")]
        Posting = 4,
    }
}
