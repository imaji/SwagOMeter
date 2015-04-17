namespace Swagometer.Interfaces
{
    public interface ISwag : IThing<ISwag>
    {
        string Company { get; set; }
        string Thing { get; set; }
    }
}
