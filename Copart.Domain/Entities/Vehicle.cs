using Copart.Domain.Enums;

namespace Copart.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        public VehicleType Type { get; set; }

        public DamageType Damage { get; set; }

        public string Vin { get; set; } = default!;

        public string Make { get; set; } = default!;

        public string Model { get; set; } = default!;

        public DateTime Year { get; set; }
    }
}
