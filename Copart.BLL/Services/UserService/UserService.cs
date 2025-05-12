using AutoMapper;
using Copart.BLL.Models.BidModels;
using Copart.BLL.Models.UserModels;
using Copart.BLL.Results;
using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Copart.BLL.Services.BidderService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper, ILogger<UserService> logger)
        {
            _logger = logger;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result> Add(UserAddModel user, CancellationToken token = default)
        {
            await _uow.UserRepository.AddAsync(_mapper.Map<User>(user), token);
            await _uow.Save(token);
            return Result.Ok();
        }

        public async Task<Result> AddBid(int id, BidAddModel bid, CancellationToken token = default)
        {
            var u = await _uow.UserRepository.GetByIdAsync(id, token);
            if (u is null) return Result.Fail($"User with id {id} not found");
            await _uow.UserRepository.AddBid(u, _mapper.Map<Bid>(bid));
            await _uow.Save(token);
            return Result.Ok("Bid added to user");
        }

        public async Task<Result> Delete(int id, CancellationToken token = default)
        {
            var u = await _uow.UserRepository.GetByIdAsync(id, token);
            if (u is null) return Result.Fail($"User with id {id} not found");
            await _uow.UserRepository.DeleteAsync(u);
            await _uow.Save(token);
            return Result.Ok("Deleted");
        }

        public async Task<Result<IEnumerable<UserModel>>> GetAll(CancellationToken token = default)
        {
            return Result<IEnumerable<UserModel>>.Ok((await _uow.UserRepository.GetAllAsync(token)).Select(u => _mapper.Map<UserModel>(u)));
        }

        public async Task<Result<UserModel>> GetById(int id, CancellationToken token = default)
        {
            var u = await _uow.UserRepository.GetByIdAsync(id, token);
            if (u is null) return Result<UserModel>.Fail($"User with id {id} not found");
            return Result<UserModel>.Ok(_mapper.Map<UserModel>(u));
        }

        public async Task<Result> Update(int id, UserUpdateModel user, CancellationToken token = default)
        {
            var u = await _uow.UserRepository.GetByIdAsync(id, token);
            if (u is null) return Result.Fail($"User with id {id} not found");
            u.Name = user.Name ?? u.Name;
            u.Email = user.Email ?? u.Email;
            await _uow.UserRepository.UpdateAsync(u, token);
            await _uow.Save(token);
            return Result.Ok("Updated");
        }
    }
}
