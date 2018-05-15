using System.Web;
using System.Web.Mvc;

namespace MovieRental
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // To enable global authorization across all pages in our app
            filters.Add(new AuthorizeAttribute());

            // To enable our app to run on secured HTTP channel
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
