using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using System.Resources;
using testandoBancodDo0.Context;
using testandoBancodDo0.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Adicionar os serviços necessários
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("pt-BR") };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddSingleton(sp =>
    new ResourceManager("testandoBancodDo0.Resources.ResourceMensagensErro", Assembly.GetExecutingAssembly()));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ValidarImagemController>();

// Adicionar o serviço de sessão
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = System.TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Adicionar o DbContext
builder.Services.AddDbContext<AproveDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Adicionar o uso do serviço de sessão
app.UseSession();

// Configurar localização
var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(locOptions);

app.MapControllerRoute(
    name: "Cadastro",
    pattern: "{controller=Cadastro}/{action=Cadastrar}/{id?}");

app.MapControllerRoute(
    name: "Cadastro",
    pattern: "{controller=Cadastro}/{action=SolicitarReceita}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Site}/{action=PrincipalHome}/{id?}");

app.Run();
