using FluentValidation;

namespace Raga.Server.Features.Gacha.Queries.GetPlayerStats;

public class GetPlayerStatsQueryValidator : AbstractValidator<GetPlayerStatsQuery>
{
    public GetPlayerStatsQueryValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty().WithMessage("PlayerId is required.");
    }
}