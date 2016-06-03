using System;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Threading;

namespace _2FAHealthCheckGUI
{
    //  Black = "#000000"
    //  White = "#FFFFFF", Tan = "#FCE5CC", Turquoise = "#CEF5FB"
    //  Pink = "#FCC7C7", Lavander = "#DAA5FB", LightBlue = "#99A5FF"
    //  LightGreen = "#B3FF92", LightYellow = "#FCF598", IceBlue = "#E5E5FF"
    //  DarkBlue = "#0000FF", LightGrey = "#C4C4C4"
    class ExcelWriter
    {
        Application xlApp;
        Workbook wb;
        Worksheet ws;

        public ExcelWriter()
        {
            xlApp = new Application();
            wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            ws = (Worksheet)wb.Worksheets[1];
            xlApp.Visible = false;
        }

        /// <summary>
        /// fill excelsheet with italic style, common for upper headers
        /// </summary>
        /// <param name="startCell">left upper cell</param>
        /// <param name="data">String array</param>
        public void writeHeader(string startCell, object[,] data)
        {
            Range workSheetRange = ws.Range[startCell, startCell];
            int x = data.GetLength(0);
            int y = data.GetLength(1);
            workSheetRange = workSheetRange.get_Resize(x, y);
            workSheetRange.set_Value(Missing.Value, data);
            workSheetRange.Font.Italic = true;
        }

        /// <summary>
        /// Fill excelsheet without formatting
        /// </summary>
        /// <param name="startCell">left upper cell</param>
        /// <param name="data">String array</param>
        public void fill(string startCell, String[,] data)
        {

            Range workSheetRange = ws.Range[startCell, startCell];
            int x = data.GetLength(0);
            int y = data.GetLength(1);
            workSheetRange = workSheetRange.get_Resize(x, y);

            workSheetRange.Style.Font.Size = 11;
            workSheetRange.set_Value(Missing.Value, data);
        }

        /// <summary>
        /// set filteroption for spezified range
        /// </summary>
        /// <param name="startCell">left top cell</param>
        /// <param name="endCell">bottum right cell</param>
        public void setFilter(String startCell, String endCell)
        {
            Range workSheetRange = ws.Range[startCell, endCell];
            workSheetRange.AutoFilter(1, Type.Missing, XlAutoFilterOperator.xlAnd, Type.Missing, true);
        }

        /// <summary>freezes rows
        /// </summary>
        ///<param name="startCell">left upper cell</param>
        /// <param name="endCell">right bottom cell</param>
        public void FreezeRange(string startCell, string endCell)
        {
            Range workSheetRange = ws.get_Range(startCell, endCell);
            workSheetRange.Select();
            ws.Application.ActiveWindow.FreezePanes = true;
        }

        /// <summary>
        ///  get the cellName (no index! starting at num 1)
        /// </summary>
        /// <param name="i">row</param>
        /// <param name="k">column</param>
        /// <returns>CellName as String</returns>
        public string getCellName(int i, int k)
        {
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            i = i > 0 ? i : 1;

            int p = k > 0 ? k - 1 : 0;
            int p1 = p / 26;
            int p2 = p % 26;

            String col = "A";
            if (p1 == 0)
            {
                col = "" + alphabet[p2];
            }
            else
            {
                col = alphabet[p1 - 1] + "" + alphabet[p2];
            }
            return col + i;
        }

        /// <summary>merges cells in specified range.
        /// </summary>
        /// <param name="startCell">left top cell</param>
        /// <param name="endCell">bottum right cell</param>
        public void mergeCells(string startCell, string endCell)
        {
            Range workSheetRange = ws.get_Range(startCell, endCell);
            workSheetRange = ws.get_Range(startCell, endCell);
            workSheetRange.Merge(System.Type.Missing);
        }

        /// <summary>
        /// optimize cell size, so that every Value is completely displayed
        /// </summary>
        /// <param name="startCell">top-left cell</param>
        /// <param name="endCell">buttom right cell</param>
        public void OptimizeCellSize(String startCell, String endCell)
        {
            Range workSheetRange = ws.get_Range(startCell, endCell);
            workSheetRange.Rows.AutoFit();
            workSheetRange.Columns.AutoFit();
        }

        /// <summary>
        /// format text:bold, font size 13p, Italic=false, centerAlingment
        /// </summary>
        /// <param name="startCell">left upper cell</param>
        /// <param name="endCell"> right bottom cell</param>
        public void setHeader(String startCell, String endCell)
        {
            Range workSheetRange = ws.get_Range(startCell, endCell);
            workSheetRange.Font.Bold = true;
            workSheetRange.Font.Size = 13;
            workSheetRange.Font.Italic = false;
            workSheetRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;  //center the text
        }

        /// <summary>
        /// save and close excel
        /// </summary>
        /// <param name="path">path of the file</param>
        public void Close(String path)
        {
            if (xlApp != null)
            {
                wb.SaveAs(path);
                this.xlApp.ActiveWorkbook.Save();
                xlApp.Workbooks.Close();
                xlApp.Quit();
                GC.WaitForPendingFinalizers();    
            }
        }

        /// <summary>
        /// Read from excel workbook
        /// </summary>
        /// <param name="sheet">worksheet</param>
        /// <param name="startCell">left upper cell</param>
        /// <param name="endCell">right bottom cell<param>
        /// <returns></returns>
        public String readExcelWorkbook(int sheet, int startCell, int endCell)
        {
            ws = (Worksheet)wb.Sheets[sheet];
            Range workSheetRange = ws.UsedRange;
            Range currentCell = ((Range)ws.Cells[startCell, endCell]);
            string value = currentCell.Value2 == null ? string.Empty : currentCell.Value2.ToString();
            return value;
        }

        /// <summary>applies specified color to specified range.</summary>
        /// <param name="startCell">left upper cell</param>
        /// <param name="endCell">right bottom cell<param>
        /// <param name="colorName">see colortable in excelWriter</param>               
        public void setCellColor(string startCell, string endCell, string colorName)
        {
            Range workSheetRange = ws.get_Range(startCell, endCell);
            workSheetRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorName));
        }

        /// <summary> draw the grid thicker </summary>
        /// <param name="startCell">top left cell</param>
        /// <param name="endCell">buttom right cell</param>
        public void drawGridLines(String startCell, String endCell)
        {
            Range workSheetRange = ws.get_Range(startCell, endCell);
            workSheetRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
        }

        /// <summary>
        /// draw the left border of a cell thick
        /// </summary>
        /// <param name="startCell">top left cell</param>
        /// <param name="endCell">buttom right cell</param>
        public void bigLeftGridLine(String startCell, String endCell)
        {
            Range workSheetRange = ws.get_Range(startCell, endCell);
            workSheetRange.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlSlantDashDot;
        }
    }
}
             