using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingSite.api.Data.DataModels.Entity;
using ShoppingSite.api.Data.DataModels.Model;
using ShoppingSite.api.Data.Services.SqlImplementations;

namespace ShoppingSite.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;
        private readonly IMapper mapper;

        public UsersController(UserService userService, IMapper mapper)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            UserEntity? userEntity = await userService.GetUserByIdAsync(id);
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
    }
}
