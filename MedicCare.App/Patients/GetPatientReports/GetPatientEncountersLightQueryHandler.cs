using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MedicCare.App.Patients.GetPatientReports
{
    public class GetPatientEncountersLightQueryHandler : IRequestHandler<GetPatientsEncountersLightQuery, List<EncounterReportResult>>
    {
        private const int CategoryAgeLimit = 16;

        private readonly IPatientRepository _patientRepository;

        public GetPatientEncountersLightQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<EncounterReportResult>> Handle(GetPatientsEncountersLightQuery request, CancellationToken cancellationToken)
        {
            var encounters = await _patientRepository.GetEncountersLightAsync().ConfigureAwait(false);
            var response = FormatResponse(encounters);
            return response;
        }

        private List<EncounterReportResult> FormatResponse(IEnumerable<EncounterReport> encounters)
        {
            var result = new List<EncounterReportResult>();
            foreach (var encounter in encounters)
            {
                result.Add(new EncounterReportResult
                {
                    Name = $"{encounter.LastName}, {encounter.FirstName}",
                    Cities = encounter.Cities,
                    Category = encounter.Age < CategoryAgeLimit ? 'A' : 'B',
                });
            }

            return result;
        }
    }
}
