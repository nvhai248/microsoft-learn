using Grpc.Net.Client;
using grpc_client;

var channel = GrpcChannel.ForAddress("http://localhost:5169");
var webDevClient = new WebDev.WebDevClient(channel);

var webDevReply = webDevClient.CreateProject(new Project { Name = "Nashtech", Id = 1 });
Console.WriteLine(webDevReply.Message);