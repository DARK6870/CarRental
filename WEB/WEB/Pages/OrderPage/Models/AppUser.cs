using Microsoft.AspNetCore.Identity;

namespace WEB.Pages.OrderPage.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
    }
}
