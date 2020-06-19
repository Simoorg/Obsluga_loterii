using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ObslugaLoterii.Models
{
    /// <summary>
    /// Klasa przechowuj¹ca dane kuponu
    /// </summary>
    [Table("Kupony")]

    public class Kupon
    {
        /// <summary>
        /// Gets or sets the kupon identifier.
        /// </summary>
        /// <value>
        /// kupon identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KuponID { get; set; }
        /// <summary>
        /// Gets or sets the wygrana.
        /// </summary>
        /// <value>
        /// wygrana.
        /// </value>
        public double Wygrana { get; set; }

        /// <summary>
        /// Gets or sets the typy to string.
        /// Zmienna potrzebna do przechowywania w bazie danych listy typów
        /// </summary>
        /// <value>
        /// typy to string.
        /// </value>
        public string TypyToStr { get; set; }
        /// <summary>
        /// Gets or sets the typy.
        /// </summary>
        /// <value>
        /// typy.
        /// </value>
        public int[] Typy
        {
            get
            {
                return Array.ConvertAll(TypyToStr.Split(';'), int.Parse);
            }
            set
            {
                var _data = value;
                TypyToStr = String.Join(";", _data.Select(p => p.ToString()).ToArray());
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [czy wyplacono].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [czy wyplacono]; otherwise, <c>false</c>.
        /// </value>
        public bool CzyWyplacono { get; set; }

        /// <summary>
        /// Gets or sets the klient.
        /// </summary>
        /// <value>
        /// klient.
        /// </value>
        [ForeignKey("IdKlienta")]
        public virtual Klient Klient { get; set; }
        /// <summary>
        /// Gets or sets the identifier klienta.
        /// </summary>
        /// <value>
        /// identifier klienta.
        /// </value>
        public int IdKlienta { get; set; }

        /// <summary>
        /// Gets or sets the loteria.
        /// </summary>
        /// <value>
        /// loteria.
        /// </value>
        [ForeignKey("IdLoterii")]
        public virtual Loteria Loteria { get; set; }
        /// <summary>
        /// Gets or sets the identifier loterii.
        /// </summary>
        /// <value>
        /// identifier loterii.
        /// </value>
        public int IdLoterii { get; set; }

    }
}
