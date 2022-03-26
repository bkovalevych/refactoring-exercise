using System.Reflection;

namespace Domain.Vehicles
{
    public static class VehicleFactory
    {
        private readonly static Dictionary<string, Func<IVehicle>> vehicles;
        static VehicleFactory()
        {
            vehicles = Assembly.GetAssembly(typeof(VehicleFactory))
                .GetTypes()
                .Where(t => !t.IsAbstract && t.IsClass && typeof(IVehicle).IsAssignableFrom(t))
                .ToDictionary<Type, string, Func<IVehicle>>(
                keySelector: t => t.Name.ToLower(),
                elementSelector: t => () => Activator.CreateInstance(t) as IVehicle);
        }

        public static IVehicle? GetInstance(string name)
        {
            vehicles.TryGetValue(name.ToLower(), out var vehicle);
            return vehicle != null
                ? vehicle()
                : null;
        }
    }
}
