using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using testandoBancodDo0.Context;
using testandoBancodDo0.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Adicionar os servi�os necess�rios
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AproveDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Adicionar o servi�o de sess�o



builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = System.TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



//Servi�os de ReCaptcha
/*builder.Services.AddRecaptcha(new RecaptchaOptions
{
    SiteKey = "chave",
    SecretKey = "segredo"
});*/


// Adicionar o HomeController
//builder.Services.AddControllersWithViews().AddApplicationPart(typeof(HomeController).Assembly);

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

// Adicionar o uso do servi�o de sess�o
app.UseSession();

app.MapControllerRoute(
    name: "Cadastro",
    pattern: "{controller=Cadastro}/{action=Cadastrar}/{id?}");

app.MapControllerRoute(
              name: "SolicitacaoReceita",
              pattern: "{controller=SolicitacaoReceita}/{action=SolicitarReceita}/{id?}");

//esse mapController � para chamar a a��o de autenticar login no banco de dados (est� funcionando sem precisas usar essa rota.)
/* app.MapControllerRoute(
     name: "Autenticar",
     pattern: "{controller=AutenticaLogin}/{action=Login}/{id?}");*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Site}/{action=SobreNos}/{id?}");

app.Run();
