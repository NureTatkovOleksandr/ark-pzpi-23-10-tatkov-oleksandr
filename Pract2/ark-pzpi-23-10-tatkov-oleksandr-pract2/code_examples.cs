public static async Task AddLog(
    string message,
    int msgType = 0,
    [CallerMemberName] string caller = "",
    [CallerFilePath] string file = "",
    [CallerLineNumber] int line = 0)
{
    msgType ??= LogLevel.Info;

          
    ConsoleColor color = msgType == LogLevel.ERROR ? ConsoleColor.Red :
                        msgType == LogLevel.WARNING ? ConsoleColor.Yellow :
                        ConsoleColor.White;

    string type = msgType == LogLevel.ERROR ? "ERR" :
                  msgType == LogLevel.WARNING ? "WAR" : "INF";

    int level = msgType == LogLevel.ERROR ? 1 :
      msgType == LogLevel.WARNING ? 2 : 3;

    if (message.Contains(WarnSymbol))
    {
        color = ConsoleColor.Yellow;
        type = "WAR";
    }
    if (message.Contains(ErrorSymbol))
    {
        color = ConsoleColor.Red;
        type = "ERR";
    }
    await AddLog(message, color, level, type, caller, file, line);
}

public static async Task AddLog(
    string message,
    LogLevel msgType = null,
    [CallerMemberName] string caller = "",
    [CallerFilePath] string file = "",
    [CallerLineNumber] int line = 0)
{
    msgType ??= LogLevel.Info;

    ConsoleColor color = msgType.GetColor();
    string type = msgType.GetTypeString();
    int level = msgType.GetValue();

    if (message.Contains(WarnSymbol))
    {
        color = ConsoleColor.Yellow;
        type = "WAR";
    }
    if (message.Contains(ErrorSymbol))
    {
        color = ConsoleColor.Red;
        type = "ERR";
    }
    await AddLog(message, color, level, type, caller, file, line);
}

public class LogLevel
{
    public static readonly LogLevel Info = new LogLevel(3, "INF", ConsoleColor.White);
    public static readonly LogLevel Warning = new LogLevel(2, "WAR", ConsoleColor.Yellow);
    public static readonly LogLevel Error = new LogLevel(1, "ERR", ConsoleColor.Red);

    private readonly int _value;
    private readonly string _typeString;
    private readonly ConsoleColor _color;

    private LogLevel(int value, string typeString, ConsoleColor color)
    {
        _value = value;
        _typeString = typeString;
        _color = color;
    }

    public int GetValue() => _value;
    public string GetTypeString() => _typeString;
    public ConsoleColor GetColor() => _color;

    public bool IsError() => this == Error;
    public bool ShouldLog(int loggingLevel) => _value <= loggingLevel;
}

public class Var
{
    public string Name { get; set; } = string.Empty;

    public object? Value
    {
        get => _value ?? DDF.Null;
        set => _value = value;
    }

    private object? _value;
    public Type? PossibleCsType { get; set; }
    public bool IsCore { get; set; } = false;
}

public class Var
{
    public string Name { get; }

    public object? Value { get; }

    public Type? PossibleCsType { get; }
    public bool IsCore { get; }

    public Var(string name, object? value, Type? possibleCsType = null, bool isCore = false)
    {
        Name = name ?? string.Empty;
        Value = value ?? DDF.Null;
        PossibleCsType = possibleCsType;
        IsCore = isCore;
    }
}

public static async Task AddDbgLog(
    string message,
    string dbgSource = null,  // По умолчанию null, если не передан - игнорируем фильтрацию
    LogLevel msgType = null,
    [CallerMemberName] string? caller = null,
    [CallerFilePath] string? file = null,
    [CallerLineNumber] int line = 0)
{
    if (!IsDebug)
    {
        return;
    }

    if (caller == null)
    {
        return;
    }

    if (file == null)
    {
        return;
    }

    if (DbgTarget != null && dbgSource == null)
    {
        return;
    }

    if (DbgTarget != null && dbgSource != DbgTarget)
    {
        return;
    }

    msgType ??= LogLevel.Info;

    ConsoleColor color = msgType.GetColor();
    string type = msgType.GetTypeString();
    int level = msgType.GetValue();

    if (message.Contains(WarnSymbol))
    {
        color = ConsoleColor.Yellow;
        type = "WAR";
    }
    if (message.Contains(ErrorSymbol))
    {
        color = ConsoleColor.Red;
        type = "ERR";
    }
    await AddLog(message, color, level, type, caller, file, line);
}

public static async Task AddDbgLog(
    string message,
    string dbgSource = null,  // По умолчанию null, если не передан - игнорируем фильтрацию
    LogLevel msgType = null,
    [CallerMemberName] string? caller = null,
    [CallerFilePath] string? file = null,
    [CallerLineNumber] int line = 0)
{
    if (!IsDebug)
    {
        return;
    }

    if (caller == null || file == null || (DbgTarget != null && (dbgSource == null || dbgSource != DbgTarget)))
    {
        return;
    }

    msgType ??= LogLevel.Info;

    ConsoleColor color = msgType.GetColor();
    string type = msgType.GetTypeString();
    int level = msgType.GetValue();

    if (message.Contains(WarnSymbol))
    {
        color = ConsoleColor.Yellow;
        type = "WAR";
    }
    if (message.Contains(ErrorSymbol))
    {
        color = ConsoleColor.Red;
        type = "ERR";
    }
    await AddLog(message, color, level, type, caller, file, line);
}
