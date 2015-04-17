using Swagometer.Interfaces;

namespace Swagometer.Dialogs
{
    public interface ICreateNewThings<out TThing>
        where TThing : IThing<TThing>
    {
        TThing NewThing { get; }

        bool? ShowDialog();
    }
}
