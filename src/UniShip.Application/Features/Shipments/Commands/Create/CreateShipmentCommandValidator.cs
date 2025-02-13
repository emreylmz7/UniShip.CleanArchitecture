using FluentValidation;

namespace UniShip.Application.Features.Shipments.Commands.Create;
public sealed class ShipmentCreateCommandValidator : AbstractValidator<CreateShipmentCommand>
{
    public ShipmentCreateCommandValidator()
    {
        RuleFor(x => x.TrackingNumber)
            .NotEmpty().WithMessage("Tracking number is required.")
            .MaximumLength(50).WithMessage("Tracking number cannot exceed 50 characters.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Sender ID is required.");

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid shipment status.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.Description != null);

        RuleFor(x => x.ReceiverName)
            .NotEmpty().WithMessage("Receiver name is required.")
            .MaximumLength(100).WithMessage("Receiver name cannot exceed 100 characters.");

        RuleFor(x => x.ReceiverAddress)
            .NotEmpty().WithMessage("Receiver address is required.")
            .MaximumLength(200).WithMessage("Receiver address cannot exceed 200 characters.");

        RuleFor(x => x.ReceiverPhone)
            .NotEmpty().WithMessage("Receiver phone is required.")
            .MaximumLength(20).WithMessage("Receiver phone cannot exceed 20 characters.");
    }
} 