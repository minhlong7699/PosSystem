using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUploadImageService _uploadImageService;

        public UserService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper,IUploadImageService uploadImageService)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _uploadImageService = uploadImageService;
        }

        // create New User
        public async Task<UserDto> CreateUserAsync(UserUpdateCreateDto userCreate, bool trackChanges)
        {
            var userNameIsExisted = _repository.UserRepository.IsUserExistAsync(userCreate.UserName, trackChanges);
            // Check UserName existed
            if (userNameIsExisted is not null)
            {
                throw new UserNameConflictException(userCreate.UserName);
            }

            /* Check RoleId existed
                Chưa làm bổ sung sau
            */

            var userEntity = _mapper.Map<User>(userCreate);
            if(userCreate.Image is not null)
            {
                userEntity.Image = _uploadImageService.GetImageUrl(userCreate.Image, "UserImages");
            }    
            // Add Value to Private feild
            userEntity.CreatedAt = DateTime.Now;
            userEntity.CreatedBy = "Admin";
            userEntity.UpdatedAt = DateTime.Now;
            userEntity.UpdatedBy = "Admin";
            _repository.UserRepository.CreateUser(userEntity);
            await _repository.SaveAsync();

            var userDto = _mapper.Map<UserDto>(userEntity);
            return userDto;
        }

        // Get All User
        public async Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUsersAsync(UserParamters userParamters, bool trackChanges)
        {
            var usersMetaData = await _repository.UserRepository.GetAllUsersAsync(userParamters, trackChanges);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersMetaData);
            return (usersDto, usersMetaData.MetaData);
        }


        // Get User By Id
        public async Task<UserDto> GetUserAsync(Guid userId, bool trackChanges)
        {
            var user = await _repository.UserRepository.GetUserAsync(userId, trackChanges);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
