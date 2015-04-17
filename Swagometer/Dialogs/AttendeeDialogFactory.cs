using Swagometer.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public class AttendeeDialogFactory : IDialogFactory<IAttendee>
    {
        public ICreateNewThings<IAttendee> CreateDialog()
        {
            var viewModel = new CreateAttendeeViewModel();

            return new CreateAttendeeDialog(viewModel);
        }
    }
}
