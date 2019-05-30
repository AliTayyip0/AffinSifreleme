using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affin_Sifreleme_Guncel
{
    class Alfabe
    {

        char[] alfabe;
        int alfabedeki_harf_sayisi;

        public Alfabe()
        {
            alfabe = new char[] { '0', '1', '2', 'A', 'B', '3', '4', 'ü', 'v', '5', 'z', 'C', '7', 'I', '8', 'y', 'Ç', '9', 'a', 'b', 'c', 'ç', 'd', 'e', 'f', 'g', 'ğ', 'h', 'U', 'Ü', 'ı', 'i', 'j', 'k', 'l', 'm', 'ö', 'p', 'r', 's', 'ş', 't', 'u', 'D', 'E', 'F', 'G', 'Ğ', 'H', 'İ', 'J', 'N', 'O', 'K', 'L', 'M', 'Ö', 'P', 'R', 'n', '6', 'o', 'S', 'Ş', 'T', 'V', 'Y', 'Z', '*','!'/*, '-' */};
            alfabedeki_harf_sayisi = alfabe.Length;
        }

        /// <summary>
        /// sonucunda harfin benim alfabemdeki index karşılığını döndürür
        /// </summary>
        /// <param name="harf"></param>
        /// <returns></returns>
        public int Get_Harf_Index_Karsiligi(char harf)
        {
            for (int i = 0; i < alfabe.Length; i++)
            {
                if (alfabe[i] == harf)
                {
                    return i;
                }
            }

            return -1;
        }
        //s


        /// <summary>
        /// sonucunda alfabe indexi verilen harfi döndürür
        /// </summary>
        /// <param name="ascii_karsiligi"></param>
        /// <returns></returns>
        public char Get_Index_Harf_Karsiligi(int alfabe_index)
        {
            return alfabe[alfabe_index % alfabedeki_harf_sayisi];    
        }
        //h



        public int Get_Alfabedeki_Harf_Satisi()
        {
            if(alfabedeki_harf_sayisi!=null)
                return alfabedeki_harf_sayisi;
            else
                return -1;
        }


    }
}
