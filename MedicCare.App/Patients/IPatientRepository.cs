using System.Collections.Generic;
using System.Threading.Tasks;
using MedicCare.App.Common;
using MedicCare.Domain;

namespace MedicCare.App.Patients
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<List<Encounter>> GetEncounters();
    }
}
