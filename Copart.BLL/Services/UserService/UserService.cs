using AutoMapper;
using Copart.BLL.Attributes;
using Copart.BLL.Models.BidModels;
using Copart.BLL.Models.UserModels;
using Copart.BLL.Results;
using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Copart.BLL.Services.BidderService
{
    public sealed class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper, ILogger<UserService> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result> AddAsync(UserAddModel model, IValidator<UserAddModel> validator, CancellationToken token = default)
        {
            _logger.LogDebug("AddAsync invoked with UserAddModel: {@Model}", model);
            try
            {
                validator.ValidateAndThrow(model);

                var entity = _mapper.Map<User>(model);

                await _uow.UserRepository.AddAsync(entity, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("User added successfully: Id={UserId}", entity.Id);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user: {@Model}", model);
                return Result.Fail("An error occurred while adding the user.");
            }
        }

        public async Task<Result> AddBidAsync(int userId, BidAddModel bidModel, CancellationToken token = default)
        {
            _logger.LogDebug("AddBidAsync invoked for UserId={UserId} with BidAddModel: {@BidModel}", userId, bidModel);
            try
            {
                var user = await _uow.UserRepository.GetByIdAsync(userId, token).ConfigureAwait(false);
                if (user is null)
                {
                    _logger.LogWarning("AddBidAsync failed: User not found, Id={UserId}", userId);
                    return Result.Fail("User not found");
                }

                var bid = _mapper.Map<Bid>(bidModel);

                await _uow.UserRepository.AddBidAsync(user, bid, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Bid added to user: UserId={UserId}, BidId={BidId}", userId, bid.Id);
                return Result.Ok("Bid added to user");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding bid to user Id={UserId}", userId);
                return Result.Fail("An error occurred while adding the bid.");
            }
        }

        public async Task<Result> DeleteAsync(int userId, CancellationToken token = default)
        {
            _logger.LogDebug("DeleteAsync invoked for UserId={UserId}", userId);
            try
            {
                var user = await _uow.UserRepository.GetByIdAsync(userId, token).ConfigureAwait(false);
                if (user is null)
                {
                    _logger.LogWarning("DeleteAsync failed: User not found, Id={UserId}", userId);
                    return Result.Fail("User not found");
                }

                await _uow.UserRepository.DeleteAsync(user, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("User deleted successfully: Id={UserId}", userId);
                return Result.Ok("User deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user Id={UserId}", userId);
                return Result.Fail("An error occurred while deleting the user.");
            }
        }

        public async Task<Result<IEnumerable<UserModel?>?>> GetAllAsync(CancellationToken token = default)
        {
            _logger.LogDebug("GetAllAsync invoked");
            try
            {
                var users = await _uow.UserRepository.GetAllAsync(token).ConfigureAwait(false);
                var models = users?.Select(u => _mapper.Map<UserModel>(u));

                _logger.LogInformation("Retrieved {Count} users", models?.Count());
                return Result<IEnumerable<UserModel>>.Ok(models)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users");
                return Result<IEnumerable<UserModel>>.Fail("An error occurred while retrieving users.")!;
            }
        }

        public async Task<Result<UserModel?>> GetByIdAsync(int userId, CancellationToken token = default)
        {
            _logger.LogDebug("GetByIdAsync invoked for UserId={UserId}", userId);
            try
            {
                var user = await _uow.UserRepository.GetByIdAsync(userId, token).ConfigureAwait(false);
                if (user is null)
                {
                    _logger.LogWarning("GetByIdAsync failed: User not found, Id={UserId}", userId);
                    return Result<UserModel>.Fail("User not found")!;
                }

                var model = _mapper.Map<UserModel>(user);
                _logger.LogInformation("User retrieved successfully: Id={UserId}", userId);
                return Result<UserModel>.Ok(model)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user Id={UserId}", userId);
                return Result<UserModel>.Fail("An error occurred while retrieving the user.")!;
            }
        }

        public async Task<Result> UpdateAsync(int userId, UserUpdateModel model, CancellationToken token = default)
        {
            _logger.LogDebug("UpdateAsync invoked for UserId={UserId} with UserUpdateModel: {@Model}", userId, model);
            try
            {
                var existing = await _uow.UserRepository.GetByIdAsync(userId, token).ConfigureAwait(false);
                if (existing is null)
                {
                    _logger.LogWarning("UpdateAsync failed: User not found, Id={UserId}", userId);
                    return Result.Fail("User not found");
                }

                existing.Name = model.Name ?? existing.Name;
                existing.Email = model.Email ?? existing.Email;

                await _uow.UserRepository.UpdateAsync(existing, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("User updated successfully: Id={UserId}", userId);
                return Result.Ok("User updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user Id={UserId}", userId);
                return Result.Fail("An error occurred while updating the user.");
            }
        }
    }
}