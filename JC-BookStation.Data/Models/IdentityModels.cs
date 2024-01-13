using Microsoft.AspNet.Identity.EntityFramework;

namespace JC_BookStation.Data.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public override string Email { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Entities")
        {
        }
    }
}