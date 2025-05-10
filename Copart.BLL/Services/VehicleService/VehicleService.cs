using AutoMapper;
using Copart.BLL.Models.VehicleModels;
using Copart.BLL.Results;
using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Copart.BLL.Services.VehicleService
{
    public class VehicleService : IVehicleService
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

        public async Task<Result> Add(VehicleAddModel vehicle, CancellationToken token = default)
        {
            _logger.LogInformation("Adding new vehicle: {@Vehicle}", vehicle);
            try
            {
                var entity = _mapper.Map<Vehicle>(vehicle);
                await _uow.VehicleRepository.AddAsync(entity, token);
                await _uow.Save(token);
                _logger.LogInformation("Vehicle added successfully with Id {Id}", entity.Id);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding vehicle: {@Vehicle}", vehicle);
                return Result.Fail("Error adding vehicle");
            }
        }

        public async Task<Result> Delete(int id, CancellationToken token = default)
        {
            _logger.LogInformation("Deleting vehicle with Id {Id}", id);
            try
            {
                var v = await _uow.VehicleRepository.GetByIdAsync(id, token);
                if (v is null)
                {
                    _logger.LogWarning("Vehicle with Id {Id} not found for deletion", id);
                    return Result.Fail($"Vehicle with id {id} not found");
                }

                _uow.VehicleRepository.Delete(v);
                await _uow.Save(token);
                _logger.LogInformation("Vehicle with Id {Id} deleted successfully", id);
                return Result.Ok("Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vehicle with Id {Id}", id);
                return Result.Fail("Error deleting vehicle");
            }
        }

        public async Task<Result<IEnumerable<VehicleModel>>> GetAll(CancellationToken token = default)
        {
            _logger.LogInformation("Retrieving all vehicles");
            try
            {
                var vs = (await _uow.VehicleRepository.GetAllAsync(token))
                    .Select(v => _mapper.Map<VehicleModel>(v));

                _logger.LogInformation("Retrieved {Count} vehicles", vs.Count());
                return Result<IEnumerable<VehicleModel>>.Ok(vs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all vehicles");
                return Result<IEnumerable<VehicleModel>>.Fail("Error retrieving vehicles");
            }
        }

        public async Task<Result<VehicleModel>> GetById(int id, CancellationToken token = default)
        {
            _logger.LogInformation("Retrieving vehicle with Id {Id}", id);
            try
            {
                var v = await _uow.VehicleRepository.GetByIdAsync(id, token);
                if (v is null)
                {
                    _logger.LogWarning("Vehicle with Id {Id} not found", id);
                    return Result<VehicleModel>.Fail("Vehicle not found");
                }

                var model = _mapper.Map<VehicleModel>(v);
                _logger.LogInformation("Vehicle with Id {Id} retrieved successfully", id);
                return Result<VehicleModel>.Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vehicle with Id {Id}", id);
                return Result<VehicleModel>.Fail("Error retrieving vehicle");
            }
        }

        public async Task<Result<IEnumerable<VehicleModel>>> GetByMake(string make, CancellationToken token = default)
        {
            _logger.LogInformation("Retrieving vehicles with make '{Make}'", make);
            try
            {
                var v = await _uow.VehicleRepository.GetByMakeAsync(make, token);
                var list = v.Select(x => _mapper.Map<VehicleModel>(x));
                _logger.LogInformation("Retrieved {Count} vehicles with make '{Make}'", list.Count(), make);
                return Result<IEnumerable<VehicleModel>>.Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vehicles with make '{Make}'", make);
                return Result<IEnumerable<VehicleModel>>.Fail("Error retrieving vehicles by make");
            }
        }

        public async Task<Result<IEnumerable<VehicleModel>>> GetByModel(string model, CancellationToken token = default)
        {
            _logger.LogInformation("Retrieving vehicles with model '{Model}'", model);
            try
            {
                var v = await _uow.VehicleRepository.GetByModelAsync(model, token);
                var list = v.Select(x => _mapper.Map<VehicleModel>(x));
                _logger.LogInformation("Retrieved {Count} vehicles with model '{Model}'", list.Count(), model);
                return Result<IEnumerable<VehicleModel>>.Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vehicles with model '{Model}'", model);
                return Result<IEnumerable<VehicleModel>>.Fail("Error retrieving vehicles by model");
            }
        }

        public async Task<Result> Update(int id, VehicleUpdateModel vehicle, CancellationToken token = default)
        {
            _logger.LogInformation("Updating vehicle with Id {Id}: {@Vehicle}", id, vehicle);
            try
            {
                var v = await _uow.VehicleRepository.GetByIdAsync(id, token);
                if (v is null)
                {
                    _logger.LogWarning("Vehicle with Id {Id} not found for update", id);
                    return Result.Fail("Vehicle not found");
                }

                var entity = _mapper.Map<Vehicle>(vehicle);
                entity.Id = id; // ensure correct Id
                _uow.VehicleRepository.Update(entity);
                await _uow.Save(token);
                _logger.LogInformation("Vehicle with Id {Id} updated successfully", id);
                return Result.Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vehicle with Id {Id}: {@Vehicle}", id, vehicle);
                return Result.Fail("Error updating vehicle");
            }
        }
    }
}
