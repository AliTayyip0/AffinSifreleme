using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;

namespace DogrusalAffinDogrulama_V2._0
{
    public partial class Form1 : Form
    {
        #region Global tanımlamalar

        int alfabedeki_harf_sayisi = 68;
        ArrayList sozluk;
        ArrayList sozluk_index;
        bool sozluk_eklendi = false;
        int[,] tumpuanlar = new int[68, 68];

        #endregion

        public Form1()
        {
            InitializeComponent();
            Encoding.GetEncoding("iso-8859-9");
            for (int i = 0; i < alfabedeki_harf_sayisi; i++)
                for (int j = 0; j < alfabedeki_harf_sayisi; j++)
                    tumpuanlar[i, j] = 0;
        }

        #region fonksiyonlar

        int Moduler_ters_var_mi(int a)
        {
            try
            {
                for (int mod_ters = 0; mod_ters < alfabedeki_harf_sayisi; mod_ters++)
                {
                    if ((a * mod_ters) % alfabedeki_harf_sayisi == 1)
                    {
                        return mod_ters;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString() + "  moduler ters"); }
            return -1; // Hatalı dönüş(-1);++
        }

        int alfabe(int karsiligi_bulunacak_olan, string istenen_islem)
        {
            try
            {
                char[] alfabe = { '0', '1', '2', 'A', 'B', '3', '4', 'ü', 'v', '5',  'z', 'C', '7','I', '8', 'y', 'Ç', '9', 'a', 'b', 'c', 'ç', 'd', 'e', 'f', 'g', 'ğ', 'h','U', 'Ü', 'ı', 'i', 'j', 'k', 'l', 'm',  'ö', 'p', 'r', 's', 'ş', 't', 'u', 'D', 'E', 'F', 'G', 'Ğ', 'H',  'İ', 'J','N', 'O', 'K', 'L', 'M',  'Ö', 'P', 'R','n','6', 'o', 'S', 'Ş', 'T',  'V', 'Y', 'Z' };//68

                if (istenen_islem == "s")
                {
                    for (int i = 0; i < alfabe.Length; i++)   //   karsiligi_bulunacak_olan  ascii sayı karşılığı olmalı
                    {
                        if (Convert.ToChar(karsiligi_bulunacak_olan) == alfabe[i]) // karsiligi_bulunacak_olan harf
                        {
                            return i;
                        }
                    }
                }
                else if (istenen_islem == "h")   // karsiligi_bulunacak_olan 0-(Alfabedeki harf sayısı) arası olması
                {
                    for (int i = 0; i < alfabe.Length; i++)
                    {
                        if (karsiligi_bulunacak_olan == i)   // a sayı
                        {
                            return Convert.ToInt32(alfabe[i]);  // karsiligi_bulunacak_olan nın ascii karşılığını
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString() + "  alfabe"); }
            return -1; // hatalı dönüş (-1);++
        }

        string E(string sifrelenecek, int anahtarA, int anahtarB)
        {
            string sifreli_metin = "";
            try
            {
                for (int i = 0; i < sifrelenecek.Length; i++)
                {
                    if (alfabe(Convert.ToInt32(sifrelenecek[i]), "s") != -1)
                    {
                        int isleme_girecek = alfabe(Convert.ToInt32(sifrelenecek[i]), "s");
                        if (isleme_girecek != -1 && anahtarA != 0)
                        {
                            isleme_girecek = (anahtarA * isleme_girecek + anahtarB) % alfabedeki_harf_sayisi;

                            sifreli_metin += Convert.ToChar(alfabe(isleme_girecek, "h"));
                        }
                    }
                    else
                    {
                        sifreli_metin += sifrelenecek[i];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "  //E");
            }
            
            return sifreli_metin;
        }

        string[,] DE4(string cozulecek)
        {
            string[,] dizi = new string[alfabedeki_harf_sayisi, alfabedeki_harf_sayisi];
            int moduler_a = 0, y = 0;
            try
            {
                int a = 0, b = 0, sona = alfabedeki_harf_sayisi, sonb = alfabedeki_harf_sayisi;

                #region Anahtarlı çözüm için

                if (radioButton1.Checked == true && textBox2.Text != "" && textBox3.Text != "")
                {
                    a = Convert.ToInt32(textBox2.Text);
                    b = Convert.ToInt32(textBox3.Text);
                    sona = Convert.ToInt32(textBox2.Text) + 1;
                    sonb = Convert.ToInt32(textBox3.Text) + 1;
                }
                else
                {
                    a = 0;
                    b = 0;
                    sona = alfabedeki_harf_sayisi;
                    sonb = alfabedeki_harf_sayisi;
                }

                #endregion

                for (; a < sona; a++)
                {
                    moduler_a = Moduler_ters_var_mi(a);

                    if (moduler_a != -1) // Hatalı dönüş yakalama;
                    {
                        if (radioButton1.Checked == false) b = 0;
                        for (; b < sonb; b++)
                        {
                            bool boslukgeldi = false; // bunun sayesinde yalnızca ilk kelime sözlükte kontrole tabi tutuluyor // ilk aşamada
                            for (int i = 0; i < cozulecek.Length; i++)
                            {
                                if (alfabe(Convert.ToInt32(cozulecek[i]), "s") != -1)
                                {
                                    int x = alfabe(Convert.ToInt32(cozulecek[i]), "s");
                                    y = (moduler_a * (x - b)) % alfabedeki_harf_sayisi;
                                    if (y < 0) y = y + alfabedeki_harf_sayisi;
                                    dizi[a, b] += Convert.ToChar(alfabe(y, "h")).ToString();
                                }
                                else
                                {
                                    dizi[a, b] += cozulecek[i];
                                    if (cozulecek[i] == ' ') boslukgeldi = true;
                                }

                                if (boslukgeldi == true)
                                {
                                    string[] bosluk_sayisi_icin = dizi[a, b].Split(' ');
                                    tumpuanlar[a, b] += son_sozluk_kontrol2(dizi[a,b]);
                                    if (tumpuanlar[a, b] < 2 * bosluk_sayisi_icin.Length / 2) break;

                                    //if (son_sozluk_kontrol(dizi[a, b]) < 2 * bosluk_sayisi_icin.Length / 2) break;   //TODO: son_sozluk_kontrol burdan çağırılırsa her kelime için kontrol sağlana bilir

                                    boslukgeldi = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString() + "  //DE4"); }
            return dizi;
        }

        #endregion
        
        #region Sözlükle alakalı bölüm!
        
        int son_sozluk_kontrol2(string kontrol_edilecek)
        {
            int puan = 0;
            try
            {
                string[] dizi = kontrol_edilecek.Split(' ');
                int isleme_girecek = dizi.Length - 1;
                if (dizi[dizi.Length - 1] == "") isleme_girecek = isleme_girecek - 1;

                if (dizi[isleme_girecek] != "")
                        puan = sozluk_puan_sistemi_try1(dizi[isleme_girecek]);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString() + "  son sözlük kontrol"); }
            return puan;

        }
        
        int sozluk_puan_sistemi_try1(string kontrol_edilecek)
        {
            int gecici_puan = 0, genel_puan = 0;

            int sayisi = 0;
            for (int i = 0; i < kontrol_edilecek.Length; i++)
            {
                if (kontrol_edilecek[i].ToString() == "0" || kontrol_edilecek[i].ToString() == "1" || kontrol_edilecek[i].ToString() == "2" || kontrol_edilecek[i].ToString() == "3" || kontrol_edilecek[i].ToString() == "4" || kontrol_edilecek[i].ToString() == "5" || kontrol_edilecek[i].ToString() == "6" || kontrol_edilecek[i].ToString() == "7" || kontrol_edilecek[i].ToString() == "8" || kontrol_edilecek[i].ToString() == "9")
                {
                    sayisi++;
                }
            }
            if (kontrol_edilecek.Length == sayisi && sayisi != 0)
            {
                return sayisi;
            }

            int j, bayrak = 0;
            for (j = 0; j < sozluk_index.Count - 1; j++)
            {
                if (sozluk[Convert.ToInt32(sozluk_index[j])].ToString()[0] == kontrol_edilecek[0])
                {
                    bayrak = 1;
                    break;
                }
            }
            if (bayrak == 1)
            {
                if (j != sozluk_index.Count - 1)
                {
                    for (int i = Convert.ToInt32(sozluk_index[j]); i < Convert.ToInt32(sozluk_index[j + 1]); i++)
                    {
                        gecici_puan = 0;
                        if (kontrol_edilecek.Length > sozluk[i].ToString().Length)
                        {
                            for (int k = 0; k < sozluk[i].ToString().Length; k++)
                            {
                                if (kontrol_edilecek.ToLower()[k] == sozluk[i].ToString().ToLower()[k])
                                {
                                    gecici_puan += 1;
                                }
                                else break;
                            }
                        }
                        if (gecici_puan > genel_puan)
                        {
                            genel_puan = gecici_puan;
                        }
                    }
                }
                else
                {
                    for (int i = Convert.ToInt32(sozluk_index[j]); i < sozluk.Count; i++)
                    {
                        gecici_puan = 0;
                        if (kontrol_edilecek.Length > sozluk[i].ToString().Length)
                        {
                            for (int k = 0; k < sozluk[i].ToString().Length; k++)
                            {
                                if (kontrol_edilecek.ToLower()[k] == sozluk[i].ToString().ToLower()[k])
                                {
                                    gecici_puan += 1;
                                }
                                else break;
                            }
                        }
                        if (gecici_puan > genel_puan)
                        {
                            genel_puan = gecici_puan;
                        }
                    }
                }
            }
            return genel_puan;

        }

        void sozluk_indeksleme()
        {
            try
            {
                sozluk_index = new ArrayList();
                sozluk_index.Add("0");
                for (int i = 0; i < sozluk.Count; i++)
                {
                    if (alfabe(sozluk[Convert.ToInt32(sozluk_index[sozluk_index.Count - 1])].ToString()[0], "s") < alfabe(sozluk[i].ToString()[0], "s"))
                    {
                        sozluk_index.Add(i);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString() + "  sözlük indeksleme"); }
        }

        #endregion
        
        #region Genellikle görsel!

        private void button1_Click(object sender, EventArgs e)   // E fonksiyonunun icre edilmesini sağlayan buton -E-şifreleme
        {
            if (txtAnahtarA.Text == "" || txtAnahtarB.Text == "")
            {
                MessageBox.Show("anahtarları giriniz");
            }
            else
            {
                richTextBox2.Text = "";
                int anahtarA, bayrak = 1;

                #region Aralarında asalmılar diye baktığımız yer
                anahtarA = Convert.ToInt32(txtAnahtarA.Text) % alfabedeki_harf_sayisi;
                for (int i = 2; i <= anahtarA; i++)
                {
                    if (anahtarA % i == 0 && alfabedeki_harf_sayisi % i == 0) { bayrak = 1; break; }
                    else bayrak = 0;
                }
                #endregion

                if (bayrak == 0)
                {
                    txtAnahtarA.Text = Convert.ToString(Convert.ToInt32(txtAnahtarA.Text) % alfabedeki_harf_sayisi);
                    txtAnahtarB.Text = Convert.ToString(Convert.ToInt32(txtAnahtarB.Text) % alfabedeki_harf_sayisi);
                    richTextBox2.Text += E(txtSifrelenecek.Text, Convert.ToInt32(txtAnahtarA.Text), Convert.ToInt32(txtAnahtarB.Text));
                }
                else MessageBox.Show("A anahtar değeri ve ALFABEDEKİ HARF SAYISI sayısı birbiriyle arasında asal sayılar olmalı.");
            }
        }

        private void button2_Click(object sender, EventArgs e)   // şifrelenen metni kopyalamak için
        {
            if (richTextBox2.Text != "")
            {
                Clipboard.SetText(richTextBox2.Text);
                MessageBox.Show("kopyalandı:  " + Clipboard.GetText() + " \n kelime sayısı:" + richTextBox2.TextLength);
            }
            else MessageBox.Show("Kopyalanacak bir metin yok!");
        }

        private void button3_Click(object sender, EventArgs e)   // Şifrelenmiş metni çözme ve listbox a yazma
        {
            if (sozluk_eklendi == true)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                richTextBox1.Text = "";
                textBox1.Text = textBox1.Text.Trim();
                ArrayList array = new ArrayList();
                string[,] dizi = DE4(textBox1.Text);  // TODO: baştan ve sondan boşluklar atılıp fonk ole çağırılacak

                int enbuyuk = tumpuanlar[0, 0];
                string en_buyugun_yazisi = "";

                for (int i = 0; i < alfabedeki_harf_sayisi; i++)
                {
                    for (int j = 0; j < alfabedeki_harf_sayisi; j++)
                    {
                        if (dizi[i, j] != null && dizi[i,j].Length==textBox1.Text.Length)
                        { 
                        if (enbuyuk < tumpuanlar[i, j])
                        {
                            enbuyuk = tumpuanlar[i, j];
                            listBox2.Items.Add(dizi[i,j]);
                            en_buyugun_yazisi = dizi[i, j];
                        }
                        listBox1.Items.Add(dizi[i,j]);
                        }
                    }
                }
                
                richTextBox1.Text = en_buyugun_yazisi;
               
            }
            else MessageBox.Show("Sözlüğü seçiniz");
        }

        private void button4_Click(object sender, EventArgs e)   // Sözlük için dosya seçimi
        {
            //sözlüğü içeri aktarmak için
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Sözlüğünüzü Seçiniz!";
            file.Filter = "txt Dosyası .txt|*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                string DosyaYolu1 = file.FileName;
                sozluk = new ArrayList();
                StreamReader sr = new StreamReader(DosyaYolu1);
                int i = 0;
                string satir = "a";
                do
                {
                    sozluk.Add(satir.ToString());
                    i++;
                    satir = sr.ReadLine();

                } while (!(null == satir));
                sozluk_eklendi = true;
                sozluk_indeksleme();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                Clipboard.SetText(richTextBox1.Text);
                MessageBox.Show("kopyalandı:  " + Clipboard.GetText()+"\nKarakter sayısı:"+richTextBox1.Text.Length);
            }
            else MessageBox.Show("Kopyalanacak bir metin yok!");
        }

        private void txtAnahtarA_KeyPress(object sender, KeyPressEventArgs e)  // Anahtar değerlerine yalnızca rakam girilmesi için
        {
            e.Handled = Char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) e.Handled = false;//eğer rakamsa  yazdır.
            else if ((int)e.KeyChar == 8) e.Handled = false;//eğer basılan tuş backspace ise yazdır.
            else e.Handled = true;//bunların dışındaysa hiçbirisini yazdırma
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Visible = !textBox2.Visible;
            textBox3.Visible = !textBox3.Visible;
            label6.Visible = !label6.Visible;
            label7.Visible = !label7.Visible;
            label8.Visible = !label8.Visible;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = listBox2.SelectedItem.ToString();
        }

        #endregion
        
    }
}