﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmetRepository departmetRepository { get; set; }
        public IEmployeeRepository employeeRepository { get; set; }
        int Complete();
    }
}
