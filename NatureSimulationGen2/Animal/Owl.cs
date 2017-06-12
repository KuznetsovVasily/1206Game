using System.Collections.Generic;
using System.Linq;
using NatureSimulationGen2.Global;
using System;

namespace NatureSimulationGen2.Animal
{
    [FabricMethod("Animal")]
    public class Owl : Animal
    {
        protected static int RandomDelta { get; set; }
        protected int Timer { get; set; }
        public bool Predator { get; set; }
        public Owl(int x, int y, Gender gender, ConsumptionType consumptionType, World world)
            : base(x, y, gender, consumptionType, world)
        {
            Predator = consumptionType == ConsumptionType.Predator;
            this.world = world;
        }
        public override List<SurfaceType> GetSurfaces()
        {
            return new List<SurfaceType> { Global.SurfaceType.Ground, Global.SurfaceType.Water };
        }
        public override int Speed => 1;
        public override bool IsEatable => false;
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
                if (objectsAtTheSamePoint.Any(e => e.GetType() == typeof(Owl) && (Gender != ((Owl)e).Gender)))
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
                                Owl owlMale = new Owl(X, Y, Gender.Female, ConsumptionType, world);
                                world.AddEntity(owlMale);
                            }
                            Owl owlFemale = new Owl(X, Y, Gender.Male, ConsumptionType, world);
                            world.AddEntity(owlFemale);
                            PregnantTimer = 0;
                        }
                    }
                }
            }
        }
    }
}


