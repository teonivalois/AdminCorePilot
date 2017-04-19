using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("api/[controller]")]
    public class SampleController : Controller
    {
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Get(int page = 0, int pageSize = 25, string search = null, string searchColumn = null, string orderBy = null, bool orderByAscending = true)
		{
            #region Do this with your data. This is only a sample!
            string rawData = await new System.Net.Http.HttpClient().GetStringAsync("https://jsonplaceholder.typicode.com/posts");
            IQueryable<Post> data = Newtonsoft.Json.JsonConvert.DeserializeObject<Post[]>(rawData).AsQueryable();
            #endregion

			string generalSearch = string.IsNullOrEmpty(searchColumn) ? search : "";

			if (!string.IsNullOrEmpty(search))
			{
				if (string.IsNullOrEmpty(searchColumn))
					data = data.Where(_ => _.title != null && _.title.ToLower().Contains(search.ToLower()));
				else
					data = data.Where($"{searchColumn} != null && {searchColumn}.ToLower().Contains(@0)", search.ToLower());
			}

			if (string.IsNullOrEmpty(orderBy))
				data = data.OrderBy(_ => _.title);
			else
				data = data.OrderBy($"{orderBy} {(orderByAscending ? "asc" : "desc")}");

			return Ok(new
			{
				data = data.Skip(page * pageSize).Take(pageSize).ToList(),
				count = data.Count()
			});
		}
    }

    class Post {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
