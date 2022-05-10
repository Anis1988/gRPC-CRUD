using Grpc.Core;
using static Person;

namespace Serving
{
    public class MessageImpl : PersonBase
    {
        List<PersoneModel> list = new List<PersoneModel>();
        public override Task<personeCreateResponse> createPersone(personeCreateRequest request, ServerCallContext context)
        {
            string gui = Guid.NewGuid().ToString();
            var obj = new PersoneModel()
            {
                Id = gui,
                FirstName = request.Persone.FirstName,
                LastName = request.Persone.LastName

            };
                var req = new personeCreateRequest{Persone = obj};

                list.Add(obj);

                return Task.FromResult(new personeCreateResponse {Result ="Persone added to the List ==> " + gui + " "+ request.Persone.FirstName});

        }
        public override Task<personeListResponse> ListPersone(personeListRequest request, ServerCallContext context)
        {
            var res =  new personeListResponse();
            foreach (var item in list)
            {
                res.Persone.Add(item);
            }

            return Task.FromResult(res);

        }

        public override async Task<personReadResponse> readPerson(personReadRequest request, ServerCallContext context)
        {
            string req = request.Id;

                foreach (var item in list)
                {
                    if (item.Id.Equals(req))
                         return await Task.FromResult(new personReadResponse{Persone = item});
                }

            throw new RpcException(new Status(StatusCode.NotFound,"The persone with ID "+ req + " "+ "Doesn't exsit"));
        }
        public override async Task<personUpdateResponse> updatePerson(personUpdateRequest request, ServerCallContext context)
        {
            var first = request.ToFind;

            foreach (var item in list)
            {
                if (item.FirstName.Equals(first) || item.LastName.Equals(first) || item.Id.Equals(first))
                {
                    item.FirstName = request.Persone.FirstName;
                    item.LastName = request.Persone.LastName;

                    return await Task.FromResult(new personUpdateResponse { Persone = item });
                }
            }
            throw new RpcException(new Status(StatusCode.NotFound, "The persone with ID " + first + " " + "Doesn't exsit"));

        }
        public override Task<personeDeleteResponse> deletePerson(personeDeleteRequest request, ServerCallContext context)
        {
            var first = request.ToFind;

            foreach (var item in list)
            {
                if (item.FirstName.Equals(first) || item.LastName.Equals(first) || item.Id.Equals(first))
                {
                    list.Remove(item);
                    return Task.FromResult(new personeDeleteResponse());
                }

            }
                 throw new RpcException(new Status(StatusCode.NotFound, "The persone with ID " + first + " " + "Doesn't exsit"));

        }

    }
}
