namespace EventHub.API.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        public Event()
        {
        }

        public Event(int id, string name, string description, DateTime date, string location, int capacity)
        {
            Name = name;
            Description = description;
            Date = date;
            Location = location;
            Capacity = capacity;
        }
    }
}
