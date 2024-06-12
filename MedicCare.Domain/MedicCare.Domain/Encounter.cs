using MedicCare.Domain.Common;

namespace MedicCare.Domain
{
    public class Encounter : IEntity
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Facility Facility { get; set; }
        public Payer Payer { get; set; }

        public Encounter(Patient patient, Facility facility, Payer payer)
        {
            Patient = patient;
            Facility = facility;
            Payer = payer;
        }
    }
}
