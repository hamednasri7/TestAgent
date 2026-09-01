using Microsoft.EntityFrameworkCore;
using UserManagement.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
            field => $"{field} باید عددی باشد.");
        options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
            (value, field) => $"مقدار '{value}' برای {field} معتبر نیست.");
    });
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapGet("/", () => Results.Redirect("/Users"));
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
