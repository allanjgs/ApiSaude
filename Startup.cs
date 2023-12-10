using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ApiAuthSaude
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration {get;}
        // Este método é chamado pelo tempo de execução. Use este método para adicionar serviços ao container.

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddControllersWithViews();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            services.AddAuthentication(x=>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer (x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


        }

        //Este método é chamado pelo tempo de execução. Use este método para configurar o pipeline de solicitação HTTP. 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                var exceptionHandlerOptions = new ExceptionHandlerOptions
                {
                    ExceptionHandlingPath = "/Home/Error"
                    // Outras configurações, se necessário
                };

                app.UseExceptionHandler(exceptionHandlerOptions);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default", 
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}