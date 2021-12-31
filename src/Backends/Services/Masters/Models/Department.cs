using BasicBuildingBlocks.Domain;

namespace Masters.Models
{
    /// <summary>
    /// 部門マスタ
    /// </summary>
    public class Department : IAggregateRoot
    {/// <summary>
     /// 部門マスタ
     /// </summary>
        public Department(string code, string name)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>ID</summary>
        public string Code { get; }
        /// <summary>名称</summary>
        public string Name { get; }
    }
}
