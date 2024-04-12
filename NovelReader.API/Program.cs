using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NovelReader.API.Context;
using NovelReader.API.Profiles;
using NovelReader.API.Services.PasswordHasher;
using NovelReader.API.Services.TokenGeneratorService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

// Add CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
		builder =>
		{
			builder.WithOrigins("https://localhost:7187")
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add DbContext
builder.Services.AddDbContext<CustomDbContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});

// Add PasswordHasher service
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

// Add TokenGeneratorService
builder.Services.AddSingleton<ITokenGeneratorService, TokenGeneratorService>();

// Config JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
			ValidateIssuerSigningKey = true,
		};
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
