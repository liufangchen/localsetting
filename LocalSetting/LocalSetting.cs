using System.Text;
using System.Text.Json;

namespace LocalSettingService;

public class LocalSetting : ILocalSetting
{
    private readonly string appName;
    private readonly string settingsType;
    private readonly string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
    private readonly Dictionary<string, object?> settings = [];

    public LocalSetting(string appName = "LocalSettingApp", string settingsType = "UserSetting.json")
    {
        this.appName = appName;
        this.settingsType = settingsType;
        if (!File.Exists(FilePath))
            return;
        var jsonStr = File.ReadAllText(FilePath);
        settings = JsonSerializer.Deserialize<Dictionary<string, object?>>(jsonStr) ?? [];
    }

    private string FilePath => Path.Combine(rootPath, appName, settingsType);
    private string DirectoryPath => Path.Combine(rootPath, appName);

    public T? ReadSetting<T>(string key)
    {
        if (settings != null && settings.TryGetValue(key, out var obj))
        {
            return (T?)obj;
        }
        return default;
    }

    public void SaveSetting<T>(string key, T value)
    {
        settings[key] = value;
        // file storage
        if (!Directory.Exists(DirectoryPath))
        {
            Directory.CreateDirectory(DirectoryPath);
        }
        var fileContent = JsonSerializer.Serialize(settings);
        File.WriteAllText(FilePath, fileContent, Encoding.UTF8);
    }
}
