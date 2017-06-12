using System;
using System.Collections.Generic;
using NatureSimulationGen2.Animal;
using System.Linq;
using NatureSimulationGen2.Abiotic;
using NatureSimulationGen2.Move;

namespace NatureSimulationGen2.Global
{
    public class World
    {
        public int XMax { get; set; }
        public int YMax { get; set; }
        public World(int xMax, int yMax)
        {
            XMax = xMax - 1;
            YMax = yMax - 1;
        }
        private List<Entity> entityList = new List<Entity>();
        private Dictionary<Tuple<int, int>, SurfaceType> groundTypes = new Dictionary<Tuple<int, int>, SurfaceType>();
        public SurfaceType GetSurfaceTypeAt(int x, int y)
        {
            if (groundTypes.ContainsKey(Tuple.Create(x, y)))
            {
                return groundTypes[Tuple.Create(x, y)];
            }
            return SurfaceType.Ground;
        }
        public void SetSurfaceType(SurfaceType surfaceType, int x, int y)
        {
            groundTypes[Tuple.Create(x, y)] = surfaceType;
        }
        public int GetSurfaceTypeCount()
        {
            return groundTypes.Count;
        }
        public void AddEntity(Entity entity)
        {
            if (entity.GetSurfaces().Contains(GetSurfaceTypeAt(entity.X, entity.Y)))
            {
                entityList.Add(entity);
            }
        }
        public IEnumerable<Entity> GetObjectsAt(int x, int y)
        {
            return entityList.Where(entity => entity.X == x && entity.Y == y);
        }
        public void PreTurn()
        {
            entityList.OfType<Animal.Animal>().ToList().RemoveAll(x => (((Animal.Animal)x).Health) <= 0);
        }
        public void Turn()
        {
            {
                entityList
                    .OfType<ICanMove>()
                    .ToList()
                    .ForEach(e => e.Movement.Move());

                entityList
                   .OfType<ICanEat>()
                   .ToList()
                   .ForEach(e => e.Eat());

                entityList
                   .OfType<ICanReproduce>()
                   .ToList()
                   .ForEach(e => e.Reproduce());
            }
        }






        //foreach (Entity entity in entityList)
        //{
        //    var entityIntention = entity.RequestIntention(this);
        //    var newXCoord = entity.X + entityIntention.DeltaX;
        //    var newYCoord = entity.Y + entityIntention.DeltaY;
        //    var objectsAtTheNewPoint = GetObjectsAt(newXCoord, newYCoord);



        //review: ICanMove (от него можно (хотя не факт что нужно) унаследовать ICanWalk, ICanSwim, ICanFly)
        // interface ICanMove { IMovement Movement; }
        // interface IMovement { void Move() }
        // от Movement унаследовать WalkMovement, SwimMovement, FlyMovement
        // тогда: entity.OfType<ICanMove>.Movement.Move() вместо этого if 
        //if (entity is ICanJustWalkMarker && (!objectsAtTheNewPoint.Any(e => e is IBarrier) && (GetSurfaceTypeAt(newXCoord, newYCoord) == SurfaceType.Ground)))/*!groundTypes.ContainsKey(Tuple.Create(newXCoord, newYCoord))))*/
        //{
        //    if ((newXCoord > XMax) || newYCoord > (YMax) || (newXCoord < 1) || (newYCoord < 1))
        //    {
        //        entity.X = entity.X;
        //        entity.Y = entity.Y;
        //    }
        //    else
        //    {
        //        entity.X += entityIntention.DeltaX;
        //        entity.Y += entityIntention.DeltaY;
        //    }
        //}
        //else if (entity is ICanJustSwimMarker && (GetSurfaceTypeAt(newXCoord, newYCoord) == SurfaceType.Water))
        //{
        //    entity.X += entityIntention.DeltaX;
        //    entity.Y += entityIntention.DeltaY;
        //}


        /*else */
        //if (entity.OfType)
        //{

        //    //entity.Move();
        //}
        //        }
        //    }
        //}


        //Test
        public int NumberOfEntity
        {
            get { return entityList.Count(); }
        }
        public int NumberOfEntityExcludeMountain
        {
            get { return entityList.Count(e => e.GetType() != typeof(Mountain)); }
        }
        public int NumberOfMountain
        {
            get { return entityList.Count(e => e.GetType() == typeof(Mountain)); }
        }
        public int NumberOfOwl
        {
            get { return entityList.Count(e => e.GetType() == typeof(Owl)); }
        }
        public int NumberOfDolphin
        {
            get { return entityList.Count(e => e.GetType() == typeof(Dolphin)); }
        }

        public int NumberOfRabbit
        {
            get { return entityList.Count(e => e.GetType() == typeof(Rabbit)); }
        }

        public int NumberOfRock
        {
            get { return entityList.Count(e => e.GetType() == typeof(Rock)); }
        }

        public void PrintEntityList()
        {
            foreach (var element in entityList)
            {
                Console.WriteLine("Type = {0}, x= {1}, y={2}", element.GetType(), element.X, element.Y);
            }
        }

        public void PrintEntityListExcludeMountain()
        {
            foreach (var entity in entityList)
            {
                if (entity.GetType() != typeof(Mountain))
                {
                    Console.WriteLine("Object type: {0}, x= {1}, y={2}", entity.GetType(), entity.X, entity.Y);
                }
            }
        }
    }
}



