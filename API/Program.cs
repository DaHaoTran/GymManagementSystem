using API.Services.Implements;
using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

// Config JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Bật kiểm tra Issuer
        ValidateAudience = true, // Bật kiểm tra Audience
        ValidateLifetime = true, // Bật kiểm tra thời gian hết hạn
        ValidateIssuerSigningKey = true, // Bật kiểm tra chữ ký (SecretKey)

        ValidIssuer = jwtSettings["Issuer"], // Issuer mong đợi
        ValidAudience = jwtSettings["Audience"], // Audience mong đợi
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), // SecretKey để xác thực
        ClockSkew = TimeSpan.Zero // Yêu cầu thời gian chính xác cho việc xác thực token
    };
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

app.UseAuthentication();
app.UseAuthorization();

app.Run();
