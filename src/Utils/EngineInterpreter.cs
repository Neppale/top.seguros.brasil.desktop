using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Top_Seguros_Brasil_Desktop.Utils
{
    public class EngineInterpreter 
    {
        public HttpClient httpClient { get; set; }

        public EngineInterpreter()
        {
            this.httpClient = new HttpClient();
        }

        public EngineInterpreter(string token)
        {
            this.httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        public async Task<EngineInterpreterResponse> Request<type>(string address, string method, StringContent? data)
        {
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("Endereço inválido.");

            string[] methods = new string[] { "GET", "POST", "PUT", "DELETE" };
            if (!methods.Contains(method)) throw new ArgumentException("Método de requisição inválido.");

            HttpResponseMessage response;

            switch (method)
            {
                case "GET":
                    response = await httpClient.GetAsync(address);
                    return await Interpret<type>(response);
                case "POST":
                    response = await httpClient.PostAsync(address, data);
                    return await Interpret<type>(response);
                case "PUT":
                    response = await httpClient.PutAsync(address, data);
                    return await Interpret<type>(response);
                case "DELETE":
                    response = await httpClient.DeleteAsync(address);
                    return await Interpret<type>(response);
                default:
                    throw new ArgumentException("Método de requisição inválido.");
            }
        }

        private static async Task<EngineInterpreterResponse> Interpret<type>(HttpResponseMessage response)
        {
            try
            {
                var rawBody = await response.Content.ReadAsStringAsync();
                var convertedBody = JsonConvert.DeserializeObject<type>(rawBody);
                var statusCode = (int)response.StatusCode;
                return new EngineInterpreterResponse(statusCode, convertedBody);
            }
            catch (Exception) 
            {
                throw new EngineInterpreterException("Houve um erro ao realizar a desserialização do objeto."); 
            }
        }
    }

    public class EngineInterpreterResponse
    {

        public int StatusCode { get; }
        public dynamic? Body { get; set; }

        public EngineInterpreterResponse(int statusCode, dynamic? body)
        {
            this.StatusCode = statusCode;
            this.Body = body;
        }
    }

    class EngineInterpreterException : Exception
    {
        public EngineInterpreterException(string message) : base(message)
        {
        }
    }
}
