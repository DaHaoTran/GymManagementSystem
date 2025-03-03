using Client_FAU.Business.Implements;
using Client_FAU.Business.Interfaces;
using Client_FAU.Components;
using Microsoft.AspNetCore.ResponseCompression;
using Radzen;
using SweetAlert2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();
builder.Services.AddRadzenComponents();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSweetAlert2();

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
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseSession();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
