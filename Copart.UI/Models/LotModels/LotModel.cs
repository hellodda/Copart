using Copart.UI.Models.VehicleModels;

namespace Copart.UI.Models.LotModels
{
    public class LotModel
    {
        public int Id { get; set; }
        public string LotNumber { get; set; } = default!;
        public VehicleModels.VehicleModel Vehicle { get; set; } = default!;
        public decimal MinimalBid { get; set; }
        public decimal CurrentBid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
