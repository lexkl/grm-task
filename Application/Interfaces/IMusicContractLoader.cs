using GrmTask.Domain;

namespace GrmTask.Application.Interfaces;

public interface IMusicContractLoader
{
    IEnumerable<MusicContract> Load(string path);
}