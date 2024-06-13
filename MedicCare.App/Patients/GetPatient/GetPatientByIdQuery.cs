using MediatR;
using MedicCare.Domain;

namespace MedicCare.App.Patients.GetPatient
{
    public class GetPatientByIdQuery : IRequest<Patient>
    {
        public int Id {  get; set; }
    }
}
