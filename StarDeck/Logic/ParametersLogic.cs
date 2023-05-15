using Stardeck.Models;
using System.Text.RegularExpressions;

namespace Stardeck.Logic
{
    public class ParametersLogic
    {

        private readonly StardeckContext context;

        public ParametersLogic(StardeckContext context)
        {
            this.context = context;
        }

        public List<Parameter> GetAll()
        {
            List<Parameter> parameters= context.Parameters.ToList();
            if (parameters.Count == 0)
            {
                return null;
            }
            return parameters;
        }


        public Parameter GetParameter(string id)
        {
            var param = context.Parameters.Find(id);

            if (param == null)
            {
                return null;
            }
            return param;
        }

        public Parameter NewParameter(string id, string value)
        {
            var paramtAux = new Parameter()
            {
                Key = id,
                Value = value
            };
            context.Parameters.Add(paramtAux);

            context.SaveChanges();
            return paramtAux;

        }

    }
}
