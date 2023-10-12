<h1 align="center"> LocalSetting </h1>

<div align="center">

<p>
    <span>English</span> |  <a href="README.md">中文</a>
</p>
</div>

## 📚 Introduce

LocalSetting is a lightweight localsetting tool based on file read and write. The LocalSetting data is under the path provided by the system for AppData in general.

## 🏆 Features

- Free of Configuration
- Based on File System & JSON
- Simple API
- High Perfermance
- DI Ready

## 🚀 Install
Install LocalSetting from the package manager console:
~~~
PM> Install-Package LocalSetting
~~~
Or from the .NET CLI as:
~~~
dotnet add package LocalSetting
~~~

## 🔨 Use

Declare With AppName
~~~csharp 
var settings = new LocalSetting(); 
~~~
Save a Setting
~~~csharp 
settings.SaveSetting("RepoUrl", "https://github.com/liufangchen/localsetting");
~~~
Read a Setting
~~~csharp
var url = settings.ReadSetting<string>("RepoUrl");
~~~
Dependency Injection
~~~csharp
builder.Services.AddLocalSetting();
~~~


## Supported .NET SDK
- .NET 6, 7, 8
- .NET Standard 2.0
