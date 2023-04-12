using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IBallModel : INotifyPropertyChanged
    {
        abstract int XHandler { get; set; }
        abstract int YHandler { get; set; }
        abstract int RadiusHandler { get; }

        event PropertyChangedEventHandler? PropertyChanged;

    }
}

