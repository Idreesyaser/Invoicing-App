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
    /// Interaction logic for NewProdect.xaml
    /// </summary>
    public partial class NewProdect : Window
        {
        public NewProdect ()
            {
            InitializeComponent();
            Tuote tuote = new Tuote ();
            this.DataContext = tuote;
            }

        private void newprodect (object sender, RoutedEventArgs e)
            {
            var content = (Tuote)this.DataContext;
            using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb; Allow User Variables=true;"))
                {
                connection.Open();

                string createTableQuery = @" insert into Tuote(Nimi, Yksikkohinta, Yksikko) 
                                                          values(@nimi, @Yksikkohinta, @Yksikko);";
                MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                createDatabaseCommand.Parameters.AddWithValue("@nimi", content.Nimi);
                createDatabaseCommand.Parameters.AddWithValue("@Yksikkohinta", Convert.ToInt32(content.Yksikkohinta));
                createDatabaseCommand.Parameters.AddWithValue("@Yksikko", content.Yksikko);
                var result = createDatabaseCommand.ExecuteReader();

                
                }

            DialogResult = true;

            }
        }
    }
