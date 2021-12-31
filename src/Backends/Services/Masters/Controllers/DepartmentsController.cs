using Masters.Infrastructures;
using Masters.Models;
using Microsoft.AspNetCore.Mvc;

namespace Masters.Controllers
{
    /// <summary>
    /// 部門マスタを扱うコントローラです。
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DepartmentsController
    {
        private readonly ILogger<DepartmentsController> logger;

        /// <summary>
        /// </summary>
        /// <param name="logger"></param>
        public DepartmentsController(ILogger<DepartmentsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet(Name = "GetDepartments")]
        public IEnumerable<Department> Get([FromServices] IDepartmentRepository repository)
        {
            return repository.GetAll();
        }
        [HttpGet("{code}", Name = "GetDepartment")]
        public Department? Get(string code, [FromServices] IDepartmentRepository repository)
        {
            return repository.Get(code);
        }
    }
}
