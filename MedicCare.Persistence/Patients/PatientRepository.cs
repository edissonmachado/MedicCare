using Dapper;
using MedicCare.App.Patients;
using MedicCare.App.Patients.GetPatientReports;
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

        public async Task<List<Encounter>> GetEncountersAsync()
        {
            string query =
                @"WITH tempEncounters AS (
                     SELECT encounter.* 
                    	, ROW_NUMBER() OVER(partition by pa.Id, py.Id order by pa.Id, py.Id) AS ROWNO 
                        , pa.Id 
                        , pa.firstname AS firstName 
                        , pa.lastname AS lastName 
                        , pa.age 
                        , py.Id 
                        , py.payer_name AS CompanyName 
                        , py.city AS CompanyCity 
                        , f.Id 
                        , f.facility_name AS BranchName 
                        , f.city AS BranchCity 
                     FROM encounter 
                     INNER JOIN payer AS py ON encounter.payer_id = py.Id 
                     INNER JOIN patient AS pa ON encounter.patient_id = pa.Id 
                     INNER JOIN facility AS f ON encounter.facility_id = f.Id
                )
                 
                SELECT * FROM tempEncounters AS results 
                JOIN (SELECT patient_id, payer_id, MAX(rowno) AS maxVal 
                       FROM tempEncounters group by patient_id, payer_id) AS values 
                ON results.patient_id = values.patient_id
                AND results.payer_id = values.payer_id AND results.rowno=values.maxVal 
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

        public async Task<List<EncounterReport>> GetEncountersLightAsync()
        {
            string query =
                @"WITH tempEncounters AS (
	                SELECT pa.firstname AS firstName 
		                , pa.lastname AS lastName 
		                , pa.age 
		                , py.city AS CompanyCity 
	                FROM encounter 
	                INNER JOIN payer AS py ON encounter.payer_id = py.Id 
	                INNER JOIN patient AS pa ON encounter.patient_id = pa.Id 
	                GROUP BY firstname, lastname, age, companycity
                )

                SELECT firstname, lastname, age, string_agg(companycity, ', ') AS Cities
	                FROM tempEncounters AS results
	                GROUP BY firstname, lastname, age
                HAVING string_agg(companycity, ', ') LIKE '%,%';";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EncounterReport>(query).ConfigureAwait(false);

            if (result == null) return new List<EncounterReport>();

            return result.ToList();
        }
    }
}
