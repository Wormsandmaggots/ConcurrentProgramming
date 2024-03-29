﻿using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;

namespace Data
{
    internal class Logger : AbstractLogger
    {
        private readonly string _pathToFile;
        private Stopwatch stopWatch = new Stopwatch();
        private static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        private SemaphoreSlim _queueSemaphore = new SemaphoreSlim(0);

        public Logger()
        {
            string PathToSave = "../../../../";
            _pathToFile = PathToSave + "Logging.json";

            if (!File.Exists(_pathToFile))
            {
                FileStream file = File.Create(_pathToFile);
                StreamWriter sw = new StreamWriter(file);
            }
        }

        Object _queueLock = new Object();
        public override void ToQueue(IBall ball1, IBall ball2)
        {
            Vector2 pos1 = ball1.Position;
            Vector2 pos2 = ball2.Position;

            stopWatch.Stop();
            TimeSpan timeLasted = stopWatch.Elapsed;

            stopWatch.Start();

            string time = timeLasted.ToString();
            string id1 = ball1.GetHashCode().ToString();
            string id2 = ball2.GetHashCode().ToString();

            var ball1Json = new
            {
                Xvalue = pos1.X.ToString(),
                Yvalue = pos1.Y.ToString(),
                TimeElapsed = time.ToString(),
                BallID = id1
            };

            var ball2Json = new
            {
                Xvalue = pos2.X.ToString(),
                Yvalue = pos2.Y.ToString(),
                TimeElapsed = time.ToString(),
                BallID = id2
            };

            string serializedBall1 = JsonSerializer.Serialize(ball1Json);
            string serializedBall2 = JsonSerializer.Serialize(ball2Json);

            lock(_queueLock)
            {
                queue.Enqueue(serializedBall1);
                queue.Enqueue(serializedBall2);
            }    
            
            _queueSemaphore.Release();
        }

        private Object _lock = new object();

        public override void ToFile()
        {
            Action<Object> action = async (Object state) =>
            {
                while (true)
                {
                    await _queueSemaphore.WaitAsync();
                    
                    lock (_lock)
                    {
                        using (StreamWriter sw = File.AppendText(_pathToFile))
                        {
                            while (queue.TryDequeue(out string line))
                            {
                                sw.WriteLine(line);
                            }

                             Thread.Sleep(100);
                        }
                    }
                }
            };

            ThreadPool.QueueUserWorkItem(new WaitCallback(action));
        }

        public void stop()
        {
            stopWatch.Reset();
            stopWatch.Stop();
        }
    }
}
