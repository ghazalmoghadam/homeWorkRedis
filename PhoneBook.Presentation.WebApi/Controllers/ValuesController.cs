using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.ApplicationService;
using PhoneBook.Domain.DomainService;
using PhoneBook.DomainModel;
using PhoneBook.Presentation.WebApi.Filters;

namespace PhoneBook.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAppPhoneBookService _appPhoneBookService;
        public ValuesController(IAppPhoneBookService appPhoneBookService)
        {
            _appPhoneBookService = appPhoneBookService;
        }
       
        
        // GET api/values
        [HttpGet]
       // [ReadAuthorizeFilter]
        public IActionResult Get()
        {
            var contacts = _appPhoneBookService.GetAll();
            return Ok(contacts);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ReadAuthorizeFilter]
        public IActionResult Get(int id)
        {
            var contact = _appPhoneBookService.GetBy(id);
            if (contact == null)
                return NotFound();
            return Ok(contact);
        }

        // POST api/values
        [HttpPost]
        [WriteAuthorizeFilter]
        public IActionResult Post([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
             return BadRequest();
           var returnContact= _appPhoneBookService.Add(contact);
            return Ok(returnContact);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _appPhoneBookService.Delete(id);
            return Ok();
        }
        [HttpGet("Search")]
        public IActionResult Search(string searchString)
        {
          var contact= _appPhoneBookService.Search(searchString);
            if (contact == null)
                return NotFound();
            return Ok(contact);
        }
    }
}
