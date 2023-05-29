using Stardeck.Models;
using System.Text.RegularExpressions;

namespace Stardeck.DbAccess
{
    public class PlanetDb
    {

        private readonly StardeckContext context;

        public PlanetDb(StardeckContext context)
        {
            this.context = context;
        }


        public List<Planet> GetAllPlanets()
        {
            List<Planet> planets = context.Planets.ToList();
            if (planets.Count == 0)
            {
                return null;
            }
            return planets;
        }

        public Planet GetPlanet(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var planets = context.Planets.Find(id);

            if (planets == null)
            {
                return null;
            }
            return planets;

        }

        public Planet NewPlanet(Planet planet)
        {

            context.Planets.Add(planet);
            context.SaveChanges();
            return planet;

        }

        public Planet DeletePlanet(string id)
        {
            var planet = context.Planets.Find(id);
            if (planet != null)
            {
                context.Remove(planet);
                context.SaveChanges();
                return planet;
            }
            return null;
        }

    }
}
