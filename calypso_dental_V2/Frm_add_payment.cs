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
    public partial class Frm_add_payment : MaterialForm
    {
        SqlConnection cnn;
        SqlCommand command;
        cs_error error = new cs_error();
        private Settings settings = new Settings();
        string sql = null;
        string connetionString = null;
        public Frm_add_payment(string dr_name ,string dr_debt)
        {
            InitializeComponent();
            txt_dr_debt.Text = dr_debt;
            txt_dr_name.Text = dr_name;
            connetionString = "Data Source=" + settings.data_source + "\\SQL_2014;Initial Catalog=" + settings.initial_catalog + ";Integrated Security=True";
            cnn = new SqlConnection(connetionString);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_payment.Text == "")
            {
                MessageBox.Show("HATA : Herhangi bir tutar girilmedi");
            }
            else
            {
                 double payment = Convert.ToInt32(txt_dr_debt.Text) - Convert.ToInt32(txt_payment.Text);
                DialogResult result = MessageBox.Show(txt_dr_name.Text + " adlı doktordan " + txt_payment.Text + "₺ ödeme alınacaktır.\n Kalan borç=" + payment + "\n işlemi onaylıyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                       sql = "UPDATE tbl_dr SET dr_debt = @debt WHERE dr_name ='"+ txt_dr_name.Text + "'";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@debt",int.Parse(payment.ToString()));
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        cnn.Open();
                        string dr_id = new SqlCommand("SELECT dr_id FROM tbl_dr where dr_name='" + txt_dr_name.Text + "'", cnn).ExecuteScalar().ToString();
                        sql = "INSERT INTO tbl_pay(dr_id,pay_date,pay_price)values(@id,@date,@price)";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@id",int.Parse(dr_id));
                        command.Parameters.AddWithValue("@date",dtp_payment_date.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@price",int.Parse(txt_payment.Text) );
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        MessageBox.Show("hata" + ex);
                        throw;
                    }
                }
            }
        }
    }
}
