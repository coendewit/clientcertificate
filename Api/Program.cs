using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions => serverOptions.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
    }
));

var app = builder.Build();

app.MapGet("/certificate", (HttpContext context) =>
{
    var clientCertificate = context.Connection.ClientCertificate;
    if (clientCertificate == null) return Results.BadRequest("No client certificate provided.");
    
    var thumbprint = clientCertificate.Thumbprint;
    var subject = clientCertificate.Subject;
    var issuer = clientCertificate.Issuer;
    Console.WriteLine($"Certificate Thumbprint: {thumbprint}");
    Console.WriteLine($"Certificate Subject: {subject}");
    Console.WriteLine($"Certificate Issuer: {issuer}");

    return Results.Ok($"Certificate Thumbprint: {thumbprint}\nCertificate Subject: {subject}\nCertificate Issuer: {issuer}");
});

app.Run();