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
    public partial class frm_main :MaterialForm 
    {
       
        private Settings settings = new Settings();
        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;
        SqlDataAdapter adapter;
        DataView dv;
        string sql = null;
        string connetionString=null;
        public frm_main()
        {
            InitializeComponent();
            
            pnl_add_patient.Visible = false;
            pnl_settings.Visible = false;
            pnl_init.Visible = true;
            settings.data_source = "DESKTOP-93568HR";
            settings.initial_catalog ="db_calypso_v2";
            connetionString = "Data Source=" + settings.data_source + "\\SQL_2014;Initial Catalog=" + settings.initial_catalog + ";Integrated Security=True";
            cnn = new SqlConnection(connetionString);
        }
        
        private void btn_add_proc_Click(object sender, EventArgs e)
        {
           MaterialForm frm_proc=new  frm_add_proc();
            frm_proc.ShowDialog();
        }

        private void pB_add_pattient_Click(object sender, EventArgs e)
        {
            pnl_add_patient.Visible = true;
            pnl_settings.Visible = false;
            pnl_init.Visible = false;
            try
            {
                cnn.Open();
                sql = "SELECT dr_name FROM tbl_dr";
                command = new SqlCommand(sql,cnn);
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
                MessageBox.Show("hata :"+ex);
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
        private void doktorlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl_dr_add.Visible = true;
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
                MessageBox.Show("hata :"+Ex);
            }
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_dr_name.Text==""||txt_dr_debt.Text==""||txt_tel_no.Text=="")
            {
                MessageBox.Show("Kayıt işin boş alan bırakılmamalıdır.");
            }
            else
            {
                DialogResult mg;
              mg= MessageBox.Show(txt_dr_name.Text + Environment.NewLine+txt_tel_no.Text+Environment.NewLine+"Borç :"+txt_dr_debt.Text+Environment.NewLine+"Bilgilerine sahip doktoru eklemek istediğinize emin misiniz ?","Uyarı !",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (mg==DialogResult.Yes)
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
                        doktorlarToolStripMenuItem_Click(sender,  e);
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
            if (selectedIndex>-1)
            {
                int dr_id= int.Parse(dgv_dr_list.CurrentRow.Cells[0].Value.ToString());
                txt_dr_name_update.Text = dgv_dr_list.CurrentRow.Cells[1].Value.ToString();
                txt_dr_tel_update.Text = dgv_dr_list.CurrentRow.Cells[2].Value.ToString();
                txt_dr_debt_update.Text = dgv_dr_list.CurrentRow.Cells[3].Value.ToString();
            }
        }
    }
   
    }

