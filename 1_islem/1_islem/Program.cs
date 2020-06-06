using System;
using System.Collections.Generic;

namespace _1_islem
{
    /*Tüm işlemlerin dödürüldüğü class*/
    class Sonuc
    {
        public string cikti;
        public bool basari;
    }
    class Program
    {
        public static Sonuc getSonuc(List<int>sayilar, int ortasayi,int hedef)
        {
            Sonuc ortaSonuc = new Sonuc();
            if(ortasayi == hedef)
            {
                ortaSonuc.basari = true;
                ortaSonuc.cikti = "";
                Console.WriteLine("Toplam = " + ortasayi);
                return ortaSonuc;
            }
            foreach (int sayi in sayilar)
            {
                List<int> yeniList = new List<int>(sayilar);
                yeniList.Remove(sayi);
                /* Her seferinde listede ekstra eleman olmasını engellemek ve 
                 * ram kullanımı azaltmak için baştan her seferde siliniyor*/
                if (yeniList.Count == 0)
                {
                    /*Tüm işlemleri sırayla deneyecek en yakın sonuca ulaştırmak için 
                     *en yakın orta sayıyı bulamaya çalışıyor*/
                    if (hedef == ortasayi/sayi)
                    {
                        ortaSonuc.basari = true;
                        ortaSonuc.cikti = "/" + sayi;
                        Console.WriteLine("Toplam = " + (ortasayi / sayi));
                        return ortaSonuc;
                    }
                    if (hedef == ortasayi*sayi)
                    {
                        ortaSonuc.basari = true;
                        ortaSonuc.cikti = "*" + sayi;
                        Console.WriteLine("Toplam = " + (ortasayi * sayi));
                        return ortaSonuc;
                    }
                    if (hedef == ortasayi+sayi)
                    {
                        ortaSonuc.basari = true;
                        ortaSonuc.cikti = "+" + sayi;
                        Console.WriteLine("Toplam = " + (ortasayi + sayi));
                        return ortaSonuc;
                    }
                    if (hedef == ortasayi-sayi)
                    {
                        ortaSonuc.basari = true;
                        ortaSonuc.cikti = "-" + sayi;
                        Console.WriteLine("Toplam = " + (ortasayi - sayi));
                        return ortaSonuc;
                    }
                    ortaSonuc.basari = false;
                    ortaSonuc.cikti = "Hata!!!";
                    return ortaSonuc;
                }
                else
                {
                    /*Hala bulamadıysa orta sayıyla aynı mantıkla kombinasyonları teker teker aynı şekilde yapacak*/
                    ortaSonuc = getSonuc(yeniList, ortasayi / sayi, hedef);
                    if (ortaSonuc.basari == true)
                    {
                        ortaSonuc.cikti = "/" + sayi + ortaSonuc.cikti;
                        return ortaSonuc;
                    }
                    ortaSonuc = getSonuc(yeniList, ortasayi * sayi, hedef);
                    if (ortaSonuc.basari == true)
                    {
                        ortaSonuc.cikti = "*" + sayi + ortaSonuc.cikti;
                        return ortaSonuc;
                    }
                    ortaSonuc = getSonuc(yeniList, ortasayi + sayi, hedef);
                    if (ortaSonuc.basari == true)
                    {
                        ortaSonuc.cikti = "+" + sayi + ortaSonuc.cikti;
                        return ortaSonuc;
                    }
                    ortaSonuc = getSonuc(yeniList, ortasayi - sayi, hedef);
                    if (ortaSonuc.basari == true)
                    {
                        ortaSonuc.cikti = "-" + sayi + ortaSonuc.cikti;
                        return ortaSonuc;
                    }
                }
            }
            return ortaSonuc;
        }
        static void Main(string[] args)
        {
            Random rand = new Random();
            int target = rand.Next(100, 999);
            List<int> list = new List<int>();
            bool kontrol = false;
            /*Döngüden çıkmak için gereken bool*/
            for (int i = 0; i < 5; i++)
            {
                list.Add(rand.Next(1, 9));
            }
            list.Add(10 * rand.Next(1, 9));
            foreach (int item in list)
            {
                Console.WriteLine(item + "");
            }
            Console.WriteLine("Hedef Sayi: " + target);
            foreach (int bas in list)
            {
                if (kontrol == true)
                {
                    break;
                }
                List<int> runList = new List<int>(list);
                runList.Remove(bas);
                /*Boş liste ile başlayıp ram kullanımı minimuma indirmek için temizliyoruz*/
                Sonuc sonuc = getSonuc(runList, bas, target);
                if (sonuc.basari)
                {
                    Console.WriteLine(bas + sonuc.cikti);
                    Console.WriteLine("Sonuca Tam Ulasildi!!!");
                    Console.WriteLine("Puan: 10");
                    Console.ReadLine();
                    kontrol = true;
                }
                else
                {
                    int fark = 1;
                    if (kontrol == false)
                    {
                        while(fark < 10)
                        {
                            /*Sıralı olarak doğru çalışması için kademe kademe bool işlemi tekrarlandı*/
                            if(kontrol == true)
                            {
                                break;
                            }
                            foreach (int basam in list)
                            {
                                if (kontrol == true)
                                {
                                    break;
                                }
                                List<int> runList2 = new List<int>(list);
                                runList2.Remove(basam);
                                Sonuc fazlaSonuc = getSonuc(runList2, basam, (target + fark));
                                /*Sonuç işlemini aynı şekilde işleme koyup bu sefer fark için çalıştırıyor*/
                                Sonuc eksikSonuc = getSonuc(runList2, basam, (target - fark));
                                if (fazlaSonuc.basari == true)
                                {
                                    Console.WriteLine(fark + " Fazlasi Bulundu!!!");
                                    Console.WriteLine(basam + fazlaSonuc.cikti);
                                    Console.WriteLine("Puan: " + (10- fark));
                                    Console.ReadLine();
                                    kontrol = true;
                                }
                                if (eksikSonuc.basari == true)
                                {
                                    Console.WriteLine(fark + " Eksigi Bulundu!!!");
                                    Console.WriteLine(basam + eksikSonuc.cikti);
                                    Console.WriteLine("Puan: " + (10 - fark));
                                    Console.ReadLine();
                                    kontrol = true;
                                }
                            }
                            fark++;
                        }
                    }

                }
            }
        }
    }
}
