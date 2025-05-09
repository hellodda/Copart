using Copart.Domain.Enums;

namespace Copart.BLL.Models
{
    public class VehicleUpdateModel
    {
        public DamageType Damage { get; set; } = default!;
    }
}
