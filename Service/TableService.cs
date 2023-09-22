﻿using Contract;
using Contract.Service;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class TableService : ITableService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;

        public TableService(IRepositoryManager repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}