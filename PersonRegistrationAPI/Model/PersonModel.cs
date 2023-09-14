namespace PersonRegistrationAPI.Model
{
    public class PersonModel
    {
        public bool isAdmin { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthday { get; set; }
        public long phoneNumber { get; set; }
        public string gender { get; set; }
        public int employed { get; set; }
        public string maritalStatus { get; set; }
        public string birthplace { get; set; }
    }
}
