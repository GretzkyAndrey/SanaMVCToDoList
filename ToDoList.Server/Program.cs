using ToDoList;

using ToDoList.MsSql.Extensions;
using ToDoList.XML.Extensions;
using ToDoList.Controllers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", builder =>
    {
        builder.AllowAnyHeader()
               .WithMethods("POST")
               .WithOrigins("http://localhost:3000")
               .AllowCredentials();
    });
});

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddMsSqlDataProvider(builder.Configuration.GetConnectionString("MsSql"));
builder.Services.AddXmlDataProdiver(builder.Configuration.GetConnectionString("Xml"));

builder.Services.AddGraphQLApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("DefaultPolicy");
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: $"{{controller={nameof(ToDosController).ReplaceInEnd("Controller", string.Empty)}}}/{{action={nameof(ToDosController.Index)}}}/{{id?}}");

app.Run();
