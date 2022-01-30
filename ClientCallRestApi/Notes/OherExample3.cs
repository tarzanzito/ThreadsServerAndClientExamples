//using System;
//using System.Collections.Concurrent;
//using System.Net;
//using System.Net.Http.Headers;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Candal
//{
//    internal class Example
//    {
//        #region main

//        public static int Main()
//        {
//            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
//            stopwatch.Start();

//            WriteLog("PROGRAM STARTED", 0);

//            //InvokeThreeResource(1); //test 1: confirmed async
            
//            //InvokeThreeResourceAsync(1).Wait(); //test2: confirmed async
            
//            //InvokeSequentialGetsAsync(1).Wait(); //test3: getA, getB, getC sequential
            
//            //InvokeManyCallsOverOneResource(100); //test4: confirmed async - (call : GetU * 100)

//            //InvokeManyCallOverThreeResources(100); //test5: confirmed async -(call GetA + GetB + GetC) * 100 

//            InvokeParallel(1); //test5: confirmed async

//            WriteLog("PROGRAM FINISHED", 0);

//            stopwatch.Stop();
//            Console.WriteLine("RunTime costs: " + stopwatch.Elapsed.TotalMilliseconds);

//            return 0;
//        }

//        /// <summary>
//        /// it makes no sense the 'Main' to return a task because the OS already create a individual thread for this application
//        /// await calls must be resolved internally and the method only return int
//        /// execute InvokeThreeResourceAsync, after terminate execute InvokeSequentialGetsAsync
//        /// </summary>
//        /// <returns></returns>
//        public static async Task<int> MainB() 
//        {
//            await InvokeThreeResourceAsync(1); //confirmed async
//            await InvokeSequentialGetsAsync(1); //getA, getB, getC sequential

//            return 0;
//        }

//        #endregion

//        #region long processes (loops)

//        private static void InvokeManyCallsOverOneResource(int total)
//        {
//            List<Task> tasksInFlight = new List<Task>(total);

//            for (int c = 0; c < total; c++)
//            {
//                tasksInFlight.Add(InvokeOneResourecAsync(c)); //call InvokeOneResourecAsync because return Task
//            }

//            Task.WaitAll(tasksInFlight.ToArray());
//        }

//        /// <summary>
//        /// resolve all tasks
//        /// </summary>
//        /// <param name="total"></param>
//        private static void InvokeManyCallOverThreeResources(int total)
//        {
//            List<Task> tasksInFlight = new List<Task>(total);

//            for (int c = 0; c < total; c++)
//            {
//                tasksInFlight.Add(InvokeThreeResourceAsync(c)); //call InvokeOneV2Async because return Task
//            }

//            Task.WaitAll(tasksInFlight.ToArray());
//        }

//        #endregion

//        #region Tasks processes

//        /// <summary>
//        /// call async and return task
//        /// </summary>
//        /// <param name="paramId"></param>
//        /// <returns></returns>
//        private static async Task InvokeOneResourecAsync(int paramId)
//        {
//            await CallGetUAsync(paramId);
//        }

//        /// <summary>
//        /// Call all async and resolve all tasks
//        /// </summary>
//        /// <param name="paramId"></param>
//        private static void InvokeThreeResource(int paramId)
//        {
//            Task task1 = CallGetAAsync(paramId);
//            Task task2 = CallGetBAsync(paramId);
//            Task task3 = CallGetCAsync(paramId);
//            Task.WaitAll(task1, task2, task3);
//        }

//        /// <summary>
//        /// Call all async and return task
//        /// </summary>
//        /// <param name="paramId"></param>
//        /// <returns></returns>
//        private static async Task InvokeThreeResourceAsync(int paramId)
//        {
//            Task task1 = CallGetAAsync(paramId);
//            Task task2 = CallGetBAsync(paramId);
//            Task task3 = CallGetCAsync(paramId);

//            await Task.WhenAll(task1, task2, task3);
//        }

