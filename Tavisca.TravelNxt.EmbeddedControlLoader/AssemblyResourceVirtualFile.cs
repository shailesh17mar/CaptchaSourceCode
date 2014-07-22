using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Tavisca.TravelNxt.ControlLoader
{
    public class AssemblyResourceVirtualFile : VirtualFile
    {
        private readonly string _path;

        public AssemblyResourceVirtualFile(string virtualPath)
            : base(virtualPath)
        {
            _path = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            var parts = _path.Split('/');
            var assemblyName = parts[2];
            var resourceName = parts[3];

            var assembly = Assembly.Load(assemblyName);
            if (assembly == null)
            {
                throw new Exception("Failed to load " + assemblyName);
            }

            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new Exception("Failed to load " + resourceName);
            }
            return stream;
            
        }
    }
}