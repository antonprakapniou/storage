namespace Storage.Core.Repositories;

public sealed class ApiRepository<T>:IApiRepository<T> where T : BaseModel
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

#pragma warning disable CS8603 // Possible null reference return.

        return await query.FirstOrDefaultAsync(expression);

#pragma warning restore CS8603 // Possible null reference return.
    }
    public async Task<T> CreateAsync(T model)
    {
        var result=_t.Add(model);
        await _db.SaveChangesAsync();
        return result.Entity;
    }
    public async Task<T> UpdateAsync(T model)
    {
        var result= _t.Update(model);
        await _db.SaveChangesAsync();
        return result.Entity;
    }
    public async Task<bool> DeleteByAsync(Expression<Func<T, bool>> expression)
    {
        var model = await _t.FirstAsync(expression);
        var result = (_t.Remove(model) is null) ? false : true;
        await _db.SaveChangesAsync();
        return result;
    }
    public async Task<bool> IsContains(Expression<Func<T, bool>>? expression = null) => (expression == null) ? await _t.AnyAsync():await _t.AnyAsync(expression);
    public string GetModelType()=> typeof(T).Name;
}
