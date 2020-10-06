using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TranslateApp.Common;

namespace TranslateApp.Data
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TranslateAppDataContext>
	{
		public TranslateAppDataContext CreateDbContext(string[] args)
		{

			var builder = new DbContextOptionsBuilder<TranslateAppDataContext>();
			var connectionString = ConstDbString.ConnectionStringDb;
			builder.UseSqlServer(connectionString);
			return new TranslateAppDataContext(builder.Options);
		}
	}
}
