using System.Security.Cryptography.X509Certificates;

var certificate = new X509Certificate2("./client.pfx", "coen");

var handler = new HttpClientHandler();
handler.ClientCertificates.Add(certificate);

var cert = handler.ClientCertificates[0];
Console.WriteLine($"Certificate Subject: {cert.Subject}");
Console.WriteLine($"Certificate Issuer: {cert.Issuer}");

using var client = new HttpClient(handler);
var response = await client.GetAsync("https://localhost:7108/certificate");

if (response.IsSuccessStatusCode)
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);
}
else
{
    Console.WriteLine($"Failed to retrieve data: {response.StatusCode}");
}
