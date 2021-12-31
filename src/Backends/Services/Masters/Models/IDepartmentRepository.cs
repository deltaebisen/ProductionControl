
namespace Masters.Models
{
    public interface IDepartmentRepository
    {
        void Delete(Department model);
        Department? Get(string departmentCode);
        IEnumerable<Department> GetAll();
        void Insert(Department model);
        void Update(Department model);
    }
}