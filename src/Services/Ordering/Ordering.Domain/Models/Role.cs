namespace Ordering.Domain.Models
{
    public sealed class Role : Enumeration<Role>
    {
        public static readonly Role Administrator = new(1, "Administrator");

        public Role(int id, string name) : base(id, name)
        {
        }

        public ICollection<Permission> Permissions { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
