using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using testandoBancodDo0.Context;

namespace testandoBancodDo0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //parte da conexao com banco / register class AproveDbcontext
            builder.Services.AddDbContext<AproveDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //era pra esse mapController ligar na ação de cadastrar no Controller de cadastro,att(esta cadastrando).
            app.MapControllerRoute(
                name: "Cadastro",
                pattern: "{controller=Cadastro}/{action=Cadastrar}/{id?}");

            app.MapControllerRoute(
                name: "Cadastro",
                pattern: "{controller=Cadastro}/{action=SolicitarReceita}/{id?}");

            //esse mapController é para chamar a ação de autenticar login no banco de dados (está funcionando sem precisas usar essa rota.)
            /* app.MapControllerRoute(
                 name: "Autenticar",
                 pattern: "{controller=AutenticaLogin}/{action=Login}/{id?}");*/

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Site}/{action=Home}/{id?}");
                 

            app.Run();
        }
    }
}