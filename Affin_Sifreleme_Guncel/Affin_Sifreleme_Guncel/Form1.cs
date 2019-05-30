using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affin_Sifreleme_Guncel
{
    public partial class Form1 : Form
    {

        public static string sozluk_yolu = "";


        public Form1()
        {
            InitializeComponent();
            sozluk_yolu = Dosya_Yolu_Al(); 

        }


        private string Dosya_Yolu_Al()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text Dosyası |*.txt";  
            file.Title = "Sözlük Dosyası Seçiniz..";
            file.ShowDialog();

            string DosyaYolu = file.FileName;
            string DosyaAdi = file.SafeFileName;
            return DosyaYolu;
        }


        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnAffin_Click(object sender, EventArgs e)
        {
            affin_Sifreleme1.Visible = !affin_Sifreleme1.Visible;
        }
    }
}
