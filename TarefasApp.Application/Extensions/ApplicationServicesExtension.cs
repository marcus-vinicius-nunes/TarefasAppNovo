using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Application.Data;
using TarefasApp.Application.Interfaces;
using TarefasApp.Application.Services;

namespace TarefasApp.Application.Extensions
{
    /// <summary>
    /// Classe de extensão para configurar os serviços
    /// da camada de aplicação do sistema
    /// </summary>
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices
       (this IServiceCollection services)
        {
            //configurando o MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(TarefaAppService).Assembly);
            });

            //configurando o AutoMapper
            services.AddAutoMapper(typeof(TarefaAppService).Assembly);


            //registrando as interfaces/classes de serviço da aplicação
            services.AddTransient<ITarefaAppService, TarefaAppService>();

            return services;
        }
    }
}