using System.Collections.Generic;

namespace NatureSimulationGen2.Global
{
    public abstract class Entity : ICoord, IProduceBehavior
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsBarrier { get; set; }
        protected World world;
        public List<SurfaceType> SurfaceType { get; set; }
        protected Entity(int x, int y, World world)
        {
            X = x;
            Y = y;
            this.world = world;
            IsBarrier = GetBarrier();
        }
        public abstract Intention RequestIntention();
        public abstract List<SurfaceType> GetSurfaces();
        public abstract bool GetBarrier();

        public void SetCoordinates()
        {
            this.X = RandomHolder.GetInstance().Random.Next(1, 14);
            this.Y = RandomHolder.GetInstance().Random.Next(1, 14);
        }


       
    }
}
