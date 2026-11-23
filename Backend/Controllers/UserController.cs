using Backend.Repository;
using Backend.Repository.Interfaces;
using Core;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private IUserRepo uRepo;

    public UserController(IUserRepo repo)
    {
        uRepo = repo;
    }
    //definere vores repo som vi har gjort 1000 gange før.
    
    [HttpPost]
    [Route("login/{username}")]
    public User Login(string username) //vi returner som en user objekt så vi kan gemme det i localstorage.
    { //I en ikke mvp situation vil man ikke lave login sådan her; der vil man nok få user/pass som JSON fra body.
        //Vi er altså også ligeglad med password lige nu, da alle er null.
        return uRepo.Login(username);
    }
}