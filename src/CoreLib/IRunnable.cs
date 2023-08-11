using System.Threading.Tasks;
using CoreLib.Model;

namespace CoreLib
{
    public interface IRunnable
    {
        Task<OperationResult> Run();
    }
}
