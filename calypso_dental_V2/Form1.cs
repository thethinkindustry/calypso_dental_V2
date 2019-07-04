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
namespace calypso_dental_V2
{
    public partial class frm_main : MaterialForm
    {

        private Settings settings = new Settings();
        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;
        SqlDataAdapter adapter;
        DataView dv;
        public List<inproc> lst = new List<inproc>();
       
        int id = 0;
        string sql = null;
        string connetionString = null;
        public frm_main()
        {
            InitializeComponent();

            pnl_add_patient.Visible = false;
            pnl_settings.Visible = false;
            pnl_init.Visible = true;
            settings.data_source = "DESKTOP-93568HR";
            settings.initial_catalog = "db_calypso_v2";

            settings.Save();
            connetionString = "Data Source=" + settings.data_source + "\\SQL_2014;Initial Catalog=" + settings.initial_catalog + ";Integrated Security=True";
            cnn = new SqlConnection(connetionString);
        }

        private void btn_add_proc_Click(object sender, EventArgs e)
        {
            MaterialForm frm_proc = new frm_add_proc();
            frm_proc.ShowDialog();
            dgv_inproc.DataSource = lst;
        }

        private void pB_add_pattient_Click(object sender, EventArgs e)
        {
            pnl_add_patient.Visible = true;
            pnl_settings.Visible = false;
            pnl_init.Visible = false;
            cb_doctor_name.Items.Clear();
            try
            {
                cnn.Open();
                sql = "SELECT dr_name FROM tbl_dr";
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
                MessageBox.Show("hata :" + ex);
                throw;
            }

        }

        private void pb_settings_Click(object sender, EventArgs e)
        {
            pnl_add_patient.Visible = false;
            pnl_settings.Visible = true;
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
    }
    }

