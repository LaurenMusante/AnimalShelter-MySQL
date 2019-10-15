using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AnimalShelter.Models
{
    public class Animal
    {
        public string Type { get; set; }
        public string Name { get; set; } 
        public string Gender { get; set; }
        public int AdmittanceDate { get; set; }
        public string Breed { get; set; } 
        public int AnimalId { get; set; }

        public Animal(string type, string name, string gender, int admittanceDate, string breed, int id)
        {
            Type = type;
            Name = name;
            Gender = gender;
            AdmittanceDate = admittanceDate;
            Breed = breed;
            AnimalId= id;
        }
        
        public Animal(string name, int id)
        {
            Name = name;
            AnimalId = id;
        }

        public static List<Animal> GetAll()
        {
          List<Animal> allAnimals = new List<Animal> { };
            MySqlConnection conn = DB.Connection();
            conn.Open(); // opens DB connection
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM AnimalShelter;"; //this is b/c we are defining our GetAll() method, and this is what we want it to do (get all of our items from the database)
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            // this line is responsible for reading the data returned from the database. it also casts it as a mysql data-reader object.
            while (rdr.Read())
            {
                int AnimalId = rdr.GetInt32(0);
                string AnimalName = rdr.GetString(1);
                Animal newAnimal = new Animal(AnimalName, AnimalId);
                allAnimals.Add(newAnimal);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allAnimals;
        }
         public static void ClearAll()
            {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"TRUNCATE TABLE AnimalShelter;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
            
            }

        public static Animal Find(int id)
        {
            // We open a connection.
            MySqlConnection conn = DB.Connection();
            conn.Open();

            // We create MySqlCommand object and add a query to its CommandText property. We always need to do this to make a SQL query.
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `AnimalShelter` WHERE id = @thisId;";

            // We have to use parameter placeholders (@thisId) and a `MySqlParameter` object to prevent SQL injection attacks. This is only necessary when we are passing parameters into a query. We also did this with our Save() method.
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            // We use the ExecuteReader() method because our query will be returning results and we need this method to read these results. This is in contrast to the ExecuteNonQuery() method, which we use for SQL commands that don't return results like our Save() method.
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int AnimalId = 0;
            string AnimalName = "";
            while (rdr.Read())
            {
            AnimalId = rdr.GetInt32(0);
            AnimalName = rdr.GetString(1);
            }
            Animal foundAnimal= new Animal(AnimalName, AnimalId);

            // We close the connection.
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
            return foundAnimal;
        }
        public void Save()
            {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO AnimalShelter (name) VALUES (@AnimalName);";
            //  the parameter placeholder (@ItemDescription) is similar to how we store and conceal API keys. it hides potentially sensitive information, but allows us to still use its value while communicating to the database.
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@AnimalName";
            name.Value = this.Name;
            cmd.Parameters.Add(name);    
            cmd.ExecuteNonQuery();
            // this line executes the command we defined above in cmd.commandtext
            AnimalId = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
                {
                conn.Dispose();
                }
            }

    }
}