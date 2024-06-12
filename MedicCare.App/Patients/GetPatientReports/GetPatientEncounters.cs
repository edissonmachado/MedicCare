using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MedicCare.Domain;

namespace MedicCare.App.Patients.GetPatientReports
{
    public class GetPatientEncountersQueryHandler : IRequestHandler<GetPatientsEncountersQuery, List<Encounter>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientEncountersQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<Encounter>> Handle(GetPatientsEncountersQuery request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetPatientReports().ConfigureAwait(false);
        }
    }

    public class GetPatientsEncountersQuery : IRequest<List<Encounter>>
    {
    }
}
