using System.Collections.Generic;

namespace MedicCare.App.Patients.GetPatientReports
{
    public class EncounterReport
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int Age { get; set; }
        public string Cities { get; set; } = default!;
    }
}
