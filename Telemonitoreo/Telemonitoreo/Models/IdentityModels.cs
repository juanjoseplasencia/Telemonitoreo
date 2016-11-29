using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Telemonitoreo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //public string Nombres { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            var dbUsuarios = new DataAccess.TelemonitoreoEntities().Usuarios;
            string userIdentityId = userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var usuarioLogeado = dbUsuarios.FirstOrDefault(u => u.Id == userIdentityId && !u.Eliminado.Value);
            if (usuarioLogeado != null) {
                userIdentity.AddClaim(new Claim("ApellidosNombres", string.Concat(usuarioLogeado.Nombres, " ", 
                                                            usuarioLogeado.APaterno, " ", 
                                                            usuarioLogeado.AMaterno)));
                userIdentity.AddClaim(new Claim("EstablecimientoIdRestriccion", 
                    manager.IsInRole<ApplicationUser,string>(this.Id, "Personal de salud") 
                    ? usuarioLogeado.EstablecimientoId.ToString()
                    : string.Empty));
            }
            dbUsuarios = null;
            // Add custom user claims here
            //userIdentity.AddClaim(new Claim("Nombres", this.Nombres));
            return userIdentity;
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}