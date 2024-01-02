namespace CarModels.Services.Interfaces;

public interface ICarMakeService : IService
{
    int GetMakeIdByName(string makeName);
}
