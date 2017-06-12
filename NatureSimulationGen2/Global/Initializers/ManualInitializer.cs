using NatureSimulationGen2.Animal;

using NatureSimulationGen2.Plant;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using NatureSimulationGen2.Abiotic;


namespace NatureSimulationGen2.Global.Initialize
{
    public class ManualInitializer
    {
        public World World { get; set; }
        public ManualInitializer(World world)
        {
            World = world;
        }
        public World InitializeWorld()
        {
            BorderInitialize();
            CreateWater(10);
            return World;
        }
        public void CreateWater(int range = 5)
        {
            var randomWrap = new RandomWrap(World);
            var randXWaterStart = randomWrap.GetRandom();
            var randYWaterStart = randomWrap.GetRandom();
            var perimetr = randomWrap.GetRandom();
            for (int i = (randXWaterStart - perimetr / 2); i < (randXWaterStart + perimetr / 2) && i > 0 && i < World.XMax; i++)
            {
                for (int j = (randYWaterStart - perimetr / 2); j < (randYWaterStart + perimetr / 2) && j > 0 && j < World.YMax; j++)
                {
                    World.SetSurfaceType(SurfaceType.Water, i, j);
                }
            }
            var waterPercent = (World.XMax * World.YMax * 45 / 100);
            while (World.GetSurfaceTypeCount() < waterPercent)
            {
                CreateWater();
            }
        }
        public void BorderInitialize()
        {
            for (int i = 0; i < World.XMax; i++)
            {
                for (int j = 0; j < World.YMax; j++)
                {
                    if (i == 0 || i == World.XMax || j == 0 || j == World.YMax)
                    {
                        World.AddEntity(new Mountain(i, j, World));
                    }
                }
            }
        }

        public Func<Type, Entity> GetGeneratorForType(Type entityType)
        {
            var _entityGenerators = new Dictionary<string, Func<Type, Entity>>
            {
                {"Animal", GetNewAnimal},
                {"Plant", GetNewPlant},
                {"Abiotic", GetNewAbiotic}
            };

            var atts = Attribute.GetCustomAttributes(entityType, typeof(FabricMethodAttribute))
                .OfType<FabricMethodAttribute>();
            if (!atts.Any())
            {
                throw new TypeInitializationException(
                    $"Entity type {entityType.Name} lacks FabricMethod attribute, please provide one"
                    , null);
            }

            var fmAttribute = atts.First();
            if (!_entityGenerators.ContainsKey(fmAttribute.Name))
            {
                throw new TypeInitializationException(
                    $"Entity type {entityType.Name} lacks fabric method implemented or contains wrong fabric method definition"
                    , null);
            }

            return _entityGenerators[fmAttribute.Name];


        }
        
        
        
        public void GenerateRandomEntities(Type entityType, string count)
        {
                var counter = Convert.ToInt32(count);
                var localNumberOfEntity = World.NumberOfEntity;
                while (localNumberOfEntity + counter < World.NumberOfEntity)
                {
                    var entity = GetGeneratorForType(entityType)(entityType);
                    World.AddEntity(entity);
                }


        }

        public Animal.Animal GetNewAnimal(Type animalType)
        {
            var x = RandomHolder.GetInstance().Random.Next(1, World.XMax);
            var y = RandomHolder.GetInstance().Random.Next(1, World.XMax);
            var gender = (Gender) RandomHolder.GetInstance().Random.Next(0, 1);
            var consumptionType = RandomHolder.GetInstance().Random.Next(0, 1) == 0 ? ConsumptionType.Vegan : ConsumptionType.Predator;
            var newAnimal = Activator.CreateInstance(animalType, x, y, gender, consumptionType);
            return newAnimal as Animal.Animal;
        }

        public Plant.Plant GetNewPlant(Type plantType)
        {
            var x = RandomHolder.GetInstance().Random.Next(1, World.XMax);
            var y = RandomHolder.GetInstance().Random.Next(1, World.XMax);
            var health = RandomHolder.GetInstance().Random.Next(1, Plant.Plant.MaxHealth);
            
            var newPlant = Activator.CreateInstance(plantType, x, y, health);
            return newPlant as Plant.Plant;
        }

        public Abiotic.Abiotic GetNewAbiotic(Type abioticType)
        {
            var x = RandomHolder.GetInstance().Random.Next(1, World.XMax);
            var y = RandomHolder.GetInstance().Random.Next(1, World.XMax);
            var newAbiotic = Activator.CreateInstance(abioticType, x, y);
            return newAbiotic as Abiotic.Abiotic;
        }

    }
}


 
