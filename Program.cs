using Gestions_Client_Commande.Data;

var builder = WebApplication.CreateBuilder(args);

// Récupération de la chaîne de connexion dans appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Enregistrement des dépôts dans le conteneur d'injection de dépendances
builder.Services.AddSingleton<DepotClients>(provider => new DepotClients(connectionString));
builder.Services.AddSingleton<DepotCommandes>(provider => new DepotCommandes(connectionString));

// Ajout des services MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configuration du routage par défaut
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Index}/{id?}");

app.Run();

