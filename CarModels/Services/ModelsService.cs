using CarModels.DTOs;
using CarModels.Services.Interfaces;

namespace CarModels.Services;

public class ModelsService : IModelsService
{
    private readonly IServiceProvider _serviceProvider;

    public ModelsService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<IEnumerable<Model>> Get(int modelyear, string make)
    {
        if (modelyear.Equals(default))
            throw new ArgumentOutOfRangeException(nameof(modelyear));

        ArgumentException.ThrowIfNullOrEmpty(make, nameof(make));

        var makeId = _serviceProvider.GetRequiredService<ICarMakeService>()
            .GetMakeIdByName(make);

        var client = _serviceProvider.GetRequiredService<IHttpClientFactory>()
            .CreateClient("CarModelsClient");

        var requestUrl = GetRequestUrl(modelyear, makeId);

        var apiResponse = await client.GetFromJsonAsync<ApiResponse<Model>>(requestUrl);
        var models = apiResponse?.Results?.DistinctBy(m => m.Model_ID) ?? Enumerable.Empty<Model>();

        return models;
    }

    private string GetRequestUrl(int modelyear, int makeId)
    {
        var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
        var requestUrlSettingValue = configuration.GetValue<string>("Settings:GetModelsForMakeIdYearApi:Endpoint");

        if (string.IsNullOrWhiteSpace(requestUrlSettingValue))
            throw new InvalidOperationException($"The setting 'Settings:GetModelsForMakeIdYearApi:Endpoint' is missing or empty.");

        var requestUrl = string.Format(requestUrlSettingValue, makeId, modelyear);

        var format = configuration.GetValue("Settings:GetModelsForMakeIdYearApi:Format", "json");

        requestUrl += $"?format={format}";

        return requestUrl;
    }
}
