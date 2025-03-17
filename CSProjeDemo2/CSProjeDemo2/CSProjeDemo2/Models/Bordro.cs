using Newtonsoft.Json;

namespace CSProjeDemo2.Models
{
    public class Bordro
    {
        [JsonProperty("Personel Ismi")]
        public string PersonelIsmi { get; set; }

        [JsonProperty("Calisma Saati")]
        public int CalismaSaati { get; set; }

        [JsonProperty("Ana Odeme")]
        public string AnaOdeme { get; set; }

        [JsonProperty("Mesai")]
        public string Mesai { get; set; }

        [JsonProperty("Toplam Odeme")]
        public string ToplamOdeme { get; set; }

        public Bordro(string personelIsmi, int calismaSaati, decimal anaOdeme, decimal mesai)
        {
            PersonelIsmi = personelIsmi;
            CalismaSaati = calismaSaati;
            AnaOdeme = FormatParaBirimi(anaOdeme);
            Mesai = FormatParaBirimi(mesai);
            ToplamOdeme = FormatParaBirimi(anaOdeme + mesai);
        }

        private static string FormatParaBirimi(decimal tutar)
        {
            return string.Format("₺{0:N2}", tutar);
        }
    }
} 