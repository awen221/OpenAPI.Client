using System.Net;

using RestSharp;
using Newtonsoft.Json;

namespace OpenAPI.Client
{
    abstract public class ClientBase
    {
        abstract protected string API_URL { get; }
        protected string Execute(RestRequest request)
        {
            using (RestClient client = new RestClient(API_URL))
            {
                var response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //優先拋Content
                    if (response.Content != "")
                    {
                        throw new Exception(response.Content);
                    }

                    //沒Content再拋ErrorException
                    if (response.ErrorException is not null)
                    {
                        throw new Exception(response.ErrorException.Message);
                    }

                    //都沒有就拋StatusDescription
                    throw new Exception(response.StatusDescription);
                }

                if (response.Content is null)
                {
                    throw new Exception("response.Content is null");
                }

                return response.Content;
            }
        }
        protected T ExecuteJsonConvert<T>(RestRequest request)
        {
            var json = Execute(request);
            var result = JsonConvert.DeserializeObject<T>(json);
            if (result is null) throw new Exception("result is null");

            return result;
        }
    }
}
