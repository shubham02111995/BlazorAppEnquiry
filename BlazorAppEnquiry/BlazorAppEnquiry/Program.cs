using BlazorAppEnquiry.Components;
using BlazorAppEnquiry.Data;
using BlazorAppEnquiry.Services;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure database connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), 
        sqlOptions => sqlOptions.EnableRetryOnFailure()
        )
    );

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Register services
builder.Services.AddScoped<EnquiryService>();
builder.Services.AddScoped<AuthService>();

// Add Blazor and API support
builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();

builder.Services.AddAntiforgery(options =>
{
    // Set custom settings if necessary
    options.HeaderName = "X-XSRF-TOKEN"; // This is often needed in Single Page Apps.
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapBlazorHub();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();


app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorAppEnquiry.Client._Imports).Assembly);

app.Run();
