using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class DeleteAvisoHandler : IRequestHandler<DeleteAvisoRequest, IOperationResult<object>>
    {
        private readonly IServiceProvider _serviceProvider;
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public DeleteAvisoHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<object>> Handle(DeleteAvisoRequest request, CancellationToken cancellationToken)
        {
            var aviso = await _avisoRepository.FindAsync(request.Id, cancellationToken);
            if (aviso == null || aviso.IsDeleted)
                return OperationResult<object>.ReturnNotFound();

            aviso.IsDeleted = true;
            aviso.Ativo = false;
            aviso.DataEdicao = DateTime.UtcNow;

            await _avisoRepository.DeleteAsync(aviso, cancellationToken);
            return OperationResult<object>.ReturnNoContent();
        }
    }
}
