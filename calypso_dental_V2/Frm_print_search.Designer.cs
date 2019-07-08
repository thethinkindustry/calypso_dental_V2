namespace calypso_dental_V2
{
    partial class Frm_print_search
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.prntDataTable = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ds_inproc = new calypso_dental_V2.ReportGenerator.DataSet.ds_inproc();
            this.dsinprocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.inprocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ds_inproc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsinprocBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inprocBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // prntDataTable
            // 
            this.prntDataTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.inprocBindingSource;
            this.prntDataTable.LocalReport.DataSources.Add(reportDataSource1);
            this.prntDataTable.LocalReport.ReportEmbeddedResource = "calypso_dental_V2.ReportGenerator.CrystalReport.rpt_inproc.rdlc";
            this.prntDataTable.Location = new System.Drawing.Point(-1, 64);
            this.prntDataTable.Name = "prntDataTable";
            this.prntDataTable.Size = new System.Drawing.Size(1534, 857);
            this.prntDataTable.TabIndex = 0;
            this.prntDataTable.Load += new System.EventHandler(this.prntDataTable_Load);
            // 
            // ds_inproc
            // 
            this.ds_inproc.DataSetName = "ds_inproc";
            this.ds_inproc.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsinprocBindingSource
            // 
            this.dsinprocBindingSource.DataSource = this.ds_inproc;
            this.dsinprocBindingSource.Position = 0;
            // 
            // inprocBindingSource
            // 
            this.inprocBindingSource.DataMember = "inproc";
            this.inprocBindingSource.DataSource = this.dsinprocBindingSource;
            // 
            // Frm_print_search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1534, 921);
            this.Controls.Add(this.prntDataTable);
            this.Name = "Frm_print_search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yazdırma Sayfası";
            this.Load += new System.EventHandler(this.Frm_print_search_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ds_inproc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsinprocBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inprocBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer prntDataTable;
        private System.Windows.Forms.BindingSource dsinprocBindingSource;
        private ReportGenerator.DataSet.ds_inproc ds_inproc;
        private System.Windows.Forms.BindingSource inprocBindingSource;
    }
}