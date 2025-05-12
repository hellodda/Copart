using Copart.BLL.Models.LotModels;

namespace Copart.BLL.Models.BidModels
{
    public class BidModel
    {
        public int Id { get; set; }

        public int LotId { get; set; }

        public LotModel Lot { get; set; } = default!;

        public int UserId { get; set; }

        public UserModel User { get; set; } = default!;

        public int Amount { get; set; }
    }
}
