using Masters.Models;

namespace Masters.Infrastructures
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MasterDbContext dbContext;

        public DepartmentRepository(MasterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Department> GetAll()
        {
            var histories = dbContext.DepartmentHistories
                .GroupBy(h => h.DepartmentCode)
                .Select(h => new { Code = h.Key, Revision = h.Max(m => m.Revision) })
                .Join(dbContext.DepartmentHistories, summary => new { summary.Code, summary.Revision }, h => new { Code = h.DepartmentCode, h.Revision }, (summary, history) => history)
                ?? Enumerable.Empty<DepartmentHistory>();
            if ( !histories.Any() )
            {
                return Enumerable.Empty<Department>();
            }
            return histories
                .Select(h => new Department(h.DepartmentCode, h.Name))
                .OrderBy(d => d.Name);
        }

        public Department? Get(string departmentCode)
        {
            var histories = dbContext.DepartmentHistories
                .Where(h => h.DepartmentCode == departmentCode)
                .GroupBy(h => h.DepartmentCode)
                .Select(h => new { Code = h.Key, Revision = h.Max(m => m.Revision) })
                .Join(dbContext.DepartmentHistories, summary => new { summary.Code, summary.Revision }, h => new { Code = h.DepartmentCode, h.Revision }, (summary, history) => history)
                ?? Enumerable.Empty<DepartmentHistory>();
            if ( !histories.Any() )
            {
                return default;
            }
            return histories
                .Select(h => new Department(h.DepartmentCode, h.Name))
                .Single();
        }

        public void Insert(Department model)
        {
            dbContext.DepartmentHistories.Add(
                new DepartmentHistory(model.Code, 1, model.Name, false, DateTime.Now));
        }

        public void Update(Department model)
        {
            var revision = dbContext.DepartmentHistories.Where(h => h.DepartmentCode.Equals(model.Code, StringComparison.OrdinalIgnoreCase))?.Max(h => h.Revision) ?? 0;
            dbContext.DepartmentHistories.Add(
                new DepartmentHistory(model.Code, revision + 1, model.Name, false, DateTime.Now));
        }

        public void Delete(Department model)
        {
            var revision = dbContext.DepartmentHistories.Where(h => h.DepartmentCode.Equals(model.Code, StringComparison.OrdinalIgnoreCase))?.Max(h => h.Revision) ?? 0;
            dbContext.DepartmentHistories.Add(
                new DepartmentHistory(model.Code, revision + 1, model.Name, true, DateTime.Now));
        }
    }
}
