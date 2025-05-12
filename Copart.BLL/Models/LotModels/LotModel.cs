using Copart.BLL.Models.BidModels;
using Copart.BLL.Models.VehicleModels;

namespace Copart.BLL.Models.LotModels
{
    public class LotModel
    {
        public int Id { get; set; }
        public string LotNumber { get; set; } = default!;
        public VehicleModel Vehicle { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<BidModel> Bids { get; set; } = new List<BidModel>();
    }
}
