using NatureSimulationGen2.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatureSimulationGen2.Move
{
    public class WalkMovement : IMovement
    {
        private Entity Entity;
        private World World;

        public WalkMovement(Entity entity, World world)
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

            if ((newXCoord > World.XMax) || newYCoord > (World.YMax) || (newXCoord < 1) || (newYCoord < 1))
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
