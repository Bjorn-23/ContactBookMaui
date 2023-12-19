﻿using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ContactBook_Shared.Services;


public class FileServices : IFileServices
{
    private readonly List<IPContact> _list =[];
 
    public IEnumerable<IPContact> GetFile(string filePath)
    {
        try
        {
            if (filePath != null) //File.Exists(filePath)
            {
                using (var sr = new StreamReader(filePath))
                {
                    var data = sr.ReadToEnd();
                    var listToUpdate = JsonConvert.DeserializeObject<IEnumerable<IPContact>>(data, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects,
                    });
                    
                    if (listToUpdate != null)
                    {
                        return listToUpdate; //.Cast<IPContact>().ToList();
                    }
                    else
                    {
                        return _list;
                    }
                }
            }            
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return _list;
    }

    public bool WriteToFile(List<IPContact> contactList, string filePath)
    {
        try
        {
            using (var sw = new StreamWriter(filePath))
            {
                var data = JsonConvert.SerializeObject(contactList, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });
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
