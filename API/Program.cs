using API.Services.Implements;
using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "V1",
        Title = "Gym management API document",
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//Add dbContext
builder.Services.AddDbContext<GymManagementSystemDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("GymConnection"));
});

//Add services
builder.Services.AddScoped<Branch_Int, Branch_Imp>();
builder.Services.AddScoped<Equipment_Int, Equipment_Imp>();
builder.Services.AddScoped<Account_Int, Account_Imp>();
builder.Services.AddScoped<Salary_Int, Salary_Imp>();
builder.Services.AddScoped<WorkingCheck_Int, WorkingCheck_Imp>();
builder.Services.AddScoped<EmployeeSalary_Int, EmployeeSalary_Imp>();
builder.Services.AddScoped<Customer_Int, Customer_Imp>();
builder.Services.AddScoped<Fine_Int, Fine_Imp>();
builder.Services.AddScoped<CustomersVoucher_Int, CustomerVoucher_Imp>();
builder.Services.AddScoped<ServicePackage_Int, ServicePackage_Imp>();
builder.Services.AddScoped<Role_Int, Role_Imp>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
