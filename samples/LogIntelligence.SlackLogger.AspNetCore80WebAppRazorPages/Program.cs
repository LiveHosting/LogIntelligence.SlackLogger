using LogIntelligence.SlackLogger;

var builder = WebApplication.CreateBuilder(args);

// Configure the Slack logger using configuration from appsettings.json
builder.Logging.AddSlackLogger(options =>
{
    builder.Configuration.GetSection("Logging:SlackLogger").Bind(options);
});

//builder.Logging.AddSlackLogger("https://hooks.slack.com/services/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");

// Alternatively, configure programmatically
//builder.Logging.AddSlackLogger(configure =>
//{
//    configure.WebHooksUrl = "your-slack-webhook-url";
//    configure.LogLevel = LogLevel.Information;
//    configure.ApplicationName= "LogIntelligence.SlackLogger.AspNetCore80WebAppRazorPages";
//    configure.EnvironmentName="Development";
//});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
