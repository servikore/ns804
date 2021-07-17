using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{    
    [Authorize]
    public class UsersController : ApiController
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
        // GET api/<controller>
        public async Task<List<UserDto>> Get()
        {
            var users = await this.userService.GetAll();
            return mapper.Map<List<UserDto>>(users);
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var user = await this.userService.FindById(id);
            
            if (user == null)
                return NotFound();

            return Ok(mapper.Map<UserDto>(user));

        }

        // GET api/<controller>/getbyemail/user@domain.com
        [Route("api/users/getbyemail")]
        public async Task<IHttpActionResult> GetByEmail(string email)
        {
            var user = await this.userService.FindByEmail(email);

            if (user == null)
                return NotFound();

            return Ok(mapper.Map<UserDto>(user));
        }

        [AllowAnonymous]
        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody] User user)
        {
            try
            {
                var registeredUser = await this.userService.Register(user);
                return Created($"/{registeredUser.Id}", mapper.Map<UserDto>(registeredUser));
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // PUT api/<controller>
        public async Task<IHttpActionResult> Put([FromBody] User user)
        {
            try
            {
                await this.userService.Modify(user);
                return Ok();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                await this.userService.Block(id);
                return Ok();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}