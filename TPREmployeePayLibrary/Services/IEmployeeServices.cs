﻿using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Services
{
    public interface IEmployeeServices
    {
        public double CalcWeeksWorked(Employee employee);        
    }
}
