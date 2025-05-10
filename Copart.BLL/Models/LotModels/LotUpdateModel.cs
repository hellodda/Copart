namespace Copart.BLL.Models.LotModels
{
    public class LotUpdateModel
    {
        public decimal MinimalBid { get; set; }
        public decimal CurrentBid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
