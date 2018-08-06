using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace FlowBot
{
    class Program
    {
        async static Task<string> Get(string uri)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(uri);
                request.Headers.Add("Ocp-Apim-Subscription-Key", Keys.SubscriptionKey);

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
                request.Headers.Add("Authorization", "EndpointKey " + Keys.SubscriptionKey);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        async static void GetAnswers(string question)
        {
            var uri = Keys.Host + Requests.Service + Requests.GenerateAnswer;
            Console.WriteLine("Calling " + uri + ".");
            var response = await Post(uri, question);

            var answers = JsonConvert.DeserializeObject<Answers>(response);
            Console.WriteLine(response);
            Console.WriteLine("Press any key to continue.");
        }
        
        static void Main(string[] args)
        {

            var question = Console.ReadLine();
            var botQuestion= new BotQuestion()
            {
                question = question,
                top = 3
            };

            var serialized = JsonConvert.SerializeObject(botQuestion);

            GetAlterations();



            GetAnswers(serialized);
            Console.ReadLine();
        }

        async static void GetAlterations()
        {
            var uri = Keys.Host + Requests.Service + Requests.Knowledgebases;
            Console.WriteLine("Calling " + uri + ".");
            var response = await Get(uri);

            Console.WriteLine(response);
            Console.WriteLine("Press any key to continue.");
        }
    }
    
}
