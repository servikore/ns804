//using Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SQLiteRepository
//{
//	public class SQLiteDbContext : DbContext
//	{
//		public DbSet<User> Users { get; set; }
//		protected override void OnConfiguring(DbContextOptions builder)
//		{
//			builder.UseSQLite("Filename=ns804.sqlite;");
//		}

//		protected override void OnModelCreating(ModelBuilder builder)
//		{
//			builder.Entity<User>().Key(m => m.Id);
//			base.OnModelCreating(builder);
//		}
//	}
//}