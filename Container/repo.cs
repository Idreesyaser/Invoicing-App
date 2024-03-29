using System.Collections.ObjectModel;
using MySqlConnector;
namespace Harjoitustyön_kokeillu
    {
    internal class repo
        {
        private string connectionString = "Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1;";
        private string connectionStringwhithdata = "Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb;";
        private string connectionStringwhithdatavar = "Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=laskutusdb; Allow User Variables=true;";



        public void makeDb ()
            {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                connection.Open();

                string createDatabaseQuery = "drop database if EXISTS LaskutusDB;  CREATE DATABASE LaskutusDB;";
                MySqlCommand createDatabaseCommand = new MySqlCommand(createDatabaseQuery, connection);
                createDatabaseCommand.ExecuteNonQuery();

                string createTableQuery = @"
                    Use LaskutusDB;
                    CREATE TABLE IF NOT EXISTS Tuote (
                        TuoteId INT PRIMARY KEY AUTO_INCREMENT,
                        Nimi VARCHAR(100),
                        Yksikkohinta DECIMAL(18, 2),
                        Yksikko VARCHAR(20)
                    );
                    CREATE TABLE IF NOT EXISTS Asiakas (
                        Id INT PRIMARY KEY AUTO_INCREMENT,
                        Nimi VARCHAR(100),
                        Osoite VARCHAR(50)
                    );
                    CREATE TABLE IF NOT EXISTS Lasku (
                        LaskunNumero INT PRIMARY KEY,
                        LaskuttajanNimi VARCHAR(100),
                        LaskuttajanOsoite VARCHAR(100),
                        LaskunPvm DATE,
                        EraPvm DATE,
                        Lisatiedot VARCHAR(50),
                        AsiakasId int,
                        FOREIGN KEY (AsiakasId) REFERENCES Asiakas(Id)
                    );
                    CREATE TABLE IF NOT EXISTS Laskurivi (
                        LaskuriviId INT PRIMARY KEY AUTO_INCREMENT,
                        LaskunNumero INT,
                        TuoteId INT,
                        Maara INT,
                        FOREIGN KEY (LaskunNumero) REFERENCES Lasku(LaskunNumero),
                        FOREIGN KEY (TuoteId) REFERENCES Tuote(TuoteId)
                    );    

                    insert into Tuote (Nimi, Yksikkohinta, Yksikko )
                    values('Työ', 30, 'H'),
                           ('siivous', 20, 'H'),
                            ('osia', 100, 'Kpl');
                    
                    insert into Asiakas (Nimi, Osoite)
                                values('motonet', 'vantaa');
                    



";
                createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                createDatabaseCommand.ExecuteNonQuery();
                }
            }


        public ObservableCollection<Tuote> GetTuotes ()
            {
            var tuotteet = new ObservableCollection<Tuote>();
            using (MySqlConnection connection = new MySqlConnection(connectionStringwhithdata))
                {
                connection.Open();

                string createTableQuery = @"select * from Tuote;";
                
                MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
               
                var rt = createDatabaseCommand.ExecuteReader();
                while (rt.Read())
                    {
                    Tuote tuote = new Tuote();
                    tuote.tuoteId = rt.GetInt32("TuoteId");
                    tuote.Nimi = rt.GetString("Nimi") + " / ";
                    tuote.Nimi += rt.GetString("Yksikko") + " / ";
                    tuote.Nimi += rt.GetInt32("Yksikkohinta").ToString() + "e";
                    tuotteet.Add(tuote);
                    }
                }
            return tuotteet;
            }

        
        public void AddCustomer (asiakas asiakas)
            {
            using (MySqlConnection connection = new MySqlConnection(connectionStringwhithdata))
                {
                connection.Open();

                string createTableQuery = @" INSERT INTO Asiakas(Nimi, Osoite)
                                            VALUES(@Nimi, @Osoite);";
                MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                createDatabaseCommand.Parameters.AddWithValue("@Nimi", asiakas.AsiakkaanNimi);
                createDatabaseCommand.Parameters.AddWithValue("@Osoite", asiakas.AsiakkaanOsoite);
                createDatabaseCommand.ExecuteNonQuery();
                }
            }
        public void UpdateCustomer (asiakas asiakas)
            {
            using (MySqlConnection connection = new MySqlConnection(connectionStringwhithdatavar))
                {
                connection.Open();

                string createTableQuery = @" UPDATE Asiakas
                                             SET Nimi = @name, Osoite = @Osoite
                                             WHERE ID = @id;";
                MySqlCommand createDatabaseCommand = new MySqlCommand(createTableQuery, connection);
                createDatabaseCommand.Parameters.AddWithValue("@id", Convert.ToInt32(asiakas.AsiakkaanId));
                createDatabaseCommand.Parameters.AddWithValue("@name", Convert.ToString(asiakas.AsiakkaanNimi));
                createDatabaseCommand.Parameters.AddWithValue("@Osoite", Convert.ToString(asiakas.AsiakkaanOsoite));

                var result = createDatabaseCommand.ExecuteNonQuery();
                }
            }

        public void updateProdect (Tuote tuote)
            {
            using (MySqlConnection connection = new MySqlConnection(connectionStringwhithdatavar))
                {
                connection.Open();
                string Query = @"UPDATE Tuote SET NIMI = @NIMI, Yksikkohinta = @Yksikkohinta, Yksikko = @Yksikko  where TuoteId = @TuoteId";
                MySqlCommand mySqlCommand = new MySqlCommand(Query, connection);
                mySqlCommand.Parameters.AddWithValue("@TuoteId", Convert.ToInt32(tuote.tuoteId));
                mySqlCommand.Parameters.AddWithValue("@NIMI", Convert.ToString(tuote.Nimi));
                mySqlCommand.Parameters.AddWithValue("@Yksikkohinta", Convert.ToInt32(tuote.Yksikkohinta));
                mySqlCommand.Parameters.AddWithValue("@Yksikko", Convert.ToString(tuote.Yksikko));
                var result = mySqlCommand.ExecuteNonQuery();

                }

            }





        public void NewBill (LaskuTiedot lasku)
            {
            using (MySqlConnection connection = new MySqlConnection(connectionStringwhithdatavar))
                {
                connection.Open();

                string insertQuery = @"
                    INSERT INTO Lasku (LaskunNumero, LaskuttajanNimi, LaskuttajanOsoite, AsiakasId , LaskunPvm, EraPvm, Lisatiedot) 
                    VALUES (@LaskunNumero, @LaskuttajanNimi, @LaskuttajanOsoite, @AsiakasId, @LaskunPvm, @EraPvm, @Lisatiedot);";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@LaskunNumero", lasku.LaskuNumero);
                insertCommand.Parameters.AddWithValue("@LaskuttajanNimi", lasku.laskuttaja);
                insertCommand.Parameters.AddWithValue("@LaskuttajanOsoite", lasku.laskuttajanOsoite);
                insertCommand.Parameters.AddWithValue("@AsiakasId", lasku.AsiakkaanId);
                insertCommand.Parameters.AddWithValue("@LaskunPvm", lasku.Paivays);
                insertCommand.Parameters.AddWithValue("@EraPvm", lasku.Erapaiva);
                insertCommand.Parameters.AddWithValue("@Lisatiedot", lasku.lisaatiedot);
                insertCommand.ExecuteNonQuery();


                foreach (var line in lasku.laskurivit)
                    {
                    MySqlCommand cmdIns = new MySqlCommand("INSERT INTO laskurivi (maara, LaskunNumero, tuoteId) VALUES (@amount, @LaskunNumero, @tuoteID)", connection);
                    cmdIns.Parameters.AddWithValue("@amount", line.maara);
                    cmdIns.Parameters.AddWithValue("@LaskunNumero", lasku.LaskuNumero);
                    cmdIns.Parameters.AddWithValue("@tuoteID", line.TuoteId);
                    cmdIns.ExecuteNonQuery();
                    }
                }               
            }

        }

    }

