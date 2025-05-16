using AutoMapper;
using Copart.BLL.Attributes;
using Copart.BLL.Models.BidModels;
using Copart.BLL.Results;
using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Copart.BLL.Services.BidService
{
    public sealed class BidService : IBidService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<BidService> _logger;
        private readonly IMapper _mapper;

        public BidService(IUnitOfWork uow, IMapper mapper, ILogger<BidService> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result> AddAsync(BidAddModel model, IValidator<BidAddModel> validator, CancellationToken token = default)
        {
            _logger.LogDebug("Add invoked with BidAddModel: {@BidModel}", model);
            try
            {
                validator.ValidateAndThrow(model);

                var bidEntity = _mapper.Map<Bid>(model);

                await _uow.BidRepository.AddAsync(bidEntity, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Bid added successfully: Id={BidId}", bidEntity.Id);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding bid: {@BidModel}", model);
                return Result.Fail("An error occurred while adding the bid.");
            }
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken token = default)
        {
            _logger.LogDebug("Delete invoked for Bid Id={BidId}", id);
            try
            {
                var bid = await _uow.BidRepository.GetByIdAsync(id, token).ConfigureAwait(false);
                if (bid is null)
                {
                    _logger.LogWarning("Delete failed: Bid not found, Id={BidId}", id);
                    return Result.Fail($"Bid with id {id} not found");
                }

                await _uow.BidRepository.DeleteAsync(bid, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Bid deleted successfully: Id={BidId}", id);
                return Result.Ok("Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting bid Id={BidId}", id);
                return Result.Fail("An error occurred while deleting the bid.");
            }
        }

        public async Task<Result<IEnumerable<BidModel?>?>> GetAllAsync(CancellationToken token = default)
        {
            _logger.LogDebug("GetAll invoked");
            try
            {
                var bids = await _uow.BidRepository.GetAllAsync(token).ConfigureAwait(false);
                var models = bids?.Select(b => _mapper.Map<BidModel>(b));

                _logger.LogInformation("Retrieved {Count} bids", models?.Count());
                return Result<IEnumerable<BidModel>>.Ok(models)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all bids");
                return Result<IEnumerable<BidModel>>.Fail("An error occurred while retrieving bids.")!;
            }
        }

        public async Task<Result<BidModel?>> GetByIdAsync(int id, CancellationToken token = default)
        {
            _logger.LogDebug("GetById invoked for Bid Id={BidId}", id);
            try
            {
                var bid = await _uow.BidRepository.GetByIdAsync(id, token).ConfigureAwait(false);
                if (bid is null)
                {
                    _logger.LogWarning("GetById failed: Bid not found, Id={BidId}", id);
                    return Result<BidModel>.Fail($"Bid with id {id} not found")!;
                }

                var model = _mapper.Map<BidModel>(bid);
                _logger.LogInformation("Bid retrieved successfully: Id={BidId}", id);
                return Result<BidModel>.Ok(model)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bid Id={BidId}", id);
                return Result<BidModel>.Fail("An error occurred while retrieving the bid.")!;
            }
        }

        public async Task<Result> UpdateAsync(int id, BidUpdateModel model, CancellationToken token = default)
        {
            _logger.LogDebug("Update invoked for Bid Id={BidId} with BidUpdateModel: {@BidModel}", id, model);
            try
            {
                var bid = await _uow.BidRepository.GetByIdAsync(id, token).ConfigureAwait(false);
                if (bid is null)
                {
                    _logger.LogWarning("Update failed: Bid not found, Id={BidId}", id);
                    return Result.Fail($"Bid with id {id} not found");
                }

                bid.Amount = model.Amount;
                await _uow.BidRepository.UpdateAsync(bid, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);
                _logger.LogInformation("Bid updated successfully: Id={BidId}", id);
                return Result.Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating bid Id={BidId}", id);
                return Result.Fail("An error occurred while updating the bid.");
            }
        }
    }
}