namespace CyberInfection.Extension.Pool
{
    public interface IPoolContainer
    {
        void Push(IPoolable poolObject);
        IPoolable Pop();
    }
}