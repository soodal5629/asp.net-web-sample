using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HelloAspMVC.Models;
using BusinessLayer.DTO;
using BusinessLayer.Services;

namespace HelloAspMVC.Controllers;

[Route("[controller]/[action]")]
public class TestController : Controller
{
    private readonly ILogger<TestController> _logger;
    private ILoginService loginService;

    public TestController(ILogger<TestController> logger, ILoginService service)
    {
        _logger = logger;
        loginService = service;
    }

    public string test()
    {
        return "test test";
    }

    public string test2()
    {
        return "test test22";
    }

    public string test3(string userid, int age)
    {
        return "user id: " + userid + " age: " + age;
    }

    // public string test4()
    // {
    //     // 사용자의 요청에서 쿼리 파라미터 값 얻어오기
    //     string? userid = Request.Query["userid"];
    //     string? age = Request.Query["age"]; // ? 는 null 도 허용한다는 의미
    //     return "user id: " + userid + " age: " + age;
    // }

    // public string test5(Users user)
    // {
    //     string userid = user.UserId;
    //     int age = user.Age;
    //     return "user id: " + userid + " age: " + age + "!!";
    // }

    // IActionResult 은 View 인터페이스 구현체
    public IActionResult testView()
    {
        //return View("~/Views/Home/test.cshtml"); 
        return View(); // 경로 생략도 가능 - 컨트롤러 이름에서 앞부분만 딴 폴더 이름 생성 ex) Views/Test/액션(메서드) 이름.cshtml
    }

    public IActionResult testDataView()
    {
        // 1. ViewData 이용 - 딕셔너리 구조
        ViewData["myMsg"] = "Hello reponse";
        ViewData["num1"] = 100;

        // 2. ViewBag 이용 - 동적으로 속성 추가 가능
        ViewBag.MyTest = "hello myTest";
        ViewBag.num2 = 200;
        ViewBag.arr = new List<string> { "abc", "def" };

        // 3. ViewModel 이용 - return View 파라미터에 넘겨줘야 함
        var numList = new List<int> { 1, 2, 3 };

        return View(numList);
    }

    public IActionResult testForm()
    {
        return View(); // 경로 생략도 가능 - 컨트롤러 이름에서 앞부분만 딴 폴더 이름 생성 ex) Views/Test/액션(메서드) 이름.cshtml
    }

    [HttpPost]
    public IActionResult testForm(string userId, int age)
    {
        Console.WriteLine("userId = " + userId);
        Console.WriteLine("age = " + age);
        return Redirect("/test/testView");
    }

    public IActionResult testNewAspTestUser()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAspTestUser(CreateAspTestDTO request)
    {
        Console.WriteLine("userId = " + request.Userid);
        Console.WriteLine("username = " + request.Username);
        Console.WriteLine("point = " + request.Point);
        // 비즈니스 계층  LoginService
        // 비동기 함수 호출 -> awiat 추가
        await loginService.CreateAspTestUser(request); // 회원가입
        return Redirect("/test/testView");
    }

    // select All
    public async Task<IActionResult> GetAllAspTestUser()
    {
        return View(await loginService.GetAllAspTestUser());
    }

    public IActionResult SearchUserId()
    {
        return View();
    }

    public IActionResult testLoginView()
    {
        ResponseAspTestUserDTO? user = HttpContext.Session.Get<ResponseAspTestUserDTO>("LoginUser");
        var list = new List<string> {};
        if (user != null)
        {
            if (user.Userid != null) { 
                list.Add(user.Userid);    
            }
            if (user.Username != null)
            { 
                list.Add(user.Username);    
            }
        }

        return View(list);
    }

    public async Task<IActionResult> GetAspTestUser(GetAspTestUserDTO request)
    {
        if (!ModelState.IsValid)
        {
            return Redirect("/test/SearchUserId");
        }
        // 로그인한 유저라 가정
        ResponseAspTestUserDTO responseDTO = await loginService.GetAspTestUser(request);
        // 직렬화 객체 <-> json
        // 세션 저장 
        HttpContext.Session.Set("LoginUser", responseDTO); // 파라미터: key, value

        return Redirect("/test/testLoginView");
    }

}
