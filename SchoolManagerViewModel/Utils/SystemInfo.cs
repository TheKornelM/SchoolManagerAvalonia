using System.Runtime.InteropServices;

namespace SchoolManagerViewModel.Utils;

public static class SystemInfo
{
    public static bool IsMobilePlatform()
    {
        /*
         * https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.runtimeinformation.runtimeidentifier?view=net-9.0
         * https://learn.microsoft.com/en-us/dotnet/core/rid-catalog
         */
        return RuntimeInformation.RuntimeIdentifier.StartsWith("android") 
               || RuntimeInformation.RuntimeIdentifier.StartsWith("ios");
    }
}