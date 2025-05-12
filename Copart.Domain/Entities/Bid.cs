namespace Copart.Domain.Entities
{
    public class Bid
    {
        public int Id { get; set; }

        public int LotId { get; set; }

        public Lot Lot { get; set; } = default!;

        public int UserId { get; set; }

        public User User { get; set; } = default!;

        public int Amount { get; set; }
    }
}
