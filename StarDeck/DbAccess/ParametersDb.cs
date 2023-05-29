using Stardeck.Models;

namespace Stardeck.DbAccess
{
    public class ParametersDb
    {

        private readonly StardeckContext context;

        public ParametersDb(StardeckContext context)
        {
            this.context = context;
        }

        public List<Parameter>? GetAllParameters()
        {
            List<Parameter> parameters = context.Parameters.ToList();
            if (parameters.Count == 0)
            {
                return null;
            }
            return parameters;
        }

        public Parameter? GetParameter(string id)
        {
            var param = context.Parameters.Find(id);

            if (param == null)
            {
                return null;
            }
            return param;
        }

        public Parameter NewParameter(Parameter param)
        {
            context.Parameters.Add(param);

            context.SaveChanges();
            return param;

        }

    }
}
