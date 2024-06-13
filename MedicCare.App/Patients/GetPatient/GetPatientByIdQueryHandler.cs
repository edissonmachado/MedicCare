using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MedicCare.Domain;

namespace MedicCare.App.Patients.GetPatient
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Patient>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Patient> Handle(GetPatientByIdQuery query, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetByIdAsync(query.Id).ConfigureAwait(false);
        }
    }
}
