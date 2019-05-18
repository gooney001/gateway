using System;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    public class Class1
    {
        public async Task<string> Get()
        {
            var info = $"----------->1:{Thread.CurrentThread.ManagedThreadId}";
            Task<string> task1 = TaskCaller1();
            Task<string> task2 = TaskCaller2();
            var infoTaskRunning = $"---------->2:{Thread.CurrentThread.ManagedThreadId}";
            var infoTask1 = await task1;

            var infoTaskFinished1 = $"-------->3:{Thread.CurrentThread.ManagedThreadId}";
            var infoTask2 = await task2;

            var infoTaskFinished2 = $"-------->4:{Thread.CurrentThread.ManagedThreadId}";
            var result = $"{info},{infoTask1},{infoTask2},{infoTaskRunning},{infoTaskFinished1},{infoTaskFinished2}}}";
            Console.WriteLine(result);
            return result;
        }
        private async Task<string> TaskCaller1()
        {
            await Task.Delay(2500);
            return $"--------->5:{Thread.CurrentThread.ManagedThreadId}";
        }
        private async Task<string> TaskCaller2()
        {
            await Task.Delay(500);
            return $"---------6:{Thread.CurrentThread.ManagedThreadId}";
        }
    }
}
