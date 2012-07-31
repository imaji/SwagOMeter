using Swagometer.Interfaces;

namespace Swagometer
{
    public interface ISwag : IThing<ISwag>
    {
        string Company { get; set; }
        string Thing { get; set; }
    }
}
