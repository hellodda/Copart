﻿using Copart.UI.Models.BidModels;
using Copart.UI.Models.LotModels;
using System.Net.Http.Json;

namespace Copart.UI.Apis.LotApi
{
    public class LotApi : ILotApi
    {
        private readonly HttpClient _client;

        public LotApi(HttpClient client)
        {
            _client = client;
        }

        public async Task AddBid(int id, BidAddModel bid, CancellationToken token = default)
        {
            try
            {
                await _client.PostAsJsonAsync($"Lot/{id}/bid", bid, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching lots: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<LotModel>> GetAll(CancellationToken token)
        {
            try
            {
                return await _client.GetFromJsonAsync<IEnumerable<LotModel>>("/Lot", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching lots: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<BidModel>> GetAllBids(int id, CancellationToken token = default)
        {
            try
            {
                return await _client.GetFromJsonAsync<IEnumerable<BidModel>>($"/Lot/{id}/bids/all", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching lots: {ex.Message}");
                throw;
            }
        }

        public async Task<BidModel> GetBiggestBid(int id, CancellationToken token = default)
        {
            try
            {
                return await _client.GetFromJsonAsync<BidModel>($"/Lot/{id}/bids/biggest", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching lots: {ex.Message}");
                throw;
            }
        }

        public async Task<LotModel> GetLotByNumber(string number, CancellationToken token = default)
        {
            try
            {
                return await _client.GetFromJsonAsync<LotModel>($"/Lot/{number}", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching lots: {ex.Message}");
                throw;
            }
        }
    }
}
