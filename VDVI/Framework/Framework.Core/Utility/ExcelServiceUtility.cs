using Framework.Core.Utility.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Utility
{
    public class ExcelServiceUtility : IExcelServiceUtility
    {
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public async Task<FileContentResult> ExportAsExcelReportAsync(object dynamicData, string tableTitle = "N/A")
        {
            var fileDownloadName = "excel-report.xlsx";

            var dataTable = ToBindDataTable(dynamicData);

            using var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add("Data");

            worksheet.Cells["A1"].LoadFromDataTable(dataTable, PrintHeaders: true);

            for (var col = 1; col < dataTable.Columns.Count + 1; col++)
            {
                worksheet.Column(col).AutoFit();
            }

            // style excel 
            var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: dataTable.Rows.Count + 1, toColumn: dataTable.Columns.Count), "Data");

            tbl.TableStyle = TableStyles.Dark9;

            var reportBytes = package.GetAsByteArray();

            var xlFileContent = new FileContentResult(reportBytes, XlsxContentType)
            {
                FileDownloadName = fileDownloadName
            };

            return await Task.FromResult(xlFileContent);
        }


        public async Task<FileContentResult> ExportAsCSVReport(object dynamicData)
        {
            var _csvContentType = "texst/csv";
            byte[] reportBytes;

            DataTable dataTable = new DataTable();
            dataTable = ToBindDataTable(dynamicData);

            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append(column.ToString() + ",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            reportBytes = Encoding.UTF8.GetBytes(fileContent.ToString());

            var csvFileContent = new FileContentResult(reportBytes, _csvContentType);

            return csvFileContent;
        }

        private DataTable ToBindDataTable(dynamic dynamicData)
        {
            DataTable dataTable = new DataTable();

            var json = JsonConvert.SerializeObject(dynamicData);

            dataTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

            return dataTable;
        }
    }
}
