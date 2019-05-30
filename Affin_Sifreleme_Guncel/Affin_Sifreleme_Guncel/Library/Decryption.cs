using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Affin_Sifreleme_Guncel.Library;

namespace Affin_Sifreleme_Guncel
{
    class Decryption
    {
        // Sözlük ve Alfabe classları ile entegre çalışır!

        Alfabe alfabem;
        Keys anahtar_islemleri;
        Sozluk_Kontrol Sozluk_Islemleri;
        
        int alfabedeki_harf_sayisi;



        public Decryption()
        {
            alfabem = new Alfabe();
            alfabedeki_harf_sayisi = alfabem.Get_Alfabedeki_Harf_Satisi();

            Sozluk_Islemleri = new Sozluk_Kontrol();
            Sozluk_Islemleri.set_sozluk();

            anahtar_islemleri = new Keys();
        }

 

        




        #region Metinlerin harflere göre parçalayıp şifre çözücü programa verip sonucu döndüren fonksiyonlar ( PUBLİC )

        /// <summary>
        /// Metini Anahtarsız çözdüğünden dolayı alfabedeki harf sayısının karesi kadar sonucu döndürür. Tüm olası sonuclar dondurulmuş olacaktır.
        /// </summary>
        /// <param name="sifreli_metin"></param>
        /// <returns></returns>
        public string Metnin_Sifresini_Coz(string sifreli_metin)
        {
            string[,] cozulen_metinler = new string[alfabedeki_harf_sayisi, alfabedeki_harf_sayisi];
            int moduler_ters_A = -1;
            char sifresi_cozulmus_harf = ' ';
            char sifresi_cozulecek_harf = ' ';


            // Bu altta tüm çözülen metinlerin bir diziye toplanması işlemi yapılıyor
            for (int anahtarA = 0; anahtarA < alfabedeki_harf_sayisi; anahtarA++)
            {
                if (!anahtar_islemleri.Get_Sayilarin_Aralarinda_Asalligi(anahtarA, alfabedeki_harf_sayisi)) continue;   //"Hata: A Anahtarı asallıktan dolayı Kullanılamaz";

                moduler_ters_A = anahtar_islemleri.Get_Moduler_Ters_Dondur(anahtarA, alfabedeki_harf_sayisi);//Moduler_Ters_Döndür(anahtarA);
                if (moduler_ters_A == -1) continue;
                for (int anahtarB = 0; anahtarB < alfabedeki_harf_sayisi; anahtarB++)
                {
                    for (int cozulecek_harf_index = 0; cozulecek_harf_index < sifreli_metin.Length; cozulecek_harf_index++)
                    {
                        sifresi_cozulecek_harf = sifreli_metin[cozulecek_harf_index];
                        sifresi_cozulmus_harf = Harfin_Sifresini_Coz(sifresi_cozulecek_harf, anahtarA, anahtarB, moduler_ters_A);
                        cozulen_metinler[anahtarA, anahtarB] += sifresi_cozulmus_harf;
                    }
                }
            }

            //return cozulen_metinler;


            // Burdan altta ise çözülen metinler arasından en yüksek puana sahip olanın bulunma işlemi
            int[] dogru_anahtar_indexleri = Cozulmus_Metinler_Arasindan_Dogru_Anahtarlarin_Indexlerini_Getir(cozulen_metinler);

            int dogru_anahtar_A = dogru_anahtar_indexleri[0];

            int dogru_anahtar_B = dogru_anahtar_indexleri[1];

            string dogru_metin = cozulen_metinler[dogru_anahtar_A, dogru_anahtar_B];


            return dogru_metin;



        }

        public string Metnin_Sifresini_Coz(string sifreli_metin, int anahtarA, int anahtarB)
        {
            // Bu anahtar değerleri biliniyorsa kullanılıyor
            // TODO: eğer alfabe aynı alfabe değilse??!!


            if (!anahtar_islemleri.Get_Sayilarin_Aralarinda_Asalligi(anahtarA, alfabedeki_harf_sayisi)) return null;//"Hata: A Anahtarı Kullanılamaz";
            // TODO: bu ifin silinmesi gerekebilir


            string cozulmus_metin = "";
            char sifresi_cozulmus_harf = ' ';
            char sifresi_cozulecek_harf = ' ';
            for (int i = 0; i < sifreli_metin.Length; i++)
            {
                sifresi_cozulecek_harf = sifreli_metin[i];
                sifresi_cozulmus_harf = Harfin_Sifresini_Coz(sifresi_cozulecek_harf, anahtarA, anahtarB);
                cozulmus_metin += sifresi_cozulmus_harf;
            }

            return cozulmus_metin;
        }

        #endregion




        #region Harflerin Şifrelerinin Anahtarlı ve Anahtarsız Çözümleri İçin Gereken Fonksiyonlar

