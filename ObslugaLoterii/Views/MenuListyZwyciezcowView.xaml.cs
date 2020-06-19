using ObslugaLoterii.ViewModels;
using System;
using System.Windows;

namespace ObslugaLoterii.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MenuListyZwyciezcowWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuListyZwyciezcowWindow"/> class.
        /// </summary>
        public MenuListyZwyciezcowWindow()
        {
            InitializeComponent();
            var vm = new MenuListyZwyciezcowVM();
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.ZmienOkno);
            Show();
        }

        /// <summary>
        /// Zmien okno.
        /// </summary>
        public void ZmienOkno()
        {
            new WprowadzanieKuponuWindow();
            Close();
        }
    }
}
