
namespace Ordering.Domain.Abstractions
{
    public interface IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastmodifieddAt { get; set; }
        public string? LastmodifiedBy { get; set; }
    }
    public interface IEntity<T>: IEntity
    {
        public T Id { get; set; }
    }
}
