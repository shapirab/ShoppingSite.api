using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingSite.api.Data.DataModels.Entity;
using ShoppingSite.api.Data.DataModels.Model;
using ShoppingSite.api.Data.Services.Interfaces;
using ShoppingSite.api.Data.Services.SqlImplementations;

namespace ShoppingSite.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            IEnumerable<UserEntity> userEntities = await userService.GetAllUsersAsync();
            return Ok(mapper.Map<User>(userEntities));
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            UserEntity? userEntity = await userService.GetUserByIdAsync(id);
            if (userEntity == null)
            {
                return NotFound("User with this id was not found");
            }
            return Ok(mapper.Map<User>(userEntity));
        }

        [HttpGet("{sub}")]
        public async Task<ActionResult<User>> GetUserBySub(string sub)
        {
            UserEntity? userEntity = await userService.GetUserBySub(sub);
            if (userEntity == null)
            {
                return NotFound("User with this id was not found");
            }
            return Ok(mapper.Map<User>(userEntity));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddUser(User user)
        {
            UserEntity userEntity = mapper.Map<UserEntity>(user);
            await userService.AddUserAsync(userEntity);
            return Ok(await userService.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            UserEntity? userEntity = await userService.GetUserByIdAsync(id);
            if (userEntity == null)
            {
                return BadRequest("User id was not found");
            }
            await userService.DeleteUserAsync(id);
            return Ok(await userService.SaveChangesAsync());
        }
    }
}
