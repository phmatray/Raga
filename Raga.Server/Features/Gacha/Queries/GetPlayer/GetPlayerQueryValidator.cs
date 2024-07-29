using FluentValidation;

namespace Raga.Server.Features.Gacha.Queries.GetPlayer;

public class GetPlayerQueryValidator : AbstractValidator<GetPlayerQuery>
{
    public GetPlayerQueryValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty().WithMessage("PlayerId is required.");
    }
}