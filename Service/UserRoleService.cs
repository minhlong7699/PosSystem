using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Serilog;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class UserRoleService : IUserRoleService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UserRoleService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserRoleDto>> GetAllUserRoleAsync(bool trackChanges)
        {
            var roleEntity = await _repository.UserRoleRepository.GetAllUserRolesAsync(trackChanges);
            var roleDto = _mapper.Map<IEnumerable<UserRoleDto>>(roleEntity);
            return roleDto;
        }

        public async Task<UserRoleDto> GetUserRoleAsync(Guid userRoleId, bool trackChanges)
        {
            var roleEntity = await _repository.UserRoleRepository.GetUserRoleAsync(userRoleId, trackChanges);
            if(roleEntity is null)
            {
                throw new UserRoleNotFoundException(userRoleId);
            }
            var roleDto = _mapper.Map<UserRoleDto>(roleEntity);
            return roleDto;
        }
    }
}
