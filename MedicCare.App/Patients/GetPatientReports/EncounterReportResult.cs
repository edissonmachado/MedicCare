using System.Collections.Generic;

namespace MedicCare.App.Patients.GetPatientReports
{
    public class EncounterReportResult
    {
        public string Name { get; set; } = default!;
        public string Cities { get; set; } = default!;
        public char Category {  get; set; }
    }
}
