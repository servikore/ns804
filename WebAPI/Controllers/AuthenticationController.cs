using Domain.Repositories;
using System.Threading.Tasks;
using System.Web.Http;

using WebAPI.App_Start;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly IUserRepository userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/authentication/login")]
        public async Task<IHttpActionResult> Login(LoginDto loginDto)
        {
            var user = await userRepository.GetByEmail(loginDto.Email);
            if (user == null)
                return Unauthorized();

            if(user.Password != loginDto.Password)
                return Unauthorized();

            if (user.IsBlocked)
                return Unauthorized();

            var token = new AuthHelper().GenerateTokenJwt(user);
            
            return Ok(new { token = token });
        }
    }
}