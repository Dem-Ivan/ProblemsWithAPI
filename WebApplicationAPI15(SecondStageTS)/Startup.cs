using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebApplicationAPI15_SecondStageTS_.Models;

namespace WebApplicationAPI15_SecondStageTS_
{
	public class Startup
	{	
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; set; }
		public void ConfigureServices(IServiceCollection services)
		{
			string connection = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
			services.AddControllers();

			services.AddSwaggerGen(c => c.SwaggerDoc("v1",new OpenApiInfo { Title = "My API", Version = "v1"}));
		}
		public void Configure(IApplicationBuilder app)
		{
			app.UseDeveloperExceptionPage();
			// Enable middleware to serve generated Swagger as a JSON endpoint
			app.UseSwagger();

			// Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
			app.UseSwaggerUI(c=>
			{
				c.SwaggerEndpoint(url:"/swagger/v1/swagger.json", name:"My API V1");
			});

			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
