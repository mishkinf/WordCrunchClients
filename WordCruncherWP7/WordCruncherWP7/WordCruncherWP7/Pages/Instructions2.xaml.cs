using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WordCruncherWP7
{
    public partial class Instructions2 : PhoneApplicationPage
    {
        public Instructions2()
        {
            InitializeComponent();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                NavigationService.Navigate(new Uri("/Pages/Instructions3.xaml", UriKind.Relative));
            });
        }
    }
}
