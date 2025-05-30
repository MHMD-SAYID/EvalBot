using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using GraduationProject.Services;
using FluentValidation.AspNetCore;
using Hangfire;

namespace GraduationProject
{

    public static partial class DependencyInjection
{

  
        public static IServiceCollection AddDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers();

            services.AddHybridCache();

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!)
                )
            );

        services.AddAuthConfig(configuration);

           
            var connectionString = configuration.GetConnectionString("Localconstr") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            services
                .AddSwaggerServices()
                .AddMapsterConfig()
                .AddFluentValidationConfig();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();

            //services.AddScoped<IResultService, ResultService>();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.AddBackgroundJobsConfig(configuration);
            services.AddHttpContextAccessor();

            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }

        private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        private static IServiceCollection AddAuthConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();


            services.AddSingleton<IJwtProvider, JwtProvider>();

            //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
         
              services.Configure<IdentityOptions>(options =>
              {

                  options.Password.RequireDigit = true;
                  options.Password.RequiredLength = 8;
                  options.Password.RequireNonAlphanumeric = true;
                  options.Password.RequireUppercase = true;
                  options.Password.RequireLowercase = true;
                  options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                  //options.Password.RequireDigit = false;
                  //options.Password.RequireLowercase = true;
                  //options.Password.RequireNonAlphanumeric = false;
                  //options.Password.RequireUppercase = false;
                  //options.Password.RequiredLength = 8;
                  //options.Password.RequiredUniqueChars = 0;
              });
           
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }
        private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("LocalHnagfireconstr")));

            services.AddHangfireServer();

            return services;
        }
    }

}

        //{                         
        //    public static IServiceCollection AddDependencies(this IServiceCollection services,
        //        IConfiguration configuration)
        //    {
        //        services.AddControllers();
        //        //services.AddScoped<IAuthService, AuthService>();

        //        services.AddAuthConfig(configuration);

        //        var connectionString = configuration.GetConnectionString("constr") ??
        //            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        //        services.AddDbContext<AppDbContext>(options =>
        //            options.UseSqlServer(connectionString));

        //        services
        //            .AddSwaggerServices()
        //            .AddMapsterConfig();


        //        return services;
        //    }

        //    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        //    {
        //        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //        services.AddEndpointsApiExplorer();
        //        services.AddSwaggerGen();

        //        return services;
        //    }

        //    private static IServiceCollection AddMapsterConfig(this IServiceCollection services) 
        //    {
        //        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        //        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        //        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        //        return services;
        //    }

        //    //private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        //    //{
        //    //    services
        //    //        .AddFluentValidationAutoValidation()
        //    //        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //    //    return services;
        //    //}

        //    //private static IServiceCollection AddAuthConfig(this IServiceCollection services)
        //    //{
        //    //    services.AddSingleton<IJwtProvider, JwtProvider>();

        //    //    services.AddIdentity<ApplicationUser, IdentityRole>()
        //    //        .AddEntityFrameworkStores<AppDbContext>();
        //    //    services.Configure<IdentityOptions>(options =>
        //    //    {
        //    //        options.Password.RequireDigit = false;
        //    //        options.Password.RequireLowercase = true;
        //    //        options.Password.RequireNonAlphanumeric = false;
        //    //        options.Password.RequireUppercase = false;
        //    //        options.Password.RequiredLength = 8;
        //    //        options.Password.RequiredUniqueChars = 0;
        //    //    });
        //    //    services.AddAuthentication(options =>
        //    //    {
        //    //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    //    })
        //    //    .AddJwtBearer(o =>
        //    //    {
        //    //        o.SaveToken = true;
        //    //        o.TokenValidationParameters = new TokenValidationParameters
        //    //        {
        //    //            ValidateIssuerSigningKey = true,
        //    //            ValidateIssuer = true,
        //    //            ValidateAudience = true,
        //    //            ValidateLifetime = true,
        //    //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0e14d9bbcee6c5fd0a711e8da494ec63574619f86603b1fefbc22ec5c0d5a73b522c139399aa51c9092857d3414d82afdfe8941823abcd1a3bc5083a71830f0e7ce9cb19f683b4a9e6164b0753e2002b5ecf3a35796c6e33b6e11fb71cf0f78883e2efe9c99e0b560b156dec39ff3d58f9a6fc244f220f02d12762dfdcd938fdc4724b4496f41d6932331707ad9cfa7e495f53c9a29c2a2358efb2582f61251f160c50107d236171ad916d411ca1ec3b4a5d2dba1eaa49d7585115010112afad34f38387a57b13cc4a9ad97c29bf02568368b4fc3285777e0ce0aca206411a937c2f4c6b14f21226a0ae8a95017df625ccb5579330ed3b64e2bd52a2d0c2ea07")),
        //    //            ValidIssuer = "EvalBot",
        //    //            ValidAudience = "EvalBot users"
        //    //        };
        //    //    });

        //    //    return services;
        //    //}


        //    private static IServiceCollection AddAuthConfig(this IServiceCollection services,
        //        IConfiguration configuration)
        //    {
        //        services.AddIdentity<ApplicationUser, IdentityRole>()
        //          .AddEntityFrameworkStores<AppDbContext>()
        //          .AddDefaultTokenProviders();


        //        services.AddSingleton<IJwtProvider, JwtProvider>();

        //        //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        //        services.AddOptions<JwtOptions>()
        //            .BindConfiguration(JwtOptions.SectionName)
        //            .ValidateDataAnnotations()
        //        .ValidateOnStart();
        //        //    services.AddAuthentication()
        //        //.AddGoogle(options =>
        //        //{
        //        //    options.ClientId = "213357481232-ll7rbg6sjhk8gvqfk29glu25kjg9f4qs.apps.googleusercontent.com";
        //        //    options.ClientSecret = "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";
        //        //    options.CallbackPath ="/api/Account/Google"; // This should match the URI in Google Cloud
        //        //});
        //        services.AddAuthentication()
        //                     .AddGoogle(options =>
        //                     {
        //                         IConfigurationSection googleAuthSection = configuration.GetSection("Authentication:Google");
        //                         options.ClientId = googleAuthSection["ClientId"];
        //                         options.ClientSecret = googleAuthSection["ClientSecret"];
        //                     });
        //        var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        //        services.AddAuthentication(options =>
        //        {
        //            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        })
        //        .AddJwtBearer(o =>
        //        {
        //            o.SaveToken = true;
        //            o.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuerSigningKey = true,
        //                ValidateIssuer = true,
        //                ValidateAudience = true,
        //                ValidateLifetime = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
        //                ValidIssuer = jwtSettings?.Issuer,
        //                ValidAudience = jwtSettings?.Audience
        //            };
        //        });

        //        services.Configure<IdentityOptions>(options =>
        //        {
        //            options.Password.RequiredLength = 8;

        //            options.SignIn.RequireConfirmedEmail = true;
        //            options.User.RequireUniqueEmail = true;
        //        });

        //        return services;
        //    }
        //}
  