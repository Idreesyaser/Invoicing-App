using System.Windows;

namespace Harjoitustyön_kokeillu
    {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
        repo repo;
        public MainWindow ()
            {
            InitializeComponent();
            repo = new repo();
            repo.makeDb();


            }



        private void AddNewCustomer (object sender, RoutedEventArgs e)
            {
            NewCustomer newCustomer = new NewCustomer();
            var result = newCustomer.ShowDialog();

            }

        private void UpdateCustomer (object sender, RoutedEventArgs e)
            {
            päivitäAsiakas päivitäAsiakas = new päivitäAsiakas();
            var result = päivitäAsiakas.ShowDialog();
            }

        private void makeBill (object sender, RoutedEventArgs e)
            {

            Lasku lasku = new Lasku();
            lasku.ShowDialog();

            }

        private void showBill (object sender, RoutedEventArgs e)
            {
            int asikkaanId = Convert.ToInt32(AsiakasId.Text);
            laskuNäkymä laskuNäkymä = new laskuNäkymä();
            if (laskuNäkymä.ShowBill(asikkaanId).Count() > 0)
                {
                laskuNäkymä.ShowBill(asikkaanId);
                laskuNäkymä.ShowDialog();
                }
            else
                {
                MessageBox.Show("Asiakkaalla ei ole laskuja  ");
                }

            }

        private void ShowAllBills (object sender, RoutedEventArgs e)
            {
            kaikkiLaskut kaikkiLaskut = new kaikkiLaskut();
            if (kaikkiLaskut.ShowBill().Count() > 0)
                {
                kaikkiLaskut.ShowBill();
                kaikkiLaskut.ShowDialog();
                }
            else
                {
                MessageBox.Show("Asiakkaalla ei ole laskuja  ");
                }

            }

        private void addProdect (object sender, RoutedEventArgs e)
            {
            NewProdect newProdect = new NewProdect();
            newProdect.ShowDialog();
            }

        private void UpdateProdect (object sender, RoutedEventArgs e)
            {
            UpdateProdect updateProdect = new UpdateProdect();
            updateProdect.ShowDialog();

            }

        private void käyttöOhjee (object sender, RoutedEventArgs e)
            {
            MessageBox.Show("Ohjelman käyttöohjeet:\n\n" +
                            "1. Asiakkaiden ja tuotteiden hallinta:\n" +
                            "- Voit lisätä uuden asiakkaan käyttämällä 'Lisää asiakas' -toimintoa.\n" +
                            "- Voit päivittää tai poistaa asiakkaan käyttämällä vastaavia toimintoja.\n" +
                            "- Samoin voit lisätä, päivittää ja poistaa tuotteita vastaavilla toiminnoilla.\n" +
                            "- Huomioi, että 1 asiakasta ja 3 tuottetta on luotu etukäteen. asiakkaan id (1)\n\n" +

                            "2. Laskun luominen:\n" +
                            "- Voit luoda uuden laskun valitsemalla 'Luo lasku' -toiminnon.\n" +
                            "- Laskua luodessasi sinun tarvitsee vain antaa asiakkaan ID ja lisätä haluamasi määrä tuotteita.\n" +
                            "- Laskuun ei tarvitse erikseen kirjoittaa asiakkaan tietoja, vaan pelkkä asiakkaan ID riittää.\n\n" +

                            "3. Laskujen hallinta:\n" +
                            "- Voit hakea tietyn asiakkaan laskut kijoittamalla asiakkaan id ja käyttämällä 'Hae tietyn asiakkaan laskut' toimintoa.\n" +
                            "- Laskunäkymässä näet asiakkaan tiedot, ei pelkkää ID:tä.\n" +
                            "- Jos päivität asiakkaan tai tuotteen tietoja, muutokset vaikuttavat kaikkiin liittyviin laskuihin.\n\n" +

                            "4. Kaikkien laskujen hallinta:\n" +
                            "- Voit hakea kaikki tehdyt laskut valitsemalla 'Hae kaikki laskut' -toiminnon.\n" +
                            "- Tässä näkymässä on samat tiedot kuin haettaessa tietyn asiakkaan laskuja.\n" +
                            "- Voit nähdä jokaisen laskun yhteydessä asiakkaan tiedot, ei pelkkää ID:tä.");
            }
        }
        }
    


    public class Tuote
        {
        public int tuoteId {  get; set; }
        public string Nimi { get; set; }
        public double Yksikkohinta { get; set; }
        public string Yksikko { get; set; }
        }

    public class laskurivi
        {


        public int LaskuriviId {  get; set; }
        public int LaskuId { get; set; }
        public int TuoteId { get; set;}
        public int maara {  get; set; }

        }



    public class asiakas 
        {
        public int AsiakkaanId { get; set; }
        public string AsiakkaanNimi { get; set; }
        public string AsiakkaanOsoite { get; set; }
        }

      public class LaskuTiedot : asiakas 
        {

        // Laskun tiedot
        public int LaskuNumero { get; set; }
        public string laskuttaja { get; set; }
        public string laskuttajanOsoite { get; set; }
        public DateTime Paivays { get; set; }
    public DateTime Erapaiva { get; set; }
    public string lisaatiedot { get; set; }
        // Lista laskuriveistä
        public List<laskurivi> laskurivit { get; set; }
        public LaskuTiedot ()
            {
            laskuttaja = "Rakennus Oy";
            laskuttajanOsoite = "Helsinki";
            Paivays = DateTime.Now;
            Erapaiva = DateTime.Now;
            laskurivit = new List<laskurivi>();
            }
        public int total { get; set; }

        }
    
