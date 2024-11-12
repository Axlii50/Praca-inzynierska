using System.Diagnostics;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            byte[] imageArray = System.IO.File.ReadAllBytes(@"Capybara2.jpg");
            string encoded = Convert.ToBase64String(imageArray);
            byte[] data = Encoding.ASCII.GetBytes(encoded);
            string API_KEY = "x1LQbdKlYcIeYRgFrViX";
            string DATASET_NAME = "inz-gnlau";
            string DATASET_VERSION = "1";

            string uploadURL =
                    "https://detect.roboflow.com/" + DATASET_NAME + "/" + DATASET_VERSION + "?api_key=" + API_KEY
                + $"&name={Guid.NewGuid().ToString()}.jpg";

            using (var httpClient = new HttpClient())
            {
                // Configure request
                var content = new ByteArrayContent(data);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                // Send request
                var response = httpClient.PostAsync(uploadURL, content).Result;

                // Handle response

                Debug.WriteLine(response.Content.ReadAsStringAsync().Result);

                response.EnsureSuccessStatusCode();
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(responseContent);
            }

        }
    }
}
