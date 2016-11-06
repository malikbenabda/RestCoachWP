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
    public sealed partial class AboutPage : Page
    {
        readonly string APPDESCRIPTION_XL = "Forget those hours spent on counting the hours you've spent managing work time.\n" +
            "This application is the convinient solution for you not only to focus on your studies but also to increase your productivity.\n" +
            "With RestCoach application you'll be able to organise your work schedule within your needs of rest.\n"
            + "RestCoach will help you avoid being overwhelmed by work by distracting you every now and then by sending notifications to alert you to take a break.\n"
            + "RestCoach uses a humouristic way to distract you from tiring yourself over work by poping funny advices and proverbs and sometimes inspirational quotes.\n"
            + "RestCoach has also built-in feathures like statistics so you can keep monitoring your work time , periods and efficiency levels.\n"
            + "If you would like even more information on why tracking your time work is such an important task, please visit the RestCoach Facebook Page today!\n" +
            "We are pleased to share a video with interested readers to help you to increase your productivity. \n";

        readonly string APPDESCRIPTION_S = "Forget those hours spent managing work time.\n" +
                "This application is the convinient solution for you! \n" +
                    "Not only to focus on your studies but also to increase your productivity.\n" +
                "RestCoach lets you organise your work schedule within your needs of rest.\n"
                + "Our App will help you avoid being overwhelmed by work.\n"
                 + "RestCoach uses a humouristic way to distract you from tiring yourself.\n"
                + "It has built-in feathures like statistics to be more efficient.\n"
                + "If you would like even more information visit our Facebook Page!\n";

  //      readonly string Kestouri_Description;
//        readonly string Benabda_Description;


        public AboutPage()
        {
            this.InitializeComponent();
            descriptionTextBox.Text = APPDESCRIPTION_S;
        }



        private void goHome(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
          //  float taux=  this.MaxWidth/


            if (this.ActualWidth > 1000)
            { descriptionTextBox.Text = APPDESCRIPTION_XL;
                descriptionTextBox.FontSize = 14;

            }else
                   if (this.ActualWidth > 600)
            {
                descriptionTextBox.Text = APPDESCRIPTION_S;
                descriptionTextBox.FontSize = 16;

            }

            else
            {
                descriptionTextBox.Text = APPDESCRIPTION_S;
                descriptionTextBox.FontSize = 13;
            }

            float x = (float)(this.ActualHeight / 640.00);
            contentScroller.ChangeView(null, null, x);
            contentScroller.Height = this.ActualHeight-55;




        }

        private void descriptionScroller_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            descriptionScroller.ChangeView(null,null,1.0f);

        }

        private void developpersScroll_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            developpersScroll.ChangeView(null, null, 1.0f);
        }
        private async void openBrowser(string url)

        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }


        // contact malik links
        private void fbIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {

            openBrowser("https://www.facebook.com/Luca.B.A1992");
        }         
         private void GMailIconM_Tapped(object sender, TappedRoutedEventArgs e)
        {
            openBrowser("mailto:malik.benabda@esprit.tn");
        }
            private void LinkedInIconM_Tapped(object sender, TappedRoutedEventArgs e)
        {
         
            openBrowser("https://tn.linkedin.com/in/malikbenabda");
        }
                private void TwitterIconM_Tapped(object sender, TappedRoutedEventArgs e)
        {
            openBrowser("https://twitter.com/MalikBenabda");

        }



        private void fbIconH_Tapped(object sender, TappedRoutedEventArgs e)
        {
            openBrowser("https://www.facebook.com/hedi.kestouri");
        }

        private void GMailIconH_Tapped(object sender, TappedRoutedEventArgs e)
        {
            openBrowser("medhedhi.kestouri@esprit.tn");

        }

        private void LinkedInIconH_Tapped(object sender, TappedRoutedEventArgs e)
        {
            openBrowser("https://tn.linkedin.com/in/hedi-kestouri-0646789b/en");

        }

        private void TwitterIconH_Tapped(object sender, TappedRoutedEventArgs e)
        {
            openBrowser("https://twitter.com/HediKass117");
        }
    }
}
