﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace TMWcfService
{
    internal class Service : IService
    {
        private DataManager Dm => DataManager.Instance;
    }
}
