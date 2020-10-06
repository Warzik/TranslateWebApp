using Microsoft.EntityFrameworkCore;
using TranslateApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TranslateApp.Data
{
	public class TranslateAppDataContext : DbContext
	{
		public TranslateAppDataContext(DbContextOptions<TranslateAppDataContext> conn) : base(conn)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		//entities
		public DbSet<ProjectDAL> Projects { get; set; }
		public DbSet<TranslateDAL> Translate { get; set; }
	}
}
