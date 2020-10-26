using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Buddy.Api.Authentication;
using Buddy.Api.Exceptions;
using Buddy.Data;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleInjector;

namespace Buddy.Api
{
	public class Startup
	{
		const string CorsPolicyConfiguration = "AppCORSPolicy";

		private Container container = new Container();

		public Startup(IConfiguration configuration)
		{
			container.Options.ResolveUnregisteredConcreteTypes = false;
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
			services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
			services.AddDbContext<BuddyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("buddy")));

			services.AddOData();
			services.AddControllers();
			services.AddHttpContextAccessor();


			services.AddCors(o => o.AddPolicy(CorsPolicyConfiguration, builder =>
			{
				builder.AllowAnyOrigin()
							 .AllowAnyMethod()
							 .AllowAnyHeader();
			}));
			
			services.AddSimpleInjector(container, options =>
			{
				options.AddAspNetCore()
						.AddControllerActivation();

				options.AddLogging();
			});

			container.Initialize();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseExceptionHandler(appBuilder =>
			{
				appBuilder.Use(async (context, next) =>
				{
					var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
					if (error?.Error != null)
					{
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						context.Response.ContentType = "application/json";

						var response = error.Error.CreateODataError(env.IsDevelopment());
						await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
					}

					// when no error, do next.
					else await next();
				});
			});


			app.UseCors(CorsPolicyConfiguration);
			app.UseODataBatching();
			app.UseRouting();
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.Select().Filter().OrderBy().Count().Expand().MaxTop(null);
				endpoints.MapODataRoute("odata", "odata", ApplicationConfiguration.GetEdmModel(), new DefaultODataBatchHandler());
			});


			container.Verify();
		}
	}
}
