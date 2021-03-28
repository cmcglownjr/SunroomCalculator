using Avalonia.Controls;
using MessageBox.Avalonia;

namespace SunroomCalculatorAvalonia.Models
{
    public static class SunroomMessageBox
    {
        public static void SunroomMessageBoxDialog(string title, string message)
        {
            var msBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBox.Avalonia.DTO.MessageBoxStandardParams{
                    ContentTitle = title,
                    ContentMessage = message,
                    Icon = MessageBox.Avalonia.Enums.Icon.Error,
                    WindowIcon = DiagramModel.SunroomIcon,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                });
            msBoxStandardWindow.Show();
        }
    }
}