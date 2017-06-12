using NatureSimulationGen2.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatureSimulationGen2.Move
{
    public class SwimMovement : IMovement
    {
        private Entity Entity;
        private World World;

        public SwimMovement(Entity entity, World world)
        {
            this.Entity = entity;
            World = world;
        }

        public void Move()
        {
            var entityIntention = Entity.RequestIntention();
            var newXCoord = Entity.X + entityIntention.DeltaX;
            var newYCoord = Entity.Y + entityIntention.DeltaY;
            var objectsAtTheNewPoint = World.GetObjectsAt(newXCoord, newYCoord);

            if (World.GetSurfaceTypeAt(newXCoord, newYCoord) == SurfaceType.Water)
            {
                Entity.X = Entity.X;
                Entity.Y = Entity.Y;
            }
            else
            {
                Entity.X += entityIntention.DeltaX;
                Entity.Y += entityIntention.DeltaY;
            }
        }
    }
}
