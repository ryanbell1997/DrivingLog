using Application.LogEntrys;
using Domain;
using OfficeOpenXml;

namespace Application.Excel
{
    public class ExcelService : IExcelService
    {
        private readonly ILogEntryService _logEntryService;

        public ExcelService(ILogEntryService logEntryService)
        {
            _logEntryService = logEntryService;
        }

        public (MemoryStream stream, string Name) GetExcelDownloadByDate(DateTime monthYear)
        {
            List<LogEntry> lstLogEntries = _logEntryService.GetLogEntriesByDate(monthYear).ToList();
            MemoryStream stream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(lstLogEntries, true);
                workSheet.Column(2).Style.Numberformat.Format = "d-mmm-yy";
                workSheet.Column(3).Style.Numberformat.Format = "h:mm";
                workSheet.Column(4).Style.Numberformat.Format = "h:mm";
                workSheet.Column(5).Style.Numberformat.Format = "£#,##0.00";
                workSheet.Column(6).Style.Numberformat.Format = "h:mm";
                workSheet.Column(7).Style.Numberformat.Format = "0.00";
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells.AutoFitColumns();
                package.Save();
            }
            stream.Position = 0;
            var excelName = $"{monthYear.Month}-{monthYear.Year}-driving-log.xlsx";
            return (stream, excelName);
        }
    }
}
