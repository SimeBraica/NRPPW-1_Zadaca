using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL {
    public abstract class Repository<T> : IDisposable where T : class {
        public Zadaca1Context Context { get; set; }
        public DbSet<T> Entities { get; set; }

        public Repository(Zadaca1Context context) {
            Context = context;
            Entities = Context.Set<T>();
        }

        public int SaveChanges() {
            return Context.SaveChanges();
        }

        public void Dispose() {
            Context.Dispose();
        }
    }
}