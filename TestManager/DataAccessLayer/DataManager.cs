using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal sealed class DataManager
    {
        private static DataManager _active = null;
        private static object _syncRoot = new object();

        public static DataManager Instance
        {
            get
            {
                lock (_syncRoot)
                    if (_active == null)
                        _active = new DataManager();
                return _active;
            }
        }
    }
}
