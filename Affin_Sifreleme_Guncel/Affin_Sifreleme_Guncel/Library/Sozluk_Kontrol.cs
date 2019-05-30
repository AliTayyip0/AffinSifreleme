using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Affin_Sifreleme_Guncel.Library
{
    class Sozluk_Kontrol
    {
        public Sozluk_Kontrol()
        {
            
        }

        public void set_sozluk()
        {
            // TODO: yolda hata varsa engelleyici
            StreamReader sr = new StreamReader(Form1.sozluk_yolu, Encoding.GetEncoding("windows-1254"));
            string sozluge_eklenecek_kelime = sr.ReadLine();
            do
            {
                sozluk.Add(sozluge_eklenecek_kelime.ToLower());
                sozluge_eklenecek_kelime = sr.ReadLine();

            } while (sozluge_eklenecek_kelime != null);
        }

        ArrayList sozluk = new ArrayList();

        public int Metnin_Dogruluk_Puanini_Dondur(string puanı_hesaplanacak_metin)
        {
            int metinde_gelinilen_yerin_uzunlugu = 0;
            int toplam_puan=0,gecici_puan=0;
            string[] kelimeler = puanı_hesaplanacak_metin.Split(' ');

            for (int kelime_index = 0; kelime_index < kelimeler.Length; kelime_index++)
            {
                gecici_puan = Kelimenin_Puanı(kelimeler[kelime_index]);
                toplam_puan += gecici_puan;
                metinde_gelinilen_yerin_uzunlugu += kelimeler[kelime_index].Length;

                if (kelime_index < 2) continue; // bu satır en az 2+1 kelimeye bakıldıktan sonra yüzdelik hesaba bak demek

                if (metinde_gelinilen_yerin_uzunlugu*0.5f>toplam_puan) // yüzde 50 oranında eşitliğe bakılıyor
                {
                    return toplam_puan;
                }
            }

            return toplam_puan;
        }

        private int Kelimenin_Puanı(string kelime )    // burada hocanın gösterdiği en uzun alt dizin sayısı bulma algoritması kullanmak mantıklı
        {
            if (kelime == null || kelime.Length <= 0) return 0;
            int Puan=0,SeciliPuan=0, kelime_harf_index=0;

            string sozlukten_gelen_kelime = "";

            for (int sozluk_kelime_index = 0; sozluk_kelime_index < sozluk.Count; sozluk_kelime_index++)
            {
                sozlukten_gelen_kelime = sozluk[sozluk_kelime_index].ToString();
                Puan = 0;
                kelime_harf_index = 0;

                for (int sozluk_kelime_harf_index = 0; sozluk_kelime_harf_index < sozlukten_gelen_kelime.Length; sozluk_kelime_harf_index++)  // çok fazla sıkıntılı durum var bvurda
                {
                    if (kelime[kelime_harf_index].ToString().ToLower() == sozlukten_gelen_kelime[sozluk_kelime_harf_index].ToString().ToLower())  // kesinlikle hocanın gösterdiği algoritma kullanılmalı!!!!
                    {
                        Puan++;

                        kelime_harf_index++;

                        if (SeciliPuan < Puan) SeciliPuan = Puan;
                        
                        if (kelime.Length <= kelime_harf_index) break; 
                    }
                }
            }



            return SeciliPuan;
        }



    }
}
