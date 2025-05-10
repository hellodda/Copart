using Copart.BLL.Models.VehicleModels;

namespace Copart.BLL.Models.LotModels
{
    public class LotAddModel
    {
        public VehicleModel Vehicle { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
