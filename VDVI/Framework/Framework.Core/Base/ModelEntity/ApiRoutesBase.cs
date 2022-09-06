namespace Framework.Core.Base.ModelEntity
{
    public abstract class ApiRoutesBase
    {
        public const string Register = "register";
        public const string Update = "update";
        public const string Delete = "delete/{id}";
        public const string GetById = "getById/{id}";
        public const string GetDropDown = "getAll/dropdown";
        public const string GetAllByCompanyId = "getAll";
        public const string DownloadById = "download/{id}";
        public const string ExportExcelFile = "exportExcelFile";
        public const string ExportCSVFile = "exportCsvFile";
    }
}