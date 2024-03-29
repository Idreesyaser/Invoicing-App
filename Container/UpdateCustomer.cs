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
    /// Interaction logic for päivitäAsiakas.xaml
    /// </summary>
    public partial class päivitäAsiakas : Window
        {
        public päivitäAsiakas ()
            {
            InitializeComponent();


            }

        public void SelecteClick (object sender, RoutedEventArgs e)
            {
            asiakas asiakas = new asiakas();
            using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb; Allow User Variables=true;"))
                {
                connection.Open();

                string createTableQuery = @" select Nimi, Osoite 
                                             From Asiakas
                                             where id = @id;";
                MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                createDatabaseCommand.Parameters.AddWithValue("@id", Convert.ToInt32(AsiakasId.Text));
                var result = createDatabaseCommand.ExecuteReader();
                while (result.Read())
                    {
                    asiakas.AsiakkaanNimi = Convert.ToString(result["Nimi"]);
                    asiakas.AsiakkaanOsoite = Convert.ToString(result["osoite"]);
                    }
                this.DataContext = asiakas;
                }
            }

        private void UpdateClick (object sender, RoutedEventArgs e)
            {
            asiakas asiakas = new asiakas();
            asiakas.AsiakkaanId = Convert.ToInt32(AsiakasId.Text);
            asiakas.AsiakkaanNimi = Convert.ToString(AsiakasNimi.Text);
            asiakas.AsiakkaanOsoite = Convert.ToString(AsiakasOsoite.Text);
            repo repo = new repo();
            repo.UpdateCustomer(asiakas);

            DialogResult = true;
            }

        private void DeleteCustomer (object sender, RoutedEventArgs e)
            {
            List<int> laskut = new List<int>();
            var asiakas = (asiakas)this.DataContext;
            if (asiakas != null)
                {
                using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb; Allow User Variables=true;"))
                    {
                    connection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand(@"select LaskunNumero from Lasku where AsiakasId = @id", connection);
                    mySqlCommand.Parameters.AddWithValue("@id", Convert.ToInt32(AsiakasId.Text));
                    var rt = mySqlCommand.ExecuteReader();
                    while(rt.Read())
                        {
                        laskut.Add(rt.GetInt32("LaskunNumero"));
                        }

                    }
                deleteitems(laskut);
                DialogResult = true;

                }
            void deleteitems(List<int> laskunumerot)
                {
                if(laskunumerot.Count() > 0)
                    {
                    foreach (var item in laskut)
                        {
                        using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb; Allow User Variables=true;"))
                            {
                            connection.Open();
                            string createTableQuery = @" delete from Laskurivi Where LaskunNumero = @id; delete from Lasku Where LaskunNumero = @id;";
                            MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                            createDatabaseCommand.Parameters.AddWithValue("@id", Convert.ToInt32(item));
                            var result = createDatabaseCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    
                    using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb; Allow User Variables=true;"))
                        {
                        connection.Open();
                        string createTableQuery = @"delete from Asiakas where Id = @asiakasId";
                        MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                        createDatabaseCommand.Parameters.AddWithValue("@asiakasId", Convert.ToInt32(AsiakasId.Text));

                        var result = createDatabaseCommand.ExecuteNonQuery();
                        
                    }

                }
            }







        }
        }
    