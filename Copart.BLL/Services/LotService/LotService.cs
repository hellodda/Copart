using AutoMapper;
using Copart.BLL.Models.LotModels;
using Copart.BLL.Results;
using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Copart.BLL.Services.LotService
{
    public class LotService : ILotService
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

        public async Task<Result> Add(LotAddModel lot, CancellationToken token = default)
        {
            _logger.LogInformation("Adding new lot: {@Lot}", lot);
            try
            {
                var entity = _mapper.Map<Lot>(lot);

                entity.LotNumber = Convert.ToBase64String(Encoding.UTF8.GetBytes(entity.Vehicle.Vin));

                await _uow.LotRepository.AddAsync(entity, token);
                await _uow.Save(token);
                _logger.LogInformation("Lot added successfully with Id {Id}", entity.Id);
                return Result.Ok("lot added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding lot: {@Lot}", lot);
                return Result.Fail("Error adding lot");
            }
        }

        public async Task<Result> Delete(int id, CancellationToken token = default)
        {
            _logger.LogInformation("Deleting lot with Id {Id}", id);
            try
            {
                var l = await _uow.LotRepository.GetByIdAsync(id, token);
                if (l is null)
                {
                    _logger.LogWarning("Lot with Id {Id} not found for deletion", id);
                    return Result.Fail("Lot not found");
                }

                await _uow.LotRepository.DeleteAsync(l, token);
                await _uow.Save(token);
                _logger.LogInformation("Lot with Id {Id} deleted successfully", id);
                return Result.Ok("Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting lot with Id {Id}", id);
                return Result.Fail("Error deleting lot");
            }
        }

        public async Task<Result<IEnumerable<LotModel>>> GetAll(CancellationToken token = default)
        {
            _logger.LogInformation("Retrieving all lots");
            try
            {
                var list = (await _uow.LotRepository.GetAllAsync(token)).Select(l => _mapper.Map<LotModel>(l));
                _logger.LogInformation("Retrieved {Count} lots", list.Count());
                return Result<IEnumerable<LotModel>>.Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all lots");
                return Result<IEnumerable<LotModel>>.Fail("Error retrieving lots");
            }
        }

        public async Task<Result<LotModel>> GetById(int id, CancellationToken token = default)
        {
            _logger.LogInformation("Retrieving lot with Id {Id}", id);
            try
            {
                var l = await _uow.LotRepository.GetByIdAsync(id, token);
                if (l is null)
                {
                    _logger.LogWarning("Lot with Id {Id} not found", id);
                    return Result<LotModel>.Fail("Lot not found");
                }

                var model = _mapper.Map<LotModel>(l);
                _logger.LogInformation("Lot with Id {Id} retrieved successfully", id);
                return Result<LotModel>.Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving lot with Id {Id}", id);
                return Result<LotModel>.Fail("Error retrieving lot");
            }
        }

        public async Task<Result<LotModel>> GetByLotNumber(string lotNumber, CancellationToken token = default)
        {
            _logger.LogInformation("Retrieving lot with lot number '{LotNumber}'", lotNumber);
            try
            {
                var l = await _uow.LotRepository.GetByLotNumberAsync(lotNumber, token);
                if (l is null)
                {
                    _logger.LogWarning("Lot with lot number '{LotNumber}' not found", lotNumber);
                    return Result<LotModel>.Fail("Lot not found");
                }

                var model = _mapper.Map<LotModel>(l);
                _logger.LogInformation("Lot with lot number '{LotNumber}' retrieved successfully", lotNumber);
                return Result<LotModel>.Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving lot with lot number '{LotNumber}'", lotNumber);
                return Result<LotModel>.Fail("Error retrieving lot by number");
            }
        }

        public async Task<Result> Update(int id, LotUpdateModel lot, CancellationToken token = default)
        {
            _logger.LogInformation("Updating lot with Id {Id}: {@Lot}", id, lot);
            try
            {
                var existing = await _uow.LotRepository.GetByIdAsync(id, token);
                if (existing is null)
                {
                    _logger.LogWarning("Lot with Id {Id} not found for update", id);
                    return Result.Fail("Lot not found");
                }

                var entity = _mapper.Map<Lot>(lot);
                entity.Id = id;
                await _uow.LotRepository.UpdateAsync(entity, token);
                await _uow.Save(token);
                _logger.LogInformation("Lot with Id {Id} updated successfully", id);
                return Result.Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating lot with Id {Id}: {@Lot}", id, lot);
                return Result.Fail("Error updating lot");
            }
        }
    }
}