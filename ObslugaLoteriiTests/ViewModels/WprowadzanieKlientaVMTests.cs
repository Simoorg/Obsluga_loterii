using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObslugaLoterii.ViewModels.Tests
{
    [TestClass]
    public class WprowadzanieKlientaVMTests
    {
        WprowadzanieKlientaVM wprowadzanieKlienta = new WprowadzanieKlientaVM();

        [TestMethod]
        public void SprawdzPoprawnoscPoprawneDaneBezEmail()
        {
            wprowadzanieKlienta.imie = "Konstanty";
            wprowadzanieKlienta.nazwisko = "Karoński-Barski";
            wprowadzanieKlienta.nrDowodu = "ASD456123";
            Assert.IsTrue(wprowadzanieKlienta.SprawdzPoprawnosc());
        }

        [TestMethod]
        public void SprawdzPoprawnoscPoprawneDaneZEmail()
        {
            wprowadzanieKlienta.imie = "Konstanty";
            wprowadzanieKlienta.nazwisko = "Karoński-Barski";
            wprowadzanieKlienta.nrDowodu = "ASD456123";
            wprowadzanieKlienta.email = "KonKar@gmail.com";
            Assert.IsTrue(wprowadzanieKlienta.SprawdzPoprawnosc());
        }

        [TestMethod]
        public void SprawdzPoprawnoscDaneZBlednymEmail()
        {
            wprowadzanieKlienta.imie = "Konstanty";
            wprowadzanieKlienta.nazwisko = "Karoński-Barski";
            wprowadzanieKlienta.nrDowodu = "ASD456123";
            wprowadzanieKlienta.email = "KonKar@gmail";
            Assert.IsFalse(wprowadzanieKlienta.SprawdzPoprawnosc());
        }

        [TestMethod]
        public void SprawdzPoprawnoscDaneZBlednymImieniem()
        {
            wprowadzanieKlienta.imie = "Konsta4nty";
            wprowadzanieKlienta.nazwisko = "Karoński-Barski";
            wprowadzanieKlienta.nrDowodu = "ASD456123";
            wprowadzanieKlienta.email = "KonKar@gmail.com";
            Assert.IsFalse(wprowadzanieKlienta.SprawdzPoprawnosc());
        }

        [TestMethod]
        public void SprawdzPoprawnoscDaneZBlednymNazwiskiem()
        {
            wprowadzanieKlienta.imie = "Konstanty";
            wprowadzanieKlienta.nazwisko = "Karoński_Barski";
            wprowadzanieKlienta.nrDowodu = "ASD456123";
            wprowadzanieKlienta.email = "KonKar@gmail.com";
            Assert.IsFalse(wprowadzanieKlienta.SprawdzPoprawnosc());
        }
        [TestMethod]
        public void SprawdzPoprawnoscDaneZBlednymNrDowodu()
        {
            wprowadzanieKlienta.imie = "Konstanty";
            wprowadzanieKlienta.nazwisko = "Karoński-Barski";
            wprowadzanieKlienta.nrDowodu = "AS4556123";
            wprowadzanieKlienta.email = "KonKar@gmail.com";
            Assert.IsFalse(wprowadzanieKlienta.SprawdzPoprawnosc());
        }
        [TestMethod]
        public void SprawdzPoprawnoscDanePuste()
        {
            wprowadzanieKlienta.imie = "";
            wprowadzanieKlienta.nazwisko = "";
            wprowadzanieKlienta.nrDowodu = "";
            wprowadzanieKlienta.email = "";
            Assert.IsFalse(wprowadzanieKlienta.SprawdzPoprawnosc());
        }
        [TestMethod]
        public void SprawdzPoprawnoscDaneWszystkieZle()
        {
            wprowadzanieKlienta.imie = "Kon2stanty";
            wprowadzanieKlienta.nazwisko = "Karoń[ski-Barski";
            wprowadzanieKlienta.nrDowodu = "ASD45623";
            wprowadzanieKlienta.email = "KonKar@gmail.com.com";
            Assert.IsFalse(wprowadzanieKlienta.SprawdzPoprawnosc());
        }
    }
}