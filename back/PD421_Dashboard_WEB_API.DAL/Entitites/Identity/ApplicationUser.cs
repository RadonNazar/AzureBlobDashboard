using Microsoft.AspNetCore.Identity;

namespace PD421_Dashboard_WEB_API.DAL.Entitites.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<ApplicationUserClaim> Claims { get; set; } = [];
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; } = [];
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; } = [];
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
    }
}
