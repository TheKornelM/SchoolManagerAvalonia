using System.Resources;

namespace SchoolManagerModel.Utils;

public static class ResourceManagerExtensions
{
    /// <summary>
    /// Get string of the key from the resource.
    /// </summary>
    /// <param name="resourceManager">Resource variable</param>
    /// <param name="key">String key</param>
    /// <returns>Current language string of the key or the key if key not exists in resource file</returns>
    public static string GetStringOrDefault(this ResourceManager resourceManager, string key)
    {
        return resourceManager.GetString(key) ?? key;
    }
}