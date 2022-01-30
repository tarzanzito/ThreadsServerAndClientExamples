using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
					
public class Program
{
	public static void Main()
	{
		var task1 = Task.Factory.StartNew(()=> {
			Console.WriteLine("task1 body");
			var innerTask1 = Task.Factory.StartNew(async()=> {
				Console.WriteLine("inner task1 body");
				//Thread.Sleep(sleepMilliseconds);
				await Task.Delay(TimeSpan.FromMilliseconds(1000));
				Console.WriteLine("inner task1 Finished");
			}).Unwrap();
			innerTask1.Wait();
		}, TaskCreationOptions.LongRunning);
		
		var task2 = Task.Factory.StartNew(()=> {
			Console.WriteLine("task2 body");
			var innerTask2 = Task.Factory.StartNew(async()=> {
				Console.WriteLine("inner task2 body");
				//Thread.Sleep(sleepMilliseconds);
				await Task.Delay(TimeSpan.FromMilliseconds(2000));
				Console.WriteLine("inner task2 Finished");
			});
			innerTask2.Wait();
		}, TaskCreationOptions.LongRunning);
		
		Task.WaitAll(new []{task1,task2});
		
		Console.WriteLine(task1.Status);
		Console.WriteLine(task2.Status);
		System.Threading.Thread.Sleep(5000);
		Console.ReadLine();
	}
}