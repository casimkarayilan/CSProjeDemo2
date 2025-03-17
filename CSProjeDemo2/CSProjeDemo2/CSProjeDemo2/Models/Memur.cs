using System;

namespace CSProjeDemo2.Models
{
    public class Memur : Personel
    {
        private const int STANDART_CALISMA_SAATI = 180;
        private const decimal VARSAYILAN_SAATLIK_UCRET = 500M;
        private const decimal MESAI_CARPANI = 1.5M;

        public int Derece { get; private set; }

        public Memur(string adSoyad, int derece = 1) : base(adSoyad, "Memur")
        {
            Derece = derece;
            SaatlikUcret = VARSAYILAN_SAATLIK_UCRET;
        }

        public void DereceAyarla(int yeniDerece)
        {
            if (yeniDerece < 1)
                throw new ArgumentException("Derece 1'den küçük olamaz!");
            
            Derece = yeniDerece;
            // Derece değişikliğinde maaş güncellemesi yapılabilir
            SaatlikUcret = VARSAYILAN_SAATLIK_UCRET * (1 + ((decimal)(Derece - 1) * 0.1M));
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

            return temelMaas + mesaiUcreti;
        }
    }
} 