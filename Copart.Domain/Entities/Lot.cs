namespace Copart.Domain.Entities
{
    public class Lot
    {
        public int Id { get; set; }
        public string LotNumber { get; set; } = default!;
        public Vehicle Vehicle { get; set; } = default!;
        public decimal MinimalBid { get; set; }
        public decimal CurrentBid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
