using Collapsenav.Net.Tool;

namespace EasyChatGptBot;

/// <summary>
/// 一个简单的 对象容器
/// </summary>
public class ObjContainer
{
    public ObjContainer()
    {
        ObjMap = new();
        Types = new();
    }

    public Dictionary<Type, object> ObjMap { get; set; }
    public List<Type> Types { get; set; }
    /// <summary>
    /// 所有已经注册过的 Type
    /// </summary>
    public List<Type> AllTypes { get => ObjMap.Keys.Concat(Types).Unique().ToList(); }

    /// <summary>
    /// 直接注册单实例的对象
    /// </summary>
    public ObjContainer AddOrUpdate(Type type, object obj)
    {
        if (obj == null)
            return this;
        if (ObjMap.ContainsKey(type))
            ObjMap[type] = obj;
        else
            ObjMap.Add(type, obj);
        return this;
    }
    /// <summary>
    /// 直接注册单实例的对象
    /// </summary>
    public ObjContainer AddOrUpdate<T>(T obj)
    {
        return AddOrUpdate(typeof(T), obj);
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    public object GetObj(Type type)
    {
        var keys = ObjMap.Keys.Where(item => item.IsType(type));
        if (keys.Count() > 1)
            throw new Exception($"无法创建 {type.ToString()} 类型的依赖, 容器中匹配到多个适用的类型");
        if (keys.Count() == 1)
        {
            var keyType = keys.First();
            if (ObjMap[keys.First()] != null)
                return ObjMap[keyType];
            else
            {
                ObjMap[keyType] = ConstructObject(keys.First());
                return ObjMap[keyType];
            }
        }

        var types = Types.Where(item => item.IsType(type));
        if (types.Count() != 1)
            throw new Exception($"无法创建 {type.ToString()} 类型的依赖, 容器中未注册对应的类型");
        var obj = ConstructObject(types.First());
        // if (obj != null)
        //     AddOrUpdate(type, obj);
        return obj;
    }
    /// <summary>
    /// 获取对象
    /// </summary>
    public T GetObj<T>() where T : class
    {
        return (GetObj(typeof(T)) as T)!;
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    public object ConstructObject(Type type)
    {
        var cons = type.GetConstructors();
        if (cons.Any(item => item.GetParameters().Length == 0))
            return Activator.CreateInstance(type)!;
        var constructor = cons.First();
        var parameterTypes = constructor.GetParameters().Select(item => item.ParameterType).ToArray();
        if (parameterTypes.Any(item => !AllTypes.Any(t => t.IsType(item))))
            throw new Exception("容器中未注册对应的依赖");
        var parameters = parameterTypes.Select(item => GetObj(item)).ToArray();
        return Activator.CreateInstance(type, parameters)!;
    }
    /// <summary>
    /// 注册type
    /// </summary>
    public ObjContainer AddType<T>()
    {
        if (Types.Contains(typeof(T)))
            return this;
        Types.Add(typeof(T));
        return this;
    }
}