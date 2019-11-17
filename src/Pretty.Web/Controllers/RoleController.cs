using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pretty.Core.Domain.Roles;
using Pretty.Services.Dto;
using Pretty.Services.Roles.Dto;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories;
using Pretty.WebFramework.Factories.Interface;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;

namespace Pretty.Web.Controllers
{
    //[Route("api/[controller]/[action]")]
    public class RoleController : PrettyController
    {

        private IRoleModelFactory _roleModelFactory;

        public RoleController(IRoleModelFactory roleModelFactory)
        {
            _roleModelFactory = roleModelFactory;
        }

        [HttpPost]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> Post(RoleInsertModel model)
        {
            return _roleModelFactory.InsertRole(model);
        }

        [HttpPatch]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> Patch(RoleUpdataModel model)
        {
            return _roleModelFactory.UpdateRole(model);
        }

        [HttpGet]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public Paged<RolesDto> Get(Pagingable command)
        {
            return _roleModelFactory.GetAll(command);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> Delete(string id)
        {
            return _roleModelFactory.DeleteRole(id);
        }
    }
}