using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RestCoach
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Details : Page
    {
        WorkSession wks;
        int order;
        public Details()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            order = 0;
            order= Int16.Parse(e.Parameter.ToString());
            wks = new WorkSession();
            wks = SharedInfo.myWorkSessions.ElementAt(order);

            try
            {    titleTextbox.Text = wks.Title;
                descriptionTextBox.Text = wks.Description;
            }
            catch (Exception)
            {       }



        }


        private void goBack(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotificationHistory));
        }

  

        private void save(object sender, TappedRoutedEventArgs e)
        {
            wks.Title = titleTextbox.Text;
            wks.Description = descriptionTextBox.Text;
            SharedInfo.myWorkSessions.RemoveAt(order);
            SharedInfo.myWorkSessions.Insert(order, wks);
            Frame.Navigate(typeof(NotificationHistory));
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            float x = (float)(this.ActualHeight / 640.00);
            scroller.ChangeView(null, null, x);
            scroller.Height = this.ActualHeight -100;
        }
    }
}
