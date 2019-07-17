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
using System.Security.Cryptography;
using System.Threading;
using System.IO;

namespace calypso_dental_V2
{
    public partial class Frm_login : MaterialForm
    {
        SqlConnection cnn;
        SqlDataAdapter adapter;
        DataTable table = new DataTable();
       public cs_error error = new cs_error();
        public Frm_login()
        {
            InitializeComponent();
            txt_password.PasswordChar = '*';
            Settings st = new Settings();
            string connetionString = "Data Source="+st.data_source+"\\SQL_2014;Initial Catalog="+st.initial_catalog+";Integrated Security=True";
            cnn = new SqlConnection(connetionString);
        }
        private void btn_eye_MouseDown(object sender, MouseEventArgs e)
        {
            txt_password.PasswordChar = '\0';
        }
        private void btn_eye_MouseUp(object sender, MouseEventArgs e)
        {
            txt_password.PasswordChar = '*';
        }
        public static string MD5eDonustur(string metin)
        {
            MD5CryptoServiceProvider pwd = new MD5CryptoServiceProvider();
            return encryption(metin, pwd);
        }
        private static string encryption(string metin, HashAlgorithm alg)
        {
            byte[] byteDegeri = System.Text.Encoding.UTF8.GetBytes(metin);
            byte[] sifreliByte = alg.ComputeHash(byteDegeri);
            return Convert.ToBase64String(sifreliByte);
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                string md5password = MD5eDonustur(txt_password.Text);
                string sql = "SELECT * FROM tbl_user WHERE user_password='" + md5password + "'and user_name='" + txt_user_name.Text + "'";
                adapter = new SqlDataAdapter(sql, cnn);
                adapter.Fill(table);
                if (table.Rows.Count <= 0)
                {
                    MessageBox.Show("Şifre veya şifre hatalı");
                    cnn.Close();
                }
                else
                {
                    this.Hide();
                    frm_main frm1 = new frm_main();
                   
                    frm1.ShowDialog();
                    this.Close();
                }
            }
            catch (Exception ex )
            {
                error.write_error(ex);
               MessageBox.Show("hata : "+ex.Message);
                if (cnn.State==ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Frm_settings frm_setting = new Frm_settings();
            frm_setting.ShowDialog();
        }
    }
}
