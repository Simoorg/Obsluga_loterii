using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using ObslugaLoterii.Models;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ObslugaLoterii.ViewModels
{
    /// <summary>
    /// VM obsługujący View 'WprowadzanieKlientaView'
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    class WprowadzanieKlientaVM : BindableBase
    {
        /// <summary>
        /// Gets or sets the close action.
        /// </summary>
        /// <value>
        /// close action.
        /// </value>
        public Action CloseAction { get; set; }
        /// <summary>
        /// Gets or sets the dodaj klienta.
        /// </summary>
        /// <value>
        /// dodaj klienta.
        /// </value>
        public ICommand DodajKlienta { get; set; }
        /// <summary>
        /// Gets or sets the powrot.
        /// </summary>
        /// <value>
        /// powrot.
        /// </value>
        public ICommand Powrot { get; set; }
        /// <summary>
        /// repository
        /// </summary>
        public Repozytorium repozytorium = new Repozytorium();
        /// <summary>
        /// klienci
        /// </summary>
        public List<Klient> klienci;
        /// <summary>
        /// imie
        /// </summary>
        public string imie;
        /// <summary>
        /// Gets or sets the imie.
        /// </summary>
        /// <value>
        /// imie.
        /// </value>
        public string Imie
        {
            get { return imie; }
            set { SetProperty(ref imie, value); }
        }
        /// <summary>
        /// nazwisko
        /// </summary>
        public string nazwisko;
        /// <summary>
        /// Gets or sets the nazwisko.
        /// </summary>
        /// <value>
        /// nazwisko.
        /// </value>
        public string Nazwisko
        {
            get { return nazwisko; }
            set { SetProperty(ref nazwisko, value); }
        }
        /// <summary>
        /// email
        /// </summary>
        public string email;
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// email.
        /// </value>
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        /// <summary>
        /// nr dowodu
        /// </summary>
        public string nrDowodu;
        /// <summary>
        /// Gets or sets the nr dowodu.
        /// </summary>
        /// <value>
        /// nr dowodu.
        /// </value>
        public string NrDowodu
        {
            get { return nrDowodu; }
            set { SetProperty(ref nrDowodu, value); }
        }
        /// <summary>
        /// tekst bledu
        /// </summary>
        public string tekstBledu;
        /// <summary>
        /// Gets or sets the tekst bledu.
        /// </summary>
        /// <value>
        /// tekst bledu.
        /// </value>
        public string TekstBledu
        {
            get { return tekstBledu; }
            set { SetProperty(ref tekstBledu, value); }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="WprowadzanieKlientaVM"/> class.
        /// </summary>
        public WprowadzanieKlientaVM()
        {
            DodajKlienta = new DelegateCommand(DodajKlientaWykonaj);
            Powrot = new DelegateCommand(PowrotWykonaj);
            klienci = new List<Klient>(repozytorium.DajKlientow());
        }

        /// <summary>
        /// Powrot wykonaj.
        /// </summary>
        public void PowrotWykonaj()
        {
            CloseAction();
        }

        /// <summary>
        /// Dodaj klienta wykonaj.
        /// </summary>
        public void DodajKlientaWykonaj()
        {
            if (SprawdzPoprawnosc())
            {
                Klient klient = new Klient
                {
                    Imie = imie,
                    Nazwisko = nazwisko,
                    EMail = email,
                    NrDowodu = nrDowodu
                };
                repozytorium.DodajKlienta(klient);
                CloseAction();
            }
            else
            {
                tekstBledu = "Sprawdź czy wszystkie dane są wprowadzone";
                RaisePropertyChanged("TekstBledu");
            }
        }

        /// <summary>
        /// Sprawdz poprawnosc.
        /// </summary>
        /// <returns>
        /// Funkcja zwraca true lub false zależnie od wyniku sprawdzania poprawności prowadzonych danych;
        /// </returns>
        public bool SprawdzPoprawnosc()
        {
            if (imie != null && nazwisko != null && nrDowodu != null)
            {
                foreach(Klient klient in klienci)
                {
                    if (klient.NrDowodu == nrDowodu)
                        return false;
                }
                string wzorImieNazwisko = @"^[A-ZĆŁŃŚŻŹ]{1,1}[a-ząćęłńóśżźA-ZĆŁŃŚŻŹ-]{2,30}$";
                string wzorNrDowodu = @"^[A-Z]{3}[0-9]{6}$";

                Regex regexImieNazwisko = new Regex(wzorImieNazwisko);
                Regex regexNrDowodu = new Regex(wzorNrDowodu);
                if (!(regexImieNazwisko.IsMatch(imie) && regexImieNazwisko.IsMatch(nazwisko) &&
                    regexNrDowodu.IsMatch(nrDowodu)))
                {
                    return false;
                }
                if (email != null && email != "")
                {
                    EmailAddressAttribute addressAttribute = new EmailAddressAttribute();
                    return addressAttribute.IsValid(email);
                }
                else
                {
                    email = "";
                    return true;
                }
            }
            return false;
        }
    }
}
