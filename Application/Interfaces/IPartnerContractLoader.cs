using GrmTask.Domain;

namespace GrmTask.Application.Interfaces;

public interface IPartnerContractLoader
{
    IEnumerable<PartnerContract> Load(string path);
}