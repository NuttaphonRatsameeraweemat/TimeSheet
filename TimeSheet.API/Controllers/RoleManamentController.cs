﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeSheet.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RoleManamentController : ControllerBase
    {
    }
}