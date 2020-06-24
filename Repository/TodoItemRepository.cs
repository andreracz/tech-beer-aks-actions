


namespace tech_beer_aks_actions.Repository {

	using System.Collections.Generic;
	using Microsoft.EntityFrameworkCore;
	
	public class TodoItemRepository : DbContext {

		public TodoItemRepository(DbContextOptions<TodoItemRepository> options)
			: base(options)
		{ }

		public DbSet<TodoItem> Itens { get; set; }

	}


}