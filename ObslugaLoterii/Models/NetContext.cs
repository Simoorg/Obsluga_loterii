using System.Data.Entity;

namespace ObslugaLoterii.Models
{
    /// <summary>
    /// Klasa Obsługująca bazę danych
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    class NetContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetContext"/> class.
        /// </summary>
        public NetContext() : base()
        {
        }

        /// <summary>
        /// Gets or sets the klienci.
        /// </summary>
        /// <value>
        /// klienci.
        /// </value>
        public virtual IDbSet<Klient> Klienci { get; set; }
        /// <summary>
        /// Gets or sets the kupony.
        /// </summary>
        /// <value>
        /// kupony.
        /// </value>
        public virtual IDbSet<Kupon> Kupony { get; set; }
        /// <summary>
        /// Gets or sets the loterie.
        /// </summary>
        /// <value>
        /// loterie.
        /// </value>
        public virtual IDbSet<Loteria> Loterie { get; set; }

    }
}
