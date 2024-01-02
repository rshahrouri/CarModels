using CarModels.DTOs;

namespace CarModels.Services.Interfaces;

public interface IModelsService : IService
{
    Task<IEnumerable<Model>> Get(int modelyear, string make);
}
