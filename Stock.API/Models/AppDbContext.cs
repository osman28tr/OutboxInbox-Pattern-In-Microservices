using Microsoft.EntityFrameworkCore;
using Stock.API.Models.Entities;

namespace Stock.API.Models
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<OrderInbox> OrderInboxs { get; set; }
	}
}
