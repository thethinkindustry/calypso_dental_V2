using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;

namespace calypso_dental_V2
{
    public partial class Frm_print_search : MaterialForm
    {
        public Frm_print_search()
        {
            InitializeComponent();
        }
        public frm_main frm1;
        private void Frm_print_search_Load(object sender, EventArgs e)
        {

            this.prntDataTable.RefreshReport();
        }
        ReportDataSource rs = new Microsoft.Reporting.WinForms.ReportDataSource();
        private void prntDataTable_Load(object sender, EventArgs e)
        {
           
            

            this.prntDataTable.RefreshReport();
           
            rs.Name = "DataSet1";
            rs.Value = frm1.lst;
            prntDataTable.LocalReport.DataSources.Clear();
            prntDataTable.LocalReport.DataSources.Add(rs);
            prntDataTable.LocalReport.ReportEmbeddedResource = "calypso_dental_V2.ReportGenerator.CrystalReport.rpt_inproc.rdlc";
         /*   Microsoft.Reporting.WinForms.ReportParameter[] rParams = new Microsoft.Reporting.WinForms.ReportParameter[]
      {
             
             // new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter_lastpayment",frm1.p_info.dr_last_payment.ToString()+"₺")
      };*/
           


            //this.reportViewer1.RefreshReport();
            this.prntDataTable.RefreshReport();
        }

    }
}

