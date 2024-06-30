using GreekShoping.CartAPI.Data.ValueObjects;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GreekShoping.CartAPI.Repository._Coupon;

public class CouponRepository : ICouponRepository
{
    private readonly HttpClient _client;
    public CouponRepository(HttpClient HttpClient)
    {
        _client = HttpClient;
    }
    public async Task<CouponVO> GetCoupon(string couponCode, string token)
    {//"api/v1/coupon"
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync($"/api/v1/coupon/{couponCode}");

        var content = await response.Content.ReadAsStringAsync();
        if (response.StatusCode != HttpStatusCode.OK) return new CouponVO();

        return JsonSerializer.Deserialize<CouponVO>(content, new JsonSerializerOptions
           { PropertyNameCaseInsensitive = true });
    }
}
