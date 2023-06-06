namespace TestDatabase.Models
{
    public class Template
    {
        //properties
        public int id { get; set; }
        public string game { get; set; }
        public string name { get; set; }
        public int minimumPlayers { get; set; }
        public string rules { get; set; }
        public string winCondition { get; set; }

        //constructor
        public Template()
        {
      
        }

    }
}
