using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RestCoach
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatisticsPage : Page
    {
        int Stressed=0, Tired = 0, Comfortable = 0, Noise = 0, OnDesk = 0;
        int count = 1;

        public StatisticsPage()
        {
            this.InitializeComponent();

            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
            Application.Current.Resuming += new EventHandler<Object>(App_Resuming);
            loadStats();
        }

        private void App_Resuming(object sender, object e)
        {
            try
            {
              //  SharedInfo.loadSessions();

            }
            catch (Exception)
            { }

        }

        private void App_Suspending(object sender, SuspendingEventArgs e)
        {
            try
            {
                SharedInfo.saveSessions();
                Paramaters.savePreferencesData();
              //  Paramaters.saveTime(hours, minutes);
            }
            catch (Exception) { }
        }

        private void ScrollViewer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            scrollviewerStat.ChangeView(null, null, 1.0f);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            float x = (float)(this.ActualHeight / 640.00);
            scrollviewerStat.ChangeView(null, null, x);
            scrollviewerStat.Height = this.ActualHeight-50;
        }

        private void loadStats()
        {
            try
            {
                short Mon = 0, Tues = 0, Wed = 0, Thu = 0, Fri = 0, Sat = 0;
                count = SharedInfo.myWorkSessions.Count;
                for (int i = 0; i < count; i++)
                {
                    WorkSession w = new WorkSession();
                    w = SharedInfo.myWorkSessions[i];

                    if (w.Stressed) Stressed++;
                    if (w.Tired) Tired++;
                    if (w.Comfortable) Comfortable++;
                    if (w.Noise) Noise++;
                    if (w.OnDesk) OnDesk++;

                    int dur = 0;
                    dur = w.DurationInMinutes;
                    int bonus = (int)(w.overallState * 0.2 * dur);

                    short totTime = 0;
                    totTime = (short)(dur + bonus);

                    if (w.EndWorkTime.DayOfWeek == DayOfWeek.Monday) { Mon += totTime; }
                    if (w.EndWorkTime.DayOfWeek == DayOfWeek.Tuesday) { Tues += totTime; }
                    if (w.EndWorkTime.DayOfWeek == DayOfWeek.Wednesday) { Wed += totTime; }
                    if (w.EndWorkTime.DayOfWeek == DayOfWeek.Thursday) { Thu += totTime; }
                    if (w.EndWorkTime.DayOfWeek == DayOfWeek.Friday) { Fri += totTime; }
                    if (w.EndWorkTime.DayOfWeek == DayOfWeek.Saturday) { Sat += totTime; }
                }
                makePieCharts();
                doCollumn(Mon, Tues, Wed, Thu, Fri, Sat);
            }
            catch (Exception)
            { }

        }

    

        private void makePieCharts()
        {

            List<Tuple<string, int>> myList = new List<Tuple<string, int>>()
               {
                  new Tuple<string, int>("Relaxed", count-Stressed),

                      new Tuple<string, int>("Stressed",Stressed)
                     
            };

            (QOW1.Series[0] as PieSeries).ItemsSource = myList;

            //-------------------------------------


            myList = new List<Tuple<string, int>>()
               {
                       new Tuple<string, int>("Energetic", count-Tired),

                      new Tuple<string, int>("Tired",Tired)
            };

            (QOW2.Series[0] as PieSeries).ItemsSource = myList;

            //-------------------------------------

            myList = new List<Tuple<string, int>>()
               {
                      new Tuple<string, int>("Comfortable",Comfortable),
                       new Tuple<string, int>("Uneased", count-Comfortable)

            };

            (QOW3.Series[0] as PieSeries).ItemsSource = myList;


            //-------------------------------------

            myList = new List<Tuple<string, int>>()
               {
                   
                       new Tuple<string, int>("not disturbed ", count-Noise),
                          new Tuple<string, int>("disturbed by noise",Noise)
            };

            (QOW4.Series[0] as PieSeries).ItemsSource = myList;


            //-------------------------------------


            myList = new List<Tuple<string, int>>()
               {
                      new Tuple<string, int>("working On Desk",OnDesk),
                       new Tuple<string, int>("On the go", count-OnDesk)

            };

            (QOW5.Series[0] as PieSeries).ItemsSource = myList;

            //-------------------------------------






        }
        private void doCollumn(short Mon, short Tues, short Wed, short Thu, short Fri, short Sat)
        {       List<Tuple<string, int>> myList = new List<Tuple<string, int>>()
               {
                  new Tuple<string, int>("M", Mon),
                    new Tuple<string, int>("Tu", Tues),
                   new Tuple<string, int>("W",Wed),
                   new Tuple<string, int>("T", Thu),
                       new Tuple<string, int>("F",Fri),
                         new Tuple<string, int>("S", Sat)                  

            };

            (QOWD.Series[0] as ColumnSeries).ItemsSource = myList;
        }

      


        private void goBack(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
