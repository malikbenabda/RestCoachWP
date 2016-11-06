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
    public sealed partial class Config2Page : Page
    {
       
        

        public Config2Page()
        {
            this.InitializeComponent();

          
        
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            Paramaters.initialized = true;
           
                Paramaters.WorkHours = WorkTimePicker.Time.Hours;
                Paramaters.WorkMin = WorkTimePicker.Time.Minutes;
                
            setPreference();
            Paramaters.savePreferencesData();
           
            
            Frame.Navigate(typeof(MainPage));

        }
        
        private void setPreference()
        {

            Paramaters.PrefWorkHour = WorkTimePicker.Time.Hours;
            Paramaters.PrefWorkMin = WorkTimePicker.Time.Minutes;
            Paramaters.PrefSnoozeHours = SnoozeTimePicker.Time.Hours;
            Paramaters.PrefSnoozeMinutes = SnoozeTimePicker.Time.Minutes;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            setTimePickers();
            

        }
        private void setTimePickers()
        {

            WorkTimePicker.Time = new TimeSpan(Paramaters.PrefWorkHour, Paramaters.PrefWorkMin, 0);
            SnoozeTimePicker.Time = new TimeSpan(Paramaters.PrefSnoozeHours, Paramaters.PrefSnoozeMinutes, 0);
        }
        private void btnCancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            float x = (float)(this.ActualHeight / 640.00);
            scroller.ChangeView(null, null, x);
            scroller.Height = this.ActualHeight-50;
        }
    }
}
