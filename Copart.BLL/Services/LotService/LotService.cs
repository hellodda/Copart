using AutoMapper;
using Copart.BLL.Attributes;
using Copart.BLL.Models.BidModels;
using Copart.BLL.Models.LotModels;
using Copart.BLL.Results;
using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Copart.BLL.Services.LotService
{
    public sealed class LotService : ILotService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<LotService> _logger;
        private readonly IMapper _mapper;

        public LotService(IUnitOfWork uow, IMapper mapper, ILogger<LotService> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result> AddAsync(LotAddModel model, IValidator<LotAddModel> validator, CancellationToken token = default)
        {
            _logger.LogDebug("Add invoked with LotAddModel: {@Model}", model);
            try
            {
                validator.ValidateAndThrow(model);

                var entity = _mapper.Map<Lot>(model);
                entity.LotNumber = Convert.ToBase64String(Encoding.UTF8.GetBytes(entity.Vehicle.Vin));

                await _uow.LotRepository.AddAsync(entity, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Lot added successfully: Id={LotId}, Number={LotNumber}", entity.Id, entity.LotNumber);
                return Result.Ok("Lot added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding lot: {@Model}", model);
                return Result.Fail("An error occurred while adding the lot.");
            }
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken token = default)
        {
            _logger.LogDebug("Delete invoked for Lot Id={LotId}", id);
            try
            {
                var entity = await _uow.LotRepository.GetByIdAsync(id, token).ConfigureAwait(false);
                if (entity is null)
                {
                    _logger.LogWarning("Delete failed: Lot not found, Id={LotId}", id);
                    return Result.Fail("Lot not found");
                }

                await _uow.LotRepository.DeleteAsync(entity, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Lot deleted successfully: Id={LotId}", id);
                return Result.Ok("Lot deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting lot Id={LotId}", id);
                return Result.Fail("An error occurred while deleting the lot.");
            }
        }

        public async Task<Result<IEnumerable<LotModel?>?>> GetAllAsync(CancellationToken token = default)
        {
            _logger.LogDebug("GetAll invoked");
            try
            {
                var entities = await _uow.LotRepository.GetAllAsync(token).ConfigureAwait(false);
                var models = entities?.Select(l => _mapper.Map<LotModel>(l));

                _logger.LogInformation("Retrieved {Count} lots", models?.Count());
                return Result<IEnumerable<LotModel>>.Ok(models)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all lots");
                return Result<IEnumerable<LotModel>>.Fail("An error occurred while retrieving lots.")!;
            }
        }

        public async Task<Result<LotModel?>> GetByIdAsync(int id, CancellationToken token = default)
        {
            _logger.LogDebug("GetById invoked for Lot Id={LotId}", id);
            try
            {
                var entity = await _uow.LotRepository.GetByIdAsync(id, token).ConfigureAwait(false);
                if (entity is null)
                {
                    _logger.LogWarning("GetById failed: Lot not found, Id={LotId}", id);
                    return Result<LotModel>.Fail("Lot not found")!;
                }

                var model = _mapper.Map<LotModel>(entity);
                _logger.LogInformation("Lot retrieved successfully: Id={LotId}", id);
                return Result<LotModel>.Ok(model)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving lot Id={LotId}", id);
                return Result<LotModel>.Fail("An error occurred while retrieving the lot.")!;
            }
        }

        public async Task<Result<LotModel?>> GetByLotNumberAsync(string lotNumber, CancellationToken token = default)
        {
            _logger.LogDebug("GetByLotNumber invoked with Number={LotNumber}", lotNumber);
            try
            {
                var entity = await _uow.LotRepository.GetByLotNumberAsync(lotNumber, token).ConfigureAwait(false);
                if (entity is null)
                {
                    _logger.LogWarning("GetByLotNumber failed: Lot not found, Number={LotNumber}", lotNumber);
                    return Result<LotModel>.Fail("Lot not found")!;
                }

                var model = _mapper.Map<LotModel>(entity);
                _logger.LogInformation("Lot retrieved successfully: Number={LotNumber}, Id={LotId}", lotNumber, entity.Id);
                return Result<LotModel>.Ok(model)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving lot by number {LotNumber}", lotNumber);
                return Result<LotModel>.Fail("An error occurred while retrieving the lot by number.")!;
            }
        }

        public async Task<Result<BidModel?>> GetBiggestBidAsync(int lotId, CancellationToken token = default)
        {
            _logger.LogDebug("GetBiggestBid invoked for Lot Id={LotId}", lotId);
            try
            {
                var entity = await _uow.LotRepository.GetByIdAsync(lotId, token).ConfigureAwait(false);
                if (entity is null)
                {
                    _logger.LogWarning("GetBiggestBid failed: Lot not found, Id={LotId}", lotId);
                    return Result<BidModel>.Fail("Lot not found")!;
                }

                var topBid = entity.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
                if (topBid is null)
                {
                    _logger.LogInformation("No bids found for Lot Id={LotId}", lotId);
                    return Result<BidModel>.Fail("No bids available")!;
                }

                var model = _mapper.Map<BidModel>(topBid);
                _logger.LogInformation("Biggest bid: LotId={LotId}, BidId={BidId}, Amount={Amount}", lotId, topBid.Id, topBid.Amount);
                return Result<BidModel>.Ok(model)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving biggest bid for Lot Id={LotId}", lotId);
                return Result<BidModel>.Fail("An error occurred while retrieving the biggest bid.")!;
            }
        }

        public async Task<Result> AddBidAsync(int lotId, BidAddModel model, CancellationToken token = default)
        {
            _logger.LogDebug("AddBid invoked for Lot Id={LotId} with BidAddModel: {@BidModel}", lotId, model);
            try
            {
                var entity = await _uow.LotRepository.GetByIdAsync(lotId, token).ConfigureAwait(false);
                if (entity is null)
                {
                    _logger.LogWarning("AddBid failed: Lot not found, Id={LotId}", lotId);
                    return Result.Fail("Lot not found");
                }

                if (!entity.IsActive)
                {
                    _logger.LogWarning("AddBid failed: Lot not active, Id={LotId}", lotId);
                    return Result.Fail("Lot is not active");
                }

                if (entity.EndDate < DateTime.UtcNow)
                {
                    _logger.LogWarning("AddBid failed: Lot ended, Id={LotId}, EndDate={EndDate}", lotId, entity.EndDate);
                    return Result.Fail("Lot has ended");
                }

                var bidEntity = _mapper.Map<Bid>(model);
                await _uow.LotRepository.AddBidAsync(entity, bidEntity, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Bid added: LotId={LotId}, BidId={BidId}", lotId, bidEntity.Id);
                return Result.Ok("Bid added to lot");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding bid to Lot Id={LotId}", lotId);
                return Result.Fail("An error occurred while adding the bid.");
            }
        }
    
        public async Task<Result> UpdateAsync(int id, LotUpdateModel model, CancellationToken token = default)
        {
            _logger.LogDebug("UpdateAsync invoked for Lot Id={LotId} with LotUpdateModel: {@Model}", id, model);
            try
            {
                var existing = await _uow.LotRepository.GetByIdAsync(id, token).ConfigureAwait(false);
                if (existing is null)
                {
                    _logger.LogWarning("UpdateAsync failed: Lot not found, Id={LotId}", id);
                    return Result.Fail("Lot not found");
                }

                existing.StartDate = model.StartDate;
                existing.EndDate = model.EndDate;

                await _uow.LotRepository.UpdateAsync(existing, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Lot updated successfully: Id={LotId}", id);
                return Result.Ok("Lot updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating lot Id={LotId}", id);
                return Result.Fail("An error occurred while updating the lot.");
            }
        }

    }
}
