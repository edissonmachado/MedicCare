namespace MedicCare.Domain
{
    public class Facility : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public Facility(string name, string city)
        {
            Name = name;
            City = city;
        }
    }
}
