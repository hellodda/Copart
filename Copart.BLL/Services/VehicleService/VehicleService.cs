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
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public VehicleService(IUnitOfWork uow, ILogger<VehicleService> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result> Add(VehicleAddModel vehicle, CancellationToken token = default)
        {
            await _uow.VehicleRepository.AddAsync(_mapper.Map<Vehicle>(vehicle), token);
            await _uow.Save(token);
            return Result.Ok();
        }

        public async Task<Result> Delete(int id, CancellationToken token = default)
        {
            var v = await _uow.VehicleRepository.GetByIdAsync(id, token);
            if (v is null) return Result.Fail($"Vehicle with id {id} not found");
            _uow.VehicleRepository.Delete(v);
            await _uow.Save(token);
            return Result.Ok("Deleted");
        }

        public async Task<Result<IEnumerable<VehicleModel>>> GetAll(CancellationToken token = default)
        {
            var vs = (await _uow.VehicleRepository.GetAllAsync(token)).Select(v => _mapper.Map<VehicleModel>(v));
            return Result<IEnumerable<VehicleModel>>.Ok(vs);
        }

        public async Task<Result<VehicleModel>> GetById(int id, CancellationToken token = default)
        {
            var v = await _uow.VehicleRepository.GetByIdAsync(id, token);
            if (v is null) return Result<VehicleModel>.Fail("Vehicle not found");
            return Result<VehicleModel>.Ok(_mapper.Map<VehicleModel>(v));
        }

        public async Task<Result<IEnumerable<VehicleModel>>> GetByMake(string make, CancellationToken token = default)
        {
            var v = await _uow.VehicleRepository.GetByMakeAsync(make, token);
            return Result<IEnumerable<VehicleModel>>.Ok(v.Select(v => _mapper.Map<VehicleModel>(v)));
        }

        public async Task<Result<IEnumerable<VehicleModel>>> GetByModel(string model, CancellationToken token = default)
        {
            var v = await _uow.VehicleRepository.GetByModelAsync(model, token);
            return Result<IEnumerable<VehicleModel>>.Ok(v.Select(v => _mapper.Map<VehicleModel>(v)));
        }

        public async Task<Result> Update(int id, VehicleUpdateModel vehicle, CancellationToken token = default)
        {
            var v = await _uow.VehicleRepository.GetByIdAsync(id, token);
            if (v is null) return Result.Fail("Vehicle not found");
            _uow.VehicleRepository.Update(_mapper.Map<Vehicle>(vehicle));
            await _uow.Save(token);
            return Result.Ok("Updated");
        }
    }
}
