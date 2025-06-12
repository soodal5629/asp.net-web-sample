using BusinessLayer.DTO;
namespace BusinessLayer.Services;

public interface ILoginService
{
    public void CreateAspTestUser(CreateAspTestDTO request);
}
