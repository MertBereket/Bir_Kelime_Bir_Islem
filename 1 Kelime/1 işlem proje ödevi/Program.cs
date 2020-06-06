using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bir_kelime_proje_ödevi
{
    class Program
    {
        public static List<string> dosya_okuma(List<string> okunan_kelime_list)
        {
            string dosya_yolu = @"D:\sozluk.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);

            string okunan_kelime = sw.ReadLine();           //Dosyadan satırdaki kelimeyi okuyor.
            while (okunan_kelime != null)           //Okunan kelime null (boş) olana kadar dönecek.
            {
                okunan_kelime_list.Add(okunan_kelime);          //İşlemler daha hızlı yapılsın diye okunan kelimeleri listeye atacak.
                okunan_kelime = sw.ReadLine();
            }
            sw.Close();
            fs.Close();

            return okunan_kelime_list;
        }

        public static List<char> harf_alma(List<char> karakter)
        {
            Random rastgele = new Random();
            char[] alfabe = { 'a', 'b', 'c', 'ç', 'd', 'e', 'f', 'g', 'ğ', 'h', 'i', 'ı', 'j', 'k', 'l', 'm', 'n', 'o', 'ö', 'p', 'r', 's', 'ş', 't', 'u', 'ü', 'v', 'y', 'z' };            //Rastgele harfler alabilmek için oluşturulmuş bir alfabe dizisi.
            int rastgele_sayi;

            while (true)            //'e' veya 'h' olacak şekilde doğru cevabı alacak kadar dönmesini sağlıyor.
            {
                Console.WriteLine("Harfler random atılsın mı?(e/h)");
                char c = Convert.ToChar(Console.ReadLine());
                if (c == 'e')           //Rastgele harf almak için 'e' harfi girilmiş mı onu kontrol ediyor.
                {
                    for (int j = 0; j < 8; j++)
                    {
                        rastgele_sayi = rastgele.Next(0, 29);           //Diziden rastgele harf almak için rastgele sayı alıyor.
                        karakter.Add(alfabe[rastgele_sayi]);            //Alınan rastgele sayı ile dizinin o indexteki elemanını karakter isimli listeye ekliyor.
                    }
                    break;
                }
                if (c == 'h')           //Harfleri kullanıcıdan almak için 'h' harfi girilmiş mı onu kontrol ediyor.
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("{0}. harfi giriniz=", i + 1);
                        karakter.Add(Convert.ToChar(Console.ReadLine()));
                    }
                    break;
                }
                else            //Eğer 'e' veya 'h' harflerinden biri girilmediyse "Hatalı GİRİŞ!!" yazısını bastırarak döngüye tekrar girer.
                {
                    Console.WriteLine("Hatalı GİRİŞ!!");
                }
            }

            for (int k = 0; k < 8; k++)         //Döngü 8 defa dönerek karakter isimli listedeki harfleri ekrana yazdırır.
            {
                Console.Write(karakter[k] + " ");
            }
            Console.WriteLine();

            return karakter;
        }

        public static bool kontrol(List<char> karakter, string item, List<char> sayac, bool bool_kontrol)
        {
            List<char> okunan_kelime_harf = new List<char>(item.ToCharArray());         //Main methodun içerisindeki foreach fonksonundan gelen item kelimesini harflere ayırarark okunan kelime harf listesine ekliyor.
            for (int i = 7; i >= 0; i--)
            {
                for (int j = okunan_kelime_harf.Count - 1; j >= 0; j--)
                {
                    int harf_kontrol = string.Compare(Convert.ToString(karakter[i]), Convert.ToString(okunan_kelime_harf[j]));          //okunan_kelime_harf isimli liste ile karakter isimli liste tek tek karşılaştırılıyor eğer iki harf aynı ise harf_kontrol isimli değişkene 0 değeri atanıyor.
                    if (harf_kontrol == 0)          //Eğer harf_kontrol isimli değişken 0'a eşit ise if içerisine girerek okunan_kelime_harf listesinden herf silinir ve sayaç isimli listeye '*' simgesi eklenir.
                    {
                        okunan_kelime_harf.Remove(okunan_kelime_harf[j]);
                        sayac.Add('*');
                        break;
                    }
                }
            }
            if (item.Length == sayac.Count || item.Length == sayac.Count + 1)           //Eğer item değişkeninin içindeki kelimenin uzunluğu sayac listesinin uzunluğuna eşitse veya item değişkeninin içindeki kelimenin uzunluğu sayac listesinin uzunluğunun bir fazlası ise item kelimsini ekrana yazdır ve bool_kontrol isimli değişkenin değerini true yapar.
            {
                Console.WriteLine(item);
                bool_kontrol = true;
            }
            sayac.Clear();
            return bool_kontrol;
        }

        public static int puanlama (string item, int puan)
        {
            switch (item.Length)            //item isimli değişkenin içerisindeki kelimenin uzunluğuna göre puanı belirliyor.
            {
                case 9:
                    puan = 15;
                    break;

                case 8:
                    puan = 11;
                    break;

                case 7:
                    puan = 9;
                    break;

                case 6:
                    puan = 7;
                    break;

                case 5:
                    puan = 5;
                    break;

                case 4:
                    puan = 4;
                    break;

                case 3:
                    puan = 3;
                    break;
            }
            return puan;
        }

        static void Main(string[] args)
        {
            List<string> okunan_kelime_list = new List<string>();
            List<char> karakter = new List<char>();
            List<char> sayac = new List<char>();
            bool bool_kontrol = false;
            int puan = 0;
            //Gerekli değişkenler oluşturuldu.

            dosya_okuma(okunan_kelime_list);
            harf_alma(karakter);

            for (int n = 9; n >= 3; n--)            //Bulunabilecek en uzun kelimeyi bulmak için 9 dan geriye 3 e kadar saydırıyoruz.
            {
                foreach (string item in okunan_kelime_list)         //okunan_kelime_list listesinin uzunluğu kadar döngüyü devam ettiriyor. Böylelikle her kelime tek tek kontrol ediliyor.
                {
                    if (item.Length == n)           //Eğer item değişkeni içerisindeki kelime uzunluğu n değişkeninin içerisindeki sayıya eşit ise kontrol methodunu çalıştırır ve methottan döndürülen değeri bool_kontrol değişkenine atar.
                    {
                        bool_kontrol = kontrol(karakter, item, sayac, bool_kontrol);
                    }
                    if (bool_kontrol)           //Eğer bool_kontrol değişkeni true ise puanlama methodunu çalıştırarak döndürülen değeri puan değişkenine atar ve foreach döngüsünden çıkar.
                    {
                        puan = puanlama(item, puan);
                        break;
                    }
                }
                if (bool_kontrol)           //Eğer bool_kontrol değişkeni true ise for değişkeninden çıkar.
                    break;
            }
            Console.WriteLine("Puanınız= {0}", puan);
            Console.ReadLine();
        }
    }
}