using Copart.BLL.Models.BidModels;

namespace Copart.BLL.Models.UserModels
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

    }
}