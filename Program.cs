var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "http://192.168.1.158:8080/realms/master";
        options.ClientId = "testn";
        options.ClientSecret = "SNVMbJArpyVFZQskiqmcQpOjVV2a9l5x"; // If required
        options.ResponseType = "code";
        options.RequireHttpsMetadata = false; // debug

        options.SaveTokens = true;

        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");

        // Optional: Configure additional settings like token validation parameters
    });

// Add services for controllers, views, etc.
builder.Services.AddControllersWithViews();
///

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
