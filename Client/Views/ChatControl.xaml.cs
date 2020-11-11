using ChatService.Client.Service;
using ChatService.Client.ViewModels;
using ChatService.Shared.Models.Messages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatService.Client.Views
{
    /// <summary>
    /// Interaktionslogik für ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {
        public ChatControl()
        {
            InitializeComponent();
        }

        private void OpenImageDialoge_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "alle |*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                if(this.DataContext is ChatViewModel viewModel)
                {
                    viewModel.SendCommand.Execute(openFileDialog.FileName);
                }
            }
        }

        private void MessagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is ListBox listBox && listBox.SelectedIndex >= 0 && listBox.SelectedItem is Message message)
            {
                MessageService.OpenMessage(message.Content);
                listBox.SelectedIndex = -1;
            }
        }
    }
}
