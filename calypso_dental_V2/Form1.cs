using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;

namespace calypso_dental_V2
{
    public partial class frm_main : MaterialForm
    {
        Frm_print_search frm_print_search ;
        Frm_print frm_print;
        Frm_print_payment frm_print_pay;
        private Settings settings = new Settings();
        private id reg_no = new calypso_dental_V2.id();
        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;
        SqlDataAdapter adapter;
        DataView dv;
        DataTable table = new DataTable();
        DataTable search_table = new DataTable();
        public List<inproc> lst = new List<inproc>();
        public List<patient> lst_print = new List<patient>();
        public List<payment> lst_pay = new List<payment>();
        public print_info p_info= new  print_info();
        public cs_error error = new cs_error();
        int reg_id = 0;
        int id = 0;
        string sql = null;
        string connetionString = null;
        public frm_main()
        {
            InitializeComponent();
            pnlVisible(pnl_init);
            frm_print_search = new Frm_print_search();
            frm_print_pay = new Frm_print_payment();
            frm_print = new Frm_print();
            frm_print_search.frm1 = this;
            frm_print.frm1 = this;
            frm_print_pay.frm1 = this;
            
            connetionString = "Data Source=" + settings.data_source + "\\SQL_2014;Initial Catalog=" + settings.initial_catalog + ";Integrated Security=True";
           cnn = new SqlConnection(connetionString);
        }
        void dgv_inproc_fill()
        {
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                  cnn.Open();
                }
                adapter = new SqlDataAdapter("SELECT * FROM tbl_inproc WHERE reg_no=" + reg_id + "", cnn);
                table.Clear();
                adapter.Fill(table);
                dgv_inproc.DataSource = table;
                adapter.Dispose();
                cnn.Close();
            }
            catch (Exception ex )
            {
                error.write_error(ex);
                MessageBox.Show(""+ex.Message);
                throw;
            }
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
        private void btn_add_proc_Click(object sender, EventArgs e)
        {
            MaterialForm frm_proc = new frm_add_proc();
            frm_proc.ShowDialog();
            dgv_inproc_fill();
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
        void pb_default_pic()
        {
            pB_add_pattient.Image = Resource_picture.hasta_ekle_ikon;
            pb_search.Image = Resource_picture.işlem_arama;
            pb_settings.Image = Resource_picture.ayarlar;
            pB_data_view.Image = Resource_picture.Yazdır;
            pb_aboutUS.Image = Resource_picture.hakkımızda;
        }
        private void pB_add_pattient_Click(object sender, EventArgs e)
        {
            pb_default_pic();
            pB_add_pattient.Image = Resource_picture.hasta_ekle_ikon_ters;
           pnlVisible(pnl_add_patient);
            cb_doctor_name.Items.Clear();
            try
            {
                
                cnn.Open();
                sql = "SELECT TOP(1) reg_no  FROM tbl_reg  order by reg_no desc ";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    reg_id = int.Parse(dataReader["reg_no"].ToString());
                    reg_id += 1;
                }
                //MessageBox.Show("reg_id :" + reg_id.ToString());
                dataReader.Close();
                cnn.Close();
                txt_reg_no.Text = (reg_id).ToString();
                cnn.Open();
                adapter = new SqlDataAdapter("SELECT * FROM tbl_inproc WHERE reg_no=" + reg_id + "", cnn);
                table.Clear();
                adapter.Fill(table);
                dgv_inproc.DataSource = table;
                dgv_inproc_fill();
                adapter.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                MessageBox.Show("hata :" + ex.Message);
                throw;
            }
            try
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
            catch (Exception ex)
            {
                error.write_error(ex);
                MessageBox.Show("hata :" + ex.Message);
                throw;
            }

        }

