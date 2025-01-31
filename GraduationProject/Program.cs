
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GraduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using GraduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GraduationProject.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GraduationProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(
                options =>
                    options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("constr"));

            });
            var emailConfig = builder.Configuration
                .GetSection("EmailConfiguration").Get<EmailConfiguration>();

            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddTransient<IEmailSender, EmailSender>(); // Fix this
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider; // Configure PasswordResetTokenProvider here

            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders(); // Ensure default token providers are added
            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(1));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Use JWT for user authentication
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;   // Challenge unauthorized users with JWT
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; // Only for development; enable in production if HTTPS is enforced
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Ensure the token's issuer matches the valid issuer
                    ValidIssuer = "http://localhost:5203", // Replace with your actual issuer
                    ValidateAudience = true, // Ensure the token's audience matches the valid audience
                    ValidAudience = "http://localhost:4200", // Replace with your actual audience
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ksdlkjljskj2325#!#!vnl1jk2!#!@3213!#kjvljicojckl")) // Use a secure, strong key
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy => {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseCors("MyPolicy");


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
