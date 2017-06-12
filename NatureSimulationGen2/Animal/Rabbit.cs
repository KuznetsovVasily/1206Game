using System;
using System.Collections.Generic;
using System.Linq;
using NatureSimulationGen2.Global;

namespace NatureSimulationGen2.Animal
{
    [FabricMethod("Animal")]
    public class Rabbit : Animal, ICanEat, ICanReproduce
    {
        protected static int RandomDelta { get; set; }
        protected int Timer { get; set; }
        public Rabbit(int x, int y, Gender gender, ConsumptionType consumptionType, World world)
            : base(x, y, gender, consumptionType, world)
        {
            
        }
        public override List<SurfaceType> GetSurfaces()
        {
            return new List<SurfaceType> { Global.SurfaceType.Ground };
        }
        public override int Speed => 1;
        public override bool IsEatable => true;
        public override Intention RequestIntention()
        {
          
            RandomDelta = RandomHolder.GetInstance().Random.Next(-1, 2);
            while (RandomDelta == 0)
            {
                RandomDelta = RandomHolder.GetInstance().Random.Next(-1, 2);
            }
            var rand = RandomHolder.GetInstance().Random.Next(2);
            if (rand == 0)
            {
                return new Intention { DeltaX = 0, DeltaY = (RandomDelta * Speed) };
            }
            return new Intention { DeltaX = (RandomDelta * Speed), DeltaY = 0 };
        }

        public void Eat()
        {
            var objectsAtTheSamePoint = world.GetObjectsAt(X, Y).Where(e => !e.Equals(this));
            if (Timer % 3 == 0 && objectsAtTheSamePoint is Plant.Plant)
            {
                if (objectsAtTheSamePoint.Any(e => ((Plant.Plant)e).IsFoodForVegan = true))
                {
                    Timer++;
                    Health++;
                }
            }
        }

        public void Reproduce()
        {
            var objectsAtTheSamePoint = world.GetObjectsAt(X, Y).Where(e => !e.Equals(this));
            if (Timer % 2 == 0)
            {
                if (objectsAtTheSamePoint.Any(e => e.GetType() == typeof(Rabbit) && (Gender != ((Rabbit)e).Gender)))
                {
                    Timer++;
                    if (Gender == Gender.Female && PregnantTimer > 0)
                    {
                        PregnantTimer++;
                        if (PregnantTimer == 4)
                        {
                            Random randomForGender = new Random();
                            if (randomForGender.Next(2) == 1)
                            {
                                Rabbit rabbitMale = new Rabbit(X, Y, Gender.Female, ConsumptionType, world);
                                world.AddEntity(rabbitMale);
                            }
                            Rabbit rabbitFemale = new Rabbit(X, Y, Gender.Male, ConsumptionType, world);
                            world.AddEntity(rabbitFemale);
                            PregnantTimer = 0;
                        }
                    }
                }
            }
        }
    }
}