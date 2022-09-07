using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Framework.Core.Utility.Interfaces
{
   public interface IExcelServiceUtility
    {
        Task<FileContentResult> ExportAsExcelReportAsync(object dynamicData, string tableTitle = "N/A");
        Task<FileContentResult> ExportAsCSVReport(object dynamicData);
    }
}
