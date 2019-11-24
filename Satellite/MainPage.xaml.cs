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
using Microsoft.EntityFrameworkCore;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Satellite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<AddListItem> ListItems { get; set; } = new List<AddListItem>();
        public MainPage()
        {
            this.InitializeComponent();
            CheckLoginStatus();
        }
   
        private void CheckLoginStatus()
        {
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            ApplicationDataCompositeValue number = (ApplicationDataCompositeValue)roamingSettings.Values["RoamingPages"];
            if (number != null)
            {
                string s = number["MainPagePosts"] as string;
                int i = Convert.ToInt32(s);
                if (i > 0)
                {
                    for (int n = 0; n < i; ++n)
                    {
                        if (Posts.Count == 0)
                            Posts.Add(new Post() { Header = "Текущее расписание" });
                        else
                            Posts.Add(new Post() { Header = "Внешкольное расписание" });
                    }
                    MainGridView.ItemsSource = Posts;
                }
            }
        }
        private void AddSheduleButton_Click(object sender, RoutedEventArgs e)
        {
            //if (Posts.Count == 0)
            //    Posts.Add(new Post() { Header = "Текущее расписание" });
            //else
            //    Posts.Add(new Post() { Header = "Внешкольное расписание" });

            //MainGridView.ItemsSource = null;
            //MainGridView.ItemsSource = Posts;
            //int s = Posts.Count;
            //ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            //ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue
            //{
            //    ["MainPagePosts"] = s.ToString()
            //};
            //roamingSettings.Values["RoamingPages"] = composite;
            AddDBListGrid.Visibility = Visibility.Visible;
        }
        private void ClearSheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Posts.Clear();
            MainGridView.ItemsSource = null;
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue
            {
                ["MainPagePosts"] = "0"
            };
            roamingSettings.Values["RoamingPages"] = composite;
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeGrid.Visibility = Visibility.Visible;
            ListsGrid.Visibility = Visibility.Collapsed;
        }
        private void ListsButton_Click(object sender, RoutedEventArgs e)
        {
            HomeGrid.Visibility = Visibility.Collapsed;
            ListsGrid.Visibility = Visibility.Visible;
        }
        private void OrbitButton_Click(object sender, RoutedEventArgs e)
        {
            HomeGrid.Visibility = Visibility.Collapsed;
            ListsGrid.Visibility = Visibility.Collapsed;
        }
        private void DailyShButton_Click(object sender, RoutedEventArgs e)
        {
        }
        private void SchoolShButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddDBListGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AddDBListGrid.Visibility = Visibility.Collapsed;
        }

        private void AddDBListItemButton_Click(object sender, RoutedEventArgs e)
        {
            if(PointTextBox.Text != null)
            {
                ListItems.Add(new AddListItem() { Point = PointTextBox.Text });
                NewSListView.ItemsSource = null;
                NewSListView.ItemsSource = ListItems;
            }
        }

        private void NewDBListButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Core())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                // Adds a publisher
                var publisher = new Publisher
                {
                    Name = "Твоя папаша"
                };
                context.Publisher.Add(publisher);

                // Adds some books
                context.Post.Add(new Post
                {
                    ID = "16102018",
                    Header = "Какое-то охуевшее расписание",
                    SPoint = "Урок сРачевой",
                    Publisher = publisher
                });
                // Saves changes
                context.SaveChanges();
            }
        }
    }
}
