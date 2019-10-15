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

    }
}