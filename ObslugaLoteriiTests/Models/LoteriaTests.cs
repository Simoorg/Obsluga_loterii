using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ObslugaLoterii.Models.Tests
{
    [TestClass]
    public class LoteriaTests
    {
        Loteria loteria = new Loteria
        {
            Pula = 10000,
            Wylosowane = new int[6] {1,2,3,4,5,6}
        };
        

        [TestMethod]
        public void LiczWygraneTestBezTrafien()
        {
            List<Kupon> kupony = new List<Kupon>();
            Assert.AreEqual(kupony, loteria.LiczWygrane(kupony));

            kupony.Add(new Kupon { Typy = new int[] { 7, 8, 9, 10, 11, 12 } });
            Assert.AreEqual(0, kupony.ElementAt(0).Wygrana);
        }

        [TestMethod]
        public void LiczWygraneTestJednoTrafienie()
        {
            List<Kupon> kupony = new List<Kupon>();
            Assert.AreEqual(kupony, loteria.LiczWygrane(kupony));

            kupony.Add(new Kupon { Typy = new int[] { 7, 8, 9, 10, 11, 12 } });

            kupony.Add(new Kupon { Typy = new int[] { 1, 8, 9, 10, 11, 12 } });
            loteria.LiczWygrane(kupony);
            Assert.AreEqual(loteria.Pula, kupony.ElementAt(1).Wygrana);
          
        }

        [TestMethod]
        public void LiczWygraneTestDwaTrafienia()
        {
            List<Kupon> kupony = new List<Kupon>();
            Assert.AreEqual(kupony, loteria.LiczWygrane(kupony));
            kupony.Add(new Kupon { Typy = new int[] { 7, 8, 9, 10, 11, 12 } });
            kupony.Add(new Kupon { Typy = new int[] { 1, 8, 9, 10, 11, 12 } });
            kupony.Add(new Kupon { Typy = new int[] { 1, 2, 9, 10, 11, 12 } });
            loteria.LiczWygrane(kupony);

            Assert.AreEqual(kupony.ElementAt(1).Wygrana * 4, kupony.ElementAt(2).Wygrana);
        }

        [TestMethod]
        public void LiczWygraneTestTrafionaSzostka()
        {
            List<Kupon> kupony = new List<Kupon>();
            Assert.AreEqual(kupony, loteria.LiczWygrane(kupony));
            kupony.Add(new Kupon { Typy = new int[] { 7, 8, 9, 10, 11, 12 } });
            kupony.Add(new Kupon { Typy = new int[] { 1, 2, 9, 10, 11, 12 } });
            kupony.Add(new Kupon { Typy = new int[] { 1, 2, 3, 4, 5, 6 } });
            loteria.LiczWygrane(kupony);

            //delta jest wymagana przez błąd zaookrąglenia
            Assert.AreEqual(kupony.ElementAt(1).Wygrana * 256, kupony.ElementAt(2).Wygrana,1);
        }


    }
}