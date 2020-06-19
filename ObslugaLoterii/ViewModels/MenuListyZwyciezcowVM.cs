using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ObslugaLoterii.Models;
using System.Xml.Linq;

namespace ObslugaLoterii.ViewModels
{
    /// <summary>
    /// VM obsługujący View 'MenuListyZwyciezcow'
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    class MenuListyZwyciezcowVM : BindableBase
    {
        /// <summary>
        /// Gets or sets the close action.
        /// </summary>
        /// <value>
        /// close action.
        /// </value>
        public Action CloseAction { get; set; }
        /// <summary>
        /// Gets or sets the generuj liste.
        /// </summary>
        /// <value>
        /// generuj liste.
        /// </value>
        public ICommand GenerujListe { get; set; }
        /// <summary>
        /// Gets or sets the udostepnij liste.
        /// </summary>
        /// <value>
        /// udostepnij liste.
        /// </value>
        public ICommand UdostepnijListe { get; set; }
        /// <summary>
        /// Gets or sets the wyslij emaile.
        /// </summary>
        /// <value>
        /// wyslij emaile.
        /// </value>
        public ICommand WyslijEmaile { get; set; }
        /// <summary>
        /// Gets or sets the przejdz do wprowadzania.
        /// </summary>
        /// <value>
        /// przejdz do wprowadzania.
        /// </value>
        public ICommand PrzejdzDoWprowadzania { get; set; }
        /// <summary>
        /// lista zwyciezcow
        /// </summary>
        public ObservableCollection<DataGridRowObject> listaZwyciezcow = new ObservableCollection<DataGridRowObject>();
        /// <summary>
        /// Gets or sets the lista zwyciezcow.
        /// </summary>
        /// <value>
        /// lista zwyciezcow.
        /// </value>
        public ObservableCollection<DataGridRowObject> ListaZwyciezcow
        {
            get { return listaZwyciezcow; }
            set
            {
                listaZwyciezcow = value;
                RaisePropertyChanged("ListaZwyciezcow");
            }
        }
        /// <summary>
        /// tekst potwierdzenia
        /// </summary>
        public string tekstPotwierdzenia;
        /// <summary>
        /// Gets or sets the tekst potwierdzenia.
        /// </summary>
        /// <value>
        /// tekst potwierdzenia.
        /// </value>
        public string TekstPotwierdzenia
        {
            get { return tekstPotwierdzenia; }
            set { SetProperty(ref tekstPotwierdzenia, value); }
        }

        /// <summary>
        /// repozytorium
        /// </summary>
        Repozytorium repozytorim = new Repozytorium();


        /// <summary>
        /// Initializes a new instance of the <see cref="MenuListyZwyciezcowVM"/> class.
        /// Jeśli nie ma w bazie loterii - tworzy przykładową loterię (brak implementacji wprowadzania loterii)
        /// </summary>
        public MenuListyZwyciezcowVM()
        {
            GenerujListe = new DelegateCommand(GenerujListeWykonaj);
            UdostepnijListe = new DelegateCommand(UdostepnijListeWykonaj);
            WyslijEmaile = new DelegateCommand(WyslijEmaileWykonaj);
            PrzejdzDoWprowadzania = new DelegateCommand(PrzejdzDoWprowadzaniaWykonaj);

            if (repozytorim.DajLoterie().Count == 0)
            {
                Loteria loteria = new Loteria
                {
                    Pula = 100000,
                    Data = DateTime.Now,
                    Wylosowane = new int[] { 1, 2, 3, 4, 5, 6 }
                };
                repozytorim.DodajLoterie(loteria);
            }
        }

        /// <summary>
        /// Przejdz the do wprowadzania wykonaj.
        /// </summary>
        private void PrzejdzDoWprowadzaniaWykonaj()
        {
            CloseAction();
        }

        /// <summary>
        /// Generuj the liste wykonaj.
        /// Dodatkowo funkcja wyświetla wygenerową listę w oknie.
        /// </summary>
        private void GenerujListeWykonaj()
        {
            int idLoterii = 1;
            List<Kupon> kupony = repozytorim.DajKuponyDotyczaceLoteriiOID(idLoterii);
            kupony = repozytorim.DajLoterieOID(idLoterii).LiczWygrane(kupony);
            repozytorim.ZaktualizujKuponyOWygrane(kupony);

            listaZwyciezcow.Clear();
            foreach (Kupon kup in kupony)
            {
                bool znalezionoKlienta = false;
                foreach (DataGridRowObject DGObj in listaZwyciezcow)
                {
                    if (DGObj.NrDowodu == kup.Klient.NrDowodu)
                    {
                        DGObj.Kwota += kup.Wygrana;
                        znalezionoKlienta = true;
                        break;
                    }
                }
                if (kup.Wygrana > 0 && !znalezionoKlienta)
                    listaZwyciezcow.Add(new DataGridRowObject
                    {
                        ImieNazwisko = kup.Klient.Imie + " " + kup.Klient.Nazwisko,
                        Email = kup.Klient.EMail,
                        Kwota = kup.Wygrana,
                        NrDowodu = kup.Klient.NrDowodu,
                        CzyWyplacono = kup.CzyWyplacono
                    }
                    );
            }
            RaisePropertyChanged("ListaZwyciezcow");
        }

        /// <summary>
        /// Udostepnij liste wykonaj.
        /// Metoda tworzy plik XML zawierajacy dane zwyciężców i zapisuje go w katalogu z programem.
        /// </summary>
        private void UdostepnijListeWykonaj()
        {
            if (ListaZwyciezcow != null && ListaZwyciezcow.Count != 0)
            {
                tekstPotwierdzenia = "Czekaj...";
                RaisePropertyChanged("TekstPotwierdzenia");
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "no"),
                    new XComment("XML zawierajacy dane zwyciężców loterii"),
                    new XElement("Zwyciężcy")
                );
                foreach (DataGridRowObject zwyciezca in listaZwyciezcow)
                {
                    xml.Root.Add(new XElement("Zwyciężca",
                    new XAttribute("ImieNazwisko", zwyciezca.ImieNazwisko),
                    new XAttribute("Email", zwyciezca.Email),
                    new XAttribute("Kwota", zwyciezca.Kwota),
                    new XAttribute("NumerDowowdu", zwyciezca.NrDowodu),
                    new XAttribute("CzyWyplacono", zwyciezca.CzyWyplacono)));
                };
                xml.Save("ListaZwyciezcow_.xml");
                tekstPotwierdzenia = "Pomyślnie wygenerowano plik .xml";
                RaisePropertyChanged("TekstPotwierdzenia");
            }
        }

        /// <summary>
        /// Wyslij emaile wykonaj.
        /// Funkcja nie działa ze względu na problemy z protokołami email
        /// </summary>
        private void WyslijEmaileWykonaj()
        {
            if (ListaZwyciezcow != null && ListaZwyciezcow.Count != 0)
            {
                tekstPotwierdzenia = "Czekaj...";
                RaisePropertyChanged("TekstPotwierdzenia");
                WysylkaEMail wysylkaEmail = new WysylkaEMail();
                foreach (DataGridRowObject zwyciezca in listaZwyciezcow)
                {
                    if (zwyciezca.Email != null && zwyciezca.Email != "")
                    {
                        wysylkaEmail.WyslijEmail("erasmus9601@gmail.com", zwyciezca.Email, "Gratulacje " + zwyciezca.ImieNazwisko + "!!!", "Wygrałeś/aś w loterii " + zwyciezca.Kwota.ToString() +
                            "zł. Odbierz Swoją Wygraną w najbliższym oddziale i nie zapomnij swojego dowodu osobistego!");
                        // break; //break dodany żeby nie zasypać skrzynki dziesiątkami wiadomości
                    }
                }
                tekstPotwierdzenia = "Pomyślnie wysłano wiadomości email";
                RaisePropertyChanged("TekstPotwierdzenia");
            }
        }
    }
}
