using ObslugaLoterii.ViewModels;
using System;
using System.Windows;

namespace ObslugaLoterii.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class WprowadzanieKlientaWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WprowadzanieKlientaWindow"/> class.
        /// </summary>
        public WprowadzanieKlientaWindow()
        {
            InitializeComponent();
            var vm = new WprowadzanieKlientaVM();
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.ZmienOkno);
            Show();
        }

        /// <summary>
        /// Zmien okno.
        /// </summary>
        private void ZmienOkno()
        {
            new WprowadzanieKuponuWindow();
            Close();
        }
    }
}
