﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Module
{
    public record UpdateMainInfoRequest(
     string Title,
     string Description);
}
