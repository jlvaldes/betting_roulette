using Microsoft.AspNetCore.Mvc;
using System;

namespace Roulette.Api.Models
{
    public abstract class Input
    {
        [FromHeader(Name = "User-Id")]
        public Guid UserId { get; set; }
    }
}
