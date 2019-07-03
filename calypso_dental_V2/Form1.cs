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

namespace calypso_dental_V2
{
    public partial class frm_main :MaterialForm 
    {
        private Settings ayarlar = new Settings();
        public frm_main()
        {
            InitializeComponent();
            //  ayarlar.ServerIP = "192.168.1.15";

            var teet = grb_teeth.Controls.OfType<PictureBox>();
            foreach (PictureBox te in teet)
            {
                te.MouseClick += new MouseEventHandler((o, a) => onClickList(te));
            }
           
           
        }

        public void onClickList(PictureBox te)
        {
            if (te.BackColor == Color.DarkSlateGray)
            {
                te.BackColor = Color.Transparent;
            }
            else
            {
                te.BackColor = Color.DarkSlateGray;
            }

        }     
    }
   
    }

