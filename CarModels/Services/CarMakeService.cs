using CarModels.DTOs;
using CarModels.Services.Interfaces;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace CarModels.Services;

public class CarMakeService : ICarMakeService
{
    private readonly IServiceProvider _serviceProvider;

    public CarMakeService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public int GetMakeIdByName(string makeName)
    {
        ArgumentException.ThrowIfNullOrEmpty(makeName, nameof(makeName));

        var csvFilePathSettingKey = "Settings:CarMakeFilePath";

        var csvFilePath = _serviceProvider.GetRequiredService<IConfiguration>()
            .GetValue<string>(csvFilePathSettingKey);

        if (string.IsNullOrWhiteSpace(csvFilePath))
            throw new InvalidOperationException($"The setting '{csvFilePathSettingKey}' is missing or empty.");

        if (File.Exists(csvFilePath) is false)
            throw new InvalidOperationException($"The file specified in '{csvFilePathSettingKey}' ('{csvFilePath}') does not exist.");

        using var reader = new StreamReader(csvFilePath);

        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        csv.Context.RegisterClassMap<CarMakeMap>();

        var allRecords = csv.GetRecords<CarMake>();

        var matchingRecord = allRecords
            .FirstOrDefault(r => r.MakeName.Equals(makeName, StringComparison.OrdinalIgnoreCase));

        return matchingRecord?.MakeId ?? throw new Exception($"Make Id was not found for make name: '{makeName}'.");
    }
}
