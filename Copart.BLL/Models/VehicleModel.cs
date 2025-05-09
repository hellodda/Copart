using Copart.Domain.Enums;

namespace Copart.BLL.Models
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
