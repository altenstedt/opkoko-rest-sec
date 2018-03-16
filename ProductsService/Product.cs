using System;
using System.Security.Claims;
using System.Security.Principal;

namespace ProductsService
{
    public interface IAuthorizedResource
    {
        bool CanRead(IPrincipal principal);
    }

    public class Product : IAuthorizedResource
    {
        public Product(ProductId id)
        {
            Id = id;
        }

        public ProductId Id { get; }

        public string Name => "My Product";

        public bool CanRead(IPrincipal principal)
        {
            return
                ((ClaimsPrincipal) principal).HasClaim(c => c.Type == ClaimSettings.Scope && c.Value.Contains(ClaimSettings.ReadProduct)) &&
                ((ClaimsPrincipal) principal).HasClaim(c => c.Type == ClaimSettings.UrnLocalProductId && string.Equals(c.Value, Id.Value, StringComparison.Ordinal));
        }
    }
}