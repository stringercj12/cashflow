﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly CashFlowDbContext _dbContext;

        public UnitOfWork(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task Commit() => await _dbContext.SaveChangesAsync();

    }
}