        private void pb_settings_Click(object sender, EventArgs e)
        {
            pb_default_pic();
            pb_settings.Image = Resource_picture.ayarlar_ters;
            pnlVisible(pnl_settings);
            var pnl = pnl_settings.Controls.OfType<Panel>();
            foreach (var item in pnl)
            {
                item.Visible = false;
            }
        }
        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        #region DoctorSettings
        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_settingsVisible(pnl_dr_add);
            grb_dr_update.Enabled = false;
            try
            {
                cnn.Open();
                sql = "SELECT * FROM tbl_dr";
                adapter = new SqlDataAdapter(sql, cnn);
                DataTable drtable = new DataTable();
                adapter.Fill(drtable);
                dgv_dr_list.DataSource = drtable;
                dgv_dr_list.Columns[0].HeaderText = "ID";
                dgv_dr_list.Columns[1].HeaderText = "Doktor Adı";
                dgv_dr_list.Columns[2].HeaderText = "Tel No";
                dgv_dr_list.Columns[3].HeaderText = "Doktor Borcu";
                dgv_dr_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adapter.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                adapter.Dispose();
                cnn.Close();
                MessageBox.Show("hata :" + ex);
            }
        }
 private void button1_Click(object sender, EventArgs e)
        {
            if (txt_dr_name.Text == "" || txt_dr_debt.Text == "" || txt_tel_no.Text == "")
            {
                MessageBox.Show("Kayıt işin boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_dr_name.Text + Environment.NewLine + txt_tel_no.Text + Environment.NewLine + "Borç :" + txt_dr_debt.Text + Environment.NewLine + "Bilgilerine sahip doktoru eklemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "INSERT tbl_dr(dr_name,dr_tel,dr_debt) VALUES(@dr_name,@dr_tell,@dr_debt)";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.Add(new SqlParameter("@dr_name", txt_dr_name.Text));
                        command.Parameters.Add(new SqlParameter("@dr_tell", txt_tel_no.Text));
                        command.Parameters.Add(new SqlParameter("@dr_debt", int.Parse(txt_dr_debt.Text)));
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        colorsToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        cnn.Close();
                        MessageBox.Show("Hata " + ex);

                    }
                }

            }
        }

        private void dgv_dr_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            grb_dr_update.Enabled = true;
            int selectedIndex = dgv_dr_list.CurrentCell.RowIndex;
            if (selectedIndex > -1)
            {
                id = int.Parse(dgv_dr_list.CurrentRow.Cells[0].Value.ToString());
               txt_new_dr_name.Text= txt_dr_name_update.Text = dgv_dr_list.CurrentRow.Cells[1].Value.ToString();
                txt_new_dr_tell.Text= txt_dr_tel_update.Text = dgv_dr_list.CurrentRow.Cells[2].Value.ToString();
               txt_new_dr_debt.Text= txt_dr_debt_update.Text = dgv_dr_list.CurrentRow.Cells[3].Value.ToString();
            }
        }
        private void btn_dr_delete_Click(object sender, EventArgs e)
        {
            DialogResult mg;
            mg = MessageBox.Show(txt_dr_name_update.Text + "\n"+txt_dr_tel_update.Text + "\n" + txt_dr_debt_update.Text + "\n" + " Adlı doktoru  silmek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mg == DialogResult.Yes)
            {
                try
                {
                    cnn.Open();
                    sql = "DELETE FROM tbl_dr WHERE dr_id=" + id + "";
                    command = new SqlCommand(sql, cnn);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    colorsToolStripMenuItem_Click(sender, e);
                }
                catch (Exception ex)
                {
                    error.write_error(ex);
                    cnn.Close();
                    MessageBox.Show("Hata  :" + ex);

                }
            }
        }
        private void btn_dr_update_Click(object sender, EventArgs e)
        {
            if (txt_new_dr_name.Text == "" || txt_new_dr_debt.Text == "" || txt_new_dr_tell.Text == "")
            {
                MessageBox.Show("Kayıt işin boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_new_dr_name.Text + Environment.NewLine + txt_new_dr_tell.Text + Environment.NewLine + "Borç :" + txt_new_dr_debt.Text + Environment.NewLine + "Bilgilerine sahip doktoru güncellemek  istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "UPDATE tbl_dr SET dr_name=@dr,dr_tel=@tel,dr_debt=@debt WHERE dr_id=@id";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@dr", txt_new_dr_name.Text);
                        command.Parameters.AddWithValue("@tel", txt_new_dr_tell.Text);
                        command.Parameters.AddWithValue("@debt", int.Parse(txt_new_dr_debt.Text));
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        colorsToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        cnn.Close();
                        MessageBox.Show("Hata  :" + ex);

                    }
                }
            }
        }
        #endregion
        #region ProcessSettings
        private void işlemlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_settingsVisible(pnl_add_proc);
            grb_proc_update.Enabled = false;
            try
            {
                cnn.Open();
                sql = "SELECT * FROM tbl_proc";
                adapter = new SqlDataAdapter(sql, cnn);
                DataTable drtable = new DataTable();
                adapter.Fill(drtable);
                dgv_proc_list.DataSource = drtable;
                dgv_proc_list.Columns[0].HeaderText = "ID";
                dgv_proc_list.Columns[1].HeaderText = "İşlem Adı";
                dgv_proc_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adapter.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                adapter.Dispose();
                cnn.Close();
                MessageBox.Show("hata :" + ex);
            }
        }
        public void tableSeach(object sender, EventArgs e)
        {
            dv = new DataView(search_table);
            if (chk_deadline.Checked && chk_init_date.Checked)
            {
                dv.RowFilter = string.Format(CultureInfo.InvariantCulture.NumberFormat, "dr_name LIKE '%{0}%' And pat_name LIKE '%{1}%' And proc_name LIKE '%{2}%' And inproc_deadline >=#{3}# And inproc_deadline <=#{4}#And inproc_init_date>= #{5}# And inproc_init_date <= #{6}# And step_name LIKE '%{7}%'", txt_search_dr.Text, txt_search_patient.Text, txt_search_procces.Text, dtb_deadline_init.Value.ToString("yyyy-MM-dd"), dtp_deadline_upperlimit.Value.ToString("yyyy-MM-dd"), dtp_init_date_init.Value.ToString("yyyy-MM-dd"), dtp_init_date_upperlimit.Value.ToString("yyyy-MM-dd"), txt_step.Text);
            }
            else if (chk_init_date.Checked)
            {
                dv.RowFilter = string.Format(CultureInfo.InvariantCulture.NumberFormat, "dr_name LIKE '%{0}%' And pat_name LIKE '%{1}%' And proc_name LIKE '%{2}%' And inproc_init_date >=#{3}#And inproc_init_date <=#{4}#  And step_name LIKE '%{5}%' ", txt_search_dr.Text, txt_search_patient.Text, txt_search_procces.Text, dtp_init_date_init.Value.ToString("yyyy-MM-dd"), dtp_init_date_upperlimit.Value.ToString("yyyy-MM-dd"), txt_step.Text);
            }
            else if (chk_deadline.Checked)
            {
                dv.RowFilter = string.Format(CultureInfo.InvariantCulture.NumberFormat, "dr_name LIKE '%{0}%' And pat_name LIKE '%{1}%' And proc_name LIKE '%{2}%' And inproc_deadline >=#{3}# And inproc_deadline <=#{4}# And step_name LIKE '%{5}%' ", txt_search_dr.Text, txt_search_patient.Text, txt_search_procces.Text, dtb_deadline_init.Value.ToString("yyyy-MM-dd"), dtp_deadline_upperlimit.Value.ToString("yyyy-MM-dd"), txt_step.Text);
            }
            else
            {

                dv.RowFilter = string.Format(CultureInfo.InvariantCulture.NumberFormat, "dr_name LIKE '%{0}%' And pat_name LIKE '%{1}%' And proc_name LIKE '%{2}%' And step_name LIKE '%{3}%' ", txt_search_dr.Text, txt_search_patient.Text, txt_search_procces.Text, txt_step.Text);
            }


            dgv_search.DataSource = dv;

            int totalTeeth = 0; ;
            for (int i = 0; i < dgv_search.Rows.Count; i++)
            {

                string value = dgv_search.Rows[i].Cells[9].Value.ToString();
                totalTeeth += int.Parse(value);
                // totalTeeth += int.Parse(dgv_main.Rows[i].Cells[9].Value.ToString());

            }
            txt_result.Text = totalTeeth.ToString();
        }
        private void search_id(object sender, EventArgs e)
        {
            
            if (txt_id.Text!="")
            {
                dv = new DataView(search_table);
                dv.RowFilter = string.Format(CultureInfo.InvariantCulture.NumberFormat, "reg_no ={0} ", int.Parse(txt_id.Text));
                dgv_search.DataSource = dv;

                int totalTeeth = 0; ;
                for (int i = 0; i < dgv_search.Rows.Count; i++)
                {

                    string value = dgv_search.Rows[i].Cells[9].Value.ToString();
                    totalTeeth += int.Parse(value);
                    // totalTeeth += int.Parse(dgv_main.Rows[i].Cells[9].Value.ToString());

                }
                txt_result.Text = totalTeeth.ToString();
            }
            else
            {
                dv = new DataView(search_table);
                dgv_search.DataSource = dv;
            }
        }
      
        
        private void chk_deadline_CheckedChanged(object sender, EventArgs e)
        {
            if (dtb_deadline_init.Enabled)
            {
                dtb_deadline_init.Enabled = false;
                dtp_deadline_upperlimit.Enabled = false;
            }
            else
            {
                dtb_deadline_init.Enabled = true;
                dtp_deadline_upperlimit.Enabled = true;
            }
            

        }
        private void chk_save_date_CheckedChanged(object sender, EventArgs e)
        {
            if (dtp_init_date_init.Enabled)
            {
                dtp_init_date_init.Enabled = false;
                dtp_init_date_upperlimit.Enabled = false;

            }
            else
            {
                dtp_init_date_init.Enabled = true;
                dtp_init_date_upperlimit.Enabled = true;
            }
            
        }
        private void btn_proc_add_Click(object sender, EventArgs e)
        {
            if (txt_proc.Text == "")
            {
                MessageBox.Show("Kayıt işin boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_proc.Text + Environment.NewLine + "Adlı işlemi eklemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "INSERT tbl_proc(proc_name) VALUES(@proc_name)";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.Add(new SqlParameter("@proc_name", txt_proc.Text));
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        işlemlerToolStripMenuItem_Click(sender, e);//tabloyu yenilemek için
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        cnn.Close();
                        MessageBox.Show("Hata " + ex);

                    }
                }

            }
        }

        private void dgv_proc_list_DoubleClick(object sender, EventArgs e)
        {
            grb_proc_update.Enabled = true;
            int selectedIndex = dgv_proc_list.CurrentCell.RowIndex;
            if (selectedIndex > -1)
            {
                id = int.Parse(dgv_proc_list.CurrentRow.Cells[0].Value.ToString());
                txt_proc_update.Text = dgv_proc_list.CurrentRow.Cells[1].Value.ToString();
            }
        }
        private void btn_proc_delete_Click(object sender, EventArgs e)
        {
            DialogResult mg;
            mg = MessageBox.Show(txt_proc_update.Text + Environment.NewLine + Environment.NewLine + " Adlı işlemi silmek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mg == DialogResult.Yes)
            {
                try
                {
                    cnn.Open();
                    sql = "DELETE FROM tbl_proc WHERE proc_id=" +id+ "";
                    command = new SqlCommand(sql, cnn);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    işlemlerToolStripMenuItem_Click(sender, e);
                }
                catch (Exception ex)
                {
                    error.write_error(ex);
                    cnn.Close();
                    MessageBox.Show("Hata  :" + ex);

                }
            }
        }

        private void btn_proc_update_Click(object sender, EventArgs e)
        {
            if (txt_new_proc_name.Text == "")
            {
                MessageBox.Show("Kayıt için boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_new_proc_name.Text + Environment.NewLine + Environment.NewLine + " Adlı işlemi güncellemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "UPDATE tbl_proc SET proc_name=@proc WHERE proc_id=@id";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@proc", txt_new_proc_name.Text);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        işlemlerToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        cnn.Close();
                        MessageBox.Show("Hata  :" + ex);

                    }
                }
            }
        }
        #endregion
        #region ColorSettings
        private void reToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_settingsVisible(pnl_add_color);
            grb_color_update.Enabled = false;
            try
            {
                cnn.Open();
                sql = "SELECT * FROM tbl_color";
                adapter = new SqlDataAdapter(sql, cnn);
                DataTable drtable = new DataTable();
                adapter.Fill(drtable);
                dgv_color_list.DataSource = drtable;
                dgv_color_list.Columns[0].HeaderText = "ID";
                dgv_color_list.Columns[1].HeaderText = "Renk Adı";
                dgv_color_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adapter.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                adapter.Dispose();
                cnn.Close();
                MessageBox.Show("hata :" + ex);
            }
        }
        private void btn_color_add_Click(object sender, EventArgs e)
        {
            if (txt_color_add.Text == "")
            {
                MessageBox.Show("Kayıt için boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_color_add.Text + Environment.NewLine + "Adlı rengi eklemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "INSERT tbl_color(color_name) VALUES(@color_name)";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.Add(new SqlParameter("@color_name", txt_color_add.Text));
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        reToolStripMenuItem_Click(sender, e);//tabloyu yenilemek için
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        cnn.Close();
                        MessageBox.Show("Hata " + ex);

                    }
                }

            }
        }



        private void dgv_color_list_DoubleClick(object sender, EventArgs e)
        {
            grb_color_update.Enabled = true;
            int selectedIndex = dgv_color_list.CurrentCell.RowIndex;
            if (selectedIndex > -1)
            {
                id = int.Parse(dgv_color_list.CurrentRow.Cells[0].Value.ToString());
                txt_color_update.Text = dgv_color_list.CurrentRow.Cells[1].Value.ToString();
            }
        }
        private void btn_color_delete_Click(object sender, EventArgs e)
        {
            DialogResult mg;
            mg = MessageBox.Show(txt_color_update.Text + "\n" +" Adlı rengi  silmek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mg == DialogResult.Yes)
            {
                try
                {
                    cnn.Open();
                    sql = "DELETE FROM tbl_color WHERE color_id=" + id + "";
                    command = new SqlCommand(sql, cnn);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    reToolStripMenuItem_Click(sender, e);
                }
                catch (Exception ex)
                {
                    error.write_error(ex);
                    cnn.Close();
                    MessageBox.Show("Hata  :" + ex);

                }
            }
        }
        private void btn_color_update_Click(object sender, EventArgs e)
        {
            if (txt_color_update.Text == "")
            {
                MessageBox.Show("Kayıt için boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_new_color.Text + Environment.NewLine + Environment.NewLine + " Adlı rengi güncellemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "UPDATE tbl_color SET color_name=@color WHERE color_id=@id";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@color", txt_new_color.Text);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        reToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        cnn.Close();
                        MessageBox.Show("Hata  :" + ex);

                    }
                }
            }
        }
        #endregion
        #region StepSettings
        private void aşamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_settingsVisible(pnl_add_step);
            grb_step_update.Enabled = false;
            try
            {
                cnn.Open();
                sql = "SELECT * FROM tbl_step";
                adapter = new SqlDataAdapter(sql, cnn);
                DataTable drtable = new DataTable();
                adapter.Fill(drtable);
                dgv_step_list.DataSource = drtable;
                dgv_step_list.Columns[0].HeaderText = "ID";
                dgv_step_list.Columns[1].HeaderText = "Aşama Adı";
                dgv_step_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adapter.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                adapter.Dispose();
                cnn.Close();
                MessageBox.Show("hata :" + ex);
            }
        }
        private void btn_add_step_Click(object sender, EventArgs e)
        {
            if (txt_step_add.Text == "")
            {
                MessageBox.Show("Kayıt için boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_step_add.Text + Environment.NewLine + "Adlı aşamayı eklemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "INSERT tbl_step(step_name) VALUES(@step_name)";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.Add(new SqlParameter("@step_name", txt_step_add.Text));
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        aşamaToolStripMenuItem_Click(sender, e);//tabloyu yenilemek için
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        cnn.Close();
                        MessageBox.Show("Hata " + ex);

                    }
                }

            }
        }
        private void dgv_step_list_DoubleClick(object sender, EventArgs e)
        {
            grb_step_update.Enabled = true;
            int selectedIndex = dgv_step_list.CurrentCell.RowIndex;
            if (selectedIndex > -1)
            {
                id = int.Parse(dgv_step_list.CurrentRow.Cells[0].Value.ToString());
                txt_step_update.Text = dgv_step_list.CurrentRow.Cells[1].Value.ToString();
            }
        }
        private void btn_delete_step_Click(object sender, EventArgs e)
        {
            DialogResult mg;
            mg = MessageBox.Show(txt_step_update.Text + Environment.NewLine + Environment.NewLine + " Adlı aşamayı silmek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mg == DialogResult.Yes)
            {
                try
                {
                    cnn.Open();
                    sql = "DELETE FROM tbl_step WHERE step_id=" + id + "";
                    command = new SqlCommand(sql, cnn);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    aşamaToolStripMenuItem_Click(sender, e);
                }
                catch (Exception ex)
                {
                    error.write_error(ex);
                    cnn.Close();
                    MessageBox.Show("Hata  :" + ex);

                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (txt_step_update.Text == "")
            {
                MessageBox.Show("Kayıt için boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_new_step.Text + Environment.NewLine + Environment.NewLine + " Adlı aşamayı güncellemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "UPDATE tbl_step SET step_name=@step WHERE step_id=@id";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@step", txt_new_step.Text);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        aşamaToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        command.Dispose();
                        cnn.Close();
                        MessageBox.Show("Hata  :" + ex);

                    }
                }
            }
        }


        #endregion
        #region Payment
        private void ödemelerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_settingsVisible(pnl_payment);
            dgv_old_payment.DataSource = null;
            grb_pay_his.Enabled = false;
            try
            {
                cnn.Open();
                sql = "select dr_name,dr_tel,dr_debt From tbl_dr ";
                adapter = new SqlDataAdapter(sql, cnn);
                DataTable paytable = new DataTable();
                adapter.Fill(paytable);
                dgv_dr_payment.DataSource = paytable;
                int total_payment = 0;
                for (int i = 0; i < dgv_dr_payment.Rows.Count - 1; i++)
                {
                    total_payment += int.Parse(dgv_dr_payment.Rows[i].Cells[2].Value.ToString());
                }
                txt_total_debt.Text = total_payment.ToString();
                dgv_dr_payment.Columns[0].HeaderText = "Doktor Adı";
                dgv_dr_payment.Columns[1].HeaderText = "Tel. No";
                dgv_dr_payment.Columns[2].HeaderText = "Borç Miktarı";
                dgv_dr_payment.SelectionMode = DataGridViewSelectionMode.FullRowSelect; adapter.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                MessageBox.Show("ERROR" + ex.Message);
                throw;
            }
        }
        private void dgv_old_payment_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DialogResult mg;
            mg = MessageBox.Show( " n ödeme geçmişini silmek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mg == DialogResult.Yes)
            {
                try
                {
                    cnn.Open();
                    var dr_debt = new SqlCommand("SELECT dr_debt FROM tbl_dr where dr_name='" + dgv_dr_payment.CurrentRow.Cells[0].Value.ToString() + "'", cnn).ExecuteScalar().ToString();
                    //MessageBox.Show(dr_debt);
                    var dr_payment = new SqlCommand("SELECT pay_price FROM tbl_pay where pay_id='" + dgv_old_payment.CurrentRow.Cells[0].Value.ToString() + "'", cnn).ExecuteScalar().ToString();
                    cnn.Close();
                    //MessageBox.Show(dr_payment);
                    int new_debt = int.Parse(dr_debt) + int.Parse(dr_payment);
                    cnn.Open();
                    sql = "UPDATE  tbl_dr SET dr_debt=@debt WHERE dr_name='" + dgv_old_payment.CurrentRow.Cells[1].Value.ToString() + "'";
                    command = new SqlCommand(sql, cnn);
                    command.Parameters.AddWithValue("@debt", new_debt);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    cnn.Open();
                    sql = "DELETE FROM tbl_pay  WHERE pay_id=" + dgv_old_payment.CurrentRow.Cells[0].Value.ToString() + " ";
                    command = new SqlCommand(sql, cnn);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    ödemelerToolStripMenuItem_Click(sender, e);
                }
                catch (Exception ex)
                {
                    error.write_error(ex);
                    MessageBox.Show("ERROR" + ex.Message);
                    throw;
                }
            }
        }
        private void dgv_dr_payment_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            grb_pay_his.Enabled = true;
            try
            {
                cnn.Open();
                sql = "select pay_id,dr_name,pay_date,pay_price From tbl_dr INNER JOIN tbl_pay ON tbl_dr.dr_id=tbl_pay.dr_id WHERE dr_name='"+ dgv_dr_payment.CurrentRow.Cells[0].Value.ToString()+ "' ";
                adapter = new SqlDataAdapter(sql, cnn);
                DataTable oldpaytable = new DataTable();
                adapter.Fill(oldpaytable);
                cnn.Close();
                dgv_old_payment.DataSource = oldpaytable;
                dgv_old_payment.Columns[0].Visible = false;
                dgv_old_payment.Columns[1].HeaderText = "Doktor Adı";
                dgv_old_payment.Columns[2].HeaderText = "Ödeme Tarihi";
                dgv_old_payment.Columns[3].HeaderText = "Miktar";
                dgv_dr_payment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adapter.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                MessageBox.Show("ERROR" + ex.Message);
                throw;
            }
        }
        
        private void btn_add_payment_Click(object sender, EventArgs e)
        {
            string _dr_name = dgv_dr_payment.CurrentRow.Cells[0].Value.ToString();
            string _dr_debt = dgv_dr_payment.CurrentRow.Cells[2].Value.ToString();
            Frm_add_payment frm_add_payment = new Frm_add_payment(_dr_name,_dr_debt);
            frm_add_payment.ShowDialog();
            ödemelerToolStripMenuItem_Click(sender, e);
        }
        private void btn_print_payment_Click(object sender, EventArgs e)
        {
            lst_pay.Clear();
            p_info.Totaldebt =int.Parse( dgv_dr_payment.CurrentRow.Cells[2].Value.ToString());
            for (int i = 0; i < dgv_old_payment.Rows.Count-1; i++)
            {
                lst_pay.Add(new payment
                {
                    dr_name = dgv_old_payment.Rows[i].Cells[0].Value.ToString(),
                    date = dgv_old_payment.Rows[i].Cells[1].Value.ToString(),
                    pay = int.Parse(dgv_old_payment.Rows[i].Cells[2].Value.ToString())
                });
            }
            frm_print_pay.frm1.frm_print_pay.ShowDialog();
            pnlVisible(pnl_init);
        }
        #endregion
        void pnl_settingsVisible(Panel pnl)
        {
            var panel = pnl_settings.Controls.OfType<Panel>();
            foreach (var item in panel)
            {
                item.Visible = false;
            }
            pnl.Visible = true;
           // pnl_menu.Visible = true;
        }
        private void dgv_inproc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        private void dgv_inproc_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DialogResult mg;
            mg = MessageBox.Show(" Seçili işlemi kaldırmak istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mg == DialogResult.Yes)
            {
                dgv_inproc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                int selectedIndex = dgv_inproc.CurrentCell.RowIndex;
                if (selectedIndex > -1)
                {
                    int db_id = int.Parse(dgv_inproc.CurrentRow.Cells[0].Value.ToString());
                    try
                    {
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
                        MessageBox.Show("Hata : "+ex.Message);
                        throw;
                    }
                    
                    dgv_inproc.Rows.RemoveAt(selectedIndex);
                    dgv_inproc.Refresh();
                    total_price();
                }
            }
        }
        void pnlVisible(Panel pnl)
        {
            var panel = this.Controls.OfType<Panel>();
            foreach (var item in panel)
            {
                item.Visible = false;
            }
            pnl.Visible = true;
            pnl_menu.Visible = true;
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult mg;
            mg = MessageBox.Show(" Kayıdı kaldırmak istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mg == DialogResult.Yes)
            {
                try
                {
                    cnn.Open();
                    sql = "DELETE FROM tbl_inproc WHERE reg_no=" + txt_reg_no.Text + "";
                    command = new SqlCommand(sql, cnn);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();

                }
                catch (Exception ex)
                {
                    error.write_error(ex);
                    MessageBox.Show("Hata :" + ex.Message);
                    throw;
                }
                pnlVisible(pnl_init);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_doctor_notes.Text == "")
            {
                txt_doctor_notes.Text = "Yok";
            }
            if (string.IsNullOrEmpty( cb_doctor_name?.SelectedItem?.ToString())|| string.IsNullOrEmpty(txt_patient_name.Text) )
            {
                MessageBox.Show("Zorunlu alanlar doldurulmalıdır.");
            }
            else if(dgv_inproc.RowCount <= 0)
            {
                 MessageBox.Show("Herhagi bir işlem eklenmedi.Lütfen işlem Ekleyiniz");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(" Kayıdı tamamlamak istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "INSERT INTO tbl_reg(reg_no,dr_name,pat_name,reg_totalprice, reg_drnote)values(@reg,@dr,@pat,@price,@note)";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@reg", int.Parse(txt_reg_no.Text));
                        command.Parameters.AddWithValue("@dr", cb_doctor_name.Text);
                        command.Parameters.AddWithValue("@pat", txt_patient_name.Text);
                        command.Parameters.AddWithValue("@price", int.Parse(txt_all_prices.Text));
                        command.Parameters.AddWithValue("@note", txt_doctor_notes.Text);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        cnn.Open();
                        var totaldebt = new SqlCommand("SELECT dr_debt FROM tbl_dr where dr_name='" + cb_doctor_name.Text + "'", cnn).ExecuteScalar().ToString();
                        int debt = int.Parse(totaldebt)+int.Parse(txt_all_prices.Text);
                         sql = "UPDATE  tbl_dr SET dr_debt=@debt WHERE dr_name='" + cb_doctor_name.Text + "'";
                          command = new SqlCommand(sql, cnn);
                          command.Parameters.AddWithValue("@debt",debt );
                          command.ExecuteNonQuery();
                          command.Dispose();
                          cnn.Close();
                    }
                    catch (Exception ex)
                    {
                        error.write_error(ex);
                        MessageBox.Show("Hata :" + ex.Message);
                        throw;

                    }
                    pnlVisible(pnl_init);
                }
            }
            
        }

        private void pb_search_Click(object sender, EventArgs e)
        {
            pb_default_pic();
            pb_search.Image = Resource_picture.işlem_arama_ters;
            pnlVisible(pnl_search);
            dgvSearchFill();
        }
        void dgvSearchFill()
        {
            if (cnn.State==ConnectionState.Closed)
            {
                cnn.Open();
            }
            sql = "SELECT  tbl_reg.reg_no,dr_name,pat_name,proc_name,inproc_init_date,inproc_deadline,step_name,color_name,teet,teet_num,price,total_price,sent,reg_drnote FROM tbl_reg INNER JOIN tbl_inproc ON tbl_reg.reg_no=tbl_inproc.reg_no";
            adapter = new SqlDataAdapter(sql, cnn);
            search_table.Clear();
            adapter.Fill(search_table);
            dgv_search.DataSource = search_table;
            dgv_search.Columns["reg_no"].HeaderText = "Kayıt No";
            dgv_search.Columns["dr_name"].HeaderText = "Doktor Adı";
            dgv_search.Columns["pat_name"].HeaderText = "Hasta Adı";
            dgv_search.Columns["proc_name"].HeaderText = "Yapılan İşlem ";
            dgv_search.Columns["inproc_init_date"].HeaderText = "Kayıt Tarihi";
            dgv_search.Columns["inproc_deadline"].HeaderText = "İstenilen Tarih";
            dgv_search.Columns["step_name"].HeaderText = "Aşama";
            dgv_search.Columns["color_name"].HeaderText = "Renk";
            dgv_search.Columns["teet"].HeaderText = "Diş Numaraları";
            dgv_search.Columns["teet_num"].HeaderText = "Diş Adeti";
            dgv_search.Columns["price"].HeaderText = "Birim Fiyat";
            dgv_search.Columns["total_price"].HeaderText = "Toplam Fiyat";
            dgv_search.Columns["sent"].HeaderText = "Doktora Gönderildi";
            dgv_search.Columns["reg_drnote"].HeaderText = "Doktor Notu";
            dgv_search.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            adapter.Dispose();
            int totalTeeth = 0; ;
            for (int i = 0; i < dgv_search.Rows.Count; i++)
            {

                string value = dgv_search.Rows[i].Cells[9].Value.ToString();
                totalTeeth += int.Parse(value);
                // totalTeeth += int.Parse(dgv_main.Rows[i].Cells[9].Value.ToString());

            }
            txt_result.Text = totalTeeth.ToString();
            cnn.Close();
        }
        private void dgv_search_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedIndex = dgv_search.CurrentCell.RowIndex;
            if (selectedIndex > -1)
            {

                int reg = int.Parse(dgv_search.CurrentRow.Cells[0].Value.ToString());
                reg_no.Selected_id =reg;
                reg_no.Save();
            }
            frm_update_pat Frm_update = new frm_update_pat();
            Frm_update.ShowDialog();
            dgvSearchFill();

        }

        private void pb_payment_Click(object sender, EventArgs e)
        {

        }

        private void pb_printp_Click(object sender, EventArgs e)
        {
         
        }

        private void btn_print_search_Click(object sender, EventArgs e)
        {
            lst.Clear();
            for (int i = 0; i < dgv_search.Rows.Count; i++)
            {
                lst.Add(new inproc
                {
                    reg_no = int.Parse(dgv_search.Rows[i].Cells[0].Value.ToString()),
                    dr_name = dgv_search.Rows[i].Cells[1].Value.ToString(),
                    pat_name = dgv_search.Rows[i].Cells[2].Value.ToString(),
                    proc_name = dgv_search.Rows[i].Cells[3].Value.ToString(),
                    regDate = dgv_search.Rows[i].Cells[4].Value.ToString(),
                    delDate = dgv_search.Rows[i].Cells[5].Value.ToString(),
                    step = dgv_search.Rows[i].Cells[6].Value.ToString(),
                    color = dgv_search.Rows[i].Cells[7].Value.ToString(),
                    teet = dgv_search.Rows[i].Cells[8].Value.ToString(),
                    teet_num = dgv_search.Rows[i].Cells[9].Value.ToString(),
                    uprice = int.Parse(dgv_search.Rows[i].Cells[10].Value.ToString()),
                    price = int.Parse(dgv_search.Rows[i].Cells[11].Value.ToString())

                });

            }

            frm_print_search.frm1.frm_print_search.ShowDialog();  
        }

        private void pB_data_view_Click(object sender, EventArgs e)
        {
            pb_default_pic();
            pB_data_view.Image = Resource_picture.Yazdır_ters;
            pnlVisible(pnl_print);
            cb_select_dr.Text = null;
            cb_select_step.Text = null;
            cb_select_dr.Items.Clear();
            cb_select_step.Items.Clear();
            try
            {
                cnn.Open();
                sql = "SELECT dr_name FROM tbl_dr ";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_select_dr.Items.Add(dataReader["dr_name"].ToString());
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                cnn.Open();
                sql = "SELECT step_name FROM tbl_step ";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cb_select_step.Items.Add(dataReader["step_name"].ToString());
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
        private void btn_search_prt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_select_dr?.SelectedItem?.ToString()) || string.IsNullOrEmpty(cb_select_step.SelectedItem?.ToString()))
            {
                MessageBox.Show("Arama için Tüm Alanlar Doldurulmalıdır.");
            }
            else
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                sql = "SELECT  tbl_reg.reg_no,pat_name,proc_name,inproc_deadline,price,total_price,printed FROM tbl_reg INNER JOIN tbl_inproc ON tbl_reg.reg_no=tbl_inproc.reg_no Where dr_name='"+cb_select_dr.SelectedItem.ToString()+"' AND step_name='"+cb_select_step.SelectedItem.ToString()+"' ";
                adapter = new SqlDataAdapter(sql, cnn);
                DataTable table1 = new DataTable();
                table1.Clear();
                adapter.Fill(table1);
                dgv_print.DataSource = table1;
                dgv_print.Columns["reg_no"].HeaderText = "Kayıt No";
                dgv_print.Columns["pat_name"].HeaderText = "Hasta Adı";
                dgv_print.Columns["proc_name"].HeaderText = "Yapılan İşlem ";
                dgv_print.Columns["inproc_deadline"].HeaderText = "İstenilen Tarih";
                dgv_print.Columns["price"].HeaderText = "Birim Fiyat";
                dgv_print.Columns["total_price"].HeaderText = "Toplam Fiyat";
                dgv_print.Columns["printed"].HeaderText = "Yazdırıldı";
                dgv_print.Columns[6].Visible = true;
                dgv_print.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adapter.Dispose();
                cnn.Close();
            }
        }

        private void btn_print_click(object sender, EventArgs e)
        {
            try
            {
                lst.Clear();
                if (dgv_print.Rows.Count>0)
                {
                    for (int i = 0; i < dgv_print.Rows.Count - 1; i++)
                    {
                        
                        if ("Evet" == dgv_print.Rows[i].Cells[6].Value.ToString())
                        {
                            DialogResult dialogResult = MessageBox.Show(dgv_print.Rows[i].Cells[1].Value.ToString() + " Adlı hasta daha once yazdırılmış Yazdırmak istediğinizden emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.No)
                            {
                                int selectedIndex = dgv_print.Rows[i].Cells[6].RowIndex;
                                if (selectedIndex > -1)
                                {
                                    dgv_print.Rows.RemoveAt(selectedIndex);
                                    dgv_print.Refresh();
                                    if (i > -1) i--;
                                }
                            }
                        }

                    }
                    lst_print.Clear();
                    for (int i = 0; i < dgv_print.Rows.Count - 1; i++)
                {
                        cnn.Open();
                        sql = "UPDATE  tbl_inproc SET printed=@pat WHERE reg_no=@reg";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@pat", "Evet");
                        command.Parameters.AddWithValue("@reg", dgv_print.Rows[i].Cells[0].Value);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        
                        lst_print.Add(new patient
                    {
                        reg_no = int.Parse(dgv_print.Rows[i].Cells[0].Value.ToString()),
                        pName = dgv_print.Rows[i].Cells[1].Value.ToString(),
                        progName = dgv_print.Rows[i].Cells[2].Value.ToString(),
                        regDate = dgv_print.Rows[i].Cells[3].Value.ToString(),
                        uprice= int.Parse(dgv_print.Rows[i].Cells[4].Value.ToString()),
                        price = int.Parse(dgv_print.Rows[i].Cells[5].Value.ToString())
                    });
                }
                    cnn.Open();
                    var totaldebt = new SqlCommand("SELECT dr_debt FROM tbl_dr where dr_name='" + cb_select_dr.Text + "'", cnn).ExecuteScalar().ToString();
                    string debt = totaldebt.ToString();
                    p_info.Totaldebt = Convert.ToInt16(debt);
                    cnn.Close();
                    p_info.dr_name = cb_select_dr.Text;
                    p_info.fromDate = dt_fromdate.Value.ToString("yyyy-MM-dd");
                    p_info.toDate = dt_todate.Value.ToString("yyyy-MM-dd");
                   frm_print.frm1.frm_print.ShowDialog();
                    pnlVisible(pnl_init);
                }
                else
                {
                    MessageBox.Show("Yazdırlacak Öğe Seçilmedi");
                }
            }
            catch (Exception ex)
            {
                error.write_error(ex);
                MessageBox.Show("hata" + ex);
                throw;
            }
        }

      
        private void frm_main_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
        }

        private void pb_aboutUS_Click(object sender, EventArgs e)
        {
            pb_default_pic();
            pb_aboutUS.Image = Resource_picture.hakkımızda_ters;
        }

        private void pnl_print_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

