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
        cs_error error = new cs_error();
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
                    cb_color_bar.Items.Add(dataReader["color_name"].ToString().Trim());
                }
                dataReader.Close();
                command.Dispose();
                sql = "SELECT step_name FROM tbl_step";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_steps_bar.Items.Add(dataReader["step_name"].ToString().Trim());
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
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
            
            if (string.IsNullOrEmpty(cb_procces_bar?.SelectedItem?.ToString()) || string.IsNullOrEmpty(cb_color_bar.SelectedItem?.ToString()) || string.IsNullOrEmpty(cb_steps_bar?.SelectedItem?.ToString()) || string.IsNullOrEmpty(txt_unit_price.Text))
            {
                MessageBox.Show("Kayıt İçin Tüm Alanlar Doldurulmalıdır.");
            }
            else
            {
                DialogResult ms = MessageBox.Show("Bu işlemi eklemek istediğinizden emin misiniz ?", "Uyarı!", MessageBoxButtons.YesNo);
                if (ms == DialogResult.Yes)
                {


                    var picture = grb_teeth.Controls.OfType<PictureBox>();
                    int counter = 0;
                    int price = 0;
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
                    price = int.Parse(txt_unit_price.Text);
                    try
                    {

                        int reg_id = 0;
                        cnn.Open();
                        sql = "SELECT TOP(1) reg_no  FROM tbl_reg order by reg_no desc ";
                        command = new SqlCommand(sql, cnn);
                        dataReader = command.ExecuteReader();
                        if (dataReader.Read())
                        {
                            reg_id = int.Parse(dataReader["reg_no"].ToString());
                        }
                        //MessageBox.Show("reg_id :"+reg_id.ToString());
                        dataReader.Close();
                        cnn.Close();
                        reg_id += 1;
                        string dr_sent = "Hayır";
                        if (chk_sent_toDR.Checked == true)
                        {
                            dr_sent = "Evet";
                        }
                        cnn.Open();
                        sql = "INSERT tbl_inproc(reg_no,proc_name,inproc_init_date,inproc_deadline,step_name,color_name,teet,teet_num,price,total_price,sent) VALUES(@reg_no,@proc_name,@init_date,@deadline_date,@step_name,@color_name,@teet,@teet_num,@price,@total_price,@sent)";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.Add(new SqlParameter("@reg_no", reg_id));
                        command.Parameters.Add(new SqlParameter("@proc_name", cb_procces_bar.Text));
                        command.Parameters.Add(new SqlParameter("@init_date", dtp_register_date.Value.ToString("yyyy-MM-dd")));
                        command.Parameters.Add(new SqlParameter("@deadline_date", dtp_deadline.Value.ToString("yyyy-MM-dd")));
                        command.Parameters.Add(new SqlParameter("@step_name", cb_steps_bar.Text));
                        command.Parameters.Add(new SqlParameter("@color_name", cb_color_bar.Text));
                        command.Parameters.Add(new SqlParameter("@teet", teeth));
                        command.Parameters.Add(new SqlParameter("@teet_num", counter));
                        command.Parameters.Add(new SqlParameter("@price", price));
                        command.Parameters.Add(new SqlParameter("@total_price", price * counter));
                        command.Parameters.Add(new SqlParameter("@sent", dr_sent));
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        MessageBox.Show("hata :" + ex.Message);
                        
                        throw;
                    }
                    // this.Close();

                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
