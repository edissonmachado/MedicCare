using MedicCare.Domain.Common;

namespace MedicCare.Domain
{
    public class Payer : IEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } 
        public string City { get; set; }

        public Payer(string name, string city)
        {
            CompanyName = name;
            City = city;
        }

        public Payer() { }
    }
}
