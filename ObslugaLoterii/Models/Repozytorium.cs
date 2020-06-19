using System.Collections.Generic;
using System.Linq;

namespace ObslugaLoterii.Models
{
    /// <summary>
    /// Repozytorium oferujace funkcje do komunikacji z bazą danych.
    /// </summary>
    class Repozytorium
    {
        /// <summary>
        /// The context
        /// </summary>
        private NetContext context = new NetContext();

        /// <summary>
        /// Dodaj klienta.
        /// </summary>
        /// <param name="klient"> klient.</param>
        public void DodajKlienta(Klient klient)
        {
            context.Klienci.Add(klient);
            context.SaveChanges();
        }
        /// <summary>
        /// Dodaj loterie.
        /// </summary>
        /// <param name="loteria"> loteria.</param>
        public void DodajLoterie(Loteria loteria)
        {
            context.Loterie.Add(loteria);
            context.SaveChanges();
        }
        /// <summary>
        /// Dodaj kupon.
        /// </summary>
        /// <param name="kupon"> kupon.</param>
        public void DodajKupon(Kupon kupon)
        {
            context.Kupony.Add(kupon);
            context.SaveChanges();
        }

        /// <summary>
        /// Daj klientow.
        /// </summary>
        /// <returns></returns>
        public List<Klient> DajKlientow()
        {
           return context.Klienci.ToList();
        }
        /// <summary>
        /// Daj loterie.
        /// </summary>
        /// <returns></returns>
        public List<Loteria> DajLoterie()
        {
            return context.Loterie.ToList();
        }
        /// <summary>
        /// Daj kupony.
        /// </summary>
        /// <returns></returns>
        public List<Kupon> DajKupony()
        {
            return context.Kupony.ToList();
        }

        /// <summary>
        /// Daj klienta o id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns></returns>
        public Klient DajKlientaOID(int id)
        {
            return DajKlientow().ElementAt(id-1);
        }
        /// <summary>
        /// Daj loterie o id.
        /// </summary>
        /// <param name="id"> id.</param>
        /// <returns></returns>
        public Loteria DajLoterieOID(int id)
        {
            return DajLoterie().ElementAt(id-1);
        }
        /// <summary>
        /// Daj kupon o id.
        /// </summary>
        /// <param name="id"> id.</param>
        /// <returns></returns>
        public Kupon DajKuponOID(int id)
        {
            return DajKupony().ElementAt(id-1);
        }
        /// <summary>
        /// Daj kupony dotyczace loterii o id.
        /// </summary>
        /// <param name="id"> id.</param>
        /// <returns></returns>
        public List<Kupon> DajKuponyDotyczaceLoteriiOID(int id)
        {
            var kupony = DajKupony();
            var kuponyZLoterii = new List<Kupon>();
            foreach (Kupon kup in kupony)
                if (kup.Loteria.LoteriaID == id)
                    kuponyZLoterii.Add(kup);
            return kuponyZLoterii;
        }

        /// <summary>
        /// Zaktualizuj kupony o wygrane.
        /// </summary>
        /// <param name="kuponyZKwota">kupony z wpisaną wygraną kwota.</param>
        public void ZaktualizujKuponyOWygrane(List<Kupon> kuponyZKwota)
        {
            foreach (Kupon kup in kuponyZKwota)
            {
                DajKuponOID(kup.KuponID).Wygrana = kup.Wygrana;
                context.SaveChanges();
            }
        }
    }
}
