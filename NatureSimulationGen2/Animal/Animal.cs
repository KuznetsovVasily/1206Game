using NatureSimulationGen2.Global;

namespace NatureSimulationGen2.Animal
{
    public abstract class Animal : Entity
    {
        public abstract int Speed { get; }
        public int Health { get; set; }
        public abstract bool IsEatable { get; }
        public Gender Gender { get; set; }
        public int PregnantTimer { get; set; }
        public ConsumptionType ConsumptionType{get;set;}
        protected Animal(int x, int y, Gender gender, ConsumptionType consumptionType,  World world, int health = 5)
            : base(x, y, world)
        {
            ConsumptionType = consumptionType;
            Health = health;
            Gender = gender;
        }
        public override bool GetBarrier()
        {
            return false;
        }

        public void SetHealth()
        {
            Health = RandomHolder.GetInstance().Random.Next(1, 14);
        }

        public void SetHealth(int health)
        {
            this.Health = health;
        }
        public void SetGender(Gender gender)
        {
            this.Gender = gender;
        }

        public void SetGender()
        {
            Gender = RandomHolder.GetInstance().Random.Next(0, 2) == 0 ? Gender.Male : Gender.Female;
        }
    }
}
