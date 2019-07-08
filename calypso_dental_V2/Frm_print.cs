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

namespace calypso_dental_V2
{
    public partial class Frm_print : MaterialForm
    {
        public frm_main frm1;
        public Frm_print()
        {
            InitializeComponent();
        }
        Microsoft.Reporting.WinForms.ReportDataSource rs = new Microsoft.Reporting.WinForms.ReportDataSource();
        private void Frm_print_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            rs.Name = "DataSet1";
            rs.Value = frm1.lst_print;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rs);
            reportViewer1.LocalReport.ReportEmbeddedResource = "calypso_dental_V2.ReportGenerator.CrystalReport.rpt_print.rdlc";
            Microsoft.Reporting.WinForms.ReportParameter[] rParams = new Microsoft.Reporting.WinForms.ReportParameter[]
      {
              new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter_drname",frm1.p_info.dr_name.ToString() ),
              new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter_total_debt",frm1.p_info.Totaldebt.ToString()),
             // new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter_lastpayment",frm1.p_info.dr_last_payment.ToString()+"₺")
      };
            this.reportViewer1.LocalReport.SetParameters(rParams);
            
            //this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
