using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for kaikkiLaskut.xaml
    /// </summary>
    public partial class kaikkiLaskut : Window
        {
        public int counter;
        public kaikkiLaskut ()
            {
            InitializeComponent();
            InitializeComponent();
            repo repo = new repo();
            comTuoteColumn.ItemsSource = repo.GetTuotes();
            }
        public ObservableCollection<LaskuTiedot> ShowBill ()
            {
            var laskut = new ObservableCollection<LaskuTiedot>();
            using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb;"))
                {
                int count = 1;
                var lasku = new LaskuTiedot();
                connection.Open();

                string createTableQuery = @"     
                                    SELECT l.LaskunNumero, l.LaskuttajanNimi, s.Nimi, s.Id, l.LaskuttajanOsoite ,s.Osoite, l.LaskunPvm, l.EraPvm, l.Lisatiedot
                                    FROM lasku l, Asiakas s, laskurivi lv
                                    WHERE l.LaskunNumero = lv.LaskunNumero AND l.AsiakasId = s.Id";
                MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);

                var result = createDatabaseCommand.ExecuteReader();
                while (result.Read())
                    {
                    lasku = new LaskuTiedot();
                    lasku.LaskuNumero = result.GetInt32("LaskunNumero");
                    lasku.laskuttaja = result.GetString("LaskuttajanNimi");
                    lasku.AsiakkaanId = result.GetInt32("Id");
                    lasku.laskuttajanOsoite = result.GetString("LaskuttajanOsoite");
                    lasku.AsiakkaanNimi = result.GetString("Nimi");
                    lasku.AsiakkaanOsoite = result.GetString("Osoite");
                    lasku.Paivays = result.GetDateTime("LaskunPvm");
                    lasku.Erapaiva = result.GetDateTime("EraPvm");
                    lasku.lisaatiedot = result.GetString("Lisatiedot");
                    Getlines(lasku);

                    laskut.Add(lasku);
                }
            }

            if (laskut.Count > 0) { this.DataContext = laskut[counter]; }
            return laskut;
            }


        private void Getlines (LaskuTiedot lasku)
            {

            using (MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb;"))
                {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"select * from laskurivi where LaskunNumero = @laskunNumero  ", conn);
                cmd.Parameters.AddWithValue("@laskunNumero", lasku.LaskuNumero);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                    {
                    lasku.laskurivit.Add(new laskurivi
                        {
                        LaskuId = dr.GetInt32("LaskunNumero"),
                        maara = dr.GetInt32("maara"),
                        LaskuriviId = dr.GetInt32("Laskuriviid"),
                        TuoteId = dr.GetInt32("TuoteId")
                        });
                    getmount(dr.GetInt32("Laskuriviid"), lasku);
                    }
                }
        }
        public void getmount (int laskuRiviId, LaskuTiedot lasku)
            {
            using (MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb;"))
                {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"select maara, Yksikkohinta from laskurivi l , tuote t where Laskuriviid = @Laskuriviid and t.TuoteId = l.TuoteId ", conn);
                cmd.Parameters.AddWithValue("@Laskuriviid", laskuRiviId);
                var dr = cmd.ExecuteReader();


                while (dr.Read())
                    {

                    int maara = dr.GetInt32("maara");
                    int hinta = dr.GetInt32("Yksikkohinta");

                    lasku.total += maara * hinta;
                    }

                }
            }

        private void laskee (object sender, RoutedEventArgs e)
        {

            if (counter != 0)
                {
                counter--;
                ShowBill();
                }
            }
        private void kasvaa (object sender, RoutedEventArgs e)
            {
            if (counter < ShowBill().Count() - 1)
                {
                counter++;
                ShowBill();
                }
            }
        }
    }
