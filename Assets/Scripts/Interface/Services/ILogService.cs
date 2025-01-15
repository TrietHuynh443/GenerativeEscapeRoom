using DI;

namespace Interface.Services
{
    public interface ILogService : IService
    {
        public void Log(string message);
    }
}