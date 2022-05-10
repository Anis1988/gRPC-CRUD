
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;

namespace Client
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:50051");
            var client = new Person.PersonClient(channel);

            await channel.ConnectAsync().ContinueWith(task => {
                if (task.Status == TaskStatus.RanToCompletion)
                    System.Console.WriteLine("Connection Established !!!");
            });


            var obj = new PersoneModel
                {
                    FirstName = "Anis",
                    LastName = "Medini"
                };
            var obj2 = new PersoneModel
                {
                    FirstName = "Roza",
                    LastName = "Medini"
                };
            // System.Console.WriteLine("***CREATING PEOPLE***");
            // System.Console.WriteLine(Creating(client,obj));
            // System.Console.WriteLine(Creating(client, obj2));

            // System.Console.WriteLine("***READING PEOPLE***");
            // await Reading(client,"0f51ca69-c2b0-40de-9620-bb16c8e7f953");

            var objtoUpdate = new PersoneModel
            {

                FirstName = "Jill ",
                LastName = "York "
            };

            // System.Console.WriteLine("***UPDATING PEOPLE***");
            // await Updating(client,objtoUpdate,"0f51ca69-c2b0-40de-9620-bb16c8e7f953");
            
            //Deleting(client,"Anis");
            //   Deleting(client,"Roza");


            // System.Console.WriteLine("***LIST OF PEOPLE***");
            System.Console.WriteLine(await ListOfPerson(client));





            System.Console.ReadKey();
            System.Console.WriteLine("Shutting down !!!");
            await channel.ShutdownAsync();


        }







        public static string Creating (Person.PersonClient client, PersoneModel person)
        {
            var req = new personeCreateRequest {Persone = person};
            var res =  client.createPersone(req);

            return res.Result;
        }
        public static async Task<personeListResponse> ListOfPerson(Person.PersonClient client)
        {
            var res =  client.ListPersone(new personeListRequest ());
            return await Task.FromResult(res);

        }
        public static Task<personReadResponse> Reading (Person.PersonClient client, string idToFind)
        {
            var res = client.readPerson(new personReadRequest{Id = idToFind});
            System.Console.WriteLine(res.Persone.ToString());
            return Task.FromResult(res);
        }
        public static Task<personUpdateResponse> Updating(Person.PersonClient client, PersoneModel person,string idToFind)
        {
            var res = client.updatePerson(new personUpdateRequest{Persone = person,ToFind = idToFind});
            System.Console.WriteLine("the person being UPDATED "+res.Persone.ToString());
            return Task.FromResult(res);
        }
        public static void Deleting(Person.PersonClient client, string idToFind)
        {
            var res = client.deletePerson(new personeDeleteRequest{ToFind = idToFind});
        }
    }
}
