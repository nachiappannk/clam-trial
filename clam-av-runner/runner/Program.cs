using System; 
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace runner
{
    class Program
    {

        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        } 

        private static async Task MainAsync()
        {
            String fileName = String.Empty;
            HttpClient client = new HttpClient();
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Data\clam-trial\clam-av-runner\test1.txt");
            while ((fileName = file.ReadLine()) != null)
            {
                var urlEncodefileName = HttpUtility.UrlEncode(fileName);
                var url = $"http://localhost:5050/Clam/files/{urlEncodefileName}/scan";
                var response = await client.PostAsync(url, null);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response.StatusCode + "   " + content);
            }

            file.Close();
        }
    }
}
