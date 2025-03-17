using System;

namespace CSProjeDemo2.Models
{
    public class Yonetici : Personel
    {
        private const decimal MINIMUM_SAATLIK_UCRET = 500M;
        private const decimal MESAI_CARPANI = 1.5M;
        private const int STANDART_CALISMA_SAATI = 180;
        public decimal BonusOdemesi { get; set; }

        public Yonetici(string adSoyad) : base(adSoyad, "Yonetici")
        {
            // Minimum saatlik ücret kontrolü constructor'da yapılıyor
            SaatlikUcret = MINIMUM_SAATLIK_UCRET;
        }

        public void SaatlikUcretAyarla(decimal yeniUcret)
        {
            // Minimum saatlik ücret kontrolü
            if (yeniUcret < MINIMUM_SAATLIK_UCRET)
                throw new ArgumentException($"Yönetici için saatlik ücret {MINIMUM_SAATLIK_UCRET} TL'den az olamaz!");
            
            SaatlikUcret = yeniUcret;
        }

        public override decimal MaasHesapla()
        {
            decimal temelMaas;
            decimal mesaiUcreti = 0;

            if (CalismaSaati <= STANDART_CALISMA_SAATI)
            {
                temelMaas = SaatlikUcret * CalismaSaati;
            }
            else
            {
                temelMaas = SaatlikUcret * STANDART_CALISMA_SAATI;
                int mesaiSaati = CalismaSaati - STANDART_CALISMA_SAATI;
                mesaiUcreti = mesaiSaati * SaatlikUcret * MESAI_CARPANI;
            }

            return temelMaas + mesaiUcreti + BonusOdemesi;
        }
    }
} 