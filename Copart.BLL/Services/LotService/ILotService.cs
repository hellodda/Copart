﻿using Copart.BLL.Models.BidModels;
using Copart.BLL.Models.LotModels;
using Copart.BLL.Results;
using FluentValidation;

namespace Copart.BLL.Services.LotService
{
    public interface ILotService : IService
    {
        public Task<Result<IEnumerable<LotModel?>?>> GetAllAsync(CancellationToken token = default);
        public Task<Result> ChangeAllLotsStatus(CancellationToken token = default);
        public Task<Result<IEnumerable<BidModel>>> GetAllBidsAsync(int id, CancellationToken token = default);
        public Task<Result<LotModel?>> GetByIdAsync(int id, CancellationToken token = default);
        public Task<Result<LotModel?>> GetByLotNumberAsync(string lotNumber, CancellationToken token = default);
        public Task<Result<BidModel?>> GetBiggestBidAsync(int lotId, CancellationToken token = default);
        public Task<Result> AddAsync(LotAddModel model, IValidator<LotAddModel> validator, CancellationToken token = default);
        public Task<Result> AddBidAsync(int id, BidAddModel model, CancellationToken token = default);
        public Task<Result> UpdateAsync(int id, LotUpdateModel model, CancellationToken token = default);
        public Task<Result> DeleteAsync(int id, CancellationToken token = default);
    }
}
