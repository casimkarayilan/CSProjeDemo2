using CSProjeDemo2.Models;
using CSProjeDemo2.Services;

namespace MaasBodro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Maaş Bordro Programı");
            Console.WriteLine("-------------------");

            try
            {
                // Bordro klasörü oluştur
                string bordroKlasor = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bordrolar");
                Directory.CreateDirectory(bordroKlasor);

                // MaasBordro servisini başlat
                var maasBordro = new MaasBordro(bordroKlasor);

                // Personel listesini yükle
                string personelDosyasi = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "personel.json");
                maasBordro.PersonelListesiniYukle(personelDosyasi);

                while (true)
                {
                    Console.WriteLine("\nİşlem Menüsü:");
                    Console.WriteLine("1. Çalışma Saatlerini Gir");
                    Console.WriteLine("2. Bordroları Oluştur");
                    Console.WriteLine("3. Düşük Çalışma Saati Raporu");
                    Console.WriteLine("4. Çıkış");
                    Console.Write("\nSeçiminiz (1-4): ");

                    string? secim = Console.ReadLine();

                    switch (secim)
                    {
                        case "1":
                            CalisanBilgileriniGuncelle(maasBordro);
                            break;

                        case "2":
                            Console.WriteLine("\nBordrolar oluşturuluyor...");
                            maasBordro.BordroOlustur(DateTime.Now);
                            Console.WriteLine($"Bordrolar {bordroKlasor} klasörüne kaydedildi.");
                            break;

                        case "3":
                            DusukCalismaSaatiRaporuGoster(maasBordro);
                            break;

                        case "4":
                            Console.WriteLine("\nProgram sonlandırılıyor...");
                            return;

                        default:
                            Console.WriteLine("\nGeçersiz seçim! Lütfen tekrar deneyin.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nHATA: {ex.Message}");
            }
        }

        static void CalisanBilgileriniGuncelle(MaasBordro maasBordro)
        {
            Console.WriteLine("\nMevcut Personel Listesi:");
            foreach (var personel in maasBordro.PersonelListesi)
            {
                Console.WriteLine($"- {personel.AdSoyad} ({personel.Unvan})");
            }

            Console.Write("\nPersonel Adı: ");
            string? personelAdi = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(personelAdi))
            {
                Console.WriteLine("Geçersiz personel adı!");
                return;
            }

            Console.Write("Çalışma Saati: ");
            if (!int.TryParse(Console.ReadLine(), out int calismaSaati) || calismaSaati < 0)
            {
                Console.WriteLine("Geçersiz çalışma saati!");
                return;
            }

            try
            {
                maasBordro.CalisanBilgileriniGuncelle(personelAdi, calismaSaati);
                Console.WriteLine("Bilgiler güncellendi.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"HATA: {ex.Message}");
            }
        }

        static void DusukCalismaSaatiRaporuGoster(MaasBordro maasBordro)
        {
            Console.Write("\n150 saatten az çalışanlar:\n");
            var dusukCalisanlar = maasBordro.DusukCalismaSaatiRaporu();

            if (dusukCalisanlar.Count == 0)
            {
                Console.WriteLine("150 saatten az çalışan personel bulunmamaktadır.");
                return;
            }

            foreach (var personel in dusukCalisanlar)
            {
                Console.WriteLine($"- {personel.AdSoyad}: {personel.CalismaSaati} saat");
            }
        }
    }
}
