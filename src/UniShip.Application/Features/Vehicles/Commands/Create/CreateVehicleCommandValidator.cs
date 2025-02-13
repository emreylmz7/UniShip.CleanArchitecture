using FluentValidation;

namespace UniShip.Application.Features.Vehicles.Commands.Create;
public sealed class VehicleCreateCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public VehicleCreateCommandValidator()
    {
        RuleFor(x => x.PlateNumber)
            .NotEmpty().WithMessage("Plate number is required.")
            .MaximumLength(20).WithMessage("Plate number cannot exceed 20 characters.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required.")
            .MaximumLength(100).WithMessage("Model cannot exceed 100 characters.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid vehicle type.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("Branch ID is required.");
    }
} 