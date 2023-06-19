using Stardeck.DbAccess;
using Stardeck.Models;

namespace Stardeck.Logic
{
    public class ParametersLogic
    {

        private readonly StardeckContext context;
        private readonly ParametersDb parametersDB;

        public ParametersLogic(StardeckContext context)
        {
            this.context = context;
            this.parametersDB = new ParametersDb(context);
        }

        public List<Parameter>? GetAll()
        {
            List<Parameter>? parameters = parametersDB.GetAllParameters();
            if (parameters is null)
            {
                return null;
            }
            return parameters;
        }


        public Parameter? GetParameter(string id)
        {
            var param = parametersDB.GetParameter(id);

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
            parametersDB.NewParameter(paramtAux);
            return paramtAux;

        }

        public Parameter? UpdateParameter(string id, string nValue)
        {
            var param = context.Parameters.Find(id);
            if (param != null)
            {
                param.Value = nValue;

                context.SaveChanges();
                return param;
            }
            return null;

        }
        public Parameter? DeleteParameter(string id)
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

        public CardTypes GetCardType()
        {
            return new CardTypes();
        }

        public class CardTypes
        {
            public int BASIC { get; set; } = 0;
            public int NORMAL { get; set; } = 1;
            public int RARE { get; set; } = 2;
            public int VERY_RARE { get; set; } = 3;
            public int ULTRA_RARE { get; set; } = 4;
        }
    }
}
