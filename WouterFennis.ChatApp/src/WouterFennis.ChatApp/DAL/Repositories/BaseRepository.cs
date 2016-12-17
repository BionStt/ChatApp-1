using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WouterFennis.ChatApp.DAL.Repositories
{
    public abstract class BaseRepository<Entity, Key, Context> : IRepository<Entity, Key>
    where Entity : class
    where Context : DbContext
    {
        protected Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        protected abstract DbSet<Entity> GetDbSet();

        protected abstract Key GetKeyFrom(Entity item);

        public virtual bool Exists(Key id)
        {
            bool exists = false;
            if(Find(id) != null)
            {
                exists = true;
            }
            return exists;
        }

        public virtual void Delete(Key id)
        {
            Entity toRemove = Find(id);
            _context.Remove(toRemove);
            _context.SaveChanges();
        }

        public virtual Entity Find(Key id)
        {
            return GetDbSet().SingleOrDefault(a => GetKeyFrom(a).Equals(id));
        }

        public virtual IEnumerable<Entity> FindAll()
        {
            return GetDbSet().ToList();
        }

        public virtual IEnumerable<Entity> FindBy(Expression<Func<Entity, bool>> filter)
        {
            return GetDbSet().Where(filter).ToList();
        }

        public abstract IEnumerable<Entity> FindByIdWithMessages(long id);

        public virtual Key Insert(Entity item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return GetKeyFrom(item);
        }

        public virtual void Update(Entity item)
        {
            _context.Update(item);
            _context.SaveChanges();
        }

        public virtual void Dispose()
        {
            _context?.Dispose();
        }
    }
}
