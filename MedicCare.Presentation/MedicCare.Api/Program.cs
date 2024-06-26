using MedicCare.Api.ConfigServices;
using MedicCare.App.Common;
using MedicCare.App.Patients;
using MedicCare.Persistence.Common;
using MedicCare.Persistence.Patients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg
    => cfg.RegisterServicesFromAssembly(System.Reflection.Assembly.Load("MedicCare.App")));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IPatientRepository, PatientRepository>();

var connectionString = builder.Configuration.GetConnectionString("MedicCareDb");
builder.Services.AddSingleton<IDbContext>(new DapperContext(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
