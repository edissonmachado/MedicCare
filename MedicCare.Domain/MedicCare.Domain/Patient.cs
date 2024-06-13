using MedicCare.Domain.Common;

namespace MedicCare.Domain
{
    public class Patient : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Patient(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Patient() { }
    }
}
