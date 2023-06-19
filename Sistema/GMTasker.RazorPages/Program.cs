var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.UseEndpoints(endpoints =>{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=GetUsuarios}/{id?}");
});

app.Run();
