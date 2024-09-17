namespace AddEnvironmentVariables;

public class FolderPath
{
    private string _path;
    private string _relativePath;
    private const string CurrentKey = "current";
    
    public string FullPath
    {
        get
        {
            var isCurrentKey = _path.Equals(CurrentKey);
            if (!Path.IsPathFullyQualified(_path))
                _path = Path.GetFullPath(_path);

            if (isCurrentKey)
            {
                _path = _path.Remove(_path.Length - CurrentKey.Length);
            }

            return _path;
        }
    }

    public string RelativePath => _relativePath;

    public FolderPath(string path)
    {
        _path = @$"{path}";
        _relativePath = _path;
    }

    public bool IsPathOkay()
    {
        return !string.IsNullOrEmpty(_path) && Directory.Exists(FullPath);
    }
}