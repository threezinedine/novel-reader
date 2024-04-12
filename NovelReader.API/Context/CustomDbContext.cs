using Microsoft.EntityFrameworkCore;
using NovelReader.API.Models;
using NovelReader.Common.ModelDtos.User;

namespace NovelReader.API.Context
{
	public class CustomDbContext : DbContext
	{
		public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Novel> Novels { get; set; }
		public DbSet<Chapter> Chapters { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Rate> Rates { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<UserNovel> UserNovels { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Novel>()
				.HasOne(novel => novel.Author)
				.WithMany()
				.HasForeignKey(novel => novel.AuthorId);

			modelBuilder.Entity<UserNovel>()
				.HasKey(un => new { un.UserId, un.NovelId });

			modelBuilder.Entity<User>()
				.HasMany(user => user.OwnedNovels)
				.WithOne(novel => novel.Author)
				.HasForeignKey(novel => novel.AuthorId);

			modelBuilder.Entity<Tag>().HasData(
				new Tag { Id = Guid.NewGuid().ToString() , Name = "Action" },
				new Tag { Id = Guid.NewGuid().ToString() , Name = "Adventure" },
				new Tag { Id = Guid.NewGuid().ToString() , Name = "Comedy" },
				new Tag { Id = Guid.NewGuid().ToString() , Name = "Drama" }
			);
		}
	}
}
