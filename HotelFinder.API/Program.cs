using HotelFinder.API.Auth;
using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Data.Abstract;
using HotelFinder.Data.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
string key = "This is my test key";
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata= false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey= true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));

builder.Services.AddSingleton<IHotelRepository, HotelRepository>();
builder.Services.AddSingleton<IHotelService, HotelManager>();

builder.Services.AddSwaggerDocument(config=>
{
    config.PostProcess = (doc =>
    {
        doc.Info.Title = "All Hotels Api";
        doc.Info.Version= "1.0.0";
        doc.Info.Contact = new NSwag.OpenApiContact() { 
        Name = "Sumeyye Akkas",
        Email="akkasumeyye@hotmail.com"
        };
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseOpenApi();
app.UseSwaggerUi3();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


app.MapRazorPages();

app.Run();
