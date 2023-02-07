namespace Application.Excel
{
    public interface IExcelService
    {
        public (MemoryStream stream, string Name) GetExcelDownloadByDate(DateTime monthYear);
    }
}
