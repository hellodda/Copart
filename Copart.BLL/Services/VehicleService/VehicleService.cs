using AutoMapper;
using Copart.BLL.Attributes;
using Copart.BLL.Models.VehicleModels;
using Copart.BLL.Results;
using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Copart.BLL.Services.VehicleService
{
    public sealed class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<VehicleService> _logger;
        private readonly IMapper _mapper;

        public VehicleService(IUnitOfWork uow, ILogger<VehicleService> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result> AddAsync(VehicleAddModel model, IValidator<VehicleAddModel> validator, CancellationToken token = default)
        {
            _logger.LogDebug("AddAsync invoked with VehicleAddModel: {@Model}", model);
            try
            {
                validator.ValidateAndThrow(model);

                var entity = _mapper.Map<Vehicle>(model);

                await _uow.VehicleRepository.AddAsync(entity, token).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Vehicle added successfully: Id={VehicleId}", entity.Id);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding vehicle: {@Model}", model);
                return Result.Fail("An error occurred while adding the vehicle.");
            }
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken token = default)
        {
            _logger.LogDebug("DeleteAsync invoked for Vehicle Id={VehicleId}", id);
            try
            {
                var entity = await _uow.VehicleRepository.GetByIdAsync(id, token).ConfigureAwait(false);
                if (entity is null)
                {
                    _logger.LogWarning("DeleteAsync failed: Vehicle not found, Id={VehicleId}", id);
                    return Result.Fail("Vehicle not found");
                }

                await _uow.VehicleRepository.DeleteAsync(entity).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Vehicle deleted successfully: Id={VehicleId}", id);
                return Result.Ok("Vehicle deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vehicle Id={VehicleId}", id);
                return Result.Fail("An error occurred while deleting the vehicle.");
            }
        }

        [UseForSearch]
        public async Task<Result<IEnumerable<VehicleModel>>> GetAllAsync(CancellationToken token = default)
        {
            _logger.LogDebug("GetAllAsync invoked");
            try
            {
                var entities = await _uow.VehicleRepository.GetAllAsync(token).ConfigureAwait(false);
                var models = entities?.Select(v => _mapper.Map<VehicleModel>(v));

                _logger.LogInformation("Retrieved {Count} vehicles", models?.Count());
                return Result<IEnumerable<VehicleModel>>.Ok(models);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all vehicles");
                return Result<IEnumerable<VehicleModel>>.Fail("An error occurred while retrieving vehicles.");
            }
        }

        public async Task<Result<VehicleModel>> GetByIdAsync(int id, CancellationToken token = default)
        {
            _logger.LogDebug("GetByIdAsync invoked for Vehicle Id={VehicleId}", id);
            try
            {
                var entity = await _uow.VehicleRepository.GetByIdAsync(id, token).ConfigureAwait(false);
                if (entity is null)
                {
                    _logger.LogWarning("GetByIdAsync failed: Vehicle not found, Id={VehicleId}", id);
                    return Result<VehicleModel>.Fail("Vehicle not found");
                }

                var model = _mapper.Map<VehicleModel>(entity);
                _logger.LogInformation("Vehicle retrieved successfully: Id={VehicleId}", id);
                return Result<VehicleModel>.Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vehicle Id={VehicleId}", id);
                return Result<VehicleModel>.Fail("An error occurred while retrieving the vehicle.");
            }
        }

        public async Task<Result<IEnumerable<VehicleModel>>> GetByMakeAsync(string make, CancellationToken token = default)
        {
            _logger.LogDebug("GetByMakeAsync invoked with Make={Make}", make);
            try
            {
                var entities = await _uow.VehicleRepository.GetByMakeAsync(make, token).ConfigureAwait(false);
                var models = entities?.Select(v => _mapper.Map<VehicleModel>(v));

                _logger.LogInformation("Retrieved {Count} vehicles for make '{Make}'", models?.Count(), make);
                return Result<IEnumerable<VehicleModel>>.Ok(models);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vehicles by make '{Make}'", make);
                return Result<IEnumerable<VehicleModel>>.Fail("An error occurred while retrieving vehicles by make.");
            }
        }

        public async Task<Result<IEnumerable<VehicleModel>>> GetByModelAsync(string modelName, CancellationToken token = default)
        {
            _logger.LogDebug("GetByModelAsync invoked with Model={Model}", modelName);
            try
            {
                var entities = await _uow.VehicleRepository.GetByModelAsync(modelName, token).ConfigureAwait(false);
                var models = entities?.Select(v => _mapper.Map<VehicleModel>(v));

                _logger.LogInformation("Retrieved {Count} vehicles for model '{Model}'", models?.Count(), modelName);
                return Result<IEnumerable<VehicleModel>>.Ok(models);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vehicles by model '{Model}'", modelName);
                return Result<IEnumerable<VehicleModel>>.Fail("An error occurred while retrieving vehicles by model.");
            }
        }

        public async Task<Result> UpdateAsync(int id, VehicleUpdateModel model, CancellationToken token = default)
        {
            _logger.LogDebug("UpdateAsync invoked for Vehicle Id={VehicleId} with VehicleUpdateModel: {@Model}", id, model);
            try
            {
                var existing = await _uow.VehicleRepository.GetByIdAsync(id, token).ConfigureAwait(false);
                if (existing is null)
                {
                    _logger.LogWarning("UpdateAsync failed: Vehicle not found, Id={VehicleId}", id);
                    return Result.Fail("Vehicle not found");
                }

                _mapper.Map(model, existing);
                await _uow.VehicleRepository.UpdateAsync(existing).ConfigureAwait(false);
                await _uow.Save(token).ConfigureAwait(false);

                _logger.LogInformation("Vehicle updated successfully: Id={VehicleId}", id);
                return Result.Ok("Vehicle updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vehicle Id={VehicleId}", id);
                return Result.Fail("An error occurred while updating the vehicle.");
            }
        }
    }
}