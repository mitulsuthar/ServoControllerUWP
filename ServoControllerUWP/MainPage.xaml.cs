using Newtonsoft.Json;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ServoControllerUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            try
            {
                ///https://cdn-learn.adafruit.com/downloads/pdf/adafruit-16-channel-servo-driver-with-raspberry-pi.pdf
                _pca9685 = new Pca9685();
                //_pca9685.SetPWMFrequency(1000);
            }
            catch (Exception ex)
            {
                exceptionTextbox.Log(ex.Message);
                exceptionTextbox.Log(JsonConvert.SerializeObject(ex));
            }
        }

        private Pca9685 _pca9685;
        
       
        private async void initBtn_Click(object sender, RoutedEventArgs e)
        {

            exceptionTextbox.Log("Trying to initialize PCA9685");
            await _pca9685.InitPCA9685Async();

        }

        private void setPWMFrequencyBtn_Click(object sender, RoutedEventArgs e)
        {
            var pwmFrequency = Convert.ToDouble(pwmFrequencyTextbox.Text);
            exceptionTextbox.Log($"PWM Frequency : {pwmFrequency}");
            _pca9685.SetPWMFrequency(pwmFrequency);
        }
        
        private void Servo0AngleBtn_Click(object sender, RoutedEventArgs e)
        {

            //TODO: Try catch 
            var turnAngle = Convert.ToDouble(Servo0AngleTextbox.Text);
            _turnServoPin(0, turnAngle);
        }

        private void Servo1AngleBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Try catch 
            var turnAngle = Convert.ToDouble(Servo1AngleTextbox.Text);
            _turnServoPin(1, turnAngle);
        }

        private void Servo2AngleBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Try catch 
            var turnAngle = Convert.ToDouble(Servo2AngleTextbox.Text);
            _turnServoPin(2, turnAngle);
        }

        private void Servo3AngleBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Try catch 
            var turnAngle = Convert.ToDouble(Servo3AngleTextbox.Text);
            _turnServoPin(3, turnAngle);
        }

        private void _turnServoPin(int pinNumber, double angle)
        {
            var ticks = Convert.ToUInt16(120 + (2.7 * angle));
            if (ticks > 606)
            {
                exceptionTextbox.Log($"ticks {ticks} greater than {602} aka 180 degrees");
            }
            else
            {
                exceptionTextbox.Log($"SetPin({pinNumber},{ticks},false) i.e. angle {angle}");
                _pca9685.SetPin(pinNumber, ticks, false);
            }
        }
    }
    public static class Extensions
    {
        public static void Log(this TextBlock block, string message)
        {
            block.Text += "\n";
            block.Text += $"{DateTime.Now.ToString()}: {message}";
        }
    }
}

