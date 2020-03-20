using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using UserRegistrationMVC5.Models;
using UserRegistrationMVC5.Repository;

namespace UserRegistrationMVC5
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<UserRegistrationEntities>();
            container.RegisterType<IUsersRepository, UsersRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}