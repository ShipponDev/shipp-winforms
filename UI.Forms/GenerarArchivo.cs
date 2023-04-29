using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace UI.Forms
{
    public partial class GenerarArchivo : Form
    {
        public GenerarArchivo()
        {
            InitializeComponent();

        }

        private void GenerarArchivo_Load(object sender, EventArgs e)
        {

            string msj = "Seleccione el archivo origen y la carpeta destino para comenzar ejecución";
            WriteEventLog(msj);

            btnBuscarInput.Click += new System.EventHandler(this.btnBuscarInput_Click);
            btnBuscarOutput.Click += new System.EventHandler(this.btnBuscarOutput_Click);
            btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // Start the BackgroundWorker
            //backgroundWorker1.RunWorkerAsync();

        }

        private void btnBuscarInput_Click(object sender, EventArgs e)
        {

            string message = "";
            string title = "";
            OpenFileDialog fdlg = new OpenFileDialog();
            string msj = "";

            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "CSV Files (*.csv)|*.csv";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtInput.Text = fdlg.FileName;
                msj = String.Format("Archivo origen '{0}'", fdlg.FileName);
                WriteEventLog(msj);
            }
            else
            {
                message = "No seleccionó ningún archivo";
                title = "Advertencia";
                DialogResult result = MessageBox.Show(message, title);
            }
        }

        private void btnBuscarOutput_Click(object sender, EventArgs e)
        {

            string message = "";
            string title = "";
            string msj = "";
            FolderBrowserDialog fdlg = new FolderBrowserDialog();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutput.Text = folderBrowserDialog1.SelectedPath;
                msj = String.Format("Carpeta destino '{0}'", folderBrowserDialog1.SelectedPath);
                WriteEventLog(msj);
            }
            else
            {
                message = "No seleccionó ninguna carpeta de destino";
                title = "Advertencia";
                DialogResult result = MessageBox.Show(message, title);
            }

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {

            string message = "Aun no está listo xD";
            string title = "Hapi hapi hapi";
            string msj = "Inicia ejecución";
            WriteEventLog(msj);

            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 40;

            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title);
        }

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    for (int i = 1; i <= 100; i++)
        //    {
        //        // Wait 100 milliseconds.
        //        Thread.Sleep(1);
        //        // Report progress.
        //        backgroundWorker1.ReportProgress(i);
        //    }
        //}

        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{

        //    // Change the value of the ProgressBar to the BackgroundWorker progress.
        //    progressBar1.Value = e.ProgressPercentage;
        //    // Set the text.
        //    this.Text = e.ProgressPercentage.ToString();

        //}

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string msj = "Se canceló la ejecución";
            WriteEventLog(msj);

            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void txtLogEvent_TextChanged(object sender, EventArgs e)
        {

        }

        private void WriteEventLog(string msj)
        {
            string date = DateTime.Now.ToString("dd-MM-yyy hh:mm:ss");
            string eventLogMessage = string.Format("[{0}]: {1}\r\n", date, msj);
            txtLogEvent.AppendText(eventLogMessage);
        }
    }
}
