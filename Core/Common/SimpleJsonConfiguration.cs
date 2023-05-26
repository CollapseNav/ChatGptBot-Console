using System.Text.Json.Nodes;
using Collapsenav.Net.Tool;

namespace EasyChatGptBot;

/// <summary>
/// 简单的Json配置
/// </summary>
public class SimpleJsonConfiguration
{
    private Dictionary<string, JsonNode> JsonNodes;

    public Dictionary<string, object> PathValue;
    private List<FileSystemWatcher> Watchers;

    public SimpleJsonConfiguration()
    {
        JsonNodes = new();
        Watchers = new();
        PathValue = new();
    }

    public SimpleJsonConfiguration(string json) : this()
    {
        JsonNodes.Add("", json.ToJsonNode());
    }

    public SimpleJsonConfiguration(string path, bool reloadOnChanage) : this()
    {
        Add(path, reloadOnChanage);
    }
    public SimpleJsonConfiguration Add(string path, bool reloadOnChanage)
    {
        if (!Path.IsPathRooted(path))
            path = Path.GetFullPath(path);
        JsonNodes.Add(path, path.GetJsonObjectFromPath());
        if (reloadOnChanage)
        {
            var watcher = new FileSystemWatcher(Path.GetDirectoryName(path), "*.json");
            watcher.Changed += (o, e) =>
            {
                JsonNodes.AddOrUpdate(path, path.GetJsonObjectFromPath());
                Console.WriteLine($"{path} 文件发生更改");
                PathValue.Clear();
            };
            watcher.EnableRaisingEvents = true;
            Watchers.Add(watcher);
        }
        return this;
    }

    public T Get<T>(string nodePath)
    {
        nodePath = nodePath.Replace(':', '.');
        if (PathValue.ContainsKey(nodePath))
            return (T)PathValue[nodePath];
        foreach (var node in JsonNodes)
        {
            var data = Get<T>(node.Value, nodePath);
            if (data != null)
            {
                PathValue.AddOrUpdate(nodePath, data);
                return data;
            }
        }
        return default;
    }

    private T Get<T>(JsonNode jsonNode, string path)
    {
        if (path.IsEmpty())
            return default;
        var paths = path.Split(',');
        var data = jsonNode[paths.FirstOrDefault()];
        if (data == null)
            return default;
        if (paths.Length == 1)
            return data.ToObj<T>();
        return Get<T>(data, path.Skip(1).Join(","));
    }
}