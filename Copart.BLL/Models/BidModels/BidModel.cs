using Copart.BLL.Models.LotModels;
using Copart.BLL.Models.UserModels;

namespace Copart.BLL.Models.BidModels
{
    public class BidModel
    {
        public int Id { get; set; }

        public int LotId { get; set; }

        public int UserId { get; set; }

        public int Amount { get; set; }
    }
}
