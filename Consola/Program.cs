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
using System.Reflection;
using System.ComponentModel;
using System.Drawing;

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
                string pathOutput = @"C:\Users\Cassandra Marchant\Desktop\PruebaConsola\";
                char delimiter = ';';
                DataTable dt = GeneraDt();

                dt = ConvertCSVtoDataTable(path, delimiter);

                //foreach (DataRow row in dt.Rows)
                //{
                //    Console.WriteLine(row["correlativo"]);

                //}

                //APLICA FILTROS
                DataTable dtUContablexDepto = new DataTable();

                dtUContablexDepto = dt.AsEnumerable().
                    Select(row =>
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["cod_uni_contable"] = row.Field<string>("cod_uni_contable");
                        //newRow["cod_dpto"] = row.Field<string>("cod_dpto");
                        return newRow;
                    }).Distinct(DataRowComparer.Default).CopyToDataTable();

                //d.Field<string>("cod_uni_contable").ToString() == d.Field<string>("cod_uni_contable").ToString() }).ToList();
                ///.Select(m => new { m.Field<string>("cod_uni_contable"), m.Field<string>("cod_dpto") });

                foreach (DataRow row in dtUContablexDepto.Rows)
                {

                    string cod_uni_contable = "";
                    string cod_dpto = "";
                    string mensaje = "";

                    cod_uni_contable = row["cod_uni_contable"].ToString();
                    cod_dpto = row["cod_dpto"].ToString();
                    mensaje = string.Format("Unidad Contable: {0}, Centro Costo: {1}", cod_uni_contable, cod_dpto);

                    Console.WriteLine(mensaje);

                    //LOOP POR MES
                    for (int i = 0; i < 12; i++) {

                        int mes = i + 1;
                        string fileName = "";
                        string finalPath = "";
                        string mes_archivo = "mes_" + mes.ToString();
                        
                        fileName = "2022_Carga_inicial_" + cod_uni_contable + "_" + cod_dpto + "_" + mes_archivo + "_" + DateTime.Today.ToString("yyyy_MM_dd") + ".xlsx";
                        finalPath = pathOutput + fileName;
                        Console.WriteLine(mensaje + mes_archivo);

                        DataTable dtExcel = new DataTable();

                        dtExcel = dt.AsEnumerable()
                            .Where(r => r.Field<string>("cod_uni_contable").ToString() == cod_uni_contable)// && r.Field<string>("cod_dpto").ToString() == cod_dpto)
                            .CopyToDataTable();

                        //dtExcel = dt.AsEnumerable()
                        //    .Where(r => r.Field<string>("cod_uni_contable").ToString() == cod_uni_contable)// && r.Field<string>("cod_dpto").ToString() == cod_dpto)
                        //    .Select(r =>
                        //    {
                        //        DataRow newRow = dt.NewRow();
                        //        newRow["cod_uni_contable"] = r.Field<string>("cod_uni_contable");
                        //        newRow["cod_dpto"] = r.Field<string>("cod_dpto");
                        //        newRow["cod_cta"] = r.Field<string>("cod_cta");
                        //        newRow["nom_cta"] = r.Field<string>("nom_cta");
                        //        newRow[mes_archivo] = r.Field<string>(mes_archivo);
                        //        return newRow;
                        //    }).CopyToDataTable();

                        //var query = (from emp in dtExcel.AsEnumerable()
                        //             select new
                        //             {
                        //                 cod_uni_contable = emp.Field<string>("cod_uni_contable"),
                        //                 cod_dpto = emp.Field<string>("cod_dpto"),//variable
                        //                 cod_cta = emp.Field<string>("cod_cta"),//variable
                        //                 nom_cta = emp.Field<string>("nom_cta"),//variable
                        //                 valor = emp.Field<string>("mes_archivo")//variable
                        //             }).ToList();

                        DataTable dtFinal = GeneraDtFinal();

                        foreach (DataRow row2 in dtExcel.Rows)
                        {

                            string carp_auxiliar = "";
                            string cod_cta = "";
                            string fec_doc = "";
                            string cod_proyecto = "0";
                            string tipo_aux = "0";
                            string cod_auxiliar = "0";
                            string signo = "";
                            string cod_moneda = "1";
                            string tc = "1";
                            string importe = "";
                            string cod_tit = "0";
                            string cod_doc = "adig";
                            string nro_doc = "1";
                            string comentario = "";
                            string rut = "0";

                            carp_auxiliar = row2["carp_auxiliar"].ToString();

                            if (Convert.ToInt32(row2[mes_archivo]) != 0)
                            {

                                dtFinal.Rows.Add(new object[] {
                                        cod_cta,
                                        fec_doc,
                                        cod_uni_contable,
                                        cod_dpto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,
                                        cod_proyecto,

                                });

                                //if (auxiliar == "cpf_projobemp" || auxiliar == "cps_ctasctes")
                                //{
                                //    dtFinal.Rows.Add(new object[] {
                                //        row2["cod_uni_contable"],
                                //        row2["cod_dpto"],
                                //        row2["cod_cta"],
                                //        row2["nom_cta"],
                                //        row2[mes_archivo],
                                //    });
                                //}

                            }
                            
                        }

                        ConvertDataTableToExcel(dtFinal, finalPath);
                    }

                }
                //DataView dataView = dt.DefaultView;



                //ConvertDataTableToExcel(dt, pathOutput);

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
        /// LIST TO DATATABLE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ListToDataTable<T>(IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();

                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                object[] values = new object[props.Count];

                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }

                return table;
            }
            catch
            {
                return null;
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
            dt.Columns.Add(new DataColumn("auxiliar", typeof(string)));

            return dt;
        }

        /// <summary>
        /// DT FINAL
        /// </summary>
        /// <returns></returns>
        public static DataTable GeneraDtFinal()
        {
            DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn("cod_uni_contable", typeof(string)));
            //dt.Columns.Add(new DataColumn("cod_dpto", typeof(string)));
            //dt.Columns.Add(new DataColumn("cod_cta", typeof(string)));
            //dt.Columns.Add(new DataColumn("nom_cta", typeof(string)));
            //dt.Columns.Add(new DataColumn("monto", typeof(string)));

            dt.Columns.Add(new DataColumn("Cuenta", typeof(string)));
            dt.Columns.Add(new DataColumn("Fecha Emision", typeof(string)));
            dt.Columns.Add(new DataColumn("UC", typeof(string)));
            dt.Columns.Add(new DataColumn("Depto", typeof(string)));
            dt.Columns.Add(new DataColumn("Proyecto", typeof(string)));
            dt.Columns.Add(new DataColumn("Tipo Auxiliar", typeof(string)));
            dt.Columns.Add(new DataColumn("Auxiliar", typeof(string)));
            dt.Columns.Add(new DataColumn("signo", typeof(string)));
            dt.Columns.Add(new DataColumn("Simbolo MI", typeof(string)));
            dt.Columns.Add(new DataColumn("TC ING", typeof(string)));
            dt.Columns.Add(new DataColumn("Importe moneda origen", typeof(string)));
            dt.Columns.Add(new DataColumn("Cod. Titular", typeof(string)));
            dt.Columns.Add(new DataColumn("Tipo Docto", typeof(string)));
            dt.Columns.Add(new DataColumn("Nun. Docto", typeof(string)));
            dt.Columns.Add(new DataColumn("F- Venc- Docto", typeof(string)));
            dt.Columns.Add(new DataColumn("Comentario", typeof(string)));
            dt.Columns.Add(new DataColumn("Rut", typeof(string)));
            dt.Columns.Add(new DataColumn("Auxiliar", typeof(string)));

            return dt;
        }
    }
}
