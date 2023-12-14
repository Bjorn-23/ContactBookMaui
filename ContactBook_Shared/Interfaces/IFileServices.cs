using ContactBook_Shared.Models;

namespace ContactBook_Shared.Interfaces;

public interface IFileServices
{
    IEnumerable<IPContact> GetFile(string filePath);

    bool WriteToFile(List<IPContact> contactList, string filePath);
}