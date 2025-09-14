using Microsoft.OpenApi.Models;
using System.Reflection;

namespace TarefasApp.API.Extensions
{
    /// <summary>
    /// Classe de extensao para configurar swagger (OPEN API)
    /// </summary>
    public static class SwaggerDocExtension
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "TarefasApp - Treinamento C# Avançado Formação Arquiteto",
                        Description = "API para controle de tarefas de usuarios.",
                        Version = "1.0",
                        Contact = new OpenApiContact
                        {
                            Name = "Marcus Silva",
                            Email = "vinicius_rj87@yahoo.com.br",
                            Url = new Uri("https://github.com/marcus-vinicius-nunes/")
                        }
                    });

                    //configuração para incluir os comentários na documentação
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);

                }
                );

            return services;
        }

        /// <summary>
        /// Método para configurar a execução do Swagger
        /// </summary>
        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TarefasApp");
            });

            return app;
        }

    }
}
