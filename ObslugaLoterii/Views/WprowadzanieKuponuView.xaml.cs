using ObslugaLoterii.ViewModels;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ObslugaLoterii.Views
{
    /// <summary>
    /// Interaction logic for WprowadzanieKuponuWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class WprowadzanieKuponuWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WprowadzanieKuponuWindow"/> class.
        /// </summary>
        public WprowadzanieKuponuWindow()
        {
            InitializeComponent();
            var vm = new WprowadzanieKuponuVM();
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.ZmienOkno);
            if (vm.OtworzOknoDodajKlientaAction == null)
                vm.OtworzOknoDodajKlientaAction = new Action(this.OtworzOknoDodajKlienta);
            Show();
        }

        /// <summary>
        /// Zmien okno.
        /// </summary>
        private void ZmienOkno()
        {
            new MenuListyZwyciezcowWindow();
            Close();
        }

        /// <summary>
        /// Otworz okno dodaj klienta.
        /// </summary>
        public void OtworzOknoDodajKlienta()
        {
            new WprowadzanieKlientaWindow();
            Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
