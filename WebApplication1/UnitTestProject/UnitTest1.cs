using System;
using test;
using Xunit;

namespace UnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var class1 = new Class1();
            var r =await class1.Get();
            Console.WriteLine(r);
        }
    }
}
