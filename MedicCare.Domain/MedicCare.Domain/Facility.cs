using MedicCare.Domain.Common;

namespace MedicCare.Domain
{
    public class Facility : IEntity
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }

        public Facility(string name, string city)
        {
            BranchName = name;
            City = city;
        }

        public Facility() { }
    }
}
