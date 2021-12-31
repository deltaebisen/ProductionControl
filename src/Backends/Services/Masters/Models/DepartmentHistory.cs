using System.ComponentModel.DataAnnotations;

namespace Masters.Models
{
    /// <summary>
    /// 部門マスタ
    /// </summary>
    public class DepartmentHistory
    {
        private DepartmentHistory()
        {
            DepartmentCode = string.Empty;
            Name = string.Empty;
            Revision = 1;
            IsDeleted = false;
            ModifiedDatetime = DateTime.Now;
        }
        public DepartmentHistory(string departmentCode, int revision, string name,bool isDeleted, DateTime modifiedDatetime)
        {
            DepartmentCode = departmentCode;
            Revision = revision;
            Name = name;
            IsDeleted = isDeleted;
            ModifiedDatetime = modifiedDatetime;
        }

        /// <summary>ID</summary>
        public string DepartmentCode { get; private set; }
        /// <summary>Revision</summary>
        public int Revision { get; private set; }
        /// <summary>名称</summary>
        public string Name { get; private set; }
        /// <summary>削除フラグ</summary>
        public bool IsDeleted { get; private set; }

        /// <summary>更新日</summary>
        internal DateTime ModifiedDatetime { get; private set; }

        public override bool Equals(object? obj)
        {
            return obj is DepartmentHistory history &&
                     DepartmentCode == history.DepartmentCode &&
                     Revision == history.Revision;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DepartmentCode, Revision);
        }
    }
}
