using DataAccessLayer.Models;

namespace DataAccessLayer.Mappers
{
    public interface ILoginMapper
    {
        Task<AspTestUser> Create(AspTestUser user);
    }
}