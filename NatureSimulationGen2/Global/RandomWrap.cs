namespace NatureSimulationGen2.Global
{
    public class RandomWrap
    {
        public World World { get; set; }
        public RandomWrap(World world)
        {
            World = world;
        }

        public int GetRandom()
        {
            return RandomHolder.GetInstance().Random.Next(1, World.XMax);
        }

        public int GetRandomHealth()
        {
            return RandomHolder.GetInstance().Random.Next(1, 10);
        }
    }
}
