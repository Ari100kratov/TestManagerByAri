
namespace DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс сущности базы данных
    /// </summary>
    public interface IKeyedModel
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        int Id { get; set; }
    }
}
