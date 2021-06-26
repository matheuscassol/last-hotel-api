﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ClientModel
    {
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private DateTime? _createdAt;

        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value == null ? DateTime.UtcNow : value; }
        }

        private DateTime? _updatedAt;

        public DateTime? UpdatedAt
        {
            get { return _updatedAt; }
            set { _updatedAt = value; }
        }



    }
}
