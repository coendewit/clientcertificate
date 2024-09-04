using System.Net.Security;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
    serverOptions.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
        httpsOptions.CheckCertificateRevocation = false; 
        httpsOptions.ClientCertificateValidation = (certificate, _, errors) =>
        {
            if (certificate.Issuer == certificate.Subject) 
            {
                return true;
            }
            return errors == SslPolicyErrors.None;
        };
    })
);

var app = builder.Build();

app.MapGet("/certificate", (HttpContext context) =>
{
    Console.WriteLine("Retrieving client certificate.");
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