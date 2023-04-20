using System.Text;
using GroupAPIProject.Data;
using GroupAPIProject.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using GroupAPIProject.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using GroupAPIProject.Services.PurchaseOrder;
using GroupAPIProject.Services.PurchaseOrderItem;
using GroupAPIProject.Services.SalesOrder;
using GroupAPIProject.Services.SalesOrderItem;
using GroupAPIProject.Services.Supplier;
using GroupAPIProject.Services.Customer;
using GroupAPIProject.Services.Product;
using GroupAPIProject.Services.InventoryItem;
using GroupAPIProject.Services.Location;
using GroupAPIProject.Services.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure()));
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<IPurchaseOrderItemService, PurchaseOrderItemService>();
builder.Services.AddScoped<ISalesOrderService, SalesOrderService>();
builder.Services.AddScoped<ISalesOrderItemService, SalesOrderItemService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomAdminEntity", policy =>
    {
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            return user.HasClaim(c => c.Type == "Role" && c.Value == "AdminEntity");
        });
    }
    
    
    );
     options.AddPolicy("CustomRetailerEntity", policy =>
    {
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            return user.HasClaim(c => c.Type == "Role" && c.Value == "RetailerEntity");
        });
    }
    
    
    );
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GroupAPIProject.WebAPI", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \n\n Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: \"Bearer 12345abcdef\""
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
