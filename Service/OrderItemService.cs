using AutoMapper;
using Contract;
using Contract.Service;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class OrderItemService : IOrderItemService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OrderItemService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
    }
}
