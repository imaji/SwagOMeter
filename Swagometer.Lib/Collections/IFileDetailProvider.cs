
namespace Swagometer.Lib.Collections
{
    public interface IFileDetailProvider
    {
        string FileLocation { get; }
        string SwagWinnersFileFormat { get; }
    }

    public class FileDetailProvider : IFileDetailProvider
    {
        public static FileDetailProvider Create(string fileLocation, string swagWinnersFileFormat)
        {
            var fileDetailProvider = new FileDetailProvider
            {
                FileLocation = fileLocation ,
                SwagWinnersFileFormat = swagWinnersFileFormat
            };
            return fileDetailProvider;
        }

        public string FileLocation { get; private set; }
        public string SwagWinnersFileFormat { get; private set; }
    }
}
