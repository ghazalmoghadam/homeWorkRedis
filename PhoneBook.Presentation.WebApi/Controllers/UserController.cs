using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.ApplicationService;
using PhoneBook.Domain.DomainModel.Models;
using PhoneBook.Presentation.WebApi.Filters;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        public UserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }


        // GET api/values
        [HttpGet]
        [ReadAuthorizeFilterAttribute]
        public IActionResult Get()
        {
            var users = _appUserService.GetAll();
            return Ok(users);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ReadAuthorizeFilterAttribute]
        public IActionResult Get(int id)
        {
            var user = _appUserService.GetBy(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/values
        [HttpPost]
        [WriteAuthorizeFilterAttribute]
        public IActionResult Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var returnUser = _appUserService.Add(user);
            return Ok(returnUser);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _appUserService.Delete(id);
            return Ok();
        }

        //Set Cache
        [HttpGet("Login")]
        public IActionResult Login(string userName, string password)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = _appUserService.GetBy(userName, password);
            if (user == null)
                return NotFound("کاربری با این مشخصات در سیستم موجود نیست.");

            Random random = new Random();
            string sessionIdKey= random.Next(9999).ToString();
            HttpContext.Response.Cookies.Append("sessionIdKey", $"{user.Id}");

            using (IRedisClient client = new RedisClient())
            {
                var RoleDictionary = new List<Dictionary<string, string>>();
                
                DateTime expireDateTime = System.DateTime.Now.AddHours(2);

                if (user.CanGetReport)
                {
                    var role = new Dictionary<string,string>();
                    role.Add("CanGetReport", "true");
                    RoleDictionary.Add(role);
                }
                else
                {
                    var role = new Dictionary<string, string>();
                    role.Add("CanGetReport", "false");
                    RoleDictionary.Add(role);   
                }
                client.Set<List<Dictionary<string, string>>>(user.Id.ToString(), RoleDictionary, expireDateTime);

                
                if (user.CanWrite)
                {
                    var role = new Dictionary<string, string>();
                    role.Add("CanWrite", "true");
                    RoleDictionary.Add(role);
                }
                else
                {
                    var role = new Dictionary<string, string>();
                    role.Add("CanWrite", "false");
                    RoleDictionary.Add(role);
                }
                client.Set<List<Dictionary<string, string>>>(user.Id.ToString(), RoleDictionary, expireDateTime);
                

            }//Using Redis

            return Ok(user);
        }

    }
}
