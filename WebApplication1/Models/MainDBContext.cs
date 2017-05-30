using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using App.Model;

namespace WebApplication1.Models
{
    public class MainDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
    }

}