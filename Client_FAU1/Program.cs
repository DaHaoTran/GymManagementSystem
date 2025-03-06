using Client_FAU1.Business.Implements;
using Client_FAU1.Business.Interfaces;
using Client_FAU1.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//
builder.Services.AddScoped<Branch_Int, Branch_Imp>();
builder.Services.AddScoped<Salary_Int, Salary_Imp>();
builder.Services.AddScoped<ServicePackage_Int, ServicePackage_Imp>();
builder.Services.AddScoped<Account_Int, Account_Imp>();
builder.Services.AddScoped<Role_Int, Role_Imp>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
