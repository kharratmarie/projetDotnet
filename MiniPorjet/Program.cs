using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniPorjet.Context;
using MiniPorjet.Models.Repositories;



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddScoped<IPieceRepository, PieceRepository>();
builder.Services.AddScoped<IReclamationRepository, ReclamationRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IInterventionRepository, InterventionRepository>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); ;
    



builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Création des rôles au démarrage
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Client", "ResponsableSAV" };

    foreach (var role in roles)
    {
        // Vérifiez si le rôle existe, sinon créez-le
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Créez un compte pour un Responsable SAV par défaut (facultatif)
    var adminEmail = "admin@example.com";
    var adminPassword = "Admin@123";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, "ResponsableSAV");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
