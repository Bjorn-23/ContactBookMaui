namespace ContactBook_Shared.Interfaces
{
    internal interface IFileRepository
    {
        public string GetFile(string filePath);

        public bool WriteToFile(string pContactListUpdated, string filePath);

    }
}
