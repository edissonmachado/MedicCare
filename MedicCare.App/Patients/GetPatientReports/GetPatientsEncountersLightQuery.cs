using System.Collections.Generic;
using MediatR;

namespace MedicCare.App.Patients.GetPatientReports
{
    public class GetPatientsEncountersLightQuery : IRequest<List<EncounterReportResult>>
    {
    }
}
