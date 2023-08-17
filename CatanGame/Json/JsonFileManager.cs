using System;
using System.IO;
using Newtonsoft.Json;


namespace CatanGame.Json;

public class JsonFileManager<T>
{
    private readonly string _filePath;

    public JsonFileManager(string filePath)
    {
        _filePath = filePath;
    }

    // Save data to a JSON file
    public void Save(T data)
    {
        try
        {
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving to {_filePath}. Error: {ex.Message}");
        }
    }

    // Load data from a JSON file
    public T? Load()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine($"File {_filePath} does not exist.");
                return default(T);
            }

            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading from {_filePath}. Error: {ex.Message}");
            return default(T);
        }
    }
}
