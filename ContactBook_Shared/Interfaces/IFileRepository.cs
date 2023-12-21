namespace ContactBook_Shared.Interfaces;

public interface IFileRepository
{
    public string GetFile(string filePath);

    public bool WriteToFile(string pContactListUpdated, string filePath);

}
