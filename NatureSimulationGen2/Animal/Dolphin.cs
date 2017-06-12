using System;
using System.Collections.Generic;
using System.Linq;
using NatureSimulationGen2.Global;
using NatureSimulationGen2.Move;

namespace NatureSimulationGen2.Animal
{
    public class Dolphin : Animal
    {
        protected static int RandomDelta { get; set; }
        protected int Timer { get; set; }
        public Dolphin(int x, int y, Gender gender, ConsumptionType consumptionType, World world)
            : base(x, y, gender, consumptionType, world)
        {
        }
        public override List<SurfaceType> GetSurfaces()
        {
            return new List<SurfaceType> { Global.SurfaceType.Water };
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
                if (objectsAtTheSamePoint.Any(e => e.GetType() == typeof(Dolphin) && (Gender != ((Dolphin)e).Gender)))
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
                                Dolphin dolphinMale = new Dolphin(X, Y, Gender.Female, ConsumptionType, world);
                                world.AddEntity(dolphinMale);
                            }
                            Dolphin dolphinFemale = new Dolphin(X, Y, Gender.Male, ConsumptionType, world);
                            world.AddEntity(dolphinFemale);
                            PregnantTimer = 0;
                        }
                    }
                }
            }
        }
    }
}
