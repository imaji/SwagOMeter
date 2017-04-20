using Swagometer.Lib.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public class AttendeeDialogFactory : IDialogFactory<AttendeeBase>
    {
        public ICreateNewThings<AttendeeBase> CreateDialog()
        {
            var viewModel = new CreateAttendeeViewModel();

            return new CreateAttendeeDialog(viewModel);
        }
    }
}
