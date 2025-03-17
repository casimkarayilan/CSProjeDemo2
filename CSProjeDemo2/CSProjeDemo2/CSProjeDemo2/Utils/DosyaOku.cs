using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CSProjeDemo2.Models;

namespace CSProjeDemo2.Utils
{
    public class DosyaOku
    {
        public static List<Personel> PersonelListesiOku(string dosyaYolu)
        {
            var personelListesi = new List<Personel>();

            try
            {
                string jsonIcerik = File.ReadAllText(dosyaYolu);
                var jsonArray = JArray.Parse(jsonIcerik);

                foreach (var item in jsonArray)
                {
                    string ad = item["name"]?.ToString() ?? throw new JsonException("name alanı bulunamadı");
                    string unvan = item["title"]?.ToString() ?? throw new JsonException("title alanı bulunamadı");

                    Personel personel = unvan.ToLower() switch
                    {
                        "yonetici" => new Yonetici(ad),
                        "memur" => new Memur(ad),
                        _ => throw new JsonException($"Geçersiz personel unvanı: {unvan}")
                    };

                    personelListesi.Add(personel);
                }
            }
            catch (Exception ex) when (ex is JsonException || ex is IOException)
            {
                throw new Exception($"Personel listesi okuma hatası: {ex.Message}", ex);
            }

            return personelListesi;
        }

        public static void BordroKaydet(string klasorYolu, string personelAdi, DateTime tarih, object bordroVerisi)
        {
            try
            {
                // Personel klasörünü oluştur
                string personelKlasor = Path.Combine(klasorYolu, personelAdi.Replace(" ", "_"));
                Directory.CreateDirectory(personelKlasor);

                // Bordro dosya adını oluştur
                string dosyaAdi = $"bordro_{tarih:yyyy_MM}.json";
                string dosyaYolu = Path.Combine(personelKlasor, dosyaAdi);

                // Bordroyu JSON formatında kaydet
                string jsonIcerik = JsonConvert.SerializeObject(bordroVerisi, Formatting.Indented);
                File.WriteAllText(dosyaYolu, jsonIcerik);
            }
            catch (Exception ex)
            {
                throw new Exception($"Bordro kaydetme hatası: {ex.Message}", ex);
            }
        }
    }
} 