//        /// <summary>
//        /// Call getA, after terninate call getB, after terninate call getC and return async task
//        /// </summary>
//        /// <param name="paramId"></param>
//        /// <returns></returns>
//        private static async Task InvokeSequentialGetsAsync(int paramId)
//        {
//            await CallSequentialGetsAsync(paramId); //Call get1, after get2, after get3
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="paramId"></param>
//        private static void InvokeParallel(int paramId)
//        {
//            try
//            {
//                Parallel.Invoke(
//                    () => CallGetU1(paramId),
//                    () => CallGetU2(paramId+1),
//                    delegate () { CallGetU3(paramId+2); }
//                );
//            }
//            // No exception is expected in this example, but if one is still thrown from a task,
//            // it will be wrapped in AggregateException and propagated to the main thread.
//            catch (AggregateException e)
//            {
//                Console.WriteLine("An action has thrown an exception. THIS WAS UNEXPECTED.\n{0}", e.InnerException.ToString());
//            }
//        }


//        #endregion

//        #region calls REST API Async

//        private const string ENDPOINT_getU = "http://localhost:5000/Example/getU?id=";
//        private const string ENDPOINT_getA = "http://localhost:5000/Example/getU?id=";
//        private const string ENDPOINT_getB = "http://localhost:5000/Example/getU?id=";
//        private const string ENDPOINT_getC = "http://localhost:5000/Example/getU?id=";

//        /// <summary>
//        /// Call getU async and return Task
//        /// </summary>
//        /// <param name="paramId"></param>
//        /// <returns></returns>
//        private static async Task CallGetUAsync(int paramId)
//        {
//            WriteLog("CallGet(U)Async - Request BEGIN", 0);

//            HttpClient client = new HttpClient();
//            HttpResponseMessage response = await client.GetAsync(ENDPOINT_getU + paramId.ToString());  //execute return of the Task here !!!
//            if (!response.IsSuccessStatusCode) return;

//            WriteLog("CallGet(U)Async - Request AFTER WAIT 1", 0);
//            string responseContent = await response.Content.ReadAsStringAsync();  //execute return of the Task here !!!

//            WriteLog("CallGet(U)Async - Request END", paramId);
//        }

//        /// <summary>
//        /// Call getA async and return Task
//        /// </summary>
//        /// <param name="paramId"></param>
//        /// <returns></returns>
//        private static async Task CallGetAAsync(int paramId)
//        {
//            WriteLog("CallGet(A)Async - Request BEGIN", 0);

//            HttpClient client = new HttpClient();
//            HttpResponseMessage response = await client.GetAsync(ENDPOINT_getA + paramId.ToString());  //execute return of the Task here !!!
//            if (!response.IsSuccessStatusCode) return;

//            WriteLog("CallGet(A)Async - Request AFTER WAIT 1", 0);
//            string responseContent = await response.Content.ReadAsStringAsync();  //execute return of the Task here !!!

//            WriteLog("CallGet(A)Async - Request END", paramId);
//        }

//        /// <summary>
//        /// Call getB async and return Task
//        /// </summary>
//        /// <param name="paramId"></param>
//        /// <returns></returns>
//        private static async Task CallGetBAsync(int paramId)
//        {
//            WriteLog("CallGet(B)Async - Request BEGIN", 0);

//            HttpClient client = new HttpClient();
//            HttpResponseMessage response = await client.GetAsync(ENDPOINT_getB + paramId.ToString());  //execute return of the Task here !!!
//            if (!response.IsSuccessStatusCode) return;

//            WriteLog("CallGet(B)Async - Request AFTER WAIT 1", 0);
//            string responseContent = await response.Content.ReadAsStringAsync();  //execute return of the Task here !!!

//            WriteLog("CallGet(B)Async - Request END", paramId);
//        }

//        /// <summary>
//        /// Call getC async and return Task
//        /// </summary>
//        /// <param name="paramId"></param>
//        /// <returns></returns>
//        private static async Task CallGetCAsync(int paramId)
//        {
//            WriteLog("CallGet(D)Async - Request BEGIN", 0);

//            HttpClient client = new HttpClient();
//            HttpResponseMessage response = await client.GetAsync(ENDPOINT_getC + paramId.ToString()); //execute return of the Task here !!!
//            if (!response.IsSuccessStatusCode) return;

//            WriteLog("CallGet(C)Async - Request AFTER WAIT 1", 0);
//            string responseContent = await response.Content.ReadAsStringAsync();  //execute return of the Task here !!!

//            WriteLog("CallGet(C)Async - Request END", paramId);
//        }

//        #endregion

//        #region calls REST API sync

//        /// <summary>
//        /// Call getA, after terninate call getB, after terninate call getC and return async task
//        /// </summary>
//        /// <param name="paramId"></param>
//        private static async Task CallSequentialGetsAsync(int paramId)
//        {
//            WriteLog("RunOneAsync - Request BEGIN", 0);

