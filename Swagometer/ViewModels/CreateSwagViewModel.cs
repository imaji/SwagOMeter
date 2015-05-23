using Swagometer.Lib.Interfaces;
using Swagometer.Lib.Objects;
using Swagometer.Objects;

namespace Swagometer.ViewModels
{
    public class CreateSwagViewModel : CreateThingViewModel<ISwag>
    {
        public string Thing { get; set; }

        public string Company { get; set; }

        protected override void ExecuteCreate()
        {
            var newSwag = new Swag() { Company = Company, Thing = Thing };

            if (newSwag.IsValid())
                NewThing = newSwag;

            FireThingGood(newSwag.IsValid());
        }
    }
}
