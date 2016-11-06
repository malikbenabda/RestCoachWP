using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RestCoach
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var storageFile =
              await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(
                new Uri("ms-appx:///CortanaVoiceCommands.xml"));
            await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager
                .InstallCommandDefinitionsFromStorageFileAsync(storageFile);
        }

        /****        Dispacher part
      **/

        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 0;
        String adviceURIString = "http://api.adviceslip.com/advice";
        int hours;
        int minutes;
        int seconds;
        DateTime WorkSessionStartTime, WorkSessionEndTime;
        WorkSession wks;
        DateTime timeSuspended;
        Notification mynotif;
        String[] notificationType = new String[] { "START", "END", "Advice", "PAUSE", "ALERT" };
        Boolean isWorking = false;

        public MainPage()
        {
            this.InitializeComponent();
           // DataBaseController.createTable();
            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
            Application.Current.Resuming += new EventHandler<Object>(App_Resuming);
        }

        private void App_Resuming(object sender, object e)
        {
            try
            {
               
                Paramaters.loadTime();
            }
            catch (Exception)
            { }
        }
        
        private void App_Suspending(object sender, SuspendingEventArgs e)
        {
            try
            {
             //   SharedInfo.saveSessions();
                Paramaters.savePreferencesData();
                Paramaters.saveTime(hours, minutes, seconds);
                timeSuspended = DateTime.Now;
            }
            catch (Exception) { }

        }

        private void initializeTime()

        {

            if (!Paramaters.initialized)
            { // get paramaters from storage 
                Paramaters.loadPreferencesData();

             
            }


            if (Paramaters.initialized)
            {

                if (!isWorking)
                {
                    isWorking = Paramaters.WorkingState;
                    hours = Paramaters.PrefWorkHour;
                    minutes = Paramaters.PrefWorkMin;
                }
            }
            else
            {

                //to be done only first run of app

                Paramaters.WorkHours = hours;
                Paramaters.WorkMin = minutes;
                Paramaters.PrefSnoozeHours = 0;
                Paramaters.PrefSnoozeMinutes = 15;

                Paramaters.WorkingState = isWorking;
                Paramaters.initialized = true;

            }



        }
        public void DispatcherTimerSetup()  //starts a timer 
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //IsEnabled defaults to false
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            startTimeText.Text = DateTime.Now.ToString("ddd HH:mm");
            dispatcherTimer.Start();

        }
        void dispatcherTimer_Tick(object sender, object e) //ticker timer if coundown is done it stops the time and ends session
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            //Time since last tick should be very very close to Interval

            Boolean timeIsLeft = countingDown();


            TimerLog.Text = timeFormat();

            timesTicked++;
         //   notifyToRelax(Paramaters.PrefSnoozeMinutes * 60 + Paramaters.PrefSnoozeHours * 3600); //  this should be chnged to a methode that reminds user  to relax every period of workingtime
            if (!timeIsLeft)
            {
                stopTime = time;
                dispatcherTimer.Stop();
                //IsEnabled should now be false after calling stop
                span = stopTime - startTime;
                //notify of end of session
                timeup();
            }
        }
        private void timeup() // changes working state and calls endSessionTimeNotify()
        {
            isWorking = false;
            setimage();
            //--------------
            endSessionTimeNotify();
            endingWorkSession();





        }
        private bool countingDown() // return if counter is still active
        {
            if (seconds == 0 && minutes == 0 && hours == 0) { return false; }
            else
            {

                TimeSpan ts = WorkSessionEndTime.Subtract(DateTime.Now);
                seconds = ts.Seconds;
                minutes = ts.Minutes;
                hours = ts.Hours;
                return true;


            }
        }
        private String timeFormat() // formats h,m,s to string hh:mm:ss
        {
            String h = hours + "", m = minutes + "", s = seconds + "";

            if (hours < 10) { h = "0" + hours; }
            if (minutes < 10) { m = "0" + minutes; }
            if (seconds < 10) { s = "0" + seconds; }

            return h + ":" + m + ":" + s;
        }

        //********** end of timer ***********************//

        private void setimage()
        {// changes image

            if (!isWorking)
            {

                isWorkingTextBloc.Text = "Start Work";
                imageState.Source = new BitmapImage(new Uri("ms-appx:///Assets/cutomPics/working.png"));

                //changes buttonStack.Background to ornge or gold 
                // buttonStack.Background = new SolidColorBrush(Windows.UI.Colors.Blue);

            }
            else
            {
                isWorkingTextBloc.Text = "Pause";
                imageState.Source = new BitmapImage(new Uri("ms-appx:///Assets/cutomPics/resting.png"));
            }


        }
        protected override void OnNavigatedTo(NavigationEventArgs e) // gets params 
        {
            {

                initializeTime();
                 SharedInfo.loadSessions();

//                setimage();
                startTimeText.Text = DateTime.Now.ToString("ddd HH:mm");
                mylist.DataContext = SharedInfo.myNotifications;

            }


            isWorking = Paramaters.WorkingState;
            seconds = 0;


            TimerLog.Text = timeFormat();
            if (isWorking)
            {

                hours = Paramaters.WorkHours;
                minutes = Paramaters.WorkMin;
                seconds = Paramaters.WorkSeconds;
                DispatcherTimerSetup();

            }

            setTimes();
            setimage();

            base.OnNavigatedTo(e);
        }
        public void ConfigureTime()
        {
            if (isWorking)
            {
                Paramaters.WorkingState = true;
                Paramaters.WorkHours = hours;
                Paramaters.WorkMin = minutes;
                Paramaters.WorkSeconds = seconds;
            }
            else
            {
                Paramaters.WorkingState = false;

            }
            Frame.Navigate(typeof(Config2Page));
        }
        private void setTimes() // set ending time textBloc
        {

            DateTime dt = DateTime.Now.Add(new TimeSpan(hours, minutes, seconds));
            //  endTimeText.Text= dt.Hour+ ":" +dt.Minute ;
            endTimeText.Text = dt.ToString("ddd HH:mm");


        }
        private void StartStopWork(object sender, TappedRoutedEventArgs e)
        {
            if (isWorking)
            {
                endingWorkSession();
                pausedSessionNotifier();

            }
            else
            {
                //start timer
                DispatcherTimerSetup();
                //changes buttonStack.Background to ornge or gold 
                //buttonStack.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                //  -------start Worksession
                WorkSessionStartTime = DateTime.Now;
                WorkSessionEndTime = WorkSessionStartTime.AddHours(Paramaters.PrefWorkHour).AddMinutes(Paramaters.PrefWorkMin);


                startSessionTimeNotify();
            }

            isWorking = !isWorking;
            setTimes();
            setimage();
           

        }

        //        ----------------------Notification methods --------------------


        private void sendNotification(Notification notif) // adds a notification to Listview
        {
            SharedInfo.myNotifications.Insert(0, notif);
            

            try { SharedInfo.myNotifications.RemoveAt(6); }
            catch (Exception) { }

            mylist.ItemsSource = null;
            mylist.ItemsSource = SharedInfo.myNotifications;

            //---------------------TOAST-------------------------------------
            if (notif.Type.Equals("START")) {
                sendToast(0,1, "Work Session Started", ""+DateTime.Now.ToString("ddddd HH:mm"));
              sendToast((Paramaters.PrefSnoozeMinutes + Paramaters.PrefSnoozeHours * 60),1, "Break Time", NotificationSelector()); 
                }
            if (notif.Type.Equals("END")) { sendToast(0,2, "Work Session Ended", ""+DateTime.Now.ToString("dddddd HH:mm")); }
           if (notif.Type.Equals("Advice"))
            {
                if ( hours*60+minutes > (Paramaters.PrefSnoozeHours*60+Paramaters.PrefSnoozeMinutes))


                sendToast(Paramaters.PrefSnoozeMinutes + Paramaters.PrefSnoozeHours * 60,1, "Break Time Reminder", NotificationSelector()); }






            if (notif.Type.Equals("PAUSE")) { sendToast(0,2, "Work Session Paused",""+DateTime.Now.ToString("dddddd HH:mm")); }


           





        }
        private void sendToast(int timeAddedMinutes, int timeAddedSecs, string toastTitle ,string toastdescription)
        {


         

            try
            {
                DateTime dueTime = DateTime.Now.AddMinutes(timeAddedMinutes).AddSeconds(timeAddedSecs);
                // Set up the notification text.
                var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
                var strings = toastXml.GetElementsByTagName("text");
                strings[0].AppendChild(toastXml.CreateTextNode(toastTitle));
                strings[1].AppendChild(toastXml.CreateTextNode(toastdescription));

                // Create the toast notification object.
                var toast = new ScheduledToastNotification(toastXml, dueTime);
                var idNumber = 5;  // Generates a unique ID number for the notification.
                toast.Id = "Toast" + idNumber;

                // Add to the schedule.
                ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
            }
            catch (Exception)
            {        }

           



        }
        private void notifyToRelax(int seconds2Relax) // add relax notification with each  nbr of seconds 
        {

            if ((timesTicked % seconds2Relax) == 0)
            {
                String adviceToSend = NotificationSelector(); ;

                //send notification every  seconds2Relax   seconds( default 1200s - 20mins )
                Notification n = new Notification( notificationType[2], "Advice", adviceToSend);
                n.Timestamp = DateTime.Now;
                sendNotification(n);
                
            }
        }
        private void endSessionTimeNotify() //add end of session notif to listview of notifs
        {


            mynotif = new Notification();
            mynotif.Title = "Session over";

            //--TODO  -----to be filled dynamicly later
            mynotif.Description = "You have completed your work session , good job ! ";
            mynotif.Timestamp = DateTime.Now;
            mynotif.Type = notificationType[1];

            // sending notification 
            sendNotification(mynotif);



        }
        private void startSessionTimeNotify()
        {


            mynotif = new Notification();
            mynotif.Title = "Session started";

            //--TODO  -----to be filled dynamicly later
            mynotif.Description = "Work hard but not too hard! ";
            mynotif.Timestamp = DateTime.Now;
            mynotif.Type = notificationType[0];

            // sending notification 
            sendNotification(mynotif);

        }
        private void pausedSessionNotifier()
        {


            mynotif = new Notification();
            mynotif.Title = "Session Paused";

            //--TODO  -----to be filled dynamicly later
            mynotif.Description = "Finished work already ?  ";
            mynotif.Timestamp = DateTime.Now;
            mynotif.Type = notificationType[3];

            // sending notification 
            sendNotification(mynotif);

        }
        private void go2History(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotificationHistory));
        }
        private void GoStats(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(StatisticsPage));
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (isWorking)
            {
                endingWorkSession();
                isWorking = false;

                Paramaters.WorkHours = hours;
                Paramaters.WorkMin = minutes;
                Paramaters.WorkSeconds = seconds;


            }

        }
        private void endingWorkSession()
        {
            //ending worksession
            dispatcherTimer.Stop();
          
            //TODO  -------end Worksession
            wks = new WorkSession();
            wks.StartWorkTime = WorkSessionStartTime;
            wks.Title = "Work";
            wks.Description = "Same Stuff , Different Day : converting coffee and food into service and code";
            wks.EndWorkTime = DateTime.Now;
            wks.TimeLeftInMinutes = (hours * 60 + minutes + 1);
            wks.DurationInMinutes = timesTicked / 60;
            wks.percentageOfWork = (timesTicked * 100)
                / (hours * 3600 + minutes * 60 + seconds + timesTicked);

            //check states
            wks.overallState = 0;

            if (comfortableToggleButton.IsChecked.Value)
            { wks.Comfortable = true; wks.overallState++; wks.comforImage = "Assets/cutomPics/Sofa-c.png"; }
            else { wks.Comfortable = false; wks.overallState--; }

            if (noiseToggleButton.IsChecked.Value)
            { wks.Noise = true; wks.overallState--; wks.noiseImage = "Assets/cutomPics/Horn-02-WF-c.png"; }
            else { wks.Noise = false; wks.overallState++; }

            if (stressedToggleButton.IsChecked.Value)
            { wks.Stressed = true; wks.overallState--; wks.stressImage = "Assets/cutomPics/stressed-c.png"; }
            else { wks.Stressed = false; wks.overallState++; }

            if (tiredToggleButton.IsChecked.Value)
            { wks.Tired = true; wks.overallState--; wks.tiredImage = "Assets/cutomPics/tired-c.png"; }
            else { wks.Tired = false; wks.overallState++; }

            if (onDeskToggleButton.IsChecked.Value)
            { wks.OnDesk = true; wks.overallState++; wks.deskImage = "Assets/cutomPics/Student-Laptop-c.png"; }
            else { wks.OnDesk = false; wks.overallState--; }


            //--- add to  sessions list in DB
            //  DataBaseController.insertData(wks);
            if ( SharedInfo.myWorkSessions == null)
            SharedInfo.myWorkSessions = new System.Collections.Generic.List<WorkSession>();

            SharedInfo.myWorkSessions.Insert(0, wks);
           SharedInfo.saveSessions();
 

        }

        // get advice parsed fom advice page http://api.adviceslip.com/advice  and adds it to advice list in sharedInfo
        private async void parse_advice()
        {
            String advice;

            try
            {
                HttpClient cl = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, adviceURIString);
                HttpResponseMessage response = await cl.SendAsync(request);
                var data = await response.Content.ReadAsStringAsync();
                var parsedObject = JsonConvert.DeserializeObject<JObject>(data);
                JObject jsonObject = new JObject();
                jsonObject["slip"] = parsedObject["slip"];

                JObject jsonObjectcollection = new JObject();
                advice = (string)jsonObject["slip"]["advice"];


            }
            catch (Exception) { advice = ""; }

            SharedInfo.advices.Insert(0, advice);

        }
        //choses advice from local list or web
        private String NotificationSelector()
        {

            //choose wich notification text to send

            parse_advice();
            if (SharedInfo.advices[0].Equals(""))
            {
                int pos = new Random().Next(0, SharedInfo.advicesALL.Count - 1);
                SharedInfo.advices[0] = SharedInfo.advicesALL[pos];
            }

            return SharedInfo.advices[0];
        }
        //navigate to configure time when tapping settings image
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {

            ConfigureTime();
        }
        //navigate to configure time when 2xtapping timer
        private void StackPanel_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            ConfigureTime();
        }

        private void Info(object sender, TappedRoutedEventArgs e)
        {
            
            Frame.Navigate((typeof(AboutPage)));
        }


        public void TourGuide()
        {
         
          



        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        { 
            //copy in every Page 
            float x =(float) (this.ActualHeight / 640.00);
            contentScroller.ChangeView(null, null,x);
            contentScroller.Height = this.ActualHeight -50 ;
        }

        private void next_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }





    }

}





