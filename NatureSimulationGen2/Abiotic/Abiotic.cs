using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NatureSimulationGen2.Global;

namespace NatureSimulationGen2.Abiotic
{
    public abstract class Abiotic : Entity
    {
        protected Abiotic(int x, int y, World world) 
            : base(x, y, world)
        {
        }

        public override Intention RequestIntention()
        {
            return new Intention { DeltaX = 0, DeltaY = 0 };
        }

        public override bool GetBarrier()
        {
            return true;
        }

    }
}
