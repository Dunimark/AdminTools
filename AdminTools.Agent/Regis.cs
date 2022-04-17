using Microsoft.Win32;

namespace AdminTools.Agent;

public class Regis
{
    /// <summary>
    ///     Reads the ProfileList from the Windows Registry where the agent is installed on.
    /// </summary>
    /// <param name="registryKey">the registry key variable to read from</param>
    /// <param name="plist">the list to put the registry values</param>
    public static void GetProfileList(RegistryKey? registryKey, List<string> plist)
    {
        var names = registryKey?.GetSubKeyNames();

        foreach (var pl in names) 
            plist.Add(pl);
    }

    public static void GetProfileGuid(RegistryKey? registryKey, List<string> plist)
    {
        var names = registryKey.GetSubKeyNames();

        foreach (var pl in names) 
            plist.Add(pl);
    }
}