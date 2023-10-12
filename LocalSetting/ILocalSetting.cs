namespace LocalSettingService;

public interface ILocalSetting
{
    T? ReadSetting<T>(string key);

    void SaveSetting<T>(string key, T value);
}