using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IScene
    {
        abstract void GenerateBallsList(int ballsAmount, int ballsRadius);
        abstract int Width { get;}
        abstract int Height { get; }
        public List<IBallLogic> Balls { get; }
        public bool Enabled { get; set; }



    }
}
