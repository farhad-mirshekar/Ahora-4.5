using FM.Portal.Core.Service;
using FM.Portal.Domain;
using Unity;

namespace FM.Portal.FrameWork.Unity.Services
{
  public static  class OrganizationServicesDependencyRegister
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<ICommandService, CommandService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<IPositionService, PositionService>();
            container.RegisterType<IRefreshTokenService, RefreshTokenService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserAddressService, UserAddressService>();
            container.RegisterType<IAuthenticationService, FormsAuthenticationService>();
        }
    }
}
