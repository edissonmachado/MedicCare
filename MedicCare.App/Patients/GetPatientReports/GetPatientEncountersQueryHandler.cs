using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MedicCare.App.Patients.GetPatientReports
{
    public class GetPatientEncountersQueryHandler : IRequestHandler<GetPatientsEncountersQuery, List<EncounterReportResult>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientEncountersQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<EncounterReportResult>> Handle(GetPatientsEncountersQuery request, CancellationToken cancellationToken)
        {
            var encounters = await _patientRepository.GetEncountersAsync().ConfigureAwait(false);
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
                    Name = encounter.Name,
                    Cities = encounter.Cities,
                    Category = encounter.Category,
                });
            }

            return result;
        }
    }
}
