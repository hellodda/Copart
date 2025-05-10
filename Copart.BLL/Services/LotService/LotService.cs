using AutoMapper;
using Copart.BLL.Models.LotModels;
using Copart.BLL.Results;
using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.Extensions.Logging;

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
            await _uow.LotRepository.AddAsync(_mapper.Map<Lot>(lot), token);
            await _uow.Save(token);
            return Result.Ok("lot added");
        }

        public async Task<Result> Delete(int id, CancellationToken token = default)
        {
            var l = await _uow.LotRepository.GetByIdAsync(id, token);
            if (l is null) return Result.Fail("Lot not found");
            await _uow.LotRepository.DeleteAsync(l, token);
            await _uow.Save(token);
            return Result.Ok("Deleted");
        }

        public async Task<Result<IEnumerable<LotModel>>> GetAll(CancellationToken token = default)
        {
            return Result<IEnumerable<LotModel>>.Ok((await _uow.LotRepository.GetAllAsync(token)).Select(l => _mapper.Map<LotModel>(l)));
        }

        public async Task<Result<LotModel>> GetById(int id, CancellationToken token = default)
        {
            var l = await _uow.LotRepository.GetByIdAsync(id, token);
            if (l is null) return Result<LotModel>.Fail("Lot not found");
            return Result<LotModel>.Ok(_mapper.Map<LotModel>(l));
        }

        public async Task<Result<LotModel>> GetByLotNumber(string lotNumber, CancellationToken token = default)
        {
            var l = await _uow.LotRepository.GetByLotNumberAsync(lotNumber, token);
            if (l is null) return Result<LotModel>.Fail("Lot not found");
            return Result<LotModel>.Ok(_mapper.Map<LotModel>(l));
        }

        public async Task<Result> Update(int id, LotUpdateModel lot, CancellationToken token = default)
        {
            var l = await _uow.LotRepository.GetByIdAsync(id, token);
            if (l is null) return Result.Fail("Lot not found");
            await _uow.LotRepository.UpdateAsync(_mapper.Map<Lot>(lot), token);
            return Result.Ok("Updated");
        }
    }
}
