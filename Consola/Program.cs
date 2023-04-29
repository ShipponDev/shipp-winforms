using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Data;
using CsvHelper;
using System.Globalization;
using Utilidades;

namespace Consola
{
    public class Program
    {
        /// <summary>
        /// MAIN
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {

            Console.WriteLine("INICIO CONSOLA...");

            try
            {

                string path = @"C:\Users\Cassandra Marchant\Desktop\PruebaConsola\2022 Carga EERR_CORP Magnus v1.0.csv";
                string pathOutput = @"C:\Users\Cassandra Marchant\Desktop\PruebaConsola\2022 Carga EERR_CORP Magnus v1.0_" + DateTime.Today.ToString("yyyy_MM_dd") + ".xlsx";
                char delimiter = ';';
                DataTable dt = GeneraDt();

                dt = ConvertCSVtoDataTable(path, delimiter);

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["correlativo"]);

                }

                ConvertDataTableToExcel(dt, pathOutput);

            }
            catch
            {
                throw;
            }

            Console.WriteLine("FIN CONSOLA...");
            Console.ReadKey();
        }

        /// <summary>
        /// LEE CSV
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static DataTable ConvertCSVtoDataTable(string strFilePath, char delimiter)
        {

            DataTable dt = new DataTable();

            using (StreamReader sr = new StreamReader(strFilePath))
            {

                string[] headers = sr.ReadLine().Split(delimiter);

                foreach (string header in headers)
                {

                    dt.Columns.Add(header);

                }

                while (!sr.EndOfStream)
                {

                    string[] rows = sr.ReadLine().Split(delimiter);
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < headers.Length; i++)
                    {

                        dr[i] = rows[i];

                    }

                    dt.Rows.Add(dr);
                }

            }


            return dt;
        }

        /// <summary>
        /// GENERA REPORTE EXCEL
        /// </summary>
        /// <param name="dtReporte"></param>
        public static void ConvertDataTableToExcel(DataTable dt, string pathFile)
        {

            //============================================================
            // DECLARA VARIABLES
            //============================================================
            string nameSheet = "Data";

            try
            {

                Console.WriteLine("INICIO EXCEL");

                //============================================================
                // GENERA EXCEL
                //============================================================
                UExcel Excel = new UExcel();
                Excel.GeneraExcelReporte(dt, pathFile, nameSheet, "");

                Console.WriteLine("FIN EXCEL");

            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// CABECERA CSV
        /// </summary>
        /// <returns></returns>
        public static DataTable GeneraDt()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("correlativo", typeof(string)));
            dt.Columns.Add(new DataColumn("cod_uni_contable", typeof(string)));
            dt.Columns.Add(new DataColumn("cod_dpto", typeof(string)));
            dt.Columns.Add(new DataColumn("cod_cta", typeof(string)));
            dt.Columns.Add(new DataColumn("nom_cta", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_1", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_2", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_3", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_4", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_5", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_6", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_7", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_8", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_9", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_10", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_11", typeof(string)));
            dt.Columns.Add(new DataColumn("mes_12", typeof(string)));
            dt.Columns.Add(new DataColumn("total", typeof(string)));

            return dt;
        }
    }
}
