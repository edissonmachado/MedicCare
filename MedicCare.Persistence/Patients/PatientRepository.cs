using MedicCare.App.Patients;
using MedicCare.Domain;
using MedicCare.Persistence.Common;

namespace MedicCare.Persistence.Patients
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public async Task<List<Encounter>> GetPatientReports()
        {
            return await Task.FromResult(new List<Encounter>()).ConfigureAwait(false);
        }
    }
}
