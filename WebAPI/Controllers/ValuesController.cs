using Domain.Entities;
using EFInMemory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            //User usuario;
            //var options = new DbContextOptionsBuilder<MemoryDbContext>()
            //   .UseInMemoryDatabase(databaseName: "ns804")
            //   .Options;

            //using (var context = new MemoryDbContext(options))
            //{
            //    var user = new User
            //    {
            //        Id = 1,
            //        Address = new UserAddress { Id = 1 }
            //    };

            //    context.Users.AddAsync(user);
            //    context.SaveChanges();
            //    usuario = context.Users.FirstOrDefault();
            //}
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
