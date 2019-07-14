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
    public partial class Frm_settings : MaterialForm
    {
        Settings st = new Settings();
        public Frm_settings()
        {
            InitializeComponent();
            pnl_password.Visible = true;
            pnl_setting.Visible = false;
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (txt_password.Text==st.p_password)
            {
                pnl_password.Visible = false;
                pnl_setting.Visible = true;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            st.data_source = txt_datasource.Text;
            st.initial_catalog = txt_initialcatalog.Text;
            if (!string.IsNullOrEmpty(txt_update_pass.Text))
            {
                st.p_password = txt_password.Text;
            }
           
            st.Save();
            this.Close();
        }
    }
}
