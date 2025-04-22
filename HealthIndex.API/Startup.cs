using CorePush.Apple;
using CorePush.Google;
using HealthIndex.API.Logger;
using HealthIndex.Business.Implementation;
using HealthIndex.Business.Interface;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Text;


namespace WideWings.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSpecificOrigin",
            //        builder => builder.WithOrigins("https://test.instamojo.com")
            //                          .AllowAnyMethod()
            //                          .AllowAnyHeader());
            //});         

            services.AddControllers();
            //services.AddControllers();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();

            // Configure strongly typed settings objects
          
            services.AddCors();
            services.AddSingleton(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Agtonomics.apk"));
            services.AddDbContext<HealthIndexDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            //Register all injecting interfaces with implemented class
            services.AddScoped<IAppUserMasterService,AppUserMasterService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserModelService, UserModelService>();
            services.AddScoped<ITwilioSmsService, TwilioSmsService>();
            services.AddScoped<IFeedbackDetailService, FeedbackDetailService>();
            services.AddScoped<IConsultantDetailService, ConsultantDetailService>();
            services.AddScoped<IFirmDetailService,FirmDetailsService>();
            services.AddScoped<IQuestionnaireCategoryService, QuestionnaireCategoryService>();
            services.AddScoped<IQuestionMasterSevice,QuestionMasterSevice>();
            services.AddScoped<IQuestionSubscriptionService, QuestionSubscriptionService>();
            services.AddScoped<IFeedbackReplyService, FeedbackReplyService>();
            services.AddScoped<IAppDataBackupService, AppDataBackupService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IPlanSubscription, PlanSubscriptionService>();
            services.AddScoped<ISubscriptionOrderService, SubscriptionOrderService>();
            services.AddSingleton<ILog, LogNLog>();
            services.AddControllers().AddNewtonsoftJson();

            services.Configure<ConfigurationModel>(Configuration.GetSection("ConfigurationModel"));
            services.Configure<HealthIndex.Model.EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.Configure<IISServerOptions>(options =>

            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue;
            });


            // Adding Authentication  

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  

            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = Configuration["APIInfo:Title"],
                    Description = Configuration["APIInfo:Description"]
                });

                // To Enable authorization using Swagger (JWT)

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = Configuration["APISecurityDefinition:Description"],
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            //  app.UseCors("AllowSpecificOrigin");
            //temp hide
            app.UseCors(builder => builder
                .AllowAnyOrigin()  
                .AllowAnyMethod()  
                .AllowAnyHeader()
                //.WithExposedHeaders("Content-Disposition") // Allow exposing specific headers

        // Add the following line to allow requests to the Instamojo token endpoint
           //  .WithOrigins("https://test.instamojo.com")
            );


            // .WithHeaders("authorization", "accept", "content-type", "origin")); ;

            //app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
            //   .WithExposedHeaders("content-disposition"));
            //app.UseCors("AllowOriginPolicy");


            //Accesing Physical Files like img, pdf

            //Accesing Physical Files like img, pdf

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), "Resource", "QuestionImage")),
                RequestPath = "/QuestionImage",
                ServeUnknownFileTypes = true,
                DefaultContentType = "image/*"

            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), "Resource", "AppUserDataBackup")),
                RequestPath = "/AppUserDataBackup",
                ServeUnknownFileTypes = true,
                DefaultContentType = "file/*"
            });
           
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET 5 Web API v1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}

