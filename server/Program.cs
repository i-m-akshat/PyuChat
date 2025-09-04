using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var jwtsetting = builder.Configuration.GetSection("JWTSetting");
builder.Services.AddOpenApi();
builder.Services.AddDbContext<App_DbContext>(x => x.UseSqlServer("Data Source=DESKTOP-RBREH2D\\SQLEXPRESS;Database=PyuChat;Integrated Security=SSPI;Trusted_Connection=true;TrustServerCertificate=True;"));
builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<App_DbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting.GetSection("SecurityKey").Value ?? "")),
    };
});
builder.Services.AddAuthorization();

//service setup


builder.Services.AddScoped<FileUpload>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();//lets u serve static file also
app.MapAccountEndPoint();
app.Run();

