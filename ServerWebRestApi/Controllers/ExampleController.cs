using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //http://localhost:5016/swagger/index.html

    //http://localhost:5016/Example/get1

    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase, IDisposable
    {
        private readonly ILogger<ExampleController> _logger;

        private static int _instanceCount = 0;
        private static readonly object _locker = new object();
        private int _instanceId;

        public ExampleController(ILogger<ExampleController> logger)
        {
            _logger = logger;

            lock (_locker)
            {
                _instanceCount++;
                _instanceId = _instanceCount;
                WriteLog("Controller CONSTRUCTOR", 0);
            }
        }

        /// <summary>
        /// it makes no sense to return an async task because the architecture already calls this method form individual threads
        /// async calls must be resolved internally and the method only return ActionResult<T>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getUAsync")]
        //public ActionResult<string> GetUsync(int id)  //correct format
        public async Task<ActionResult<string>> GetUsync(int id) //incorrect format
        {
            WriteLog("Get(U)Async STARTED", id);
           
            //incorrect
            await Task.Delay(15000); //awaits must all be resolved within this method

            //correct
            //Task task1 = Task.Delay(15000);
            //Task task2 = Task.Delay(1000);
            //Task task3 = Task.Delay(1500););
            //Task.WaitAll(task1, task2, task3);

            WriteLog("Get(U)Async FINISHED", id);

            return Ok(id.ToString());
        }

        [HttpGet]
        [Route("getU")]
        public ActionResult<string> GetU(int id)
        {
            WriteLog("Get(U) STARTED", id);

            Task.Delay(15000).Wait();

            WriteLog("Get(U) FINISHED", id);

            return Ok(id.ToString());
        }

        [HttpGet]
        [Route("getA")]
        public ActionResult<string> GetA(int id)
        {
            WriteLog("Get(A) STARTED", id);

            for (int x = 0; x < 15; x++)
            {
                WriteLog("Get(A) LOOP", x);
                Thread.Sleep(1000);
            }

            WriteLog("Get(A) FINISHED", id);

            return Ok(id.ToString());
        }

        [HttpGet]
        [Route("getB")]
        public ActionResult<string> GetB(int id)
        {
            WriteLog("Get(B) STARTED", id);

            for (int x = 0; x < 25; x++)
            {
                WriteLog("Get(B) LOOP", x);
                Thread.Sleep(1000);
            }

            WriteLog("Get(B) FINISHED", id);

            return Ok(id.ToString());
        }

        [HttpGet]
        [Route("getC")]
        public ActionResult<string> GetC(int id)
        {
            WriteLog("GetD STARTED", id);

            for (int x = 0; x < 10; x++)
            {
                WriteLog("Get(C) LOOP", x);
                Thread.Sleep(1000);
            }

            WriteLog("Get(C) FINISHED", id);

            return Ok(id.ToString());
        }


        public void Dispose()
        {
            lock (_locker)
            {
                WriteLog("Controller DISPOSE", 0);
                _instanceCount--;
            }
        }

        private void WriteLog(string desc, int parmId)
        {
            string instance = _instanceId.ToString("00000");
            string instCount = _instanceCount.ToString("00000");
            string parameter = parmId.ToString("00000");
            string threadId = Thread.CurrentThread.ManagedThreadId.ToString("00000");
            string currTime = System.DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss.fff");

            string text = $"{desc};{instance};{instCount};{parameter};{threadId};{currTime}";
            
            System.Console.WriteLine(text);
            //_logger.LogInformation(text);
        }

        //~WeatherForecastController()
        //{
        //    _logger.LogInformation("DESCTRUCTOR:" + _instanceCount.ToString("00000"));
        //}
    }
}