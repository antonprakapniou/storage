namespace Storage.Core.Repositories
{
    public sealed class ApiRepository<T>:IApiRepository<T> where T : class
    {
        private readonly ApiDbContext _db;
        private readonly DbSet<T> _t;

        public ApiRepository(ApiDbContext db)
        {
            _db=db;
            _t=_db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>>? expression = null)
        {
            IQueryable<T> query = _t;
            query=(expression is null)?query:query.Where(expression);
            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<T> GetOneByAsync(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _t;
            return await query.FirstAsync(expression);
        }
        public async Task CreateAsync(T model)
        {
            _t.Add(model);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(T model)
        {
            _t.Update(model);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(T model)
        {
            _t.Remove(model);
            await _db.SaveChangesAsync();
        }
        public string GetModelType()=> _t.GetType().Name;
    }
}
