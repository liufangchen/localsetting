using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace LocalSettingService;

public class LocalSetting : ILocalSetting
{
    private readonly string appName;
    private readonly string fileName;
    private readonly string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
    private readonly Dictionary<string, JsonElement> settings;

    public LocalSetting(string appName, string fileName)
    {
        this.appName = appName;
        this.fileName = fileName;
        if (File.Exists(FilePath))
        {
            var jsonString = File.ReadAllText(FilePath);
            settings = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonString) ?? [];
        }
        else
        {
            settings = [];
        }
    }

    public LocalSetting(string appName) : this(appName, "UserSetting.json")
    { }

    public LocalSetting() : this(Process.GetCurrentProcess().ProcessName)
    { }


    public T? ReadSetting<T>(string key)
        => (settings is not null && settings.TryGetValue(key, out JsonElement obj)) ? obj.Deserialize<T>() : default;

    public void SaveSetting<T>(string key, T value)
    {   
        settings[key] = JsonSerializer.SerializeToElement(value);
        // file storage
        if (!Directory.Exists(DirectoryPath))
        {
            Directory.CreateDirectory(DirectoryPath);
        }
        var fileContent = JsonSerializer.Serialize(settings);
        File.WriteAllText(FilePath, fileContent, Encoding.UTF8);
    }

    private string FilePath => Path.Combine(rootPath, appName, fileName);

    private string DirectoryPath => Path.Combine(rootPath, appName);
}
