using Unity;
using System.Web.Mvc;
using FM.Portal.FrameWork.Unity.DataSources;
using FM.Portal.FrameWork.Unity.Services;
using FM.Portal.FrameWork.Unity.General;

namespace FM.Portal.FrameWork.Unity
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            #region General
            GeneralDependencyRegister.RegisterType(container);
            #endregion

            #region DataSource
            ApplicationDataSourceDependencyRegister.RegisterType(container);
            OrganizationDataSourceDependencyRegister.RegisterType(container);
            PublicDataSourceDependencyRegister.RegisterType(container);
            PortalDataSourceDependencyRegister.RegisterType(container);
            #endregion

            #region Services
            ApplicationServicesDependencyRegister.RegisterType(container);
            OrganizationServicesDependencyRegister.RegisterType(container);
            PublicServicesDependencyRegister.RegisterType(container);
            PortalServicesDependencyRegister.RegisterType(container);
            #endregion

         

            #region Factories
            FactoriesDependency.RegisterType(container);
            #endregion

          DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
