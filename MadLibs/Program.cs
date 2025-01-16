using MadLibs.Components;
using MadLibs.Data;
using MadLibs.Models;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MadLibsConnectionString")
                                        //?? Environment.GetEnvironmentVariable("SQLAZURECONNSTR_ConnectionString")
                                        ?? throw new InvalidOperationException("Connection string not found.");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddSingleton<IStoriesRepository>(builder => new StoriesRepository(connectionString));
// Register the DapperRepository as a generic service using generic types
builder.Services.AddSingleton(typeof(IRepository<>), typeof(DapperRepository<>));



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
else if (app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
