using NatureSimulationGen2.Global;

namespace NatureSimulationGen2.Plant
{
    public abstract class Plant : Entity
    {
        public int Health { get; set; }
        public bool IsFoodForVegan { get; set; }
        protected Plant(int x, int y, int health, World world, bool isFoodForVegan = true)
            : base(x, y, world)
        {
            IsFoodForVegan = isFoodForVegan;
            Health = health;
            IsBarrier = false;
        }
    }
}
