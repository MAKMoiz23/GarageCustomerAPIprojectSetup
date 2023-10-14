using BAL.Services.IService;
using BAL.Services.Service;
using DAL.DBAccess.Data;
using DAL.DBAccess.IData;
using DAL.Repositories.IRepository;
using DAL.Repositories.Repository;
using GarageCustomerAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region AddCustomServices
builder.Services.AddScoped<IGenericCrudRepository, GenericCrudRepository>();
builder.Services.AddScoped<IGarageData, GarageData>();
builder.Services.AddScoped<IGarageCustomerData, GarageCustomerData>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region ConfiguerMiddlewarePipeline
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();