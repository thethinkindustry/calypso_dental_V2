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
    public partial class Form_inproc_update : MaterialForm
    {
        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;
        cs_error error = new cs_error();
    
        int old_inproc_price = 0;
        int inproc_price = 0;
        string sql = null;
        string connetionString = null;
        private Settings settings = new Settings();
        private id reg_no = new id();
        public Form_inproc_update()
        {
            InitializeComponent();
            connetionString = "Data Source=" + settings.data_source + "\\SQL_2014;Initial Catalog=" + settings.initial_catalog + ";Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            var radioButtons = grb_teeth.Controls.OfType<PictureBox>();
            foreach (PictureBox rb in radioButtons)
            {
                rb.MouseClick += new MouseEventHandler((o, a) => onClickList(rb));
            }
            fillComboBox();
            fillText();
            fillTeeth();
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
        void fillComboBox()
        {
            try
            {
                cnn.Open();
                sql = "SELECT proc_name FROM tbl_proc ";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_procces_bar.Items.Add(dataReader["proc_name"].ToString());
                }
                dataReader.Close();
                command.Dispose();
                sql = "SELECT color_name FROM tbl_color ";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_color_bar.Items.Add(dataReader["color_name"].ToString());
                }
                dataReader.Close();
                command.Dispose();
                sql = "SELECT step_name FROM tbl_step ";
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
                error.write_error(ex);
                MessageBox.Show("hata :" + ex.Message);
                throw;
            }
        }
        void fillTeeth()
        {
            string teeth = null;
            cnn.Open();
            sql = "SELECT teet FROM tbl_inproc WHERE reg_no="+reg_no.Selected_id+"";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                teeth=dataReader["teet"].ToString();
            }
            dataReader.Close();
            command.Dispose();
            cnn.Close();
             
            string[] teethList = teeth.Split('/');
            var radioButton = grb_teeth.Controls.OfType<PictureBox>();
            foreach (PictureBox rb in radioButton)
            {
                for (int i = 0; i < teethList.Length; i++)
                {
                    if (rb.Name == teethList[i].ToString())
                    {
                        i = teethList.Length + 1;
                        rb.BackColor = Color.DarkSlateGray;
                        // MessageBox.Show(rb.Name);
                    }
                    else
                    {
                        rb.BackColor = Color.Transparent;
                    }
                }
            }
        }
        void fillText()
        {
            try
            {
                cnn.Open();
                sql = "SELECT * FROM tbl_inproc WHERE reg_no=" + reg_no.Selected_id + "";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_procces_bar.Text = dataReader["proc_name"].ToString();
                    cb_color_bar.Text = dataReader["color_name"].ToString();
                    cb_steps_bar.Text = dataReader["step_name"].ToString();
                    txt_unit_price.Text = dataReader["price"].ToString();
                    old_inproc_price = int.Parse(dataReader["total_price"].ToString());
                    if (dataReader["sent"].ToString()=="Evet")
                    {
                        chk_sent_toDR.Checked = true;
                    }
                    else
                    {
                        chk_sent_toDR.Checked =false;
                    }
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                MessageBox.Show("hata :"+ex.Message);
                throw;
            }
          
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txt_unit_price_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                  && !char.IsSeparator(e.KeyChar);
        }
        private void btn_update_proc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_procces_bar?.SelectedItem?.ToString()) || string.IsNullOrEmpty(cb_color_bar.SelectedItem?.ToString()) || string.IsNullOrEmpty(cb_steps_bar?.SelectedItem?.ToString()) || string.IsNullOrEmpty(txt_unit_price.Text))
            {
                MessageBox.Show("Kayıt İçin Tüm Alanlar Doldurulmalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show("Kaydetmek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    var picture = grb_teeth.Controls.OfType<PictureBox>();
                    int counter = 0;
                    int price = 0;
                    string teeth = "";
                    int dr_debt = 0;
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
                    inproc_price = counter * price;
                    try
                    {
                        cnn.Open();
                        sql = "SELECT  dr_debt FROM tbl_dr  WHERE dr_name ='" + reg_no.dr_name + "'";
                        command = new SqlCommand(sql, cnn);
                        dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            dr_debt = int.Parse(dataReader["dr_debt"].ToString());
                        }
                        dataReader.Close();
                        command.Dispose();
                        cnn.Close();

                        cnn.Open();
                        sql = "SELECT  total_price FROM tbl_inproc  WHERE reg_no ='" + reg_no.Selected_id + "'";
                        command = new SqlCommand(sql, cnn);
                        dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            old_inproc_price = int.Parse(dataReader["total_price"].ToString());
                        }
                        dataReader.Close();
                        command.Dispose();
                        cnn.Close();
                        inproc_price -= old_inproc_price;
                        dr_debt += inproc_price;
                        // MessageBox.Show(dr_debt.ToString());  
                        cnn.Open();
                        sql = "UPDATE tbl_dr SET dr_debt=@debt FROM tbl_dr  WHERE dr_name ='" + reg_no.dr_name + "'";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.Add(new SqlParameter("@debt", dr_debt));
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        string dr_sent = "Hayır";
                        if (chk_sent_toDR.Checked == true)
                        {
                            dr_sent = "Evet";
                        }
                        cnn.Open();
                        sql = "UPDATE tbl_inproc SET proc_name=@proc_name,inproc_init_date=@init_date,inproc_deadline=@deadline_date,step_name=@step_name,color_name=@color_name,teet=@teet,teet_num=@teet_num,price=@price,total_price=@total_price,sent=@sent WHERE inproc_id=@inproc_id";
                        command = new SqlCommand(sql, cnn);
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
                        command.Parameters.Add(new SqlParameter("@inproc_id", reg_no.inproc_id));
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        MessageBox.Show("hata" + ex.Message);
                        throw;
                    }
                }
            }
        }
    }
    }

