namespace UI.Forms
{
    partial class GenerarArchivo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.btnBuscarOutput = new System.Windows.Forms.Button();
            this.lblFileInput = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnBuscarInput = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblFileOutput = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txtLogEvent = new System.Windows.Forms.TextBox();
            this.grpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.txtLogEvent);
            this.grpBox.Controls.Add(this.btnBuscarOutput);
            this.grpBox.Controls.Add(this.lblFileInput);
            this.grpBox.Controls.Add(this.btnCancelar);
            this.grpBox.Controls.Add(this.btnEjecutar);
            this.grpBox.Controls.Add(this.txtInput);
            this.grpBox.Controls.Add(this.btnBuscarInput);
            this.grpBox.Controls.Add(this.progressBar1);
            this.grpBox.Controls.Add(this.lblFileOutput);
            this.grpBox.Controls.Add(this.txtOutput);
            this.grpBox.Location = new System.Drawing.Point(12, 12);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(635, 374);
            this.grpBox.TabIndex = 5;
            this.grpBox.TabStop = false;
            this.grpBox.Text = "uwu";
            // 
            // btnBuscarOutput
            // 
            this.btnBuscarOutput.Location = new System.Drawing.Point(495, 81);
            this.btnBuscarOutput.Name = "btnBuscarOutput";
            this.btnBuscarOutput.Size = new System.Drawing.Size(110, 30);
            this.btnBuscarOutput.TabIndex = 9;
            this.btnBuscarOutput.Text = "Seleccionar...";
            this.btnBuscarOutput.UseVisualStyleBackColor = true;
            // 
            // lblFileInput
            // 
            this.lblFileInput.AutoSize = true;
            this.lblFileInput.Location = new System.Drawing.Point(21, 46);
            this.lblFileInput.Name = "lblFileInput";
            this.lblFileInput.Size = new System.Drawing.Size(95, 16);
            this.lblFileInput.TabIndex = 2;
            this.lblFileInput.Text = "Archivo Origen";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(335, 317);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Location = new System.Drawing.Point(200, 317);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(100, 35);
            this.btnEjecutar.TabIndex = 6;
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(134, 43);
            this.txtInput.Name = "txtInput";
            this.txtInput.ReadOnly = true;
            this.txtInput.Size = new System.Drawing.Size(350, 22);
            this.txtInput.TabIndex = 0;
            // 
            // btnBuscarInput
            // 
            this.btnBuscarInput.Location = new System.Drawing.Point(495, 39);
            this.btnBuscarInput.Name = "btnBuscarInput";
            this.btnBuscarInput.Size = new System.Drawing.Size(110, 30);
            this.btnBuscarInput.TabIndex = 4;
            this.btnBuscarInput.Text = "Seleccionar...";
            this.btnBuscarInput.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(24, 135);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(581, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // lblFileOutput
            // 
            this.lblFileOutput.AutoSize = true;
            this.lblFileOutput.Location = new System.Drawing.Point(21, 88);
            this.lblFileOutput.Name = "lblFileOutput";
            this.lblFileOutput.Size = new System.Drawing.Size(104, 16);
            this.lblFileOutput.TabIndex = 3;
            this.lblFileOutput.Text = "Carpeta Destino";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(134, 85);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(350, 22);
            this.txtOutput.TabIndex = 1;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // txtLogEvent
            // 
            this.txtLogEvent.Location = new System.Drawing.Point(24, 181);
            this.txtLogEvent.Multiline = true;
            this.txtLogEvent.Name = "txtLogEvent";
            this.txtLogEvent.ReadOnly = true;
            this.txtLogEvent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogEvent.Size = new System.Drawing.Size(581, 111);
            this.txtLogEvent.TabIndex = 1;
            this.txtLogEvent.TextChanged += new System.EventHandler(this.txtLogEvent_TextChanged);
            // 
            // GenerarArchivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 398);
            this.Controls.Add(this.grpBox);
            this.Name = "GenerarArchivo";
            this.Text = "Generador de archivos";
            this.Load += new System.EventHandler(this.GenerarArchivo_Load);
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox grpBox;
        private System.Windows.Forms.Label lblFileInput;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnBuscarInput;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblFileOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnBuscarOutput;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtLogEvent;
    }
}

