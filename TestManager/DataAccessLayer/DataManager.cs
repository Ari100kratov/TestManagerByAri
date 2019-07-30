using DataAccessLayer.Repositories;

namespace DataAccessLayer
{
    /// <summary>
    /// Менеджер доступа к репозиториям базы данных
    /// </summary>
    internal sealed class DataManager
    {
        /// <summary>
        /// Репозиторий сотрудника
        /// </summary>
        internal WorkerRepository Worker { get; } = new WorkerRepository();

        /// <summary>
        /// Репозиторий подразделения
        /// </summary>
        internal DepartmentRepository Department { get; } = new DepartmentRepository();

        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        internal static string ConnectionString { get; set; }

        private static DataManager _active = null;
        private static readonly object _syncRoot = new object();

        /// <summary>
        /// Доступ к экземпляру класса DataManager
        /// </summary>
        public static DataManager Instance
        {
            get
            {
                if (_active == null)
                {
                    lock (_syncRoot)
                    {
                        if (_active == null)
                        {
                            _active = new DataManager();
                        }
                    }
                }
                return _active;
            }
        }
    }
}
