namespace TestDatabase.Models
{
    public class WedstrijdModel
    {
        public int ID { get; set; }
        public int Spel_ID { get; set; }
        public int Account_ID { get; set; }
        public int Gewonnen { get; set; }
        public int Punten { get; set; }

        public WedstrijdModel() { }
    }
}
