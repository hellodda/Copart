using Copart.UI.Models.Enums;

namespace Copart.UI.Models.VehicleModels
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Vin { get; set; } = default!;
        public string Make { get; set; } = default!;
        public string Model { get; set; } = default!;
        public DamageType Damage { get; set; }
        public VehicleType Type { get; set; }
    }
}
