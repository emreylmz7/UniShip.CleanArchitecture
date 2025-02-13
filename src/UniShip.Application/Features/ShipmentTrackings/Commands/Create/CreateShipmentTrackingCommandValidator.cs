using FluentValidation;

namespace UniShip.Application.Features.ShipmentTrackings.Commands.Create;
public sealed class ShipmentTrackingCreateCommandValidator : AbstractValidator<CreateShipmentTrackingCommand>
{
    public ShipmentTrackingCreateCommandValidator()
    {
        RuleFor(x => x.ShipmentId)
            .NotEmpty().WithMessage("Shipment ID is required.");

        RuleFor(x => x.DateTime)
            .NotEmpty().WithMessage("Date and time is required.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.Description != null);

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid shipment status.");
    }
} 