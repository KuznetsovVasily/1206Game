using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatureSimulationGen2.Move
{
    public interface ICanMove
    {
        IMovement Movement { get; set; }
    }
}
