namespace calypso_dental_V2
{
    partial class Frm_add_payment
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
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.txt_dr_name = new System.Windows.Forms.TextBox();
            this.txt_dr_debt = new System.Windows.Forms.TextBox();
            this.dtp_payment_date = new System.Windows.Forms.DateTimePicker();
            this.txt_payment = new System.Windows.Forms.TextBox();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(71, 104);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(109, 24);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "Doktor Adı :";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(50, 141);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(130, 24);
            this.materialLabel2.TabIndex = 1;
            this.materialLabel2.Text = "Doktor Borcu :";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(47, 180);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(133, 24);
            this.materialLabel3.TabIndex = 2;
            this.materialLabel3.Text = "Ödeme Tarihi :";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(46, 218);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(134, 24);
            this.materialLabel4.TabIndex = 3;
            this.materialLabel4.Text = "Ödediği Tutar :";
            // 
            // txt_dr_name
            // 
            this.txt_dr_name.Enabled = false;
            this.txt_dr_name.Location = new System.Drawing.Point(186, 106);
            this.txt_dr_name.Name = "txt_dr_name";
            this.txt_dr_name.Size = new System.Drawing.Size(234, 22);
            this.txt_dr_name.TabIndex = 4;
            // 
            // txt_dr_debt
            // 
            this.txt_dr_debt.Enabled = false;
            this.txt_dr_debt.Location = new System.Drawing.Point(186, 143);
            this.txt_dr_debt.Name = "txt_dr_debt";
            this.txt_dr_debt.Size = new System.Drawing.Size(234, 22);
            this.txt_dr_debt.TabIndex = 5;
            // 
            // dtp_payment_date
            // 
            this.dtp_payment_date.Location = new System.Drawing.Point(186, 182);
            this.dtp_payment_date.Name = "dtp_payment_date";
            this.dtp_payment_date.Size = new System.Drawing.Size(234, 22);
            this.dtp_payment_date.TabIndex = 6;
            // 
            // txt_payment
            // 
            this.txt_payment.Location = new System.Drawing.Point(186, 218);
            this.txt_payment.Name = "txt_payment";
            this.txt_payment.Size = new System.Drawing.Size(199, 22);
            this.txt_payment.TabIndex = 7;
            this.txt_payment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(390, 216);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(21, 24);
            this.materialLabel5.TabIndex = 8;
            this.materialLabel5.Text = "₺";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(328, 262);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(92, 46);
            this.btn_save.TabIndex = 9;
            this.btn_save.Text = "Kaydet";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(230, 262);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(92, 46);
            this.btn_cancel.TabIndex = 10;
            this.btn_cancel.Text = "İptal";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // Frm_add_payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 338);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.txt_payment);
            this.Controls.Add(this.dtp_payment_date);
            this.Controls.Add(this.txt_dr_debt);
            this.Controls.Add(this.txt_dr_name);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.MaximumSize = new System.Drawing.Size(487, 338);
            this.Name = "Frm_add_payment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ödeme Ekleme";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private System.Windows.Forms.TextBox txt_dr_name;
        private System.Windows.Forms.TextBox txt_dr_debt;
        private System.Windows.Forms.DateTimePicker dtp_payment_date;
        private System.Windows.Forms.TextBox txt_payment;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
    }
}