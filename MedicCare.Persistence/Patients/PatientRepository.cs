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
        private const int CategoryAgeLimit = 16;

        public PatientRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EncounterReport>> GetEncountersAsync()
        {
            string query =
                @$"WITH temp1 AS (
                    SELECT firstname, lastname, age, city
	                    , (SELECT COUNT(DISTINCT payer_id) FROM encounter WHERE encounter.patient_id = pa.id) AS visits
                    FROM patient AS pa
	                    INNER JOIN encounter ON encounter.patient_id = pa.id
	                    INNER JOIN payer AS py ON encounter.payer_id = py.id
                    ORDER BY visits
	                    ),
                    temp2 AS (
                    SELECT firstname, lastname, age, city, visits FROM temp1
                    WHERE visits > 1
                    GROUP BY firstname, lastname, age, city, visits
                    )

                    SELECT lastname || ', ' || firstname AS name
	                    , CASE WHEN age<{CategoryAgeLimit} THEN 'A' ELSE 'B' END AS Category
	                    , string_agg(city, ', ') AS Cities FROM temp2 
                    GROUP BY firstname, lastname, age, visits
                    ORDER BY visits;";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EncounterReport>(query).ConfigureAwait(false);

            if (result == null) return new List<EncounterReport>();

            return result.ToList();
        }
    }
}
