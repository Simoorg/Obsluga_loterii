using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ObslugaLoterii.Models
{
    /// <summary>
    /// Klasa przechowuj¹ca dane loterii
    /// </summary>
    [Table("Loterie")]

    public class Loteria
    {
        /// <summary>
        /// Gets or sets the loteria identifier.
        /// </summary>
        /// <value>
        /// loteria identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoteriaID { get; set; }
        /// <summary>
        /// Gets or sets the pula.
        /// </summary>
        /// <value>
        /// pula.
        /// </value>
        public double Pula { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// data.
        /// </value>
        public DateTime Data { get; set; }


        /// <summary>
        /// Gets or sets the wylosowane to string.
        /// Zmienna potrzebna do przechowywania w bazie danych listy wylosowanych liczb
        /// </summary>
        /// <value>
        /// wylosowane to string.
        /// </value>
        public string WylosowaneToStr { get; set; }
        /// <summary>
        /// Gets or sets the wylosowane.
        /// </summary>
        /// <value>
        /// wylosowane.
        /// </value>
        public int[] Wylosowane
        {
            get
            {
                return Array.ConvertAll(WylosowaneToStr.Split(';'), int.Parse);
            }
            set
            {
                var _data = value;
                WylosowaneToStr = String.Join(";", _data.Select(p => p.ToString()).ToArray());
            }
        }

        /// <summary>
        /// Gets or sets the kupony.
        /// </summary>
        /// <value>
        /// kupony.
        /// </value>
        public virtual ICollection<Kupon> Kupony { get; set; }

        /// <summary>
        /// Licz wygrane.
        /// </summary>
        /// <param name="kupony">kupony.</param>
        /// <returns>
        /// Zwraca listê kuponów z dodan¹ do nich wygran¹ kwot¹ dla ka¿dego kuponu.
        /// </returns>
        public List<Kupon> LiczWygrane(List<Kupon> kupony)
        {
            double liczbaPunktowTrafien = 0; //zaleznie od ilosci zgodnych liczb w kuponie rozne liczby trafien
                                             // 1 trafienie - 1 pkt, 2 trafienia - 4 pkt, 3 - 16 pkt 4 - 64 pkt
                                             // 5 - 256 pkt, 6 - 1024 pkt
            double kwotaZaPunkt = 0;
            foreach (Kupon kup in kupony)
            {
                int trafienia = 0;
                foreach (int typ in kup.Typy)
                {
                    if (Wylosowane.Contains(typ))
                        trafienia++;
                }
                if (trafienia == 0)
                    kup.Wygrana = 0;
                else
                    kup.Wygrana = Math.Pow(4, trafienia - 1);
                liczbaPunktowTrafien += kup.Wygrana;
            }
            if (liczbaPunktowTrafien != 0)
            {
                kwotaZaPunkt = Pula / liczbaPunktowTrafien;
            }
            foreach (Kupon kup in kupony)
            {
                kup.Wygrana = Math.Round(kup.Wygrana * kwotaZaPunkt, 2);
            }
            return kupony;
        }

    }
}
