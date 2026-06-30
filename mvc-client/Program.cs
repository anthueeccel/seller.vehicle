
using NHibernate;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ISessionFactory>(provider =>
{
    return NHibernateHelper.GetSessionFactory(builder.Configuration);
});

builder.Services.AddScoped<NHibernate.ISession>(provider =>
{
    var factory = provider.GetRequiredService<ISessionFactory>();
    return factory.OpenSession();
});

builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<CountryPhoneCodeService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<DatabaseSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    seeder.Seed();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
