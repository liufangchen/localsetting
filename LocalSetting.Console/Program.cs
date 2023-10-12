using LocalSettingService;

var settings = new LocalSetting("App1");
settings.SaveSetting("RepoUrl", "https://github.com/liufangchen/localsetting");
var url = settings.ReadSetting<string>("RepoUrl");
Console.WriteLine(url);

var repos = new Dictionary<string, string>
{
    { "A", "A" },
    { "B", "B" }
};
settings.SaveSetting("repos", repos);
var a = settings.ReadSetting<Dictionary<string, string>>("repos");
if(a is not null)
{
    foreach (var t in a)
    {
        Console.WriteLine(t.Value);
    }
}