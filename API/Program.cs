
using API.Helpers;
using Application.Service;
using Application.Service_Interface;
using Domain.Interface;
using Domain.Model;
using Infra.Data.Context;
using Infra.Data.Reposatory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;
using System.Text.Json.Serialization;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            StripeConfiguration.ApiKey = builder.Configuration["StripeSettings:SecretKey"];

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    // Set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient(typeof(IBaseRopository<>), typeof(BaseRopository<>));
            builder.Services.AddTransient<IProductService, Application.Service.ProductService>();
            builder.Services.AddTransient<IProduct_CategoryService, Product_CategoryService>();
            builder.Services.AddTransient<IProduct_InventoryService, Product_InventoryService>();
            builder.Services.AddTransient<IOrder_DetailService, Order_DetailService>();
            builder.Services.AddTransient<IOrder_ItemService, Order_ItemService>();
            builder.Services.AddTransient<IRateService, RateService>();
            builder.Services.AddTransient<IUser_AddressService, User_AddressService>();
            builder.Services.AddTransient<IPhotoService, PhotoService>();
            builder.Services.AddTransient<IDiscountService, Application.Service.DiscountService>();
            builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
            builder.Services.AddTransient(typeof(IBaseRopository<Domain.Model.Discount>), typeof(BaseRopository<Domain.Model.Discount>));
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ICartService, CartService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IOrderNumberGenerator, OrderNumberGenerator>();
            builder.Services.AddScoped<IStripeService, StripeService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseCors(options => options.WithOrigins("http://localhost:8000/")
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
