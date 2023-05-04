using Stardeck.Models;
using System.Text.RegularExpressions;

namespace Stardeck.Logic
{
    public class PlanetLogic
    {
        private readonly StardeckContext context;

        public PlanetLogic(StardeckContext context)
        {
            this.context = context;
        }


        public List<Planet> GetAll()
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

        public Planet NewPlanet(PlanetImage planet)
        {
            var planetAux = new Planet()
            {
                Id = planet.Id,
                Name = planet.Name,
                Type = planet.Type,
                Image = Convert.FromBase64String(planet.Image.ToString()),
                Active = planet.Active,
                Description = planet.Description,
                Ability = planet.Ability

            };

            while (!Regex.IsMatch(planetAux.Id, @"^P-[a-zA-Z0-9]{12}"))
            {
                planetAux.Id = string.Concat("P-", System.Guid.NewGuid().ToString().Replace("-", "").AsSpan(0, 12));
            }

            context.Planets.Add(planetAux);
            context.SaveChanges();
            return planetAux;

        }

        public Planet UpdatePlanet(string id, Planet nPlanet)
        {
            var planet = context.Planets.Find(id);
            if (planet != null)
            {
                planet.Id = nPlanet.Id;
                planet.Name = nPlanet.Name;
                planet.Type = nPlanet.Type;
                planet.Active = nPlanet.Active;
                planet.Ability = nPlanet.Ability;
                planet.Image = nPlanet.Image;
                planet.Description = nPlanet.Description;

                context.SaveChanges();
                return planet;
            }
            return null;

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