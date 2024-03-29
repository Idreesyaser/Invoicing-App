using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Harjoitustyön_kokeillu
    {
    /// <summary>
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class NewCustomer : Window
        {
        public NewCustomer ()
            {
            InitializeComponent();
            }

        private void Button_Click (object sender, RoutedEventArgs e)
            {
            asiakas asiakas = new asiakas ();
            asiakas.AsiakkaanNimi = Name.Text;
            asiakas.AsiakkaanOsoite = Osoite.Text;
            repo repo = new repo ();
            repo.AddCustomer (asiakas);
            DialogResult = true;

            }
        }
    }
