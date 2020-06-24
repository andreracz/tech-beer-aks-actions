using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tech_beer_aks_actions.Repository;

namespace tech_beer_aks_actions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemController : ControllerBase
    {

        private readonly ILogger<TodoItemController> _logger;
		private readonly TodoItemRepository _repository;

        public TodoItemController(ILogger<TodoItemController> logger, TodoItemRepository repository)
        {
            _logger = logger;
			_repository = repository;
        }

        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            return _repository.Itens.ToList();
        }

		[HttpPost]
        public IEnumerable<TodoItem> Adicionar(TodoItem item)
        {
			_repository.Itens.Add(item);
			_repository.SaveChanges();
            return _repository.Itens.ToList();
        }
    }
}
