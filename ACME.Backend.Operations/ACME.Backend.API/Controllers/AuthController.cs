using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ACME.Backend.Core.DTO;
using ACME.Backend.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
//using Newtonsoft.Json;

namespace ACME.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IRepositoryWrapper repositoryWrapper,
                IMapper mapper, IConfiguration config)
        {
            _config = config;
            _repo = repositoryWrapper.AuthRepository;
            this._mapper = mapper;
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        //{
        //    userForRegisterDTO.UserName = userForRegisterDTO.UserName.ToLower();
        //    if (await _repo.UserExists(userForRegisterDTO.UserName))
        //        return BadRequest("Username already exists.");

        //    var newUser = new User
        //    {
        //        UserName = userForRegisterDTO.UserName
        //    };
        //    var createdUser = await _repo.Register(newUser, userForRegisterDTO.Password);
        //    return StatusCode(201);
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            var userFromRepo = await _repo.Login(userForLoginDTO.UserName, userForLoginDTO.Password);
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName),
                new Claim(ClaimTypes.Role,userFromRepo.UserRole.RoleName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToReturn = _mapper.Map<UserDetailsToReturnDTO>(userFromRepo);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user = JsonConvert.SerializeObject(userToReturn)
                //user = userToReturn
            });
        }
    }
}
