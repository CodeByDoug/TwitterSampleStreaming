using Dcm.Twitter.Repository.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dcm.Business.Manager;
using Dcm.Twitter.Business.Contract;
using Dcm.Twitter.Repository;
using Dcm.Twitter.Models;
using Dcm.Twitter.Contracts;
using Dcm.Twitter.Business;

namespace TwitterApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add Services to the container.
        public void ConfigureServices(IServiceCollection Services)
        {
            Services.AddControllers();

            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Twitter Sample Api",
                    Description = "Description for Twitter Sample  Api",
                });
            });

            Services.AddSingleton<IUrlRepository, UrlRepository>();
            Services.AddSingleton<IHashtagRepository, HashtagRepository>();
            Services.AddSingleton<IEmojiRepository, EmojiRepository>();
            Services.AddSingleton<ITweetRepository, TweetRepository>();
            Services.AddSingleton<IMediaManager, MediaManager>();
            Services.AddSingleton<IUrlManager, UrlManager>();
            Services.AddSingleton<IHashtagManager, HashtagManager>();
            Services.AddSingleton<IEmojiManager, EmojiManager>();
            Services.AddSingleton<ITweetManager, TweetManager>();
            Services.AddSingleton<IStreamManager, StreamManager>();

            Services.AddSingleton<IStreamDetails, StreamDetails>();
            Services.AddSingleton<ITweetCalculationEngine, TweetCalculationEngine>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });
        }
    }
}