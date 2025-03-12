using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class CreateAvisoHandler : IRequestHandler<CreateAvisoRequest, IOperationResult<GetAvisosResponse>>
    {
        private readonly IServiceProvider _serviceProvider;

        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public CreateAvisoHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        public async Task<IOperationResult<GetAvisosResponse>> Handle(CreateAvisoRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Entering Handle method in CreateAvisoHandler");

            var aviso = new AvisoEntity
            {
                Titulo = request.Titulo,
                Mensagem = request.Mensagem,
                DataCriacao = DateTime.UtcNow
            };

            Console.WriteLine("Creating a new AvisoEntity with Title: {0}", aviso.Titulo);

            await _avisoRepository.CreateAsync(aviso, cancellationToken);

            Console.WriteLine("AvisoEntity created successfully with ID: {0}", aviso.Id);

            return OperationResult<GetAvisosResponse>.ReturnOk((GetAvisosResponse)aviso);
        }
    }
}
