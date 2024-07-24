﻿using MediatR;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Customer
{
    public class GetCustomerByIdQuery : IRequest<CustomerEntity>
    {
        public int id;

        public GetCustomerByIdQuery(int id)
        {
            this.id = id;
        }
    }
}