using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NatureSimulationGen2.Global;

namespace NatureSimulationGen2.Move
{
    public class FlyMovement : IMovement
    {
        private Entity Entity;
        private World World;

        public FlyMovement(Entity entity, World world)
        {
            this.Entity = entity;
            World = world;
        }

        public void Move()
        {
            var entityIntention = Entity.RequestIntention();
            Entity.X += entityIntention.DeltaX;
            Entity.Y += entityIntention.DeltaY;
        }
    }
}
