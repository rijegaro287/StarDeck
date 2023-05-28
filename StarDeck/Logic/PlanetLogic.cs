using Stardeck.DbAccess;
using Stardeck.Models;
using System.Text.RegularExpressions;

namespace Stardeck.Logic
{
    public class PlanetLogic
    {
        private readonly StardeckContext context;
        private readonly PlanetDb planetDB;

        public PlanetLogic(StardeckContext context)
        {
            this.context = context;
            this.planetDB=new PlanetDb(context);
        }


        public List<Planet> GetAll()
        {
            List<Planet> planets = planetDB.GetAllPlanets();
            if (planets==null)
            {
                return null;
            }
            return planets;
        }


        public Planet GetPlanet(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var planets = planetDB.GetPlanet(id);

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

            planetDB.NewPlanet(planetAux);
            return planetAux;

        }

        public Planet UpdatePlanet(string id, Planet nPlanet)
        {
            var planet = planetDB.GetPlanet(id);
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
            var planet = planetDB.DeletePlanet(id);
            if (planet != null)
            {
                return planet;
            }
            return null;
        }



    }
}