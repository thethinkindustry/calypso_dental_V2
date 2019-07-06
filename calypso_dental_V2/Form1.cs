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
namespace calypso_dental_V2
{
    public partial class frm_main : MaterialForm
    {

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
        int reg_id = 0;
        int id = 0;
        string sql = null;
        string connetionString = null;
        public frm_main()
        {
            InitializeComponent();
            pnlVisible(pnl_init);
            
            settings.data_source = "DESKTOP-93568HR";
            settings.initial_catalog = "db_calypso_v2";
          settings.Save();
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
        private void pB_add_pattient_Click(object sender, EventArgs e)
        {

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
                MessageBox.Show("hata :" + ex.Message);
                throw;
            }

        }

        private void pb_settings_Click(object sender, EventArgs e)
        {
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
        private void doktorlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_dr_add.Visible = true;
            pnl_add_proc.Visible = false;
            pnl_add_color.Visible = false;
            pnl_add_step.Visible = false;
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
            catch (Exception Ex)
            {
                adapter.Dispose();
                cnn.Close();
                MessageBox.Show("hata :" + Ex);
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
                        doktorlarToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
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
                txt_dr_name_update.Text = dgv_dr_list.CurrentRow.Cells[1].Value.ToString();
                txt_dr_tel_update.Text = dgv_dr_list.CurrentRow.Cells[2].Value.ToString();
                txt_dr_debt_update.Text = dgv_dr_list.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void btn_dr_update_Click(object sender, EventArgs e)
        {
            if (txt_dr_name_update.Text == "" || txt_dr_debt_update.Text == "" || txt_dr_tel_update.Text == "")
            {
                MessageBox.Show("Kayıt işin boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_dr_name_update.Text + Environment.NewLine + txt_dr_tel_update.Text + Environment.NewLine + "Borç :" + txt_dr_debt_update.Text + Environment.NewLine + "Bilgilerine sahip doktoru eklemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "UPDATE tbl_dr SET dr_name=@dr,dr_tel=@tel,dr_debt=@debt WHERE dr_id=@id";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@dr", txt_dr_name_update.Text);
                        command.Parameters.AddWithValue("@tel", txt_dr_tel_update.Text);
                        command.Parameters.AddWithValue("@debt", int.Parse(txt_dr_debt_update.Text));
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        doktorlarToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception Ex)
                    {
                        cnn.Close();
                        MessageBox.Show("Hata  :" + Ex);

                    }
                }
            }
        }
        #endregion
        #region ProcessSettings
        private void işlemlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_add_patient.Visible = false;
            pnl_add_proc.Visible = true;
            pnl_add_color.Visible = false;
            pnl_add_step.Visible = false;
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
            catch (Exception Ex)
            {
                adapter.Dispose();
                cnn.Close();
                MessageBox.Show("hata :" + Ex);
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
            tableSeach(sender, e);

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
            tableSeach(sender, e);
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

        private void btn_proc_update_Click(object sender, EventArgs e)
        {
            if (txt_proc_update.Text == "")
            {
                MessageBox.Show("Kayıt için boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_proc_update.Text + Environment.NewLine + Environment.NewLine + " Adlı işlemi güncellemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "UPDATE tbl_proc SET proc_name=@proc WHERE proc_id=@id";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@proc", txt_proc_update.Text);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        işlemlerToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception Ex)
                    {
                        cnn.Close();
                        MessageBox.Show("Hata  :" + Ex);

                    }
                }
            }
        }
        #endregion
        #region ColorSettings
        private void reToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_add_patient.Visible = false;
            pnl_add_proc.Visible = false;
            pnl_add_color.Visible = true;
            pnl_add_step.Visible = false;
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
            catch (Exception Ex)
            {
                adapter.Dispose();
                cnn.Close();
                MessageBox.Show("hata :" + Ex);
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

        private void btn_color_update_Click(object sender, EventArgs e)
        {
            if (txt_color_update.Text == "")
            {
                MessageBox.Show("Kayıt için boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_color_update.Text + Environment.NewLine + Environment.NewLine + " Adlı rengi güncellemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "UPDATE tbl_color SET color_name=@color WHERE color_id=@id";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@color", txt_color_update.Text);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        reToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception Ex)
                    {
                        cnn.Close();
                        MessageBox.Show("Hata  :" + Ex);

                    }
                }
            }
        }
        #endregion
        #region StepSettings
        private void aşamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_add_patient.Visible = false;
            pnl_add_proc.Visible = false;
            pnl_add_color.Visible = false;
            pnl_add_step.Visible = true;
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
            catch (Exception Ex)
            {
                adapter.Dispose();
                cnn.Close();
                MessageBox.Show("hata :" + Ex);
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
        private void button3_Click(object sender, EventArgs e)
        {
            if (txt_step_update.Text == "")
            {
                MessageBox.Show("Kayıt için boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
                mg = MessageBox.Show(txt_step_update.Text + Environment.NewLine + Environment.NewLine + " Adlı aşamayı güncellemek istediğinize emin misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mg == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        sql = "UPDATE tbl_step SET step_name=@step WHERE step_id=@id";
                        command = new SqlCommand(sql, cnn);
                        command.Parameters.AddWithValue("@step", txt_step_update.Text);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        aşamaToolStripMenuItem_Click(sender, e);
                    }
                    catch (Exception Ex)
                    {
                        command.Dispose();
                        cnn.Close();
                        MessageBox.Show("Hata  :" + Ex);

                    }
                }
            }
        }


        #endregion

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
                    MessageBox.Show("Hata :" + ex.Message);
                    throw;
                }
                pnlVisible(pnl_init);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (cb_doctor_name.Text == "" || txt_patient_name.Text == ""||txt_doctor_notes.Text=="" )
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
                        MessageBox.Show("Hata :" + ex.Message);
                        throw;

                    }
                    pnlVisible(pnl_init);
                }
            }
            
        }

        private void pb_search_Click(object sender, EventArgs e)
        {
            pnlVisible(pnl_search);
            dgvSearchFill();
        }
        void dgvSearchFill()
        {
            if (cnn.State==ConnectionState.Closed)
            {
                cnn.Open();
            }
            sql = "SELECT  tbl_reg.reg_no,dr_name,pat_name,proc_name,inproc_init_date,inproc_deadline,step_name,color_name,teet,teet_num,price,total_price,sent,printed,reg_drnote FROM tbl_reg INNER JOIN tbl_inproc ON tbl_reg.reg_no=tbl_inproc.reg_no";
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
            dgv_search.Columns["printed"].HeaderText = "Yazdırıldı";
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
    }
    }

