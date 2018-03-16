using System.Security.Principal;

namespace ProductsService
{
    public interface IAuthorizationServiceAdapter
    {
        bool CanRead(IAuthorizedResource resource, IPrincipal principal);
    }

    public class AuthorizationServiceAdapter : IAuthorizationServiceAdapter
    {
        public bool CanRead(IAuthorizedResource resource, IPrincipal principal)
        {
            return resource.CanRead(principal);
        }
    }
}