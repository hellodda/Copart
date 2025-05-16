namespace Copart.Domain.Entities
{
    public class Lot
    {
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;

        public string LotNumber { get; set; } = default!;

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; } = default!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }
}
