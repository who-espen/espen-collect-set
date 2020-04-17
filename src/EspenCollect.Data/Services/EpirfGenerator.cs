namespace EspenCollect.Data.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using EspenCollect.Data.Repositories;
    using SpreadsheetLight;
    using Excel = Microsoft.Office.Interop.Excel;

    public class EpirfGenerator : IEpirfGenerator
    {
        private readonly IOnchoEpirfRepository _onchoEpirfRepository;

        public EpirfGenerator(IOnchoEpirfRepository onchoEpirf)
        {
            Argument.IsNotNull(() => onchoEpirf);

            _onchoEpirfRepository = onchoEpirf;
        }

        public async  Task GenerateEpirfAsync(string filePath)
        {
            Argument.IsNotNullOrEmpty(() => filePath);

            var onchoData = await _onchoEpirfRepository.GetAllEpirfOnchoAsync().ConfigureAwait(false);

            var excelApp = new Excel.Application();

            excelApp.Visible = false;

            var epirfWorkBook = excelApp.Workbooks.Open(filePath, ReadOnly: false);

            var onchoSheet = epirfWorkBook.Worksheets.get_Item("ONCHO") as Excel.Worksheet;

            onchoSheet.Unprotect();

            onchoSheet.Range["A8:AE8"].Copy(onchoSheet.Range[$"A8:AE{8 + onchoData.Count}"]);

            //Excel.Range source = onchoSheet.Range["A8:AE8"].Insert(Excel.XlInsertShiftDirection.xlShiftDown);
            //var dest = onchoSheet.Range["A9"];
            //source.Copy(dest);

            onchoSheet.Protect();

            //var excelapp = new Excel.Application();
            //excelapp.Workbooks.Add();
            //string path = "Your Excel Path";
            //Excel.Workbook workbook = excelapp.Workbooks.Open(path);
            //Excel.Worksheet workSheet = workbook.Worksheets.get_Item(1);
            //Excel.Range source = workSheet.Range["A9:L9"].Insert(Excel.XlInsertShiftDirection.xlShiftDown);
            //Excel.Range dest = workSheet.Range["F10"];
            //source.Copy(dest);

            //onchoSheet.Cells[8, "A"] = "ID Number";


            //var onchoSheet = new SLDocument(filePath, "ONCHO");

            //for (var i = 0; i <= onchoData.Count; i++){
            //    onchoSheet.SetCellValue($"A{i+8}", onchoData[0].TypeOfsurvey);
            //}


            //SLWorksheetStatistics stats1 = onchoSheet.GetWorksheetStatistics();


            //for (int j = 1; j < stats1.EndRowIndex; j++)
            //{
            //    var value = onchoSheet.GetCellValueAsString(0, j);

            //}

            //onchoSheet.SetCellValue("E6", "Let's party!!!!111!!!1");

            //onchoSheet.SelectWorksheet("Sheet3");
            //onchoSheet.SetCellValue("E6", "Before anyone calls the popo!");

            //onchoSheet.AddWorksheet("DanceFloor");
            //onchoSheet.SetCellValue("B4", "Who let the dogs out?");
            //onchoSheet.SetCellValue("B5", "Woof!");

            //onchoSheet.SaveAs(filePath);
            epirfWorkBook.Save();
            epirfWorkBook.Close(true);
            excelApp.Quit();
        }
    }
}
