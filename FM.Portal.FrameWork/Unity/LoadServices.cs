using FM.Portal.FrameWork.Unity.DataSources;
using FM.Portal.FrameWork.Unity.General;
using FM.Portal.FrameWork.Unity.Services;
using Unity;

namespace FM.Portal.FrameWork.Unity
{
   public class LoadServices
    {
        private readonly UnityContainer container;
        public LoadServices(UnityContainer container)
        {
            this.container = container;
        }
        public UnityContainer Load()
        {
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

            #region General
            GeneralDependencyRegister.RegisterType(container);
            #endregion

            return container;
        }
    }
}
