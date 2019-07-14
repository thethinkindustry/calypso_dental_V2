namespace calypso_dental_V2
{
    partial class Frm_settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_setting = new System.Windows.Forms.Panel();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_initialcatalog = new System.Windows.Forms.TextBox();
            this.txt_datasource = new System.Windows.Forms.TextBox();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.pnl_password = new System.Windows.Forms.Panel();
            this.btn_connect = new System.Windows.Forms.Button();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.txt_update_pass = new System.Windows.Forms.TextBox();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.pnl_setting.SuspendLayout();
            this.pnl_password.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_setting
            // 
            this.pnl_setting.Controls.Add(this.txt_update_pass);
            this.pnl_setting.Controls.Add(this.materialLabel5);
            this.pnl_setting.Controls.Add(this.btn_save);
            this.pnl_setting.Controls.Add(this.txt_initialcatalog);
            this.pnl_setting.Controls.Add(this.txt_datasource);
            this.pnl_setting.Controls.Add(this.materialLabel3);
            this.pnl_setting.Controls.Add(this.materialLabel2);
            this.pnl_setting.Controls.Add(this.materialLabel1);
            this.pnl_setting.Location = new System.Drawing.Point(12, 69);
            this.pnl_setting.Name = "pnl_setting";
            this.pnl_setting.Size = new System.Drawing.Size(430, 301);
            this.pnl_setting.TabIndex = 0;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(270, 245);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(107, 39);
            this.btn_save.TabIndex = 11;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_initialcatalog
            // 
            this.txt_initialcatalog.Location = new System.Drawing.Point(163, 138);
            this.txt_initialcatalog.Name = "txt_initialcatalog";
            this.txt_initialcatalog.Size = new System.Drawing.Size(214, 22);
            this.txt_initialcatalog.TabIndex = 10;
            // 
            // txt_datasource
            // 
            this.txt_datasource.Location = new System.Drawing.Point(153, 98);
            this.txt_datasource.Name = "txt_datasource";
            this.txt_datasource.Size = new System.Drawing.Size(224, 22);
            this.txt_datasource.TabIndex = 9;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(21, 136);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(136, 24);
            this.materialLabel3.TabIndex = 8;
            this.materialLabel3.Text = "initial Catalog :";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(21, 96);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(126, 24);
            this.materialLabel2.TabIndex = 7;
            this.materialLabel2.Text = "Data Source  :";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(12, 37);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(407, 24);
            this.materialLabel1.TabIndex = 6;
            this.materialLabel1.Text = "lütfen ayarlar kısmında bir değişiklik yapmayın ";
            // 
            // pnl_password
            // 
            this.pnl_password.Controls.Add(this.btn_connect);
            this.pnl_password.Controls.Add(this.txt_password);
            this.pnl_password.Controls.Add(this.materialLabel4);
            this.pnl_password.Location = new System.Drawing.Point(3, 69);
            this.pnl_password.Name = "pnl_password";
            this.pnl_password.Size = new System.Drawing.Size(448, 261);
            this.pnl_password.TabIndex = 1;
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(295, 116);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(133, 34);
            this.btn_connect.TabIndex = 2;
            this.btn_connect.Text = "connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(148, 63);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(280, 22);
            this.txt_password.TabIndex = 1;
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(12, 61);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(130, 24);
            this.materialLabel4.TabIndex = 0;
            this.materialLabel4.Text = "P_Password  :";
            // 
            // txt_update_pass
            // 
            this.txt_update_pass.Location = new System.Drawing.Point(163, 179);
            this.txt_update_pass.Name = "txt_update_pass";
            this.txt_update_pass.Size = new System.Drawing.Size(214, 22);
            this.txt_update_pass.TabIndex = 13;
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(21, 177);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(134, 24);
            this.materialLabel5.TabIndex = 12;
            this.materialLabel5.Text = "Password       :";
            // 
            // Frm_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 382);
            this.Controls.Add(this.pnl_setting);
            this.Controls.Add(this.pnl_password);
            this.Name = "Frm_settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar";
            this.pnl_setting.ResumeLayout(false);
            this.pnl_setting.PerformLayout();
            this.pnl_password.ResumeLayout(false);
            this.pnl_password.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_setting;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_initialcatalog;
        private System.Windows.Forms.TextBox txt_datasource;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.Panel pnl_password;
        private System.Windows.Forms.TextBox txt_password;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.TextBox txt_update_pass;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
    }
}