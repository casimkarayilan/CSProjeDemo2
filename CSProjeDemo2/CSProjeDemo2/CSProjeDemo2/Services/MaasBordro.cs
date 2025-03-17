using System;
using System.Collections.Generic;
using CSProjeDemo2.Models;
using CSProjeDemo2.Utils;

namespace CSProjeDemo2.Services
{
    public class MaasBordro
    {
        private readonly string _bordroKlasorYolu;
        private List<Personel> _personelListesi;

        public MaasBordro(string bordroKlasorYolu)
        {
            _bordroKlasorYolu = bordroKlasorYolu;
            _personelListesi = new List<Personel>();
        }

        public void PersonelListesiniYukle(string dosyaYolu)
        {
            _personelListesi = DosyaOku.PersonelListesiOku(dosyaYolu);
        }

        public void BordroOlustur(DateTime tarih)
        {
            foreach (var personel in _personelListesi)
            {
                decimal anaOdeme = 0;
                decimal mesaiOdemesi = 0;
                const int STANDART_CALISMA_SAATI = 180;

                // Yönetici için bonus ödemesi ayarla
                if (personel is Yonetici yonetici)
                {
                    yonetici.BonusOdemesi = 1000; // Örnek bonus miktarı
                }

                // Mesai hesaplama
                if (personel.CalismaSaati > STANDART_CALISMA_SAATI)
                {
                    int mesaiSaati = personel.CalismaSaati - STANDART_CALISMA_SAATI;
                    anaOdeme = STANDART_CALISMA_SAATI * personel.SaatlikUcret;
                    mesaiOdemesi = mesaiSaati * personel.SaatlikUcret * 1.5M;

                    // Yönetici ise bonus ekle
                    if (personel is Yonetici yonetici2)
                    {
                        anaOdeme += yonetici2.BonusOdemesi;
                    }
                }
                else
                {
                    anaOdeme = personel.MaasHesapla();
                }

                var bordro = new Bordro(
                    personel.AdSoyad,
                    personel.CalismaSaati,
                    anaOdeme,
                    mesaiOdemesi
                );

                DosyaOku.BordroKaydet(_bordroKlasorYolu, personel.AdSoyad, tarih, bordro);
            }
        }

        public List<Personel> DusukCalismaSaatiRaporu(int saatLimiti = 150)
        {
            return _personelListesi.FindAll(p => p.CalismaSaati < saatLimiti);
        }

        public void CalisanBilgileriniGuncelle(string personelAdi, int calismaSaati)
        {
            var personel = _personelListesi.Find(p => p.AdSoyad.Equals(personelAdi, StringComparison.OrdinalIgnoreCase));
            
            if (personel == null)
                throw new ArgumentException($"{personelAdi} isimli personel bulunamadı.");

            personel.CalismaSaati = calismaSaati;
        }

        public List<Personel> PersonelListesi => _personelListesi;
    }
} 