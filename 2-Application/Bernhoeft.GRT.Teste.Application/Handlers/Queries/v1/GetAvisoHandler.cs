using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisoHandler : IRequestHandler<GetAvisoRequest, IOperationResult<GetAvisosResponse>>
    {
        private readonly IAvisoRepository _avisoRepository;

        public GetAvisoHandler(IAvisoRepository avisoRepository)
        {
            _avisoRepository = avisoRepository;
        }

        public async Task<IOperationResult<GetAvisosResponse>> Handle(GetAvisoRequest request, CancellationToken cancellationToken)
        {
            var aviso = await _avisoRepository.FindAsync(request.Id, cancellationToken);
            if (aviso == null)
                return OperationResult<GetAvisosResponse>.ReturnNotFound();

            return OperationResult<GetAvisosResponse>.ReturnOk((GetAvisosResponse)aviso);
        }
    }
}
