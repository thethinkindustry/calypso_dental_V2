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
    public partial class giris_panel : Form
    {
        public giris_panel()
        {
            InitializeComponent();
        }

        private void Giris_panel_Load(object sender, EventArgs e)
        {

        }

        private void MaterialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }

        private void MaterialFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            calypso_dental_V2.Properties.Settings.Default.db_name = tbox_dbname.Text;
            calypso_dental_V2.Properties.Settings.Default.db_pw = tbox_dbpw.Text;
            calypso_dental_V2.Properties.Settings.Default.db_user = tbox_dbusername.Text;
            calypso_dental_V2.Properties.Settings.Default.Save();
            db_panel.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tbox_dbname.Text = calypso_dental_V2.Properties.Settings.Default.db_name;
            tbox_dbusername.Text = calypso_dental_V2.Properties.Settings.Default.db_user;
            tbox_dbpw.Text = calypso_dental_V2.Properties.Settings.Default.db_pw;
            db_panel.Visible = true;
            
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
