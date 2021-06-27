using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class ClientPutResultDto : BasePutResultDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
