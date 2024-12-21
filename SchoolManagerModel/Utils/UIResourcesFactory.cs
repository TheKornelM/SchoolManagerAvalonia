using System.Reflection;
using System.Resources;

namespace SchoolManagerModel.Utils;

public static class UIResourceFactory
{
    public static ResourceManager GetNewResource()
    {
        return new ResourceManager("SchoolManagerModel.Resources.UI",
                Assembly.GetExecutingAssembly());
    }
}
