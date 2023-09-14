using System;

namespace PersonRegistering.Model
{
    public class PersonModel
    {
        public bool isAdmin { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthday { get; set; }
        public int phoneNumber { get; set; }
        public string gender { get; set; }
        public bool employed { get; set; }
        public string maritalStatus { get; set; }
        public string birthplace { get; set; }

        public string username { get; set; }
        public string role { get; set; }
    }
}
