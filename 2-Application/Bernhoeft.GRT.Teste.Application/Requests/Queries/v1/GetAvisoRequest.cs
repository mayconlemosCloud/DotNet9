using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1
{
    public class GetAvisoRequest : IRequest<IOperationResult<GetAvisosResponse>>
    {
        public int Id { get; set; }
    }

    public class GetAvisoRequestValidator : AbstractValidator<GetAvisoRequest>
    {
        public GetAvisoRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id deve ser maior que zero.");
        }
    }
}
