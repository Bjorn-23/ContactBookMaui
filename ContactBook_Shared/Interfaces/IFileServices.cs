using ContactBook_Shared.Models;

namespace ContactBook_Shared.Interfaces;

public interface IFileServices
{
    /// <summary>
    /// Reads data from file on disk
    /// </summary>
    /// <param name="filePath">Dir to file on computer</param>
    /// <returns>IEnumerable of IPContact</returns>
    IEnumerable<IPContact> GetFile(string filePath);

    /// <summary>
    /// Writes data to file on disk
    /// </summary>
    /// <param name="filePath">Dir to file on computer</param>
    /// <returns>Bool</returns>
    bool WriteToFile(List<IPContact> contactList, string filePath);
}