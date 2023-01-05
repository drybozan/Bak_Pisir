using BakPisir.DTO.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BakPisir.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Tabloları profilleriyle eşleştirir. Profiles klasöründe tanıttığım her profili burda belirtiyorum
            AutoMapper.Mapper.Initialize(cfg =>
            {
                //mapping tables

                cfg.AddProfile<RequestProfile>();
                cfg.AddProfile<RecipeProfile>();
                cfg.AddProfile<CommentProfile>();
                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<SubCategoryProfile>();
                cfg.AddProfile<StepProfile>();
                cfg.AddProfile<UserProfile>();
               
            });
        }
    }
}
