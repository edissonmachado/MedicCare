using System.Collections.Generic;
using System.Threading.Tasks;
using MedicCare.App.Common;
using MedicCare.App.Patients.GetPatientReports;
using MedicCare.Domain;

namespace MedicCare.App.Patients
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<List<EncounterReport>> GetEncountersAsync();
    }
}
