using System.Diagnostics;

namespace test_log
{
    class Program
    {
        public static Int64 HeavyComputation(string name)
        {
            Console.WriteLine("Start: " + name);
            var timer = new Stopwatch();
            timer.Start();
            Int64 result = 0;
            for (Int64 i = 0; i < 1000000000; i++)
            {
                var a = ((i + 15000) / (i + 30)) * (i + 10);
                result += (a % 10) - 120;
            }
            timer.Stop();
            Console.WriteLine("End: " + name + ' ' + timer.ElapsedMilliseconds);
            return result;
        }

        public static Int64 HeavyComputation2(string name)
        {
            Console.WriteLine("Start: " + name);
            var timer = new Stopwatch();
            timer.Start();
            Int64 result = 0;
            for (Int64 i = 0; i < 10000000; i++)
            {
                var a = ((i + 15000) / (i + 30)) * (i + 10);
                //result += 10;
            }
            timer.Stop();
            Console.WriteLine("End: " + name + ' ' + timer.ElapsedMilliseconds);
            return 10;
        }

        static async Task Main(string[] args)
        {
            // var timer = new Stopwatch();
            // timer.Start();
            // HeavyComputation("A");
            // HeavyComputation("B");
            // HeavyComputation("C");
            // HeavyComputation("D");
            // HeavyComputation("E");
            // timer.Stop();
            // Console.WriteLine("All: " + timer.ElapsedMilliseconds);




            var timer2 = new Stopwatch();
            timer2.Start();
            Parallel.Invoke(
               () => HeavyComputation("A"),
               () => HeavyComputation("B"),
               () => HeavyComputation("C"),
               () => HeavyComputation("D"),
               () => HeavyComputation("E")
            );
            timer2.Stop();
            Console.WriteLine("All: " + timer2.ElapsedMilliseconds);




            //var myData = new ConcurrentBag<Int64>();
            //var timer3 = new Stopwatch();
            //timer3.Start();
            //await Task.Run(() =>
            //{
            //    Parallel.Invoke(
            //        () => { myData.Add(HeavyComputation("A")); },
            //        () => { myData.Add(HeavyComputation("B")); },
            //        () => { myData.Add(HeavyComputation("C")); },
            //        () => { myData.Add(HeavyComputation("D")); },
            //        () => { myData.Add(HeavyComputation("E")); }
            //    );
            //});
            //timer3.Stop();
            //Console.WriteLine("All: " + timer3.ElapsedMilliseconds);
            //Console.WriteLine(string.Join(",", myData));





            //var timer4 = new Stopwatch();
            //timer4.Start();
            //var myData = new ConcurrentBag<Int64>();
            //try
            //{
            //    await Task.Run(() =>
            //    {
            //        Parallel.Invoke(
            //            () => { myData.Add(HeavyComputation("A")); },
            //            () =>
            //            {
            //                Console.WriteLine("Starting B");
            //                throw new Exception("B");
            //                myData.Add(HeavyComputation("B"));
            //            },
            //            () => { myData.Add(HeavyComputation("C")); },
            //            () => { myData.Add(HeavyComputation("D")); },
            //            () => { myData.Add(HeavyComputation("E")); });
            //    });
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Exception: " + e);
            //}

            //timer4.Stop();
            //Console.WriteLine("All: " + timer4.ElapsedMilliseconds);
            //Console.WriteLine(string.Join(",", myData));




            //Int64 finalResult = 0;
            //await Task.Run(() =>
            //{
            //    Parallel.For(0, 20, i =>
            //    {
            //        finalResult += HeavyComputation2(i.ToString());
            //    });
            //});
            //Console.WriteLine($"finalResult: {finalResult}");





            // await fixRaceCondition();
            // Console.WriteLine("\n\n\n");
            // await detectDeadLock();
        }


        static async Task fixRaceCondition()
        {
            Int64 finalResult = 0;
            var syncRoot = new object();
            await Task.Run(() =>
            {
                Parallel.For(0, 20, i =>
                {
                    var localResult = HeavyComputation2(i.ToString());
                    lock (syncRoot)
                    {
                        //one thread at the same time
                        finalResult += localResult;
                    }
                });
            });

            Console.WriteLine($"fixRaceCondition: {finalResult}");
        }

        static async Task detectDeadLock()
        {
            Int64 finalResult = 0;
            var syncRoot = new object();
            await Task.Run(() =>
            {
                Parallel.For(0, 20, i =>
                {
                    Int64 num = HeavyComputation2(i.ToString());
                    object obj = syncRoot;
                    bool lockTaken = false;
                    try
                    {
                        Monitor.Enter(obj, ref lockTaken);
                        finalResult += num;
                    }
                    finally
                    {
                        if (lockTaken)
                        {
                            Monitor.Exit(obj);
                        }
                    }
                });
            });

            Console.WriteLine($"detectDeadLock: {finalResult}");
        }

    }
}