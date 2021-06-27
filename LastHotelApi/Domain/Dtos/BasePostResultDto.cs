using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public abstract class BasePostResultDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
