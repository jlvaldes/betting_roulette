﻿using Microsoft.AspNetCore.Mvc;

namespace Roulette.Api.Models
{
    public abstract class Input
    {
        [FromHeader(Name = "User-Id")]
        public string UserId { get; set; }
    }
}
