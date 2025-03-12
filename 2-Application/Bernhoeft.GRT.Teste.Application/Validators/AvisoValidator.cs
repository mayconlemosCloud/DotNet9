using FluentValidation;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;

public class AvisoValidator : AbstractValidator<GetAvisosRequest>
{
    public AvisoValidator()
    {

    }
}

public class CreateAvisoValidator : AbstractValidator<CreateAvisoRequest>
{
    public CreateAvisoValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty().WithMessage("Título não pode ser vazio.");
        RuleFor(x => x.Mensagem).NotEmpty().WithMessage("Mensagem não pode ser vazia.");
    }
}

public class UpdateAvisoValidator : AbstractValidator<UpdateAvisoRequest>
{
    public UpdateAvisoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id deve ser maior que zero.");
        RuleFor(x => x.Mensagem).NotEmpty().WithMessage("Mensagem não pode ser vazia.");
    }
}

public class DeleteAvisoValidator : AbstractValidator<DeleteAvisoRequest>
{
    public DeleteAvisoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id deve ser maior que zero.");
    }
}
