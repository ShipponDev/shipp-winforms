using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;

namespace Utilidades
{
    public class UExcel
    {

        public bool GeneraExcelReporte(DataTable dataTable, string rutaExcel, string nombreHoja, string ReporType)
        {

            //============================================================
            // DECLARA VARIABLES GLOBALES
            //============================================================
            //bool retorno = false;
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            try
            {
                //============================================================
                // INICIA EXCEL Y GENERA OBJETO
                //============================================================
                excel = new Microsoft.Office.Interop.Excel.Application();


                //============================================================
                // HACE VISIBLE EXCEL
                //============================================================
                excel.Visible = false;
                excel.DisplayAlerts = false;

                //============================================================
                // GENERA LIBRO DE TRABAJO
                //============================================================
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                //============================================================
                // GENERA HOJA DE TRABAJO
                //============================================================
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = nombreHoja;


                //============================================================
                // RECORRE FILAS Y AGREGA VALORES A HOJA
                //============================================================
                int rowcount = 1;

                if (dataTable.Rows.Count > 0)
                {

                    //for (int x=0;x<dataTable.Columns.Count)
                    //excelSheet.Cells[0, 1] = dataTable.Columns[3 - 1].ColumnName;


                    /*

                    excelSheet.Cells[0, 1] = dataTable.Columns[3 - 1].ColumnName;
                    excelSheet.Cells.Font.Color = System.Drawing.Color.Black;
                    */
                }


                foreach (DataRow datarow in dataTable.Rows)
                {

                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 2)
                        {


                            excelSheet.Cells[1, i] = dataTable.Columns[i - 1].ColumnName;
                            excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                            if (i == dataTable.Columns.Count)
                            {
                                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[1, dataTable.Columns.Count]];
                                FormattingExcelCells(excelCellrange, "#305496", System.Drawing.Color.White, true);
                            }





                        }




                        //for alternate rows
                        if (rowcount > 2)
                        {
                            excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();


                            if (i == dataTable.Columns.Count)
                            {
                                if (rowcount % 2 == 0)
                                {

                                    excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    //FormattingExcelCells(excelCellrange, "#FFFFFF", System.Drawing.Color.Black, false);

                                }
                                else
                                {

                                    excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    //FormattingExcelCells(excelCellrange, "#B4C6E7", System.Drawing.Color.Black, false);

                                }

                            }

                        }

                    }


                    /*

                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 2)
                        {
                            Console.WriteLine(dataTable.Columns[i - 1].ColumnName);
                            excelSheet.Cells[1, i] = dataTable.Columns[i - 1].ColumnName;
                            excelSheet.Cells.Font.FontStyle = "Negrita";// [1, i]
                            excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                            FormattingExcelCells(excelCellrange, "#305496", System.Drawing.Color.White, false);
                            

                        }

                        

                        //for alternate rows
                        if (rowcount > 2)
                        {
                            excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();
                            excelSheet.Cells.Font.FontStyle = "Normal";// [1, i]

                            if (i == dataTable.Columns.Count)
                            {
                                if (rowcount % 2 == 0)
                                {
                                    excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    FormattingExcelCells(excelCellrange, "#B4C6E7", System.Drawing.Color.Black, false);
                                }

                            }
                        }

                    }
                    */

                }


                //============================================================
                // RECORRE FILAS Y AGREGA VALORES A HOJA
                //============================================================
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;




                excelworkBook.SaveAs(rutaExcel); ;
                excelworkBook.Close();
                excel.Quit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
                KillProcess();
            }

        }


        public static void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }

        public static void KillProcess()
        {

            Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                Console.WriteLine("Proceso: " + p.ProcessName);
                Console.WriteLine("Segundos: " + p.StartTime.ToString());

                if (!string.IsNullOrEmpty(p.ProcessName) && p.StartTime.AddSeconds(+10) < DateTime.Now)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }

        }
    }
}
