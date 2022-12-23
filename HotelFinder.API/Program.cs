using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Data.Abstract;
using HotelFinder.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
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

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseAuthorization();

app.MapRazorPages();

app.Run();
