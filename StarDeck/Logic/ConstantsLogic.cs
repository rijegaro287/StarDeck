using Stardeck.Models;
using System.Text.RegularExpressions;

namespace Stardeck.Logic
{
    public class ConstantsLogic
    {

        private readonly StardeckContext context;

        public ConstantsLogic(StardeckContext context)
        {
            this.context = context;
        }

        public List<Parameter> GetAll()
        {
            List<Parameter> constant= context.Parameters.ToList();
            if (constant.Count == 0)
            {
                return null;
            }
            return constant;
        }


        public Parameter GetConstant(string id)
        {
            var constant = context.Parameters.Find(id);

            if (constant == null)
            {
                return null;
            }
            return constant;
        }

        public Parameter NewConstant(string id, string value)
        {
            var constAux = new Parameter()
            {
                Key = id,
                Value = value
            };
            context.Parameters.Add(constAux);

            context.SaveChanges();
            return constAux;

        }

    }
}
