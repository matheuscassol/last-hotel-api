using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ClientModel : Notifiable<Notification>
    {
        private Guid _id;
        public Guid Id { get; set; }
        private string _name;
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
