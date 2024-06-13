using System.Data;
using Dapper;
using MedicCare.App.Patients;
using MedicCare.Domain;
using MedicCare.Persistence.Common;
using Npgsql;

namespace MedicCare.Persistence.Patients
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly IDbContext _db;

        public PatientRepository(IDbContext db)
        {
            _db = db;
        }

        public async Task<List<Encounter>> GetPatientReports()
        {
            string query = "SELECT encounter.*" +
                "   , pa.Id" +
                "   , pa.first_name as firstName" +
                "   , pa.last_name as lastName" +
                "   , pa.age" +
                "   , py.Id" +
                "   , py.payer_name as CompanyName" +
                "   , py.city as CompanyCity" +
                "   , f.Id" +
                "   , f.facility_name as BranchName" +
                "   , f.city BranchCity" +
                " FROM encounter" +
                " INNER JOIN payer as py on encounter.payer_id = py.Id" +
                " INNER JOIN patient as pa on encounter.payer_id = pa.Id" +
                " INNER JOIN facility as f on encounter.facility_id = f.Id;";

            using var connection = _db.CreateConnection();
            var result = await connection.QueryAsync<Encounter, Patient, Payer, Facility, Encounter>(query,
                (e, pa, py, f) =>
                {
                    e.Patient = pa;
                    e.Payer = py;
                    e.Facility = f;
                    return e;
                }).ConfigureAwait(false);

            return result.ToList();
        }
    }
}
