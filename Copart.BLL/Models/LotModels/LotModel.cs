using Copart.BLL.Models.VehicleModels;

namespace Copart.BLL.Models.LotModels
{
    public class LotModel
    {
        public int Id { get; set; }
        public string LotNumber { get; set; } = default!;
        public VehicleModel Vehicle { get; set; } = default!;
        public decimal MinimalBid { get; set; }
        public decimal CurrentBid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
