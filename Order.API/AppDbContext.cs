using Microsoft.EntityFrameworkCore;
using Order.API.Models.Entities;

namespace Order.API
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		public DbSet<Models.Entities.Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<OrderOutbox> OrderOutboxes { get; set; }
	}
}
