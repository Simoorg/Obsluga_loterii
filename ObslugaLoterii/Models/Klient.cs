using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ObslugaLoterii.Models
{
    /// <summary>
    /// Klasa przechowująca dane klienta
    /// </summary>
    [Table("Klienci")]

    public class Klient
    {
        /// <summary>
        /// Gets or sets the klient identifier.
        /// </summary>
        /// <value>
        /// klient identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KlientID { get; set; }
        /// <summary>
        /// Gets or sets the nr dowodu.
        /// </summary>
        /// <value>
        /// nr dowodu.
        /// </value>
        public string NrDowodu { get; set; }
        /// <summary>
        /// Gets or sets the e mail.
        /// </summary>
        /// <value>
        /// e mail.
        /// </value>
        public string EMail { get; set; }
        /// <summary>
        /// Gets or sets the imie.
        /// </summary>
        /// <value>
        /// imie.
        /// </value>
        public string Imie { get; set; }
        /// <summary>
        /// Gets or sets the nazwisko.
        /// </summary>
        /// <value>
        /// nazwisko.
        /// </value>
        public string Nazwisko { get; set; }

        /// <summary>
        /// Gets or sets the zakupione kupony.
        /// </summary>
        /// <value>
        /// zakupione kupony.
        /// </value>
        public virtual ICollection<Kupon> ZakupioneKupony { get; set; }
    }
}
