using System;
using System.Collections.Generic;
using MediatR;

namespace MedicCare.App.Patients.GetPatientReports
{
    public class GetPatientsEncountersQuery : IRequest<List<ReportRecord>>
    {
    }
}
