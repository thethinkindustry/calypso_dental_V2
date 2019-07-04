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
using System.Data.SqlClient;
using System.Globalization;
namespace calypso_dental_V2
{
    public partial class frm_add_proc : MaterialForm
    {
        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;
        SqlDataAdapter adapter;
        DataView dv;
        public frm_main frm1;
        string sql = null;
        string connetionString = null;
        private Settings settings = new Settings();
        public frm_add_proc()
        {
            InitializeComponent();

            connetionString = "Data Source=" + settings.data_source + "\\SQL_2014;Initial Catalog=" + settings.initial_catalog + ";Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            var radioButtons = grb_teeth.Controls.OfType<PictureBox>();
            foreach (PictureBox rb in radioButtons)
            {
                rb.MouseClick += new MouseEventHandler((o, a) => onClickList(rb));
            }
            try
            {
                cnn.Open();
                sql = "SELECT proc_name FROM tbl_proc";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_procces_bar.Items.Add(dataReader["proc_name"].ToString());
                }
                dataReader.Close();
                command.Dispose();
                sql = "SELECT color_name FROM tbl_color";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_color_bar.Items.Add(dataReader["color_name"].ToString());
                }
                dataReader.Close();
                command.Dispose();
                sql = "SELECT step_name FROM tbl_step";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_steps_bar.Items.Add(dataReader["step_name"].ToString());
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                cnn.Close();
                MessageBox.Show("hata :"+ex.Message);
                throw;
            }

        }
        public void onClickList(PictureBox rb)
        {
            if (rb.BackColor == Color.DarkSlateGray)
            {

                rb.BackColor = Color.Transparent;
            }
            else
            {
                rb.BackColor = Color.DarkSlateGray;
            }

        }
        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void btn_add_proc_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(cb_procces_bar.Text)|| string.IsNullOrEmpty(cb_color_bar.Text)|| string.IsNullOrEmpty(cb_steps_bar.Text)|| string.IsNullOrEmpty(txt_unit_price.Text))
            {
                MessageBox.Show("Kayıt İçin Tüm Alanlar Doldurulmalıdır.");
            }
            else
            {
                var picture = grb_teeth.Controls.OfType<PictureBox>();
                int counter = 0;
                string teeth = "";
                foreach (PictureBox pb in picture)
                {
                    if (pb.BackColor == Color.DarkSlateGray)
                    {
                        counter++;
                        teeth += pb.Name.ToString() + "/";
                    }
                }
                if (counter == 0)
                {
                    counter = 1;
                }
               
                this.Close();
            }
        }
    }
}
