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
            List<Parameter> parameter= context.Parameters.ToList();
            if (parameter.Count == 0)
            {
                return null;
            }
            return parameter;
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
            var paramAux = new Parameter()
            {
                Key = id,
                Value = value
            };
            context.Parameters.Add(paramAux);

            context.SaveChanges();
            return paramAux;

        }

        public Parameter UpdateParameter(string id, string nValue)
        {
            var param = context.Parameters.Find(id);
            if (param != null)
            {
                param.Value= nValue;

                context.SaveChanges();
                return param;
            }
            return null;

        }

        public Parameter DeleteParameter(string id)
        {
            var param = context.Parameters.Find(id);
            if (param != null)
            {
                context.Remove(param);
                context.SaveChanges();
                return param;
            }
            return null;
        }



    }
}
