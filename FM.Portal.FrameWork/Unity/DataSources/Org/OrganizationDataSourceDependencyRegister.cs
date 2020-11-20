using FM.Portal.DataSource;
using FM.Portal.Infrastructure.DAL;
using Unity;

namespace FM.Portal.FrameWork.Unity.DataSources
{
  public static  class OrganizationDataSourceDependencyRegister
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<ICommandDataSource, CommandDataSource>();
            container.RegisterType<IDepartmentDataSource, DepartmentDataSource>();
            container.RegisterType<IPositionDataSource, PositionDataSource>();
            container.RegisterType<IRefreshTokenDataSource, RefreshTokenDataSource>();
            container.RegisterType<IRoleDataSource, RoleDataSource>();
            container.RegisterType<IUserDataSource, UserDataSource>();
            container.RegisterType<IUserAddressDataSource, UserAddressDataSource>();
        }
    }
}
