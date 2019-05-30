using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affin_Sifreleme_Guncel.Library
{
    class Keys
    {
        
        public Keys()
        {
            
        }


        public bool Get_Sayilarin_Aralarinda_Asalligi(int birincisayi, int ikincisayi)
        {
            // TODO: parametre kontrol koy

            return Sayilarin_Aralarinda_Asalligi(birincisayi, ikincisayi);
        }


        public int Get_Moduler_Ters_Dondur(int tersialinacak, int temelsayi)
        {
            // TODO: parametre kontrol koy

            return Moduler_Ters_Döndür(tersialinacak, temelsayi);
        }


        private bool Sayilarin_Aralarinda_Asalligi(int anahtar_A, int harf_Sayisi)
        {
            // http://bilgisayarkavramlari.sadievrenseker.com/2009/10/26/obeb-gcd/
            // A nahtarının klavyeye göre asallığına bakıyoruz burada
            // asallığa bakılırken en büyük ortak böleni arıyoruz ve en büyük ortak bölenleri 1 ise aralarında asaldırlar diyoruz

            if (harf_Sayisi == 0)
            {
                if (anahtar_A == 1) return true;
                else return false;
            }
            else
            {
                return Sayilarin_Aralarinda_Asalligi(harf_Sayisi, anahtar_A % harf_Sayisi);
            }
        }

        /// <summary>
        /// temel sayi = alfabedeki harf sayısı
        /// tersi alınacak dediğimizde moduler tersini döndürmek istediğimiz sayı
        /// </summary>
        /// <param name="tersi_alinacak"></param>
        /// <param name="temel_Sayi"></param>
        /// <returns></returns>
        private int Moduler_Ters_Döndür(int tersi_alinacak, int temel_Sayi) // TODO: Bura hızlandırılabilir mi?
        {
            try
            {
                if (!Sayilarin_Aralarinda_Asalligi(tersi_alinacak, temel_Sayi)) return -1;
                int alfabedeki_harf_sayisi_gecici = temel_Sayi;
                while (true)
                {
                    //Burada sonsuz döngüye girmemesinin sebebi a sayısı ile alfabenin aralarında asal oluşu
                    if ((alfabedeki_harf_sayisi_gecici + 1) % tersi_alinacak == 0) return ((alfabedeki_harf_sayisi_gecici + 1) / tersi_alinacak);
                    else alfabedeki_harf_sayisi_gecici += temel_Sayi;
                }
            }
            catch (Exception)
            {

            }
            return -1; // Hatalı dönüş(-1);
        }

    }
}
