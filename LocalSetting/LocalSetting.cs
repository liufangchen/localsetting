using System.Text;
using System.Text.Json;

namespace LocalSettingService;

public class LocalSetting(string appName = "LocalSettingApp", string settingsType = "UserSetting.json") : ILocalSetting
{
    private readonly string applicationName = appName;
    private readonly string settingsType = settingsType;
    private readonly string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);

    private string FilePath => Path.Combine(rootPath, applicationName, settingsType);
    private string DirectoryPath => Path.Combine(rootPath, applicationName);

    private Dictionary<string, object> settings = [];
    private bool isInitialized;

    private void Initialize()
    {
        if (!isInitialized)
        {
            settings = File.Exists(FilePath) ? JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(FilePath)) ?? [] : [];
            isInitialized = true;
        }
    }

    public T? ReadSetting<T>(string key)
    {
        Initialize();
        if (settings != null && settings.TryGetValue(key, out var obj))
        {
            return JsonSerializer.Deserialize<T>((string)obj);
        }
        return default;
    }

    public void SaveSetting<T>(string key, T value)
    {
        Initialize();
        settings[key] = JsonSerializer.Serialize(value);
        // file storage
        if (!Directory.Exists(DirectoryPath))
        {
            Directory.CreateDirectory(DirectoryPath);
        }
        var fileContent = JsonSerializer.Serialize(settings);
        File.WriteAllText(FilePath, fileContent, Encoding.UTF8);
    }
}
