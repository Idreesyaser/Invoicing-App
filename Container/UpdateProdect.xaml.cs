using MySqlConnector;
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
    /// Interaction logic for UpdateProdect.xaml
    /// </summary>
    public partial class UpdateProdect : Window
        {
        repo repo;
        public UpdateProdect ()
            {
            InitializeComponent();
            }

        private void UpdateClick (object sender, RoutedEventArgs e)
            {
            var tuote = (Tuote)this.DataContext;
            repo = new repo();
            repo.updateProdect(tuote);
            DialogResult = true;
            }

        private void SelecteClick (object sender, RoutedEventArgs e)
            {
            Tuote Tuote = new Tuote();
            using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb; Allow User Variables=true;"))
                {
                connection.Open();

                string createTableQuery = @" select TuoteId, Nimi, Yksikkohinta, Yksikko 
                                             From Tuote
                                             where TuoteId = @id;";
                MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                createDatabaseCommand.Parameters.AddWithValue("@id", Convert.ToInt32(TuoteId.Text));
                var result = createDatabaseCommand.ExecuteReader();
                while (result.Read())
                    {
                    Tuote.tuoteId = Convert.ToInt32(result["TuoteId"]);
                    Tuote.Nimi = Convert.ToString(result["Nimi"]);
                    Tuote.Yksikkohinta = Convert.ToInt32(result["Yksikkohinta"]);
                    Tuote.Yksikko = Convert.ToString(result["Yksikko"]);

                    }
                this.DataContext = Tuote;
                }
            }

        private void DeleteProdect (object sender, RoutedEventArgs e)
            {
            var Tuote = (Tuote) this.DataContext;
            if (Tuote != null)
                {
                using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb; Allow User Variables=true;"))
                    {
                    connection.Open();


                    string createTableQuery = @" delete from Laskurivi Where TuoteId = @id";
                    MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                    createDatabaseCommand.Parameters.AddWithValue("@id", Convert.ToInt32(TuoteId.Text));
                    var result = createDatabaseCommand.ExecuteNonQuery();
                    deleteprodect(Tuote);

                    }
                }

            }
        private void deleteprodect (Tuote tuote)
            {

            using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb; Allow User Variables=true;"))
                {
                connection.Open();


                string createTableQuery = @" delete from Tuote Where TuoteId = @id";
                MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                createDatabaseCommand.Parameters.AddWithValue("@id", Convert.ToInt32(tuote.tuoteId));
                var result = createDatabaseCommand.ExecuteNonQuery();
                }
            DialogResult = true;
            }
        }
    }
