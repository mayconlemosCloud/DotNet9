using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class UpdateAvisoHandler : IRequestHandler<UpdateAvisoRequest, IOperationResult<GetAvisosResponse>>
    {
        private readonly IServiceProvider _serviceProvider;
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public UpdateAvisoHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<GetAvisosResponse>> Handle(UpdateAvisoRequest request, CancellationToken cancellationToken)
        {
            var aviso = await _avisoRepository.FindAsync(request.Id, cancellationToken);
            if (aviso == null || aviso.IsDeleted)
                return OperationResult<GetAvisosResponse>.ReturnNotFound();

            aviso.Mensagem = request.Mensagem;
            aviso.DataEdicao = DateTime.UtcNow;

            await _avisoRepository.UpdateAsync(aviso, cancellationToken);
            return OperationResult<GetAvisosResponse>.ReturnOk((GetAvisosResponse)aviso);
        }
    }
}
