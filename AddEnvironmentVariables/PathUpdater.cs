using System.Text;

namespace AddEnvironmentVariables;

public static class PathUpdater
{
    private const string PathStr = "Path";
    public static void UpdatePath(string newEntry, EnvironmentVariableTarget target)
    {
        var pathValue = Environment.GetEnvironmentVariable(PathStr, target);
        var sb = new StringBuilder(pathValue);
        sb.Append(newEntry);
        if (!newEntry.EndsWith(";"))
            sb.Append(';');
        
        Environment.SetEnvironmentVariable(PathStr, sb.ToString(),EnvironmentVariableTarget.User);
    }

    public static string GetEnvVariables(EnvironmentVariableTarget target)
    {
        var variables = Environment.GetEnvironmentVariable(PathStr, target);
        return variables ?? "";
    }
}