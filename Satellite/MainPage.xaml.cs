//Иван латентный пидорас
using Microsoft.EntityFrameworkCore;
using Satellite.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Satellite
{
    /// <summary>
    /// Иван лох вдвойне.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Todos.SelectedIndex = 0;
        }
        private void InsertTodoBtn_Click(object sender, RoutedEventArgs e)
        {
            App.TODO_VIEW_MODEL.InsertNewTodo(NewTodoTxtBox.Text);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Todos.ItemsSource = App.TODO_VIEW_MODEL.GetTodos();
        }
        private void AddSheduleButton_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ClearSheduleButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
