
using Grpc.Core;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;
using Serving;

var reflection = new ReflectionServiceImpl(ServerReflection.Descriptor,Person.Descriptor);

var server = new Server()
{

    Services = {Person.BindService(new MessageImpl()),
                ServerReflection.BindService(reflection)
                },
    Ports = {new ServerPort("localhost",50051,ServerCredentials.Insecure)}

};

server.Start();
System.Console.WriteLine("Server Started Successfully");

Console.ReadKey();

System.Console.WriteLine("Server Shutting Down !!!  Good Bye");


server.ShutdownAsync();
