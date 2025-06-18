using BusinessLayer.DTO;
namespace BusinessLayer.Services;

public interface ILoginService
{
    // 비동기 함수에서 반환되는 값이 없으면 void 대신에 Task 리턴
    public Task CreateAspTestUser(CreateAspTestDTO request);
}
