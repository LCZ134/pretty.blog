using Microsoft.AspNetCore.Mvc;
using Octokit;
using Pretty.Services.Githubs;
using Pretty.WebFramework.Controller;
using System.Collections.Generic;

namespace Pretty.Web.Controllers
{
    [Route("api/[controller]")]
    public class GithubController : PrettyController
    {
        private IGitHubService _githubService;
        public GithubController(
            IGitHubService githubService)
        {
            _githubService = githubService;
        }

        public IEnumerable<Repository> Get()
        {
            return _githubService.GetMyselfRepos();
        }
    }
}
