using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

//using Newtonsoft.Json;

namespace FlowBot
{
    class Program
    {
        // NOTE: Replace this with a valid host name.
        static string host = "https://flowqna.azurewebsites.net";

        // NOTE: Replace this with a valid endpoint key.
        // This is not your subscription key.
        // To get your endpoint keys, call the GET /endpointkeys method.
        static string endpoint_key = "5f64a6e8-5287-4818-bc37-1e1d14e5b808";

        // NOTE: Replace this with a valid subscription key.
        static string key = "5f64a6e8-5287-4818-bc37-1e1d14e5b808";


        // POST /knowledgebases/{knowledge base ID} method.
        static string kb = "ac32a333-1086-4e85-84b1-74589b0c2c94";

        //static string service = "/qnamaker";
        static string method = "/knowledgebases/" + kb + "/generateAnswer/";
        
        static string service = "/qnamaker";
        static string method2 = "/endpointkeys/";


        static string question = @"
                {
                    'question': 'Does it work on mobile',
                    'top': 3
                }
                ";
        async static Task<string> Get(string uri)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(uri);
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        async static Task<string> Post(string uri, string body)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + endpoint_key);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        async static void GetAnswers(string question)
        {
            var uri = host + service + method;
            Console.WriteLine("Calling " + uri + ".");
            var response = await Post(uri, question);
            PrettyPrint(response);
            Console.WriteLine(response);
            Console.WriteLine("Press any key to continue.");
        }

        static string PrettyPrint(string s)
        {
            var answers= JsonConvert.DeserializeObject<Answers>(s);

           return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(s));
        }

        static void Main(string[] args)
        {

            question = Console.ReadLine();
            var botQuestion= new BotQuestion()
            {
                question = question,
                top = 3
            };

            var serialized = JsonConvert.SerializeObject(botQuestion);

            GetAnswers(serialized);
            Console.ReadLine();
        }
    }
}
