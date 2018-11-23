using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class Repository<TEntity> where TEntity : class
    {
        ScholarHackDBContext context = new ScholarHackDBContext();

        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public object Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public int Insert(TEntity entity)
        {
            try
            {
                context.Set<TEntity>().Add(entity);
            }
            catch (Exception ex)
            {
                string exp = ex.StackTrace;
            }
            return context.SaveChanges();

        }

        public int Update(TEntity entity)
        {
            context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            return context.SaveChanges();
        }

    }
}
