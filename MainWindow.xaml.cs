using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace TaskbarCalendarIcon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        private System.Windows.Forms.NotifyIcon notifyIcon = new();

        public MainWindow()
        {
            // Tray Only
            ShowInTaskbar = false;
            Visibility = Visibility.Hidden;

            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            Loaded += MainWindow_Loaded;
            notifyIcon.Click += new EventHandler(NotifyIcon_Click);
            notifyIcon.Icon = new System.Drawing.Icon(@"d:\favicon.ico");
            notifyIcon.Visible = true;
            notifyIcon.Text = NotifyText();

            ContextMenu menu = new()
            {
                 
            };
            notifyIcon.ContextMenuStrip = new()
            {

            };


            base.OnInitialized(e);
        }

        private string NotifyText()
        {
            var now = DateTime.Now;
            KoreanLunisolarCalendar klc = new();
            var lunarYear = klc.GetYear(now);
            var leapMonth = klc.GetLeapMonth(lunarYear);
            var lunarMonth = klc.GetMonth(now);
            var lunarMonthStr = leapMonth == 0 ?
                lunarMonth.ToString() :
                Math.Sign(lunarMonth - leapMonth) switch
                {
                    -1 => lunarMonth.ToString(),
                    0 => "윤" + (lunarMonth - 1).ToString(),
                    _ => (lunarMonth - 1).ToString()
                };
            var lunarDay = klc.GetDayOfMonth(now);            
            return $"{now:yyyy년 M월 d일} (제{now.DayOfYear}일 {now:dddd})\n음력 {lunarYear}년 {lunarMonthStr}월 {lunarDay}일";
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void NotifyIcon_Click(object? sender, EventArgs e)
        {
            ShowInTaskbar = !ShowInTaskbar;
            Visibility = ShowInTaskbar ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
