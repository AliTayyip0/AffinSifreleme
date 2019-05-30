using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Affin_Sifreleme_Guncel.Library;
namespace Affin_Sifreleme_Guncel
{
    class Encryption
    {

        int alfabedeki_harf_sayisi;

        Alfabe alfabem;
        Keys anahtar_islemleri;
        public Encryption()
        {
            alfabem = new Alfabe();
            anahtar_islemleri = new Keys();
            alfabedeki_harf_sayisi = alfabem.Get_Alfabedeki_Harf_Satisi();
        }
        


        public string Metni_Sifrele(string sifrelenecek_metin, int anahtar_A, int anahtar_B)
        {
            bool anahtar_A_asal_mi = anahtar_islemleri.Get_Sayilarin_Aralarinda_Asalligi(anahtar_A, alfabedeki_harf_sayisi);


            if (sifrelenecek_metin==null && sifrelenecek_metin.Length<1 || anahtar_A==null || anahtar_B==null || anahtar_A<1 || anahtar_B<1 || !anahtar_A_asal_mi)
            {
                return "Hata: islemyapilamadı #9987";
            }
            


            string sifrelenmis_metin = "";
            char metinde_Secili_harf = ' ';
            char eklenecek_harf;

            for (int i = 0; i < sifrelenecek_metin.Length; i++)
            {
                metinde_Secili_harf = sifrelenecek_metin[i];
                eklenecek_harf = Harfi_Sifrele(metinde_Secili_harf, anahtar_A, anahtar_B);
                sifrelenmis_metin += eklenecek_harf;
            }

            return sifrelenmis_metin;
        }

        private char Harfi_Sifrele(char harf, int anahtar_A, int anahtar_B)
        {
            int alfabemdeki_index = -1; // -1 ilk atama için -1 seçme sebebimiz hatalı dönüşleri yakalamak
            char yeni_harf;

            alfabemdeki_index = alfabem.Get_Harf_Index_Karsiligi(harf); // gelen harfin kendi olusturduğumuz alfabedeki index karsılıgını buluyoruz

            if (alfabemdeki_index == -1)
            {
                yeni_harf = harf;
            }
            else  
            {
                // TODO : burada hangi şifreleme türü yapılmak isteniyorsa onu yap - yeni şifrelenme türleri eklenebilir sadece buraya bir if koyaark
                alfabemdeki_index = (anahtar_A * alfabemdeki_index + anahtar_B) % alfabedeki_harf_sayisi;
                yeni_harf = alfabem.Get_Index_Harf_Karsiligi(alfabemdeki_index);
            }



            return yeni_harf;
        }



    }
}


#region Silinecek


//private bool Sayilarin_Aralarinda_Asalligi(int anahtar_A,int harf_Sayisi)
//{
//    // http://bilgisayarkavramlari.sadievrenseker.com/2009/10/26/obeb-gcd/
//    // A nahtarının klavyeye göre asallığına bakıyoruz burada
//    // asallığa bakılırken en büyük ortak böleni arıyoruz ve en büyük ortak bölenleri 1 ise aralarında asaldırlar diyoruz

//    if (harf_Sayisi==0)
//    {
//        if (anahtar_A == 1) return true;
//        else return false;
//    }
//    else
//    {
//        return Sayilarin_Aralarinda_Asalligi(harf_Sayisi, anahtar_A % harf_Sayisi);
//    }
//}


/*
private bool Sayilarin_Aralarinda_Asalligi2(int anahtar_A) 
{
    // A nahtarının klavyeye göre asallığına bakıyoruz burada

    #region Aralarında asalmılar diye baktığımız yer
    anahtar_A = anahtar_A % alfabedeki_harf_sayisi;
    for (int i = 2; i <= anahtar_A; i++)
    {
        if (anahtar_A % i == 0 && alfabedeki_harf_sayisi % i == 0) return false;
    }
    #endregion

    return true;
}
*/


#endregion