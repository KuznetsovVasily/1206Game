using NatureSimulationGen2.Animal;

using NatureSimulationGen2.Plant;
using System;
using System.Configuration;
using System.Diagnostics;
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
            //CreateEntity(typeof(Owl), builder, (ConfigurationManager.AppSettings["Owl"]));
            CreateEntity(typeof(Rabbit), ConfigurationManager.AppSettings["Rabbit"]);
            //CreateEntity(typeof(Dolphin), builder, (ConfigurationManager.AppSettings["Dolphin"]));
            //CreateEntity(typeof(Oak), builder, (ConfigurationManager.AppSettings["Oak"]));
            //CreateEntity(typeof(Rock), builder, (ConfigurationManager.AppSettings["Rock"]));
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
        public void CreateEntity(Type entityType, string count)
        {
            
            
                var counter = Convert.ToInt32(count);
                var localNumberOfEntity = World.NumberOfEntity;
                while ((localNumberOfEntity + counter) < World.NumberOfEntity)
                {
                    var entity = GetNewAnimal(entityType as Animal.Animal,

                            RandomHolder.GetInstance().Random.Next(1, World.XMax),
                            RandomHolder.GetInstance().Random.Next(1, World.YMax),
                            (Gender)RandomHolder.GetInstance().Random.Next(0, 1),
                            RandomHolder.GetInstance().Random.Next(0, 1) == 0 ? ConsumptionType.Vegan : ConsumptionType.Predator);
                    World.AddEntity(entity);
                }
            
            
        }

        public Animal.Animal GetNewAnimal(Animal.Animal animal, int x, int y, Gender gender, ConsumptionType consumptionType)
        {
            Type typeAnimal = animal.GetType();
            var newAnimal = Activator.CreateInstance(typeAnimal, new object[] {x, y, gender, consumptionType});
            return newAnimal as Animal.Animal;
        }
        
        public Plant.Plant GetNewPlant(Plant.Plant plant, int x, int y, int health)
        {
            Type typePlant = plant.GetType();
            var newPlant = Activator.CreateInstance(typePlant, new object[] { x, y, health});
            return newPlant as Plant.Plant;
        }

        public Abiotic.Abiotic GetNewAbiotic(Abiotic.Abiotic abiotic, int x, int y)
        {
            Type typeAbiotic = abiotic.GetType();
            var newAbiotic = Activator.CreateInstance(typeAbiotic, new object[] { x, y });
            return newAbiotic as Abiotic.Abiotic;
        }

    }
}




















//private void CreatePlant(Type plantType, Builders.Builder builder, int count = 21)
//{
//    var randomWrap = new RandomWrap(World);
//    for (int i = 1; i < count; i++)
//    {
//        var randX = randomWrap.GetRandom();
//        var randY = randomWrap.GetRandom();
//        var typeBuilder = ((PlantBuilder)builder.GetBuilder(plantType))
//            .SetCoordinates(randX, randY)
//            .SetHealth(randomWrap.GetRandomHealth());
//        var entity = typeBuilder.Build();
//        World.AddEntity(entity);
//    }
//}
//public void CreateAnimal(Type animalType, Builders.Builder builder, int count = 21)
//{
//    var randomWrap = new RandomWrap(World);
//    for (int i = 1; i < count; i++)
//    {
//        var randX = randomWrap.GetRandom();
//        var randY = randomWrap.GetRandom();
//        var typeBuilder = ((AnimalBuilder)builder.GetBuilder(animalType))
//               .SetCoordinates(randX, randY)
//               .SetGender(RandomHolder.GetInstance().Random.Next(0, 2) == 1 ? Gender.Female : Gender.Male)
//               .SetHealth(randomWrap.GetRandomHealth());
//        var entity = typeBuilder.Build();
//        World.AddEntity(entity);
//    }
//}
//public void CreateEntity(Type entityType, BuilderHolder builder, string count)
//{
//    try
//    {
//        var counter = Convert.ToInt32(count);
//        for (int i = 1; i < counter; i++)
//        {
//            var typeBuilder = (builder.GetBuilder(entityType));
//            var entity = typeBuilder.Build();
//            World.AddEntity(entity);
//        }
//    }
//    catch
//    {
//        throw new EntityCountException($"Entity {entityType} number from appsettings can't be converted to int32");
//    }
//}


