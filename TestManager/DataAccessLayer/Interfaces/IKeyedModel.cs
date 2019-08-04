
namespace DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс сущности базы данных
    /// </summary>
    interface IKeyedModel
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        int Id { get; set; }
    }
}
