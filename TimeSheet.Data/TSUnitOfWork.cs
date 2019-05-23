using TimeSheet.Data.Repository.EF;

namespace TimeSheet.Data
{
    /// <summary>
    /// TSUnitOfWork class is a unit of work for manipulating about utility data in database via repository.
    /// </summary>
    public class TSUnitOfWork : EfUnitOfWork<TSContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TSUnitOfWork" /> class.
        /// </summary>
        /// <param name="dsDbContext">The TimeSheet database context what inherits from DbContext of EF.</param>
        public TSUnitOfWork(TSContext tsDbContext) : base(tsDbContext)
        { }
    }
}
