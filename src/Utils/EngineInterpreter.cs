using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;

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
            var rawBody = await response.Content.ReadAsStringAsync();
            Regex regex = new Regex("\"totalPages\":[0-9]+");
            Match match = regex.Match(rawBody);
            int? totalPages = null;
            if (match.Success)
            {
                regex = new Regex("[\\d+]");
                match = regex.Match(match.Value);
                totalPages = int.Parse(match.Value);
            }

            regex = new Regex("\"totalPages\":[0-9]+");
            match = regex.Match(rawBody);

            if (match.Success) rawBody = rawBody?.Replace(match.Value, "");

            match = regex.Match(rawBody);
            string data = match.Groups[1].Value;
            if (match.Success) data = match.Groups[1].Value;
            else data = null;

            try
            {
                if (data != null)
                {
                    var objectData = JsonConvert.DeserializeObject<IEnumerable<type>>(data);
                    var statusCode = (int)response.StatusCode;
                    return new EngineInterpreterResponse(statusCode, objectData, totalPages);
                }
                else
                {
                    var objectData = JsonConvert.DeserializeObject<type>(rawBody);
                    var statusCode = (int)response.StatusCode;
                    return new EngineInterpreterResponse(statusCode, objectData, totalPages);
                }
            }
            catch (SystemException)
            {
                throw new EngineInterpreterException("Houve um erro ao realizar a desserialização do objeto.");

            }
        }


    }

    public class EngineInterpreterResponse
    {

        public int StatusCode { get; }
        public dynamic? Body { get; set; }
        public int? TotalPages { get; set; }

        public EngineInterpreterResponse(int statusCode, dynamic? body, int? totalPages)
        {
            this.StatusCode = statusCode;
            this.Body = body;
            this.TotalPages = totalPages;
        }
    }

    class EngineInterpreterException : Exception
    {
        public EngineInterpreterException(string message) : base(message)
        {
        }
    }
}
