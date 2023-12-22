namespace ContactBook_Shared.Interfaces;

public interface IFileRepository
{
    /// <summary>
    /// Gets data from file on disk
    /// </summary>
    /// <param name="filePath">String filepath to file on disk</param>
    /// <returns>string of data</returns>
    public string GetFile(string filePath);

    /// <summary>
    /// Writes string of _pContactList to file on disk
    /// </summary>
    /// <param name="pContactListUpdated">The last iteration of _pContactList to be written to file</param>
    /// <param name="filePath">String filepath to file on disk</param>
    /// <returns>true if succesful, false if not</returns>
    public bool WriteToFile(string pContactListUpdated, string filePath);

}
