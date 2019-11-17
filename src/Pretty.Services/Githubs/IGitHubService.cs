using Octokit;
using System.Collections.Generic;

namespace Pretty.Services.Githubs
{
    public interface IGitHubService
    {
        IEnumerable<Repository> GetRepository(string nickname);

        IEnumerable<Repository> GetMyselfRepos();
    }
}
