using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.ComponentModel;

namespace Logic
{
    public interface IBallLogic : INotifyPropertyChanged
    {
        abstract int X { get; set; }
        abstract int Y { get; set; }
        abstract int Radius { get; }

        event PropertyChangedEventHandler? PropertyChanged;

        abstract void MoveBallRandomly(int xBorder, int yBorder, int moveDistance);
    }
}
