namespace UniShip.WebAPI.Modules;

public static class RoutesRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder app)
    {
        app.RegisterBranchRoutes();
        app.RegisterAuthRoutes();
        app.RegisterCustomerRoutes();
        app.RegisterShipmentRoutes();
        app.RegisterShipmentTrackingRoutes();
        app.RegisterVehicleRoutes();
    }   
}
