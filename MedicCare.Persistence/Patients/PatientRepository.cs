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
            string query =
                @"WITH tempEncounters as (
                     SELECT encounter.* 
                    	, ROW_NUMBER() OVER(partition by pa.Id, py.Id order by pa.Id, py.Id) AS ROWNO 
                        , pa.Id 
                        , pa.firstname as firstName 
                        , pa.lastname as lastName 
                        , pa.age 
                        , py.Id 
                        , py.payer_name as CompanyName 
                        , py.city as CompanyCity 
                        , f.Id 
                        , f.facility_name as BranchName 
                        , f.city as BranchCity 
                     FROM encounter 
                     INNER JOIN payer as py on encounter.payer_id = py.Id 
                     INNER JOIN patient as pa on encounter.patient_id = pa.Id 
                     INNER JOIN facility as f on encounter.facility_id = f.Id
                )
                 
                SELECT * FROM tempEncounters AS results 
                JOIN (SELECT patient_id, payer_id, MAX(rowno) as maxVal 
                       FROM tempEncounters group by patient_id, payer_id) AS values 
                ON results.patient_id=values.patient_id
                AND results.payer_id=values.payer_id AND results.rowno=values.maxVal 
                ORDER BY rowno;";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<Encounter, Patient, Payer, Facility, Encounter>(query,
                (encounter, patient, payer, facility) =>
                {
                    encounter.Patient = patient;
                    encounter.Payer = payer;
                    encounter.Facility = facility;
                    return encounter;
                }).ConfigureAwait(false);

            return result.ToList();
        }
    }
}
