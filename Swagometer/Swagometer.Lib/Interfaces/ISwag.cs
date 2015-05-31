namespace Swagometer.Lib.Interfaces
{
    public interface ISwag : IThing<ISwag>
    {
        string Company { get; set; }
        string Thing { get; set; }
    }
}
