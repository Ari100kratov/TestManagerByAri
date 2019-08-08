using TestManagerClient.WcfServiceReference;
using TestManagerClient.WcfRepositories;

namespace TestManagerClient
{
    /// <summary>
    /// Менеджер доступа к службе WCF
    /// </summary>
    internal sealed class TMDataManager
    {
        /// <summary>
        /// Репозиторий подразделения
        /// </summary>
        internal DepartmentRepository Department { get; } = new DepartmentRepository();
        
        /// <summary>
        /// Репозиторий сотрудника
        /// </summary>
        internal WorkerRepository Worker { get; } = new WorkerRepository();

        private static TMDataManager _active = null;
        private static readonly object _syncRoot = new object();

        /// <summary>
        /// Доступ к экземпляру класса TMDataManager
        /// </summary>
        public static TMDataManager Instance
        {
            get
            {
                if (_active == null)
                {
                    lock (_syncRoot)
                    {
                        if (_active == null)
                        {
                            _active = new TMDataManager();
                        }
                    }
                }
                return _active;
            }
        }
    }
}
