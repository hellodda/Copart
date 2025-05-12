namespace Copart.Domain.Entities
{
    public class Bid
    {
        public int Id { get; set; }

        public int AuctionId { get; set; }

        public int UserId { get; set; }

        public int Amount { get; set; }
    }
}
