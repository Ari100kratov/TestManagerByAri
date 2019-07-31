using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManagerClient.WcfServiceReference;

namespace TestManagerClient
{
    internal sealed class TMDataManager
    {
        internal ServiceClient TMService { get; } = new ServiceClient();
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
