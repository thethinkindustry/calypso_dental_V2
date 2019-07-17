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
    public partial class frm_update_pat : MaterialForm
    {
        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;
        SqlDataAdapter adapter;
        cs_error error = new cs_error();
        DataTable table = new DataTable();
        string sql = null;
        string connetionString = null;
        id reg_no = new id();
        private Settings settings = new Settings();
        public frm_update_pat()
        {
            InitializeComponent();
            connetionString = "Data Source=" + settings.data_source + "\\SQL_2014;Initial Catalog=" + settings.initial_catalog + ";Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            // MessageBox.Show(reg_no.Selected_id.ToString());
            try
            {
                fillCbDrName();
                fillDatas();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                MessageBox.Show("hata :" + ex);
                throw;
            }
        }

        void fillCbDrName()
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
                sql = "SELECT dr_name FROM tbl_dr ";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_doctor_name.Items.Add(dataReader["dr_name"].ToString());
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
        }
        void fillDatas()
        {
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    sql = "SELECT  tbl_reg.reg_no,dr_name,pat_name,proc_name,inproc_init_date,inproc_deadline,step_name,color_name,teet,teet_num,price,total_price,sent,printed,reg_drnote FROM tbl_reg INNER JOIN tbl_inproc ON tbl_reg.reg_no=tbl_inproc.reg_no WHERE tbl_reg.reg_no=" + reg_no.Selected_id + "";
                    command = new SqlCommand(sql, cnn);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        txt_reg_no.Text = (dataReader["reg_no"].ToString());
                        cb_doctor_name.Text = (dataReader["dr_name"].ToString());
                        txt_patient_name.Text = (dataReader["pat_name"].ToString());
                        txt_doctor_notes.Text = (dataReader["reg_drnote"].ToString());
                    }
                    dataReader.Close();
                    command.Dispose();
                    cnn.Close();
                    updateDgv_inproc();
                    dgv_inproc.Columns["inproc_id"].HeaderText = "DB ID";
                    dgv_inproc.Columns["reg_no"].HeaderText = "Kayıt No";
                    dgv_inproc.Columns["proc_name"].HeaderText = "Yapılan İşlem ";
                    dgv_inproc.Columns["inproc_init_date"].HeaderText = "Kayıt Tarihi";
                    dgv_inproc.Columns["inproc_deadline"].HeaderText = "İstenilen Tarih";
                    dgv_inproc.Columns["step_name"].HeaderText = "Aşama";
                    dgv_inproc.Columns["color_name"].HeaderText = "Renk";
                    dgv_inproc.Columns["teet"].HeaderText = "Diş Numaraları";
                    dgv_inproc.Columns["teet_num"].HeaderText = "Diş Adeti";
                    dgv_inproc.Columns["price"].HeaderText = "Birim Fiyat";
                    dgv_inproc.Columns["total_price"].HeaderText = "Toplam Fiyat";
                    dgv_inproc.Columns["sent"].HeaderText = "Doktora Gönderildi";
                    total_price();

                }
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                MessageBox.Show("hata :" + ex);
                throw;
            }
        }
        void total_price()
        {
            UInt32 prices = 0;
            for (int i = 0; i < dgv_inproc.RowCount; i++)
            {
                prices += Convert.ToUInt32(dgv_inproc.Rows[i].Cells[10].Value.ToString());
            }
            txt_all_prices.Text = prices.ToString();
        }
        void drDebtUpdate()
        {
            int dr_debt = 0;
            int debt = 0;
            int db_id = int.Parse(dgv_inproc.CurrentRow.Cells[0].Value.ToString());
            try
            {
                cnn.Open();
                sql = "Select dr_debt FROM tbl_dr  WHERE dr_name='" + cb_doctor_name.Text + "'";
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
                sql = "Select total_price FROM tbl_inproc  WHERE inproc_id=" + db_id + "";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    debt = int.Parse(dataReader["total_price"].ToString());
                }
                //MessageBox.Show("dr_totat_debt :"+ total_debt+"  selected_debt :"+debt);
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                cnn.Open();
                dr_debt -=debt;
                sql = "UPDATE tbl_dr SET dr_debt = @debt WHERE dr_name = @name";
                command = new SqlCommand(sql, cnn);
                command.Parameters.AddWithValue("@debt", dr_debt);
                command.Parameters.AddWithValue("@name", cb_doctor_name.Text);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                MessageBox.Show("Hata : "+ex.Message);
                throw;
            }
            
        }
        private void btn_delete_proc_Click(object sender, EventArgs e)
        {
            int indexCount = dgv_inproc.RowCount;
            int db_id = int.Parse(dgv_inproc.CurrentRow.Cells[0].Value.ToString());
            dgv_inproc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DialogResult mg;
            //MessageBox.Show("index :"+indexCount);
            if (indexCount == 1)
            {
               
                mg = MessageBox.Show("  Bu işlem kayıtlı son işlemdir eğer bu işlemi kaldırısanız "+txt_reg_no.Text+ " numaralı kayıt tamamen silinecektir.\nSeçili işlemi kaldırmak istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    
                    try
                    {
                        drDebtUpdate();
                        cnn.Open();
                        sql = "DELETE FROM tbl_reg  WHERE reg_no=" + txt_reg_no.Text + "";
                        command = new SqlCommand(sql, cnn);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        cnn.Open();
                        sql = "DELETE FROM tbl_inproc  WHERE reg_no=" + txt_reg_no.Text + "";
                        command = new SqlCommand(sql, cnn);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        this.Close();
                       
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        MessageBox.Show("Hata : " + ex.Message);
                        throw;
                    }
                }
             }
            else
            {
            mg = MessageBox.Show(" Seçili işlemi kaldırmak istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mg == DialogResult.Yes)
            {
                 int selectedIndex = dgv_inproc.CurrentCell.RowIndex;
                    if (selectedIndex > -1)
                { 
                    try
                    {
                            drDebtUpdate();
                            cnn.Open();
                            sql = "DELETE FROM tbl_inproc WHERE inproc_id=" + db_id + "";
                            command = new SqlCommand(sql, cnn);
                            command.ExecuteNonQuery();
                            command.Dispose();
                            cnn.Close();

                        }
                    catch (Exception ex)
                    {
                            error.write_error(ex);
                            MessageBox.Show("Hata : " + ex.Message);
                        throw;
                    }

                    dgv_inproc.Rows.RemoveAt(selectedIndex);
                    dgv_inproc.Refresh();
                    total_price();
                }
               }
            }
        }
        private void dgv_inproc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int db_id = int.Parse(dgv_inproc.CurrentRow.Cells[0].Value.ToString());
            reg_no.inproc_id = db_id;
            reg_no.dr_name = cb_doctor_name.Text;
            reg_no.Save();
            Form_inproc_update frm_inproc_update = new Form_inproc_update();
            frm_inproc_update.ShowDialog();
            updateDgv_inproc();
            total_price();

        }
        void updateDgv_inproc()
        {
            cnn.Open();
            adapter = new SqlDataAdapter("SELECT * FROM tbl_inproc WHERE reg_no=" + reg_no.Selected_id + "", cnn);
            table.Clear();
            adapter.Fill(table);
            dgv_inproc.DataSource = table;
            adapter.Dispose();
            cnn.Close();
        }
         private void btn_save_Click(object sender, EventArgs e)
        {
            DialogResult mg;
            mg = MessageBox.Show(" Güncellemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mg == DialogResult.Yes)
            {
                cnn.Open();
                sql = "UPDATE tbl_reg SET pat_name=@pat_name,reg_totalprice=@total_price,reg_drnote=@note WHERE reg_no=@reg";
                command = new SqlCommand(sql, cnn);
                command.Parameters.Add(new SqlParameter("@pat_name", txt_patient_name.Text));
                command.Parameters.Add(new SqlParameter("@total_price", int.Parse(txt_all_prices.Text)));
                command.Parameters.Add(new SqlParameter("@note", txt_doctor_notes.Text));
                command.Parameters.Add(new SqlParameter("@reg", int.Parse(txt_reg_no.Text)));
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                MessageBox.Show("Kayıt başarıyla eklendi.");
                this.Close();
            }
        }

        private void btn_add_inproc_Click(object sender, EventArgs e)
        {
            reg_no.Selected_id = int.Parse(txt_reg_no.Text);
            reg_no.Save();
            MaterialForm frm_proc = new frm_add_proc();
            frm_proc.ShowDialog();
            updateDgv_inproc();
            dgv_inproc.Refresh();
            total_price();

        }
    }
}
