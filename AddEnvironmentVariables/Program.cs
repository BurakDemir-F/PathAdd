using AddEnvironmentVariables;

string[] arguments = Environment.GetCommandLineArgs();

if (arguments.Length < 3)
{
    Console.WriteLine("Error on number of arguments");
    Console.WriteLine("type [\"user\"|\"machine\"] [\"directory to add path\"|\"current\"]");
    Console.WriteLine("type [\"user\"|\"machine\"] [\"show] to show all env variables.");
    return;
}

var targetStr = arguments[1];

var isUser = targetStr.Equals("user", StringComparison.OrdinalIgnoreCase);
var isMachine = targetStr.Equals("machine", StringComparison.OrdinalIgnoreCase);

if (!isMachine && !isUser)
{
    Console.WriteLine("First argument should be \"user\" or \"machine\"");
    return;
}

//bug here , show named folder can't be used.
var secondArg = arguments[2];
if (secondArg.Contains("show", StringComparison.OrdinalIgnoreCase))
{
    var variables = PathUpdater.GetEnvVariables(GetTargetType(isUser));
    var splitted = variables.Split(';');
    foreach (var variable in splitted)
    {
        Console.WriteLine(variable);
    }

    return;
}


var folderPath = new FolderPath(arguments[2]);
if (!folderPath.IsPathOkay())
{
    Console.WriteLine("Error on folder path");
    Console.WriteLine("type [\"user\"|\"machine\"] [\"directory to add path\"|\"current\"]");
    return;
}

PathUpdater.UpdatePath(folderPath.FullPath,GetTargetType(isUser));
Console.WriteLine($"{targetStr} path updated for {folderPath.RelativePath}");

EnvironmentVariableTarget GetTargetType(bool isUserTarget)
{
    return isUserTarget ? EnvironmentVariableTarget.User : EnvironmentVariableTarget.Machine;
}
