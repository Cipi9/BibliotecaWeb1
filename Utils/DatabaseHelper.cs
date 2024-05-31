using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PA_project.Utils
{
    public class DatabaseHelper
    {
        private readonly string masterConnectionString;
        private readonly string databaseName;

        public DatabaseHelper(string masterConnectionString, string databaseName)
        {
            this.masterConnectionString = masterConnectionString;
            this.databaseName = databaseName;
        }

        public void initializeDatabase()
        {
            createDatabaseIfNotExists(masterConnectionString);
            string connectionString = $"{masterConnectionString};Initial Catalog={databaseName};";
            createTables(connectionString);
        }

        private void createTables(string connectionString)
        {
            createUsersTable(connectionString);
            createMoviesTable(connectionString);
            createCinemasTable(connectionString);
            createTicketsTable(connectionString);
            createReviewsTable(connectionString);
        }

        private void createUsersTable(string connectionString)
        {
            // USERS table
            string[] moviesTableColumns = { "user_id INT PRIMARY KEY NOT NULL",
                                            "first_name VARCHAR(255) NOT NULL",
                                            "last_name VARCHAR(255) NOT NULL",
                                            "email VARCHAR(255) NOT NULL",
                                            "phone_number VARCHAR(20) NOT NULL" };
            createTableIsNotExists(connectionString, "USERS", moviesTableColumns.ToList());
        }

        private void createMoviesTable(string connectionString)
        {
            // MOVIES table
            string[] moviesTableColumns = { "movie_id INT PRIMARY KEY NOT NULL",
                                            "title VARCHAR(255) NOT NULL",
                                            "description VARCHAR(1000)",
                                            "release_date DATE",
                                            "duration VARCHAR(10)",
                                            "genre VARCHAR(100)" };
            createTableIsNotExists(connectionString, "MOVIES", moviesTableColumns.ToList());
        }

        private void createCinemasTable(string connectionString)
        {
            // CINEMAS table
            string[] moviesTableColumns = { "cinema_id INT PRIMARY KEY NOT NULL",
                                            "name VARCHAR(255) NOT NULL",
                                            "location VARCHAR(1000)",
                                            "contact_info varchar(20)",
                                            "capacity INT" };
            createTableIsNotExists(connectionString, "CINEMAS", moviesTableColumns.ToList());
        }

        private void createTicketsTable(string connectionString)
        {
            // TICKETS table
            string[] moviesTableColumns = { "ticket_id INT PRIMARY KEY NOT NULL",
                                            "user_id INT NOT NULL",
                                            "movie_id INT NOT NULL",
                                            "cinema_id INT NOT NULL",
                                            "screening_type VARCHAR(255)",
                                            "hour VARCHAR(10) NOT NULL",
                                            "purchase_date DATE NOT NULL",
                                            "price FLOAT NOT NULL",
                                            "seat_number INT NOT NULL",
                                            "language VARCHAR(10)",
                                            "FOREIGN KEY (user_id) REFERENCES USERS(user_id)",
                                            "FOREIGN KEY (movie_id) REFERENCES MOVIES(movie_id)",
                                            "FOREIGN KEY (cinema_id) REFERENCES CINEMAS(cinema_id)" };
            createTableIsNotExists(connectionString, "TICKETS", moviesTableColumns.ToList());
        }

        private void createReviewsTable(string connectionString)
        {
            // REVIEWS table
            string[] moviesTableColumns = { "cinema_id INT PRIMARY KEY NOT NULL",
                                            "user_id INT NOT NULL",
                                            "movie_id INT NOT NULL",
                                            "rating INT NOT NULL",
                                            "review_text VARCHAR(1000)",
                                            "creation_date DATE",
                                            "recommended INT",
                                            "FOREIGN KEY (user_id) REFERENCES USERS(user_id)",
                                            "FOREIGN KEY (movie_id) REFERENCES MOVIES(movie_id)" };
            createTableIsNotExists(connectionString, "REVIEWS", moviesTableColumns.ToList());
        }

        private void createDatabaseIfNotExists(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Checking if the database already exists
                string checkDatabaseQuery = $"SELECT COUNT (*) FROM sys.databases WHERE name = '{databaseName}'";
                SqlCommand checkCommand = new SqlCommand(checkDatabaseQuery, connection);
                int databaseCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Creating the database if it doesn't exist
                if (databaseCount == 0)
                {
                    string createDatabaseQuery = $"CREATE DATABASE {databaseName}";
                    SqlCommand createCommand = new SqlCommand(createDatabaseQuery, connection);
                    createCommand.ExecuteNonQuery();
                }
            }
        }

        private void createTableIsNotExists(string connectionString, string tableName, List<String> columns)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Checking if the table already exists
                string checkTableQuery = $"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                SqlCommand checkCommand = new SqlCommand(checkTableQuery, connection);
                int databaseCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Creating the database if it doesn't exist

                if (databaseCount == 0)
                {
                    string columnsString = string.Join(", ", columns);
                    string createTableQuery = $"CREATE TABLE {tableName} ({columnsString})";
                    SqlCommand createCommand = new SqlCommand(createTableQuery, connection);
                    createCommand.ExecuteNonQuery();
                }
            }
        }
    }
}