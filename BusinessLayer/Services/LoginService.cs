using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.Mappers;
using DataAccessLayer.Models;
namespace BusinessLayer.Services;

public class LoginService : ILoginService // ILoginService 인터페이스 구현
{
    private ILoginMapper loginMapper;
    public LoginService(ILoginMapper mapper)
    {
        loginMapper = mapper;
    }

    // 속성 유효성 검사 및 업무 규칙 적용과 같은 비즈니스 로직 처리
    public async Task CreateAspTestUser(CreateAspTestDTO request)
    {
        try
        {
            // DTO → Entity 변경
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<CreateAspTestDTO, AspTestUser>());
            Mapper mapper = new Mapper(configuration);
            AspTestUser user = mapper.Map<CreateAspTestDTO, AspTestUser>(request);

            // 비동기 함수 호출한 경우 예외 잡기 외해 await 추가
            await loginMapper.Create(user);
        }
        catch(Exception) {
            throw;
        }
        
    }
}
