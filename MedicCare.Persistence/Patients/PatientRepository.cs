using Dapper;
using MedicCare.App.Patients;
using MedicCare.Domain;
using MedicCare.Persistence.Common;

namespace MedicCare.Persistence.Patients
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly IDbContext _context;

        public PatientRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Encounter>> GetEncounters()
        {
            string query = "WITH tempEncounters as " +
                    " (SELECT " +
                    "	encounter.* " +
                    "	 , ROW_NUMBER() OVER(partition by pa.Id, py.Id order by pa.Id, py.Id) AS ROWNO " +
                    "    , pa.Id " +
                    "    , pa.firstname as firstName " +
                    "    , pa.lastname as lastName " +
                    "    , pa.age " +
                    "    , py.Id " +
                    "    , py.payer_name as CompanyName " +
                    "    , py.city as CompanyCity " +
                    "    , f.Id " +
                    "    , f.facility_name as BranchName " +
                    "    , f.city as BranchCity " +
                    " FROM encounter " +
                    " INNER JOIN payer as py on encounter.payer_id = py.Id " +
                    " INNER JOIN patient as pa on encounter.patient_id = pa.Id " +
                    " INNER JOIN facility as f on encounter.facility_id = f.Id)" +
                " SELECT * FROM tempEncounters c1 " +
                "    JOIN (SELECT patient_id, payer_id, MAX(rowno) as maxVal FROM tempEncounters group by patient_id, payer_id) c2 " +
                "    ON c1.patient_id=c2.patient_id AND c1.payer_id=c2.payer_id AND c1.rowno=c2.maxVal " +
                "    ORDER BY rowno;";

            using var connection = _context.CreateConnection();
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
