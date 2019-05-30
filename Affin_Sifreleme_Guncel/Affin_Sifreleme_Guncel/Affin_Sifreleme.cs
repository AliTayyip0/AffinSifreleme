using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Affin_Sifreleme_Guncel.Library;

namespace Affin_Sifreleme_Guncel
{
    public partial class Affin_Sifreleme : UserControl
    {
        Alfabe alfabem;
        int alfabedeki_harf_sayisi;


        public Affin_Sifreleme()
        {
            alfabem = new Alfabe();
            alfabedeki_harf_sayisi = alfabem.Get_Alfabedeki_Harf_Satisi();
            InitializeComponent();
            pnlSifreleme.Visible = true;
            pnlCozme.Visible = false;
        }




        private void btnSifrele_Click(object sender, EventArgs e)
        {
            pnlSifreleme.Visible = true;
            pnlCozme.Visible = false;
        }

        private void btnSifreCoz_Click(object sender, EventArgs e)
        {
            pnlSifreleme.Visible = false;
            pnlCozme.Visible = true;
        }



        #region Sifre Çözme

        private void btnSifreyiCoz_Click(object sender, EventArgs e)
        {
            txtCozulmusMetin.Text = "";
            // anahtar a ve b ye harf girimini engelle!!!

            int anahtarA = 0;
            int anahtarB = 0;
            if (rbtnAnahtarli.Checked == true)
            {
                if ((txtAnahtarACozme.Text == "" || txtAnahtarACozme.Text == null || txtAnahtarBCozme.Text == null || txtAnahtarBCozme.Text == ""))
                {
                    MessageBox.Show("Anahtar Değerlerini Giriniz.");
                    return;
                }
                anahtarA = Convert.ToInt32(txtAnahtarACozme.Text);
                anahtarB = Convert.ToInt32(txtAnahtarBCozme.Text);
            }

            string cozulecek_metin = txtCozulecekMetin.Text;
            string cozulmus_metin = "";



            Decryption sifre_cozme_sinifi = new Decryption();



            if (rbtnAnahtarli.Checked == true)
            {
                cozulmus_metin = sifre_cozme_sinifi.Metnin_Sifresini_Coz(cozulecek_metin, anahtarA, anahtarB);
            }
            else
            {
                cozulmus_metin = sifre_cozme_sinifi.Metnin_Sifresini_Coz(cozulecek_metin);
            }

            txtCozulmusMetin.Text = cozulmus_metin;

        }

        private void rbtnAnahtarli_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAnahtarli.Checked == true) pnlAnahtarlarKapaAc.Visible = true;
            else pnlAnahtarlarKapaAc.Visible = false;
        }

        #endregion












        #region Şifreleme

        private void btnSifre_Click(object sender, EventArgs e)
        {
            txtSifrelenmisMetin.Text = "";
            // anahtar a ve b ye harf girimini engelle!!!
            if (txtAnahtarA.Text == "" || txtAnahtarA.Text == null || txtAnahtarB.Text == null || txtAnahtarB.Text == "")
            {
                MessageBox.Show("Anahtar Değerlerini Giriniz.");
                return;
            }
            int anahtarA = Convert.ToInt32(txtAnahtarA.Text);
            int anahtarB = Convert.ToInt32(txtAnahtarB.Text);

            string sifrelenecek_metin = txtSifrelenecekMetin.Text;
            string sifrelenmis_metin = "";
            Encryption sifreleme_sinifi = new Encryption();
            sifrelenmis_metin = sifreleme_sinifi.Metni_Sifrele(sifrelenecek_metin, anahtarA, anahtarB);
            txtSifrelenmisMetin.Text = sifrelenmis_metin;
        }

        private void btnRandomAnahtar_Click(object sender, EventArgs e)
        {
            int random_A = 0;
            int random_B = 0;
            bool aralarinda_asal_mi = false;

            Library.Keys anahtar_control = new Library.Keys();

            Random rnd = new Random();
            random_B = rnd.Next(0, alfabedeki_harf_sayisi);
            while (aralarinda_asal_mi == false)
            {
                random_A = rnd.Next(0, alfabedeki_harf_sayisi - 1);
                aralarinda_asal_mi = anahtar_control.Get_Sayilarin_Aralarinda_Asalligi(random_A, alfabedeki_harf_sayisi);
            }
            txtAnahtarA.Text = random_A.ToString();
            txtAnahtarB.Text = random_B.ToString();
        }

        #endregion



    }
}