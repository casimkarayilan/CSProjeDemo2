using System;

namespace CSProjeDemo2.Models
{
    public abstract class Personel
    {
        public string AdSoyad { get; set; }
        public string Unvan { get; set; }
        public decimal SaatlikUcret { get; set; }
        public int CalismaSaati { get; set; }

        protected Personel(string adSoyad, string unvan)
        {
            AdSoyad = adSoyad;
            Unvan = unvan;
        }

        // Maaş hesaplama abstract metodu - her personel tipi kendi implementasyonunu yapacak
        public abstract decimal MaasHesapla();

        // Ortak maaş hesaplama mantığı
        protected decimal TemelMaasHesapla()
        {
            return SaatlikUcret * CalismaSaati;
        }
    }
} 