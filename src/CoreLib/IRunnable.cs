using CoreLib.Model;
using System.Threading.Tasks;

namespace CoreLib
{
    public interface IRunnable
    {
        Task<OperationResult> Run();
    }
}
