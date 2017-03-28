using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TaskManager.Entities;

namespace TaskManager.Managers
{
    public abstract class Manager<TEntity>
    {
        public static TaskEntities db
        {
            get
            {
                return DB.db;
            }
        }


        public Dictionary<int, TEntity> List;

        public abstract int Create(TEntity entity);
        public abstract TEntity Read(int id);
        public abstract void Update(int id, TEntity entity);
        public abstract void Delete(int id);

    }

    public static class DB
    {
        public static TaskEntities db = new TaskEntities();
    }
}
