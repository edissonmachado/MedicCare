using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MedicCare.Domain;

namespace MedicCare.App.Patients.GetPatientReports
{
    public class GetPatientEncountersQueryHandler : IRequestHandler<GetPatientsEncountersQuery, List<ReportRecord>>
    {
        private const int CategoryAgeLimit = 16;

        private readonly IPatientRepository _patientRepository;

        public GetPatientEncountersQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<ReportRecord>> Handle(GetPatientsEncountersQuery request, CancellationToken cancellationToken)
        {
            var encounters = await _patientRepository.GetEncountersAsync().ConfigureAwait(false);
            var response = FormatResponse(encounters);
            return response;
        }

        private List<ReportRecord> FormatResponse(IEnumerable<Encounter> encounters)
        {
            var records = new Dictionary<int, ReportRecord>();

            foreach(var encounter in encounters)
            {
                if(records.TryGetValue(encounter.Patient.Id, out var record))
                {
                    if (!record.Cities.Contains(encounter.Payer.CompanyCity))
                    {
                        if(!record.Cities.Contains(encounter.Payer.CompanyCity))
                            record.Cities.Add(encounter.Payer.CompanyCity);
                    }
                }
                else
                {
                    var newRecord = new ReportRecord()
                    {
                        Name = $"{encounter.Patient.LastName}, {encounter.Patient.FirstName}",
                        Cities = new List<string> { encounter.Payer.CompanyCity },
                        Category = encounter.Patient.Age < CategoryAgeLimit ? 'A' : 'B',
                    };
                    records.Add(encounter.Patient.Id, newRecord);
                }    
            }

            records = records.Where(kv => kv.Value.Cities.Count > 1).ToDictionary(kv => kv.Key, kv => kv.Value);

            return records.Values.ToList();
        }
    }
}
