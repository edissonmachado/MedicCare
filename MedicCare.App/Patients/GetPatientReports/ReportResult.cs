using System.Collections.Generic;

namespace MedicCare.App.Patients.GetPatientReports
{
    public class ReportRecord
    {
        public string Name { get; set; } = default!;
        public List<string> Cities { get; set; } = new List<string>();
        public char Category {  get; set; }
    }
}
