namespace ObslugaLoterii.Models
{
    /// <summary>
    /// Klasa pomocnicza wykorzystywana do prezentacji danych w UI
    /// </summary>
    class DataGridRowObject
    {
        /// <summary>
        /// Gets or sets the imie nazwisko.
        /// </summary>
        /// <value>
        /// imie i nazwisko.
        /// </value>
        public string ImieNazwisko { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// email.
        /// </value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the kwota.
        /// </summary>
        /// <value>
        /// kwota.
        /// </value>
        public double Kwota { get; set; }
        /// <summary>
        /// Gets or sets the nr dowodu.
        /// </summary>
        /// <value>
        /// nr dowodu.
        /// </value>
        public string NrDowodu { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [czy wyplacono].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [czy wyplacono]; otherwise, <c>false</c>.
        /// </value>
        public bool CzyWyplacono { get; set; }

    }
}
