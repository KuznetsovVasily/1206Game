using System.Collections.Generic;
using NatureSimulationGen2.Global;

namespace NatureSimulationGen2.Abiotic
{
    public class Mountain : Abiotic, IBarrier
    {
        public Mountain(int x, int y, World world)
            : base(x, y, world)
        {
        }
        public override List<SurfaceType> GetSurfaces()
        {
            return new List<SurfaceType> { Global.SurfaceType.Ground, Global.SurfaceType.Water };
        }
    }
}
