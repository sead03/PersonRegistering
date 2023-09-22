using System;

namespace PersonRegistering.Model
{
    public class PersonModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthday { get; set; }
        public int phoneNumber { get; set; }
        public string gender { get; set; }
        public bool employed { get; set; }
        public string maritalStatus { get; set; }
        public string birthplace { get; set; }

    }
}
