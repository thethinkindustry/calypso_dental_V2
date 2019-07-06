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
        SqlDataAdapter adapter;
        DataView dv;
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
                MessageBox.Show("hata :"+ex.Message);
                throw;
            }
          
        }
    }
}
