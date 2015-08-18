namespace Swagometer.Lib.Interfaces
{
    public interface IThing<out T>
    {
        T Duplicate();
        bool IsValid();
    }
}
