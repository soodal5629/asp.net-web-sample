using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HelloAspMVC.Models;
using HelloAspMVC.Data;

namespace HelloAspMVC.Controllers;

[Route("[controller]/[action]")]
public class StudentController : Controller
{
    private readonly ILogger<StudentController> _logger;
    private readonly SchoolContext _context;

    public StudentController(ILogger<StudentController> logger, SchoolContext context)
    {
        _logger = logger;
        _context = context;
    }

    public List<Student> list() 
    {
        List<Student> list = _context.Students.Take(5).ToList();
        Console.WriteLine("!!!! ==== ");
        Console.WriteLine(list.Count);
        foreach (Student e in list)
        {
            Console.WriteLine(e.StudentId + ", " + e.Name + ", " + e.Age);
        }
        return list;
    }    

    
}
