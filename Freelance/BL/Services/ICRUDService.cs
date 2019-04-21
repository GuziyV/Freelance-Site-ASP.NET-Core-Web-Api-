using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace DAL.Interfaces {
	public interface ICRUDService<TEntity>
		where TEntity : Entity {
		Task<List<TEntity>> GetAllAsync();
		Task<TEntity> GetOneAsync(int identifier);
		Task<TEntity> UpdateAsync(TEntity entity);
		Task<bool> TryDeleteAsync(int identifier);
		Task<TEntity> PostAsync(TEntity entity);
	}
}