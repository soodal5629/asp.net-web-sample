using System.Data;
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
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<List<ResponseAspTestUserDTO>> GetAllAspTestUser()
    {
        try
        {
            // 비동기 함수 호출한 경우 예외 잡기 외해 await 추가
            List<AspTestUser> findList = await loginMapper.GetAllAspTestUser();
            // DTO → Entity 변경
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<AspTestUser, ResponseAspTestUserDTO>());
            Mapper mapper = new Mapper(configuration);
            // C#은 List로 이렇게 entity <-> dto 변경이 가능하구나,,
            List<ResponseAspTestUserDTO> responseList = mapper.Map<List<AspTestUser>, List<ResponseAspTestUserDTO>>(findList);

            return responseList;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<ResponseAspTestUserDTO> GetAspTestUser(GetAspTestUserDTO request)
    {
        try
        {
            // DTO → Entity 변경
            var configuration = new MapperConfiguration(cfg => { }); // 명시적 구성 없이 동적객체 연결 가능
            Mapper mapper = new Mapper(configuration);
            // GetAspTestUserDTO을 딕셔너리 형태의 key-value 형태로 저장됨
            Dictionary<string, object> dc = mapper.Map<GetAspTestUserDTO, Dictionary<string, object>>(request);
            ProcCall procCall = new ProcCall();
            // 프로시저 호출
            DataTable dt = await procCall.RequestProcedure("sp_usertest", dc);
            ResponseAspTestUserDTO response = new ResponseAspTestUserDTO();
            response.Id = (int) dt.Rows[0]["id"];
            response.Userid = dt.Rows[0]["userid"].ToString();
            response.Username = dt.Rows[0]["username"].ToString();
            response.Point = (int) dt.Rows[0]["point"];

            return response;
        }
        catch (Exception)
        {
            throw;
        }

    }
}
