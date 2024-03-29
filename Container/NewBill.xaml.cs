using System.Windows;

namespace Harjoitustyön_kokeillu
    {
    /// <summary>
    /// Interaction logic for Lasku.xaml
    /// </summary>
    public partial class Lasku : Window
        {
        repo repo;
        public Lasku ()
            {
            InitializeComponent();
            LaskuTiedot lasku = new LaskuTiedot();
            this.DataContext = lasku;
            repo = new repo();
            comTuoteColumn.ItemsSource = repo.GetTuotes();

            }


        private void MakeANewBill (object sender, RoutedEventArgs e)
            {
            var lasku = (LaskuTiedot) this.DataContext;
            repo.NewBill(lasku);
            DialogResult = true;





            }
        }
    }
