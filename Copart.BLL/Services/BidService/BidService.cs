using AutoMapper;
using Copart.BLL.Models.BidModels;
using Copart.BLL.Results;
using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Copart.BLL.Services.BidService
{
    public class BidService : IBidService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<BidService> _logger;
        private readonly IMapper _mapper;

        public BidService(IUnitOfWork uow, IMapper mapper, ILogger<BidService> logger)
        {
            _logger = logger;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result> Add(BidAddModel bid, CancellationToken token = default)
        {
            await _uow.BidRepository.AddAsync(_mapper.Map<Bid>(bid), token);
            await _uow.Save(token);
            return Result.Ok();
        }

        public async Task<Result> Delete(int id, CancellationToken token = default)
        {
            var b = await _uow.BidRepository.GetByIdAsync(id, token);
            if (b is null) return Result.Fail($"Bid with id {id} not found");
            await _uow.BidRepository.DeleteAsync(b);
            await _uow.Save(token);
            return Result.Ok("Deleted");
        }

        public async Task<Result<IEnumerable<BidModel>>> GetAll(CancellationToken token = default)
        {
            return Result<IEnumerable<BidModel>>.Ok((await _uow.BidRepository.GetAllAsync(token)).Select(b => _mapper.Map<BidModel>(b)));
        }

        public async Task<Result<BidModel>> GetById(int id, CancellationToken token = default)
        {
            var b = await _uow.BidRepository.GetByIdAsync(id, token);
            if (b is null) return Result<BidModel>.Fail($"Bid with id {id} not found");
            return Result<BidModel>.Ok(_mapper.Map<BidModel>(b));
        }

        public async Task<Result> Update(int id, BidUpdateModel bid, CancellationToken token = default)
        {
            var b = await _uow.BidRepository.GetByIdAsync(id, token);
            if (b is null) return Result.Fail($"Bid with id {id} not found");
            b.Amount = bid.Amount;
            await _uow.BidRepository.UpdateAsync(b, token);
            await _uow.Save(token);
            return Result.Ok("Updated");
        }
    }
}
