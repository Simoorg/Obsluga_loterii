using ObslugaLoterii.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ObslugaLoterii.ViewModels
{

    /// <summary>
    /// VM obsługujący View 'WprowadzanieKuponuView'
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    class WprowadzanieKuponuVM : BindableBase
    {
        /// <summary>
        /// Gets or sets the close action.
        /// </summary>
        /// <value>
        /// close action.
        /// </value>
        public Action CloseAction { get; set; }
        /// <summary>
        /// Gets or sets the otworz okno dodaj klienta action.
        /// </summary>
        /// <value>
        /// otworz okno dodaj klienta action.
        /// </value>
        public Action OtworzOknoDodajKlientaAction { get; set; }
        /// <summary>
        /// Gets or sets the otworz okno dodaj klienta.
        /// </summary>
        /// <value>
        /// otworz okno dodaj klienta.
        /// </value>
        public ICommand OtworzOknoDodajKlienta { get; set; }
        /// <summary>
        /// Gets or sets the powrot.
        /// </summary>
        /// <value>
        /// powrot.
        /// </value>
        public ICommand Powrot { get; set; }
        /// <summary>
        /// Gets or sets the zatwierdz kupon.
        /// </summary>
        /// <value>
        /// zatwierdz kupon.
        /// </value>
        public ICommand ZatwierdzKupon { get; set; }
        /// <summary>
        /// Gets or sets the nr dowodu list.
        /// </summary>
        /// <value>
        /// nr dowodu lista.
        /// </value>
        public ObservableCollection<Klient> NrDowoduList { get; set; }
        /// <summary>
        /// repository
        /// </summary>
        Repozytorium repozytorium = new Repozytorium();
        /// <summary>
        /// wybrany klient
        /// </summary>
        public Klient wybranyKlient;
        /// <summary>
        /// Gets or sets the wybrany klient.
        /// </summary>
        /// <value>
        /// wybrany klient.
        /// </value>
        public Klient WybranyKlient
        {
            get { return wybranyKlient; }
            set { SetProperty(ref wybranyKlient, value); }
        }

        /// <summary>
        /// The liczba1
        /// </summary>
        public int liczba1;
        /// <summary>
        /// Gets or sets the liczba1.
        /// </summary>
        /// <value>
        /// liczba1.
        /// </value>
        public int Liczba1
        {
            get { return liczba1; }
            set { SetProperty(ref liczba1, value); }
        }
        /// <summary>
        /// The liczba2
        /// </summary>
        public int liczba2;
        /// <summary>
        /// Gets or sets the liczba2.
        /// </summary>
        /// <value>
        /// liczba2.
        /// </value>
        public int Liczba2
        {
            get { return liczba2; }
            set { SetProperty(ref liczba2, value); }
        }
        /// <summary>
        /// The liczba3
        /// </summary>
        public int liczba3;
        /// <summary>
        /// Gets or sets the liczba3.
        /// </summary>
        /// <value>
        /// liczba3.
        /// </value>
        public int Liczba3
        {
            get { return liczba3; }
            set { SetProperty(ref liczba3, value); }
        }
        /// <summary>
        /// The liczba4
        /// </summary>
        public int liczba4;
        /// <summary>
        /// Gets or sets the liczba4.
        /// </summary>
        /// <value>
        /// liczba4.
        /// </value>
        public int Liczba4
        {
            get { return liczba4; }
            set { SetProperty(ref liczba4, value); }
        }
        /// <summary>
        /// The liczba5
        /// </summary>
        public int liczba5;
        /// <summary>
        /// Gets or sets the liczba5.
        /// </summary>
        /// <value>
        /// liczba5.
        /// </value>
        public int Liczba5
        {
            get { return liczba5; }
            set { SetProperty(ref liczba5, value); }
        }
        /// <summary>
        /// The liczba6
        /// </summary>
        public int liczba6;
        /// <summary>
        /// Gets or sets the liczba6.
        /// </summary>
        /// <value>
        /// liczba6.
        /// </value>
        public int Liczba6
        {
            get { return liczba6; }
            set { SetProperty(ref liczba6, value); }
        }
        /// <summary>
        /// The tekst bledu
        /// </summary>
        private string tekstBledu;
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
        /// Initializes a new instance of the <see cref="WprowadzanieKuponuVM"/> class.
        /// </summary>
        public WprowadzanieKuponuVM()
        {
            OtworzOknoDodajKlienta = new DelegateCommand(OtworzOknoDodajKlientaWykonaj);
            Powrot = new DelegateCommand(PowrotWykonaj);
            ZatwierdzKupon = new DelegateCommand(ZatwierdzKuponWykonaj);
            UaktualnijListe();
        }

        /// <summary>
        /// Zatwierdz kupon wykonaj.
        /// Nie wykona się jeśli typy nie są poprawnie wprowadzone
        /// </summary>
        private void ZatwierdzKuponWykonaj()
        {
            if (wybranyKlient != null && liczba1 > 0 && liczba2 > 0 && liczba3 > 0 && liczba4 > 0 && liczba5 > 0 && liczba6 > 0)
            {
                int[] typyNiepsrawdzone = new int[] { liczba1, liczba2, liczba3, liczba4, liczba5, liczba6 };
                int[] typy = new int[6];
                for(int i=0; i<6; i++)
                {
                    if (typy.Contains(typyNiepsrawdzone[i]))
                    {
                        tekstBledu = "Sprawdź czy wszystkie dane są poprawnie wprowadzone";
                        RaisePropertyChanged("TekstBledu");
                        return;
                    }
                    else
                        typy[i] = typyNiepsrawdzone[i];
                }
                int idLoterii = repozytorium.DajLoterie().Last().LoteriaID;
                if(repozytorium.DajKlientaOID(wybranyKlient.KlientID).ZakupioneKupony.Count(x => x.IdLoterii == idLoterii) >= 10)
                {
                    tekstBledu = "Klient osiągnął limit kuponów";
                    RaisePropertyChanged("TekstBledu");
                    return;
                }
                Kupon kupon = new Kupon
                {
                    Wygrana = 0,
                    Typy = typy,
                    CzyWyplacono = false,
                    IdKlienta = wybranyKlient.KlientID,
                    IdLoterii = idLoterii
                };
                repozytorium.DodajKupon(kupon);
                tekstBledu = "Pomyślnie dodano";
                RaisePropertyChanged("TekstBledu");
                liczba1 = 0;
                liczba2 = 0;
                liczba3 = 0;
                liczba4 = 0;
                liczba5 = 0;
                liczba6 = 0;
                RaisePropertyChanged("liczba1");
                RaisePropertyChanged("liczba2");
                RaisePropertyChanged("liczba3");
                RaisePropertyChanged("liczba4");
                RaisePropertyChanged("liczba5");
                RaisePropertyChanged("liczba6");
            }
            else
            {
                tekstBledu = "Sprawdź czy wszystkie dane są poprawnie wprowadzone";
                RaisePropertyChanged("TekstBledu");
            }
        }

        /// <summary>
        /// Powrot wykonaj.
        /// </summary>
        private void PowrotWykonaj()
        {
            CloseAction();
        }

        /// <summary>
        /// Uaktualnia liste numerow dowodu.
        /// </summary>
        public void UaktualnijListe()
        {
            NrDowoduList = new ObservableCollection<Klient>(repozytorium.DajKlientow());
            RaisePropertyChanged("NrDowoduList");
        }

        /// <summary>
        /// Otworz okno dodaj klienta wykonaj.
        /// </summary>
        private void OtworzOknoDodajKlientaWykonaj()
        {
            OtworzOknoDodajKlientaAction();
        }
    }
}
