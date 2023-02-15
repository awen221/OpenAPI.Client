using System.Net;

using RestSharp;

namespace OpenAPI.Client
{
    public class ClientBase
    {
        protected string Execute(string api_url, RestRequest request)
        {
            using (RestClient client = new RestClient(api_url))
            {
                var response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var ErrorException_Message = response.ErrorException.Message;
                    var Content = response.Content;

                    throw new Exception($@"
ErrorException_Message：{ErrorException_Message}
Content：{Content}
");
                }

                return response.Content;
            }
        }
    }
}
