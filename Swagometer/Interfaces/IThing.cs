namespace Swagometer.Interfaces
{
    public interface IThing<out T>
    {
        T Duplicate();
        bool IsValid();
    }
}