        private char Harfin_Sifresini_Coz(char Cozulecek_harf, int anahtarA, int anahtarB)
        {
            int moduler_a = anahtar_islemleri.Get_Moduler_Ters_Dondur(anahtarA, alfabedeki_harf_sayisi); //Moduler_Ters_Döndür(anahtarA);

            if (moduler_a == -1) return Cozulecek_harf;

            char cozulmus_harf = ' ';

            #region Anahtarlı çözüm için


            int harfin_alfabemdeki_indexi = alfabem.Get_Harf_Index_Karsiligi(Cozulecek_harf);
            if (harfin_alfabemdeki_indexi == -1) return Cozulecek_harf;
            int harfin_cozulmus_indexi = (moduler_a * (harfin_alfabemdeki_indexi - anahtarB)) % alfabedeki_harf_sayisi;
            if (harfin_cozulmus_indexi < 0) harfin_cozulmus_indexi = harfin_cozulmus_indexi + alfabedeki_harf_sayisi;

            cozulmus_harf = alfabem.Get_Index_Harf_Karsiligi(harfin_cozulmus_indexi);


            #endregion

            return cozulmus_harf;
        }

        // bunu anahtarsız çözümler için yaptım moduler tersi parametre olarak vermek sistemi epey hızlandırıyor
        private char Harfin_Sifresini_Coz(char Cozulecek_harf, int anahtarA, int anahtarB, int moduler_ters)
        {
            int moduler_a = moduler_ters;

            if (moduler_a == -1) return Cozulecek_harf;

            char cozulmus_harf = ' ';

            #region Anahtarlı çözüm için


            int harfin_alfabemdeki_indexi = alfabem.Get_Harf_Index_Karsiligi(Cozulecek_harf);
            if (harfin_alfabemdeki_indexi == -1) return Cozulecek_harf;
            int harfin_cozulmus_indexi = (moduler_a * (harfin_alfabemdeki_indexi - anahtarB)) % alfabedeki_harf_sayisi;
            if (harfin_cozulmus_indexi < 0) harfin_cozulmus_indexi = harfin_cozulmus_indexi + alfabedeki_harf_sayisi;

            cozulmus_harf = alfabem.Get_Index_Harf_Karsiligi(harfin_cozulmus_indexi);


            #endregion

            return cozulmus_harf;
        }

        #endregion



        

        #region Sözlük kısmı ile etkileşime geçilen ve tüm ihtimalleri denedikten sonra olası sonucu döndüren fonksiyonlar

        int en_iyi_puan = 0;
        int en_iyi_puan_anahtarA;
        int en_iyi_puan_anahtarB;

        private int[] Cozulmus_Metinler_Arasindan_Dogru_Anahtarlarin_Indexlerini_Getir(string[,] cozulen_metinler)
        {
            int[] anahtarların_indexleri = new int[2];
            string islem_gorecek_metin = null;
            int dogruluk_Puani = 0;

            //Bu forlarda tüm çözülmüş metinlerin puanları hesaplanıyor ve en büyük olan seçiliyır
            for (int anahtarA = 0; anahtarA < alfabem.Get_Alfabedeki_Harf_Satisi()/*Math.Abs(cozulen_metinler.Length)*/; anahtarA++)
            {
                for (int anahtarB = 0; anahtarB < alfabem.Get_Alfabedeki_Harf_Satisi()/*Math.Abs(cozulen_metinler.Length)*/; anahtarB++)
                {
                    islem_gorecek_metin = cozulen_metinler[anahtarA, anahtarB];
                    if (islem_gorecek_metin == null) continue;

                    dogruluk_Puani = PuanHesapla(Cumledeki_Noktalamalarin_Atilmasi(islem_gorecek_metin));

                    if (dogruluk_Puani > en_iyi_puan)
                    {
                        en_iyi_puan = dogruluk_Puani;
                        en_iyi_puan_anahtarA = anahtarA;
                        en_iyi_puan_anahtarB = anahtarB;
                    }

                }
            }

            anahtarların_indexleri[0] = en_iyi_puan_anahtarA;
            anahtarların_indexleri[1] = en_iyi_puan_anahtarB;

            // alttakilere gerek kalmadı ama istenirse tüm cumlelerin puanını göstermek için yapılabilir
            // Hesaplanan Puanlar bir diziye atansın 
            // burda bir fonksiyon en yüksek puanı belirlesin


            return anahtarların_indexleri;
        }

        private int PuanHesapla(string puanı_hesaplanacak_metin)
        {

            int Puan = -1;

            Puan = Sozluk_Islemleri.Metnin_Dogruluk_Puanini_Dondur(puanı_hesaplanacak_metin);

            return Puan;

        }

        #endregion








        private string Cumledeki_Noktalamalarin_Atilmasi(string metin)
        {
            metin.Replace(",", " ");
            metin.Replace(".", " ");
            metin.Replace("!", " ");
            metin.Replace("\'", " ");
            metin.Replace(":", " ");
            metin.Replace(";", " ");
            //metin.Replace("  ", " ");

            return metin;
        }

        

    }
}



#region Silinecek Bölüö

//private int Moduler_Ters_Döndür1(int kontrol_edilecek_sayi) // TODO: Bura hızlandırılabilir mi?
//{
//    try
//    {
//        for (int mod_ters = 1; mod_ters < alfabedeki_harf_sayisi; mod_ters++)
//        {
//            if ((kontrol_edilecek_sayi * mod_ters) % alfabedeki_harf_sayisi == 1)
//            {
//                return mod_ters;
//            }
//        }
//    }
//    catch (Exception)
//    {
//        // MessageBox.Show(ex.Message.ToString() + "  moduler ters");
//    }
//    return -1; // Hatalı dönüş(-1);++
//}

#endregion

