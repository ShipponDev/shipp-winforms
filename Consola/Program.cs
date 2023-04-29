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

            try
            {

                Console.WriteLine("INICIO CONSOLA...");

                //============================================================
                // DECLARA VARIABLES
                //============================================================
                string pathInput = @"C:\SERVICIOS\CargasIniciales\File_Input\2022 Carga EERR_CORP Magnus v1.0.csv";
                string pathOutput = @"C:\SERVICIOS\CargasIniciales\File_Output\";
                char delimiter = ';';
                DataTable dtInput = GeneraDtInput();
                DataTable dtCtaAux = new DataTable();

                dtInput = ConvertCSVtoDataTable(pathInput, delimiter);

                //============================================================
                // FILTRO POR AUXILIAR CUENTA CONTABLE
                //============================================================
                dtCtaAux = dtInput.AsEnumerable().
                    Select(row =>
                    {
                        DataRow newRow = dtInput.NewRow();
                        newRow["auxiliar"] = row.Field<string>("auxiliar");
                        return newRow;
                    }).Distinct(DataRowComparer.Default).CopyToDataTable();

                //============================================================
                // ITERACION POR AUXILIAR CUENTA CONTABLE 
                //============================================================
                foreach (DataRow rowAux in dtCtaAux.Rows)
                {

                    string msj_consola = "";
                    string carp_auxiliar = "";
                    DataTable dtInputFiltrado = new DataTable();
                    

                    carp_auxiliar = rowAux["auxiliar"].ToString();

                    if (carp_auxiliar == "")
                    {

                        msj_consola = String.Format("Auxiliar '{0}' no se encuentra configurado", carp_auxiliar);
                        Console.WriteLine(msj_consola);
                        break;
                    }

                    dtInputFiltrado = dtInput.AsEnumerable()
                        .Where(r => r.Field<string>("auxiliar").ToString() == carp_auxiliar)
                        .CopyToDataTable();

                    msj_consola = String.Format("Auxiliar: {0}, Cantidad de registros: {1}", carp_auxiliar, dtInputFiltrado.Rows.Count.ToString());
                    Console.WriteLine(msj_consola);

                    //============================================================
                    // ITERACION POR MES
                    //============================================================
                    for (int i = 0; i < 12; i++)
                    {

                        string nombre_archivo = "";
                        string ruta_archivo = "";
                        string mes_carga = "";
                        string fec_doc = "";
                        int mes_iteracion = i + 1;
                        DateTime ult_dia_mes = DateTime.Today;
                        DataTable dtOutput = new DataTable();
                        dtOutput = GeneraDtOutput(carp_auxiliar);

                        //============================================================
                        // ITERACION PARA OBTENER ULTIMO DIA DEL MES SEGUN CABEZAL
                        //============================================================
                        foreach (DataColumn col in dtInputFiltrado.Columns)
                        {

                            string mes_carga_iteracion = col.ColumnName.ToString().Replace("mes_", "");

                            if (mes_carga_iteracion == mes_iteracion.ToString())
                            {

                                mes_carga = col.ColumnName.ToString();
                                int dia = DateTime.DaysInMonth(2022, mes_iteracion);
                                ult_dia_mes = new DateTime(2022, mes_iteracion, dia);

                            }

                        }

                        if (ult_dia_mes == DateTime.Today)
                        {

                            msj_consola = "No se puede generar archivo con la misma fecha de hoy";
                            Console.WriteLine(msj_consola);
                            return;

                        }

                        fec_doc = ult_dia_mes.ToString("dd-MM-yyyy");
                        nombre_archivo = String.Format("{0} CORP CARGA EERR {1}.xlsx", ult_dia_mes.ToString("yyyy-MM").Replace("-", "."), carp_auxiliar);
                        ruta_archivo = pathOutput + nombre_archivo;
                        msj_consola = String.Format("Se generará el archivo {0}", nombre_archivo.Replace(".xlsx", ""));

                        Console.WriteLine(msj_consola);

                        msj_consola = String.Format("Cantidad de cuentas a procesar: {0}", dtInputFiltrado.Rows.Count.ToString());
                        Console.WriteLine(msj_consola);


                        //============================================================
                        // ITERACION PARA CARGAR DATOS DEL EXCEL
                        //============================================================
                        foreach (DataRow row in dtInputFiltrado.Rows)
                        {

                            //string carp_auxiliar = "";
                            string cod_cta = "";
                            string cod_uni_contable = "";
                            string cod_dpto = "";
                            string cod_proyecto = "";
                            string tipo_auxiliar = "";
                            string cod_auxiliar = "";
                            string signo = "";
                            string cod_moneda = "";
                            string tc = "";
                            string importe = "";
                            string cod_tit = "";
                            string cod_doc = "";
                            string nro_doc = "";
                            string comentario = "";
                            string rut_tit = "";
                            string cod_ests = "";
                            string cod_articulo = "";
                            string cantidad = "";

                            //============================================================
                            // CONTABILIDAD DIRECTA O CTAS CTES
                            //============================================================
                            if (carp_auxiliar == "cpf_projobemp" || carp_auxiliar == "cps_ctasctes")
                            {

                                //carp_auxiliar = row["carp_auxiliar"].ToString();
                                cod_cta = row["cod_cta"].ToString();
                                cod_uni_contable = row["cod_uni_contable"].ToString();
                                cod_dpto = row["cod_dpto"].ToString();
                                cod_proyecto = "0";
                                tipo_auxiliar = "0";
                                cod_auxiliar = "0";
                                signo = Convert.ToInt32(row[mes_carga]) > 0 ? "1" : "-1";
                                cod_moneda = "1";
                                tc = "1";
                                importe = row[mes_carga].ToString();
                                cod_tit = row["cod_tit"].ToString();
                                cod_doc = "adig";
                                nro_doc = "1";
                                comentario = "Carga Inicial EERR CORP al " + fec_doc;
                                rut_tit = row["cod_tit"].ToString();

                                //SOLO COUNTRY 
                                cod_dpto = String.Format("'{0}.{1}", cod_dpto.Substring(0, 2), cod_dpto.Substring(2, 3));

                                //============================================================
                                // EVALUA IMPORTE
                                //============================================================
                                if (Convert.ToInt32(importe) != 0)
                                {

                                    dtOutput.Rows.Add(new object[] {
                                        cod_cta,
                                        fec_doc,
                                        cod_uni_contable,
                                        cod_dpto,
                                        cod_proyecto,
                                        tipo_auxiliar,
                                        cod_auxiliar,
                                        signo,
                                        cod_moneda,
                                        tc,
                                        Math.Abs(Convert.ToInt32(importe)),
                                        cod_tit,
                                        cod_doc,
                                        nro_doc,
                                        fec_doc,
                                        comentario,
                                        rut_tit,
                                        carp_auxiliar,
                                        importe

                                    });

                                }

                            }

                            //============================================================
                            // STOCK
                            //============================================================
                            if (carp_auxiliar == "cps_est_stock")
                            {

                                //carp_auxiliar = row["carp_auxiliar"].ToString();
                                cod_cta = row["cod_cta"].ToString();
                                cod_articulo = "'0"; //PARA EVITAR LA CONVERSIÓN A INT
                                cod_uni_contable = row["cod_uni_contable"].ToString();
                                cod_dpto = row["cod_dpto"].ToString();
                                cod_ests = "Vendido";
                                cantidad = "1";
                                signo = Convert.ToInt32(row[mes_carga]) > 0 ? "1" : "-1";
                                importe = row[mes_carga].ToString();
                                cod_tit = row["cod_tit"].ToString();
                                comentario = "Carga Inicial EERR CORP al " + fec_doc;

                                //SOLO COUNTRY 
                                cod_dpto = String.Format("'{0}.{1}", cod_dpto.Substring(0, 2), cod_dpto.Substring(2, 3));

                                //============================================================
                                // EVALUA IMPORTE
                                //============================================================
                                if (Convert.ToInt32(importe) != 0)
                                {

                                    dtOutput.Rows.Add(new object[] {
                                        cod_cta,
                                        cod_articulo,
                                        fec_doc,
                                        cod_uni_contable,
                                        cod_dpto,
                                        cod_ests,
                                        fec_doc,
                                        cantidad,
                                        Math.Abs(Convert.ToInt32(importe)),
                                        signo,
                                        cod_tit,
                                        comentario,
                                        carp_auxiliar,
                                        importe

                                    });

                                }

                            }

                        }

                        //============================================================
                        // EXPORTA A EXCEL
                        //============================================================
                        ConvertDataTableToExcel(dtOutput, ruta_archivo);

                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("FIN CONSOLA...");
                Console.ReadKey();
            }
            
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

                Console.WriteLine("Inicio generación excel...");

                //============================================================
                // GENERA EXCEL
                //============================================================
                UExcel Excel = new UExcel();
                Excel.GeneraExcelReporte(dt, pathFile, nameSheet, "");

                Console.WriteLine("Fin generación excel...");

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
        public static DataTable GeneraDtInput()
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
            dt.Columns.Add(new DataColumn("carp_auxiliar", typeof(string)));

            return dt;
        }

        /// <summary>
        /// DT FINAL
        /// </summary>
        /// <returns></returns>
        public static DataTable GeneraDtOutput(string carp_auxiliar)
        {
            DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn("cod_uni_contable", typeof(string)));
            //dt.Columns.Add(new DataColumn("cod_dpto", typeof(string)));
            //dt.Columns.Add(new DataColumn("cod_cta", typeof(string)));
            //dt.Columns.Add(new DataColumn("nom_cta", typeof(string)));
            //dt.Columns.Add(new DataColumn("monto", typeof(string)));

            if (carp_auxiliar == "cpf_projobemp" || carp_auxiliar == "cps_ctasctes")
            {

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
                dt.Columns.Add(new DataColumn("Carp Auxiliar", typeof(string)));
                dt.Columns.Add(new DataColumn("Importe", typeof(string)));

            }

            else if (carp_auxiliar == "cps_est_stock")
            {

                dt.Columns.Add(new DataColumn("Cuenta", typeof(string)));
                dt.Columns.Add(new DataColumn("Artículo", typeof(string)));
                dt.Columns.Add(new DataColumn("Fec Valor", typeof(string)));
                dt.Columns.Add(new DataColumn("UC", typeof(string)));
                dt.Columns.Add(new DataColumn("Depto", typeof(string)));
                dt.Columns.Add(new DataColumn("Cod Estado", typeof(string)));
                dt.Columns.Add(new DataColumn("Fec. Venc. Docto", typeof(string)));
                dt.Columns.Add(new DataColumn("Cantidad", typeof(string)));
                dt.Columns.Add(new DataColumn("Importe moneda origen", typeof(string)));
                dt.Columns.Add(new DataColumn("Signo", typeof(string)));
                dt.Columns.Add(new DataColumn("Cod. Titular", typeof(string)));
                dt.Columns.Add(new DataColumn("Comentario", typeof(string)));
                dt.Columns.Add(new DataColumn("Carp Auxiliar", typeof(string)));
                dt.Columns.Add(new DataColumn("Importe", typeof(string)));

            }

            return dt;
        }

        #region MODIFICAR PARA USO POR CRITERIO
        //public void ProcesarPorCriterio(DataTable dt, string pathOutput)
        //{
        //    try
        //    {

        //        //============================================================
        //        // FILTRO POR CAMPOS ESPECIFICOS
        //        //============================================================
        //        DataTable dtUContablexDepto = new DataTable();

        //        dtUContablexDepto = dt.AsEnumerable().
        //            Select(row =>
        //            {
        //                DataRow newRow = dt.NewRow();
        //                newRow["cod_uni_contable"] = row.Field<string>("cod_uni_contable");
        //                //newRow["cod_dpto"] = row.Field<string>("cod_dpto");
        //                return newRow;
        //            }).Distinct(DataRowComparer.Default).CopyToDataTable();

        //        foreach (DataRow row in dtUContablexDepto.Rows)
        //        {

        //            string msj_consola = "";
        //            string cod_uni_contable = "";

        //            cod_uni_contable = row["cod_uni_contable"].ToString();

        //            //============================================================
        //            // ITERACION POR MES
        //            //============================================================
        //            for (int i = 0; i < 12; i++)
        //            {

        //                string nombre_archivo = "";
        //                string ruta_archivo = "";
        //                string mes_doc = "";
        //                string fec_doc = "";
        //                int mes_iteracion = i + 1;


        //                DataTable dtFinal = GeneraDtOutput();

        //                DataTable dtExcel = new DataTable();
        //                dtExcel = dt.AsEnumerable()
        //                    .Where(r => r.Field<string>("cod_uni_contable").ToString() == cod_uni_contable)// && r.Field<string>("cod_dpto").ToString() == cod_dpto)
        //                    .CopyToDataTable();

        //                //============================================================
        //                // ITERACION PARA OBTENER ULTIMO DIA DEL MES
        //                //============================================================
        //                foreach (DataColumn col in dt.Columns)
        //                {

        //                    if (col.ColumnName.ToString().Replace("mes_", "") == mes_iteracion.ToString())
        //                    {

        //                        int dia = DateTime.DaysInMonth(2022, mes_iteracion);
        //                        mes_doc = new DateTime(2022, mes_iteracion, dia).ToString("yyyy-MM").Replace("-", ".");
        //                        fec_doc = new DateTime(2022, mes_iteracion, dia).ToString("dd-MM-yyyy");
        //                    }

        //                }

        //                nombre_archivo = String.Format("{0} CORP CARGA EERR.xlsx", mes_doc);
        //                ruta_archivo = pathOutput + nombre_archivo;

        //                Console.WriteLine("Generando archivo al " + fec_doc);

        //                foreach (DataRow row2 in dtExcel.Rows)
        //                {

        //                    string mes = "";
        //                    string carp_auxiliar = "";
        //                    string cod_cta = "";
        //                    string cod_dpto = "";
        //                    string cod_proyecto = "";
        //                    string tipo_auxiliar = "";
        //                    string cod_auxiliar = "";
        //                    string signo = "";
        //                    string cod_moneda = "";
        //                    string tc = "";
        //                    string importe = "";
        //                    string cod_tit = "";
        //                    string cod_doc = "";
        //                    string nro_doc = "";
        //                    string comentario = "";
        //                    string rut_tit = "";


        //                    mes = "mes_" + mes_iteracion.ToString();
        //                    carp_auxiliar = row2["carp_auxiliar"].ToString();
        //                    cod_cta = row2["cod_cta"].ToString();
        //                    cod_uni_contable = row2["cod_uni_contable"].ToString();
        //                    cod_dpto = row2["cod_dpto"].ToString();
        //                    cod_proyecto = "0";
        //                    tipo_auxiliar = "0";
        //                    cod_auxiliar = "0";
        //                    signo = Convert.ToInt32(row2[mes]) > 0 ? "1" : "-1";
        //                    cod_moneda = "1";
        //                    tc = "1";
        //                    importe = row2[mes].ToString();
        //                    cod_tit = "0";
        //                    cod_doc = "adig";
        //                    nro_doc = "1";
        //                    comentario = "Carga Inicial EERR CORP al " + fec_doc;
        //                    rut_tit = "0";


        //                    if (Convert.ToInt32(importe) != 0)
        //                    {

        //                        dtFinal.Rows.Add(new object[] {
        //                                cod_cta,
        //                                fec_doc,
        //                                cod_uni_contable,
        //                                cod_dpto,
        //                                cod_proyecto,
        //                                tipo_auxiliar,
        //                                cod_auxiliar,
        //                                signo,
        //                                cod_moneda,
        //                                tc,
        //                                Math.Abs(Convert.ToInt32(importe)),
        //                                cod_tit,
        //                                cod_doc,
        //                                nro_doc,
        //                                fec_doc,
        //                                comentario,
        //                                rut_tit,
        //                                carp_auxiliar,
        //                                importe

        //                        });

        //                        //if (auxiliar == "cpf_projobemp" || auxiliar == "cps_ctasctes")
        //                        //{
        //                        //    dtFinal.Rows.Add(new object[] {
        //                        //        row2["cod_uni_contable"],
        //                        //        row2["cod_dpto"],
        //                        //        row2["cod_cta"],
        //                        //        row2["nom_cta"],
        //                        //        row2[mes_archivo],
        //                        //    });
        //                        //}

        //                    }

        //                }

        //                ConvertDataTableToExcel(dtFinal, ruta_archivo);

        //                //StreamWriter streamWriter = new StreamWriter(ruta_archivo.Replace("xlsx","txt"));
        //                //streamWriter.Write("test");
        //                //streamWriter.Close();
        //                //streamWriter.Dispose();

        //            }

        //        }

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        #endregion

    }
}