//            HttpClient client = new HttpClient();
//            HttpResponseMessage response1 = await client.GetAsync(ENDPOINT_getA + paramId.ToString()); //faz return da Task aqui !!!
//            if (!response1.IsSuccessStatusCode) return;

//            WriteLog("RunOneAsync - Request AFTER WAIT 1", 0);
//            string responseContent1 = await response1.Content.ReadAsStringAsync(); //faz return da Task aqui !!!


//            HttpResponseMessage response2 = await client.GetAsync(ENDPOINT_getB + paramId.ToString()); //faz return da Task aqui !!!
//            if (!response2.IsSuccessStatusCode) return;

//            WriteLog("RunOneAsync - Request AFTER WAIT 2", 0);
//            string responseContent2 = await response2.Content.ReadAsStringAsync(); //faz return da Task aqui !!!

//            HttpResponseMessage response3 = await client.GetAsync(ENDPOINT_getC + paramId.ToString()); //faz return da Task aqui !!!
//            if (!response3.IsSuccessStatusCode) return;

//            WriteLog("RunOneAsync - Request AFTER WAIT 3", 0);
//            string responseContent3 = await response3.Content.ReadAsStringAsync(); //faz return da Task aqui !!!

//            WriteLog("RunOneAsync - Request END", paramId);
//        }

//        /// <summary>
//        /// Call API with internal async methods but this method is not async
//        /// </summary>
//        /// <param name="paramId"></param>
//        private static void CallGetU2(int paramId) //is not async
//        {
//            string res;

//            WriteLog("CallGet(U2) - Request BEGIN", 0);

//            HttpClient client = new HttpClient();
//            Task<HttpResponseMessage> task = client.GetAsync(ENDPOINT_getU + paramId.ToString());
//            task.Wait();

//            HttpResponseMessage httpResponseMessage = task.Result;
//            if (httpResponseMessage.IsSuccessStatusCode)
//            {
//                Task<string> taskb = httpResponseMessage.Content.ReadAsStringAsync();
//                taskb.Wait();
//                res = taskb.Result;
//            }

//            WriteLog("CallGet(U2) - Request END", paramId);
//        }

//        /// <summary>
//        /// Call API without any internal async method
//        /// </summary>
//        /// <param name="paramId"></param>
//        private static void CallGetU3(int paramId)
//        {
//            string res;

//            WriteLog("CallGet(U3) - Request BEGIN", paramId);

//            HttpClient client = new HttpClient();
//            HttpResponseMessage httpResponseMessage = client.GetAsync(ENDPOINT_getU + paramId.ToString()).Result;
//            if (httpResponseMessage.IsSuccessStatusCode)
//            {
//                res = httpResponseMessage.Content.ReadAsStringAsync().Result;
//            }

//            WriteLog("CallGet(U3) - Request END", paramId);
//        }

//        /// <summary>
//        /// Call API without any internal async method
//        /// </summary>
//        /// <param name="paramId"></param>
//        private static void CallGetU1(int paramId)
//        {
//            WriteLog("CallGet(U1) - Request BEGIN", paramId);

//            var client = new HttpClient();
//            var webRequest = new HttpRequestMessage(HttpMethod.Get, ENDPOINT_getU + paramId.ToString());
//            {
//                //Content = new StringContent("{ 'some': 'value' }", Encoding.UTF8, "application/json")
//            };

//            var response = client.Send(webRequest);

//            using var reader = new StreamReader(response.Content.ReadAsStream());
//            string res = reader.ReadToEnd();

//            WriteLog("CallGet(U1) - Request END", paramId);
//        }

//        #endregion

//        #region logs

//        /// <summary>
//        /// Log First Line for input file to excel or access
//        /// Desc;Parameter;ThreadId;CurrentTime";
//        /// </summary>
//        /// <param name="desc"></param>
//        /// <param name="parmId"></param>
//        private static void WriteLog(string desc, int parmId)
//        {
//            string parameter = parmId.ToString("00000");
//            string currTime = System.DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss.fff");
//            string threadId = Thread.CurrentThread.ManagedThreadId.ToString("00000");

//            string text = $"{desc};{parameter};{threadId};{currTime}";

//            System.Console.WriteLine(text);
//            //_logger.LogInformation(text);
//        }

//        #endregion
//    }
//}