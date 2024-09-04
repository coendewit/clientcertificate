using System.Security.Cryptography.X509Certificates;

var certificate = new X509Certificate2("./client.pfx", "coen");

var handler = new HttpClientHandler();
handler.ClientCertificates.Add(certificate);

var cert = handler.ClientCertificates[0];
Console.WriteLine($"Sending Certificate Subject: {cert.Subject}");
Console.WriteLine($"Sending Certificate Issuer: {cert.Issuer}");

using var client = new HttpClient(handler);
var response = await client.GetAsync("https://localhost:7108/certificate");

if (response.IsSuccessStatusCode)
{
    Console.WriteLine("Successfully retrieved data.");
    Console.WriteLine("Server response:");
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);
}
else
{
    Console.WriteLine($"Failed to retrieve data: {response.StatusCode}");
}