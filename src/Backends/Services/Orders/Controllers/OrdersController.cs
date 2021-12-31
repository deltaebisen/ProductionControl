using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Models;

namespace Orders.Controllers
{
    /// <summary>
    /// 注文管理を行うコントローラです。
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository repository;

       /// <summary>
       /// </summary>
       /// <param name="repository"></param>
        public OrdersController(IOrderRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// 全注文情報を取得します。
        /// </summary>
        /// <returns>取得した全注文情報。</returns>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return Enumerable.Empty<Order>();
        }

    }
}
