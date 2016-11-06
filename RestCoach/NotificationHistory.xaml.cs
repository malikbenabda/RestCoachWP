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
using Windows.Security.Authentication.Web;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel;
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RestCoach
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotificationHistory : Page
    {


        public NotificationHistory()
        {
            this.InitializeComponent();
            try
            {
                historyList.ItemsSource = SharedInfo.myWorkSessions;

            }
            catch (Exception)
            {
            }

            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
            Application.Current.Resuming += new EventHandler<Object>(App_Resuming);
        }

        private void App_Resuming(object sender, object e)
        {
            try
            {
            

            }
            catch (Exception)
            { }

        }

        private void App_Suspending(object sender, SuspendingEventArgs e)
        {
            try
            {
                Paramaters.savePreferencesData();
              
            }
            catch (Exception) { }
        }


        private void goBack(object sender, TappedRoutedEventArgs e)
        {
            SharedInfo.saveSessions();

            Frame.Navigate(typeof(MainPage));
        }

        private void deleteAction(object sender, TappedRoutedEventArgs e)
        {
            try
            {

                int selectedIndex = 0;
                selectedIndex = historyList.SelectedIndex;
                SharedInfo.myWorkSessions.RemoveAt(selectedIndex);
                historyList.ItemsSource = null;
                historyList.ItemsSource = SharedInfo.myWorkSessions;
               
            }
            catch (Exception) {            }

        }

        private void btnShare_Click(object sender, RoutedEventArgs e)
        {
            // share on facebook  title + description + duration 
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
            DataTransferManager.ShowShareUI();



        }
        private void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            try
            {
                int indx = historyList.SelectedIndex;
                WorkSession wksession = new WorkSession();
                wksession = SharedInfo.myWorkSessions[indx];

                args.Request.Data.Properties.Title = wksession.Title;
                args.Request.Data.Properties.Description = wksession.Description;
                args.Request.Data.Properties.ApplicationName = "Rest Coach";
               
                {
                    string Duration_of_work;
                    float hours;
                    int workDurationInMinutes = wksession.DurationInMinutes;
                    if (workDurationInMinutes > 60)
                    {
                        hours = (float)workDurationInMinutes / (float)60;
                        Duration_of_work = hours + " Hours";
                    }
                    else
                    {
                        Duration_of_work = workDurationInMinutes + "Minutes";
                    }
                    string workState = "";

                    if (wksession.Stressed) workState += " Stressed, "; else workState += " Relaxed, ";
                    if (wksession.Tired) workState += " Tired, "; else workState += "Energized, ";
                    if (wksession.Comfortable) workState += "Comfortable, "; else workState += "Uncomfortable, ";
                    if (wksession.Noise) workState += "Annoyed and "; else workState += "In Silence and ";
                    if (wksession.OnDesk) workState += "Sitting on my Desk"; else workState += "Working on the go";





                    args.Request.Data.SetText(" Today, I worked on "+wksession.Title+
                        " and it was basically : \n" + wksession.Description
                        +"\n My work Session for "+wksession.EndWorkTime.Date.ToString("dd/MM/yy")+
                        " lasted  from "+wksession.StartWorkTime.ToString("HH:mm")+" to  "+wksession.EndWorkTime.ToString("HH:mm") 
                        +".\n I was "+workState+" and I managed to finish "+ wksession.percentageOfWork+"% of  the total session time I had set for myself ! \n "
                        + "https://www.facebook.com/RestCoachApp/");
                    args.Request.Data.SetApplicationLink(new Uri("https://www.facebook.com/RestCoachApp/"));
                    args.Request.Data.SetWebLink(new Uri("https://www.facebook.com/RestCoachApp/"));
            //      args.Request.Data.SetHtmlFormat("");
                   
            
                }

            }
            catch (Exception)
            {            }
               
          
         
        }

        private void editing(object sender, TappedRoutedEventArgs e)
        {
            try
            {

                int selectedIndex = 0;
                selectedIndex   = (historyList.SelectedIndex);
                Frame.Navigate(typeof(Details), selectedIndex);
                         }
            catch (Exception)
            {
                          }

        }

        private void ScrollViewer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            scroller.ChangeView(null, null, 1.0f);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            float x = (float)(this.ActualHeight / 640.00);
            scroller.ChangeView(null, null, x);
           scroller.Height = this.ActualHeight;
        }
    }
}
