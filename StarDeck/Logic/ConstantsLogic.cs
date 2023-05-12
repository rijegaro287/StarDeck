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

        public List<Constant> GetAll()
        {
            List<Constant> constant= context.Constants.ToList();
            if (constant.Count == 0)
            {
                return null;
            }
            return constant;
        }


        public Constant GetConstant(string id)
        {
            var constant = context.Constants.Find(id);

            if (constant == null)
            {
                return null;
            }
            return constant;
        }

        public Constant NewConstant(string id, string value)
        {
            var constAux = new Constant()
            {
                Key = id,
                Value = value
            };
            context.Constants.Add(constAux);

            context.SaveChanges();
            return constAux;

        }

    }
}
