using CarModels.DTOs;
using CsvHelper.Configuration;

namespace CarModels.Services;

public class CarMakeMap : ClassMap<CarMake>
{
    public CarMakeMap()
    {
        Map(m => m.MakeId).Name("make_id");
        Map(m => m.MakeName).Name("make_name");
    }
}
