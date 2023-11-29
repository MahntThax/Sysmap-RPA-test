using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sysmap_udemy_test.DatabaseModule
{
    /// <summary>
    /// Uses excel as a database for ease of use to an user be able to easily check the final result
    /// for ease, it creates a folder "Data" if there isn't in the application directory
    /// </summary>
    internal static class ExcelDatabase
    {
        /// <summary>
        /// Creates the new database file for that keyword
        /// </summary>
        /// <param name="keyword">the keyword used for the search</param>
        /// <returns>the path for the database file</returns>
        public static string CreateNewDatabaseDocument(string keyword)
        {
            string databaseDirectory = Directory.GetCurrentDirectory() + "\\Data";
            string currentDate = DateTime.Now.ToString("yyyy.MM.dd");

            if (!Directory.Exists(databaseDirectory))
            {
                Directory.CreateDirectory(databaseDirectory);
            }

            string databaseFile = Path.Combine(databaseDirectory, $"Search results for {keyword}");

            // only needs to create a new one if there isn't one for that keyword already
            if (!File.Exists(databaseFile))
            {
                using (SpreadsheetDocument newDatabase = SpreadsheetDocument.Create(databaseFile, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
                {
                    // creates a blank Workbookpart
                    WorkbookPart workbookPart = newDatabase.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    // add a blank WorksheetPart
                    WorksheetPart newWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    newWorksheetPart.Worksheet = new Worksheet(new SheetData());

                    Sheets sheets = workbookPart.Workbook.GetFirstChild<Sheets>() ?? workbookPart.Workbook.AppendChild(new Sheets());
                    string relationshipId = workbookPart.GetIdOfPart(newWorksheetPart);

                    // Get a unique ID for the new worksheet.
                    uint sheetId = 1;
                    if (sheets.Elements<Sheet>().Count() > 0)
                    {
                        sheetId = (sheets.Elements<Sheet>().Select(s => s.SheetId?.Value).Max() + 1) ?? (uint)sheets.Elements<Sheet>().Count() + 1;
                    }

                    // Append the new worksheet and associate it with the workbook.
                    Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = currentDate };
                    sheets.Append(sheet);
                }

            }

            return databaseFile;
        }

        public static void WriteResultsIntoDatabase(string databasePath, List<CourseModel> courses)
        {
            foreach(CourseModel courseModel in courses)
            {
                WriteNewEntry(databasePath, courseModel);
            }
        }

        private static void WriteNewEntry(string databasePath, CourseModel courseModel)
        {
            // Open the document for editing.
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(databasePath, true))
            {
                // Get the SharedStringTablePart. If it does not exist, create a new one.
                SharedStringTablePart shareStringPart;
                if (spreadSheet.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                {
                    shareStringPart = spreadSheet.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                }
                else
                {
                    shareStringPart = spreadSheet.WorkbookPart.AddNewPart<SharedStringTablePart>();
                }

                // Check if there is already the sheet for today
                string currentDate = DateTime.Now.ToString("yyyy.MM.dd");

                WorksheetPart currentDateSheetPart = GetWorksheetPartByName(spreadSheet, currentDate);
                // if it doesn't exist, create a new one
                if (currentDateSheetPart == null)
                {
                    CreateNewSheetForCurrentDate(spreadSheet, currentDate, shareStringPart);
                }

                // Insert the each of the course information into the SharedStringTablePart.
                int index = InsertSharedStringItem(courseModel.Name, shareStringPart);

                // check for the row index
                SheetData sheetData = currentDateSheetPart.Worksheet.GetFirstChild<SheetData>();
                int rowIndex = sheetData.Elements<Row>().Count() + 1;

                Cell cell = InsertCellInWorksheet("A", (uint)rowIndex, currentDateSheetPart);
                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                index = InsertSharedStringItem(courseModel.Description, shareStringPart);

                cell = InsertCellInWorksheet("B", (uint)rowIndex, currentDateSheetPart);
                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                index = InsertSharedStringItem(courseModel.Instructors, shareStringPart);

                cell = InsertCellInWorksheet("C", (uint)rowIndex, currentDateSheetPart);
                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                index = InsertSharedStringItem(courseModel.TotalHours, shareStringPart);

                cell = InsertCellInWorksheet("D", (uint)rowIndex, currentDateSheetPart);
                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                // Save the new worksheet.
                currentDateSheetPart.Worksheet.Save();
            }
        }

        private static void CreateNewSheetForCurrentDate(SpreadsheetDocument spreadSheet, string currentDate, SharedStringTablePart shareStringPart)
        {
            WorksheetPart currentDateSheetPart = InsertWorksheet(spreadSheet.WorkbookPart, currentDate);

            int indexHeader = InsertSharedStringItem("Name", shareStringPart);

            Cell cellHeader = InsertCellInWorksheet("A", 1, currentDateSheetPart);
            cellHeader.CellValue = new CellValue(indexHeader.ToString());
            cellHeader.DataType = new EnumValue<CellValues>(CellValues.SharedString);

            indexHeader = InsertSharedStringItem("Description", shareStringPart);

            cellHeader = InsertCellInWorksheet("B", 1, currentDateSheetPart);
            cellHeader.CellValue = new CellValue(indexHeader.ToString());
            cellHeader.DataType = new EnumValue<CellValues>(CellValues.SharedString);

            indexHeader = InsertSharedStringItem("Instructors", shareStringPart);

            cellHeader = InsertCellInWorksheet("C", 1, currentDateSheetPart);
            cellHeader.CellValue = new CellValue(indexHeader.ToString());
            cellHeader.DataType = new EnumValue<CellValues>(CellValues.SharedString);

            indexHeader = InsertSharedStringItem("Total hours", shareStringPart);

            cellHeader = InsertCellInWorksheet("D", 1, currentDateSheetPart);
            cellHeader.CellValue = new CellValue(indexHeader.ToString());
            cellHeader.DataType = new EnumValue<CellValues>(CellValues.SharedString);
        }

        // Given SpreadsheetDocument and the sheet name it search for it on the document
        // returns the WorksheetPart of it
        private static WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document, string sheetName)
        {
            var workbookPart = document.WorkbookPart;
            string relationshipId = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name.Equals(sheetName))?.Id;

            var worksheetPart = (WorksheetPart)workbookPart.GetPartById(relationshipId);

            return worksheetPart;
        }

        // Given text and a SharedStringTablePart, creates a SharedStringItem with the specified text 
        // and inserts it into the SharedStringTablePart. If the item already exists, returns its index.
        private static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            // If the part does not contain a SharedStringTable, create one.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;

            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem and return its index.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        // Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet. 
        // If the cell already exists, returns it. 
        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }

        // Given a WorkbookPart, inserts a new worksheet.
        private static WorksheetPart InsertWorksheet(WorkbookPart workbookPart, string sheetName)
        {
            // Add a new worksheet part to the workbook.
            WorksheetPart newWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());
            newWorksheetPart.Worksheet.Save();

            Sheets sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
            string relationshipId = workbookPart.GetIdOfPart(newWorksheetPart);

            // Get a unique ID for the new sheet.
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Count() > 0)
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            // Append the new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
            sheets.Append(sheet);
            workbookPart.Workbook.Save();

            return newWorksheetPart;
        }
    }
}
