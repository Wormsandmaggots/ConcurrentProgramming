using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Data
{
    internal class Logger : AbstractLogger
    {
        private readonly string _pathToFile;
        //private static List<IBall> balls;
        private Stopwatch stopWatch = new Stopwatch();
        //XmlSerializer serializer = new XmlSerializer(typeof(IBall));
        private static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        //private readonly Mutex queueMutex = new();

        public Logger(/*List<IBall> balls*/)
        {
            string PathToSave = Path.GetTempPath();
            _pathToFile = PathToSave + "Logging.json";

            if (!File.Exists(_pathToFile))
            {
                FileStream file = File.Create(_pathToFile);
                StreamWriter sw = new StreamWriter(file);
                /*string data = "{\"BallsDiameter\": 20, ": ";
                sw.WriteLine(data);
                sw.Flush();
                sw.Close();*/
            }


            // balls = balls;
        }


        public override void ToQueue(IBall ball)
        {
            stopWatch.Stop();
            TimeSpan timeLasted = stopWatch.Elapsed;
            stopWatch.Start();
            string time = timeLasted.ToString();
            string id = ball.GetHashCode().ToString();

            var ballJson = new
            {
                Xvalue = ball.Position.X.ToString(),
                Yvalue = ball.Position.Y.ToString(),
                TimeElapsed = time.ToString(),
                BallID = id
            };

            /*  XmlDictionary xmlDictionary = new XmlDictionary();

              xmlDictionary.Add(id);
              xmlDictionary.Add(ball);
              xmlDictionary.Add(time);
              */


            string serializedBall = JsonSerializer.Serialize(ballJson);

            queue.Enqueue(serializedBall);
        }

        public override void ToFile()
        {
            Thread t = new Thread(() =>
            {
                //stopWatch.Start();
                while (true)
                {
                    if (/*event na zderzenie*/ stopWatch.ElapsedMilliseconds >= 5)
                    {
                        using (StreamWriter sw = File.AppendText(_pathToFile))
                        {
                            while (queue.TryDequeue(out string line))
                            {
                                sw.WriteLine(line);
                            }
                            sw.Flush();
                            sw.Close();
                            Thread.Sleep(100);
                        }
                    }
                }
            })
            {
                IsBackground = true
            };
            t.Start();
        }



        public void stop()
        {
            stopWatch.Reset();
            stopWatch.Stop();
        }
    }
}
