using System.IO;
using System.Linq;
using SPMeta2.Definitions;
using SPMeta2.Models;
using SPMeta2.Syntax.Default;
using SPMeta2Contrib.Core.Store;

namespace SPMeta2Contrib.Provision.Deployers
{
    public class FileDeployerCacheable: FileDeployer
    {
        protected readonly IHashStore<string> ProvisionCache;

        public FileDeployerCacheable(ModelNode site, ModelNode web, ListDefinition rootNode, string sourcePath,
            IHashStore<string> provisionCache) : base(site, web, rootNode, sourcePath)
        {
            ProvisionCache = provisionCache;
        }

        protected override void ProcessDirectory(string path, ModelNode parent)
        {
            var files = Directory.EnumerateFiles(path);
            foreach (var fileName in files.Where(fileName => !ProvisionCache.HasSame(fileName)))
            {
                parent.AddModuleFile(new ModuleFileDefinition
                {
                    FileName = Path.GetFileName(fileName),
                    Content = File.ReadAllBytes(fileName)
                });
                ProvisionCache.AddOrUpdate(fileName);
            }

            foreach (string name in Directory.EnumerateDirectories(path))
            {
                string folderName = name;
                parent.AddFolder(
                    new FolderDefinition { Name = name.Remove(0, name.LastIndexOf('\\') + 1) },
                    folder => ProcessDirectory(folderName, folder));
            }
        }
    }
}
