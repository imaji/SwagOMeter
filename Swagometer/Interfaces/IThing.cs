namespace Swagometer.Interfaces
{
    public interface IThing<T>
    {
        T Duplicate();
        bool IsValid();
    }
}
