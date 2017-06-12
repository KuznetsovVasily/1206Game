using System.Collections.Generic;
using System.Linq;
using NatureSimulationGen2.Global;

namespace NatureSimulationGen2.Plant
{
    [FabricMethod("Plant")]
    public class Oak : Plant
    {
        protected static int RandomDelta { get; set; }
        public Oak(int x, int y, World world, bool isFoodForVegan = true, int health = 5)
            : base(x, y, health, world, isFoodForVegan)
        {
            IsFoodForVegan = true;
        }
        public override List<SurfaceType> GetSurfaces()
        {
            return new List<SurfaceType> { Global.SurfaceType.Ground, Global.SurfaceType.Water };
        }
        public override bool GetBarrier()
        {
            return true;
        }
        public void SetHealth(int health)
        {
            this.Health = health;
        }
        public void SetHealth()
        {
            Health = RandomHolder.GetInstance().Random.Next(1, 10);
        }
        public void SetIsFoodForVegan(bool isFoodForVegan)
        {
            this.IsFoodForVegan = isFoodForVegan;
        }
        public override Intention RequestIntention()
        {
            Health++;
            var objectsAtTheSamePoint = world.GetObjectsAt(X, Y).Where(e => !e.Equals(this));
            if ((objectsAtTheSamePoint as Animal.Animal).ConsumptionType == Animal.ConsumptionType.Vegan)
            {
                Health -= 2;
                return new Intention { DeltaX = 0, DeltaY = 0 };
            }
            return new Intention { DeltaX = 0, DeltaY = 0 };
        }
    }
}