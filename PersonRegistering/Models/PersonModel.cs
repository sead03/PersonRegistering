using System;

namespace PersonRegistering.Models
{
    internal class PersonModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthday { get; set; }
        public string phoneNumber { get; set; }
        public string gender { get; set; }
        public int employed { get; set; }
        public string maritalStatus { get; set; }
        public string birthplace { get; set; }
    }
}
