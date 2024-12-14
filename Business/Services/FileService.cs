using System.Diagnostics;
using System.Text.Json;
using Business.Interfaces;

namespace Business.Services;

public class FileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;
    public FileService(string directoryPath, string fileName)
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
    }
    
    public string GetContentFromFile()
    {
        return File.Exists(_filePath) ? File.ReadAllText(_filePath) : null!;
    }
    
    public void SaveContentToFile(string content)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            File.WriteAllText(_filePath, content);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}