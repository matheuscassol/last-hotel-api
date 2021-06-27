using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public abstract class BasePutResultDto
    {
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
