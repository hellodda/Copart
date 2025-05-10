using Copart.Domain.Enums;

namespace Copart.BLL.Models.VehicleModels
{
    public class VehicleUpdateModel
    {
        public DamageType Damage { get; set; } = default!;
    }
}
