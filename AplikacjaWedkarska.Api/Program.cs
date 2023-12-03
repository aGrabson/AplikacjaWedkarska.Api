using AplikacjaWedkarska.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AplikacjaWedkarska.Api.Settings;
using AplikacjaWedkarska.Api.Services;
using QuickTickets.Api.Services;

namespace AplikacjaWedkarska.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                  policy =>
                  {
                      policy.WithOrigins("http://192.168.30.2:3000", "exp://192.168.55.105:19000")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(origin => true)
                .SetIsOriginAllowedToAllowWildcardSubdomains();
                  });
            });

            builder.Services.AddControllers();

            builder.Services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            var tokenOptions = builder.Configuration
              .GetSection(TokenOptions.CONFIG_NAME)
              .Get<TokenOptions>();
            builder.Services.Configure<TokenOptions>(
              builder.Configuration.GetSection(TokenOptions.CONFIG_NAME)
            );

            builder.Services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(
                o => {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromMinutes(1),
                        IgnoreTrailingSlashWhenValidatingAudience = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(tokenOptions.SigningKey)
                  ),
                        ValidateIssuerSigningKey = tokenOptions.ValidateSigningKey,
                        RequireExpirationTime = true,
                        RequireAudience = true,
                        RequireSignedTokens = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidAudience = tokenOptions.Audience,
                        ValidIssuer = tokenOptions.Issuer
                    };
                    o.SaveToken = true;
                }
              );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IFishingSpotService, FishingSpotService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();

            var app = builder.Build();

            

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}