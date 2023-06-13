using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class AbstractLogger
    {
        public abstract void ToQueue(IBall ball1, IBall ball2);
        public abstract void ToFile();
        
        public static AbstractLogger CreateLogger()
        {
            return new Logger();
        }
    }
}
