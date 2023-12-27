using ContactBook_Shared.Interfaces;
using System.Diagnostics;

namespace ContactBook_Shared.Repositories;

public class FileRepository : IFileRepository
{
    public string GetFile(string filePath)
    {
        try
        {
            if (filePath != null)
            {
                using (var sr = new StreamReader(filePath))
                {
                    var data = sr.ReadToEnd();
                    
                    if (data != null)
                    {
                        return data;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return string.Empty;
    }

    public bool WriteToFile(string data, string filePath)
    {
        try
        {
            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine(data);
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return false;
    }
}
