using DAL.Interfaces;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services {
	public class CRUDService<TEntity> : ICRUDService<TEntity>
		where TEntity : Entity {
		protected readonly DbContext context;

		public CRUDService(DbContext context) {
			this.context = context;
		}

		public virtual async Task<List<TEntity>> GetAllAsync() {
			return context != null ? await context.Set<TEntity>().ToListAsync() : null;
		}

		public virtual async Task<TEntity> GetOneAsync(int identifier) {
			return context != null ? await context.Set<TEntity>().FindAsync(identifier) : null;
		}

		public virtual async Task<TEntity> PostAsync(TEntity entity) {
			var resEntity = context != null ? (await context.AddAsync<TEntity>(entity)).Entity : null;
			await context.SaveChangesAsync();
			return resEntity;
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entity) {
			var resEntity = context != null ?
				context.Update<TEntity>(entity).Entity :
				null;
			await context.SaveChangesAsync();
			return resEntity;
		}

		public virtual async Task<bool> TryDeleteAsync(int identifier) {
			if (context != null) {
				var entity = context.Find<TEntity>(identifier);
				if (entity != null) {
					context.Remove<TEntity>(entity);
					await context.SaveChangesAsync();
					return true;
				}
			}

			await context.SaveChangesAsync();
			return false;
		}
	}
}