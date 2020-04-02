using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class DepositoATerminoRepository : GenericRepository<CertificadoDeDepositoATermino>, IDepositoATerminoRepository
    {
        public DepositoATerminoRepository(IDbContext context)
              : base(context)
        {

        }

    }
}
