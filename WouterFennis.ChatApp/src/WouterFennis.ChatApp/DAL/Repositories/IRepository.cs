using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WouterFennis.ChatApp.DAL.Repositories
{
    public interface IRepository<Entity, Key> : IDisposable
    {
        IEnumerable<Entity> FindAll();

        IEnumerable<Entity> FindBy(Expression<Func<Entity, bool>> filter);

        Entity Find(Key id);

        Key Insert(Entity item);

        void Update(Entity item);

        void Delete(Key id);
    }
}
