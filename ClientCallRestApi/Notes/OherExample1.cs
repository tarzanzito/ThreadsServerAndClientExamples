//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Headers;


//namespace ConsoleProgram
//{
//    public class DataObject
//    {
//        public string Name { get; set; }
//    }

//    public class Class1
//    {
//        private const string URL = "https://sub.domain.com/objects.json";
//        private static string urlParameters = "?api_key=123";

//        public static void Example1()
//        {
//            HttpClient client = new HttpClient();
//            client.BaseAddress = new Uri(URL);

//            // Add an Accept header for JSON format.
//            client.DefaultRequestHeaders.Accept.Clear();
//            client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
//            //request.AddHeader("Authorization", "Bearer qaPmk9Vw8o7r7UOiX-3b-8Z_6r3w0Iu2pecwJ3x7CngjPp2fN3c61Q_5VU3y0rc-vPpkTKuaOI2eRs3bMyA5ucKKzY1thMFoM0wjnReEYeMGyq3JfZ-OIko1if3NmIj79ZSpNotLL2734ts2jGBjw8-uUgKet7jQAaq-qf5aIDwzUo0bnGosEj_UkFxiJKXPPlF2L4iNJSlBqRYrhw08RK1SzB4tf18Airb80WVy1Kewx2NGq5zCC-SCzvJW-mlOtjIDBAQ5intqaRkwRaSyjJ_MagxJF_CLc4BNUYC3hC2ejQDoTE6HYMWMcg0mbyWghMFpOw3gqyfAGjr6LPJcIly__aJ5__iyt-BTkOnMpDAZLTjzx4qDHMPWeND-TlzKWXjVb5yMv5Q6Jg6UmETWbuxyTdvGTJFzanUg1HWzPr7gSs6GLEv9VDTMiC8a5sNcGyLcHBIJo8mErrZrIssHvbT8ZUPWtyJaujKvdgazqsrad9CO3iRsZWQJ3lpvdQwucCsyjoRVoj_mXYhz3JK3wfOjLff16Gy1NLbj4gmOhBBRb8rJnUXnP7rBHs00FAk59BIpKLIPIyMgYBApDCut8V55AgXtGs4MgFFiJKbuaKxq8cdMYEVBTzDJ-S1IR5d6eiTGusD5aFlUkAs9NV_nFw");
//            //request.AddParameter("clientId", 123);

//            // List data response.
//            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
//            if (response.IsSuccessStatusCode)
//            {
//                // Parse the response body.
//                var json = response.Content.ReadAsStringAsync().Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.
//                //var result = JsonConvert.DeserializeObject<T>(json);

//                //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll

//                //foreach (var d in dataObjects)
//                //{
//                //    Console.WriteLine("{0}", d.Name);
//                //}
//            }
//            else
//            {
//                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
//            }

//            // Make any other calls using HttpClient here.

//            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
//            client.Dispose();
//        }
//    }
//}