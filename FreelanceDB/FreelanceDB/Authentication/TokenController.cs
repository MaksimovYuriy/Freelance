using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Authentication
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TokenController : ControllerBase
    {
        //TODO: refresh()- обновлять токен
        //TODO: revoke() - обнулять токен
    }
}
