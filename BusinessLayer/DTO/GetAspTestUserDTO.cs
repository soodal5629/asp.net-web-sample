using System.ComponentModel.DataAnnotations;
namespace BusinessLayer.DTO;

public class GetAspTestUserDTO
{
    [Required(ErrorMessage = "아이디를 입력해주세요.")] // 값이 반드시 있어야 함 (@NotNull과 비슷)
    [StringLength(50, MinimumLength = 3)] // 최대 길이 50, 최소길이 2
    public string? Userid { get; set; }

    [Required(ErrorMessage = "비밀번호를 입력해주세요.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

}
