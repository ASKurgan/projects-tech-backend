﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Presentation
{
    public static class AssemblyReference
    {
        public static Assembly Assembly => typeof(AssemblyReference).Assembly;
    }
}
