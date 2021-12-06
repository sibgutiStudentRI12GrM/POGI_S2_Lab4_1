using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace lab_4_1 {
    public partial class MainWindow : Window {
        private int ClockSize = 500;
        private int ClockCenterPosition {
            get {
                return ClockSize / 2;
            }
        }
        //get degree for current time for each arrow
        private double HoursArrowDegree {
            get {
                DateTime currentTime = DateTime.Now;
                double currentHours = (double)currentTime.Hour;
                double currentHoursFraction = (double)currentTime.Minute / 60;
                double currentFullMinutes = currentHours + currentHoursFraction;
                return (360 / 24) * currentFullMinutes;
            }
        }

        private double MinutesArrowDegree {
            get {
                DateTime currentTime = DateTime.Now;
                double currentMinutes = (double)currentTime.Minute;
                double currentMinutesFraction = (double)currentTime.Second / 60;
                double currentFullMinutes = currentMinutes + currentMinutesFraction;
                return (360 / 60) * currentFullMinutes;
            }
        }

        private double SecondsArrowDegree {
            get {
                DateTime currentTime = DateTime.Now;
                double currentSeconds = (double)currentTime.Second;
                double currentSecondsFraction = (double)currentTime.Millisecond / 1000;
                double currentFullSeconds = currentSeconds + currentSecondsFraction;
                return (360 / 60) * currentFullSeconds;
            }
        }

        private Line HoursArrow = new Line();
        private Line MinutesArrow = new Line();
        private Line SecondsArrow = new Line();
        
        public MainWindow() {
            InitializeComponent();
            DrawClock();

            DispatcherTimer UpdateClockTimer = new DispatcherTimer();
            UpdateClockTimer.Tick += new EventHandler(UpdateClockTimer_Tick);
            UpdateClockTimer.Interval = new TimeSpan(0, 0, 0, 0, 16); // 60+ fps gaming clock
            UpdateClockTimer.Start();
        }

        private void DrawClock() {
            DrawClockBody(ClockSize);
            DrawHoursArrow();
            DrawMinutesArrow();
            DrawSecondsArrow();
        }

        private void DrawClockBody(int size) {
            Ellipse clockBodyElilipse = new Ellipse();
            ClockCanvas.Children.Add(clockBodyElilipse);
            // styling
            SolidColorBrush clockBodyBrush = new SolidColorBrush();
            clockBodyBrush.Color = Color.FromArgb(255, 153, 153, 153); // gray
            clockBodyElilipse.Fill = clockBodyBrush;
            clockBodyElilipse.StrokeThickness = 2;
            clockBodyElilipse.Stroke = Brushes.Black;
            // shaping
            clockBodyElilipse.Width = size;
            clockBodyElilipse.Height = size;
        }
        
        private void DrawHoursArrow() {
            ClockCanvas.Children.Add(HoursArrow);
            // styling
            HoursArrow.Stroke = System.Windows.Media.Brushes.Black;
            HoursArrow.StrokeThickness = 12;
            // start arrow in center
            HoursArrow.X1 = ClockCenterPosition;
            HoursArrow.Y1 = ClockCenterPosition;
            // end in 0H position with arrow lenght 0.5 of radius
            HoursArrow.X2 = ClockCenterPosition;
            HoursArrow.Y2 = ClockCenterPosition - ClockCenterPosition * 0.5;
        }

        private void DrawMinutesArrow() {
            ClockCanvas.Children.Add(MinutesArrow);
            // styling
            MinutesArrow.Stroke = System.Windows.Media.Brushes.Black;
            MinutesArrow.StrokeThickness = 6;
            // start arrow in center
            MinutesArrow.X1 = ClockCenterPosition;
            MinutesArrow.Y1 = ClockCenterPosition;
            // end in 0 minutes position with arrow lenght 0.6 of radius
            MinutesArrow.X2 = ClockCenterPosition;
            MinutesArrow.Y2 = ClockCenterPosition - ClockCenterPosition * 0.6;
        }

        private void DrawSecondsArrow() {
            ClockCanvas.Children.Add(SecondsArrow);
            // styling
            SecondsArrow.Stroke = System.Windows.Media.Brushes.Black;
            SecondsArrow.StrokeThickness = 3;
            // start arrow in center
            SecondsArrow.X1 = ClockCenterPosition;
            SecondsArrow.Y1 = ClockCenterPosition;
            // end in 0 seconds position with arrow lenght 0.7 of radius
            SecondsArrow.X2 = ClockCenterPosition;
            SecondsArrow.Y2 = ClockCenterPosition - ClockCenterPosition * 0.7;
        }

        private void RotateHoursArrow(double degree) {
            HoursArrow.RenderTransform = new RotateTransform(degree, ClockCenterPosition, ClockCenterPosition);
        }
        private void RotateMinutesArrow(double degree) {
            MinutesArrow.RenderTransform = new RotateTransform(degree, ClockCenterPosition, ClockCenterPosition);
        }
        private void RotateSecondsArrow(double degree) {
            SecondsArrow.RenderTransform = new RotateTransform(degree, ClockCenterPosition, ClockCenterPosition);
        }

        private void UpdateClockTimer_Tick(object sender, EventArgs e) {
            RotateSecondsArrow(SecondsArrowDegree);
            RotateMinutesArrow(MinutesArrowDegree);
            RotateHoursArrow(HoursArrowDegree);
        }
    }
}
