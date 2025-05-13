using Copart.UI.Models.VehicleModels;

namespace Copart.UI.Models.LotModels
{
    public class LotAddModel
    {
        public VehicleAddModel Vehicle { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
