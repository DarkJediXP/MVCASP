using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

namespace Core
{
    public class ContextFactory
    {
        private static readonly string contextKey = typeof(EntitiesModel1).FullName;

        public static EntitiesModel1 GetContextPerRequest()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return new EntitiesModel1();
            }
            else
            {
                EntitiesModel1 context = httpContext.Items[contextKey] as EntitiesModel1;

                if (context == null)
                {
                    context = new EntitiesModel1();
                    httpContext.Items[contextKey] = context;
                }

                return context;
            }
        }
    }
}
