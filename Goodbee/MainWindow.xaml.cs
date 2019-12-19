using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Goodbee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime Arbeitsbeginn;
        DateTime SechsStundenPause { get { return Arbeitsbeginn.AddHours(6); } }
        DateTime Feierabend { get { return Arbeitsbeginn.AddHours(7.6).AddMinutes(30); } }
        DateTime NeunStundenPause { get { return Arbeitsbeginn.AddHours(9.5); } }
        private bool soundHasBeenPlayed;
        DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Initial setup
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Arbeitsbeginn = Process.GetCurrentProcess().StartTime;

            RefreshDisplayTimes();

            // Start timer for taskbar progress
            timer.Tick += new EventHandler(UpdateNotifications);
            timer.Interval = new TimeSpan(0, 0, 10); // Fire every (h, m, s)
            timer.Start();
        }

        /// <summary>
        /// Notification handling
        /// </summary>
        private void UpdateNotifications(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            TaskbarManager.Instance.SetProgressValue((int)(currentTime - Arbeitsbeginn).TotalMinutes, (int)(Feierabend - Arbeitsbeginn).TotalMinutes); // (currentValue, maxValue) - minValue is always 0

            // Taskbar color handling
            if (currentTime > SechsStundenPause && currentTime < SechsStundenPause.AddMinutes(30))
            {
                // Pause state (color change) during 6h break
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
            }
            else if (currentTime > Feierabend)
            {
                if (!soundHasBeenPlayed)
                {
                    // Play soundfile FEIERABEND.wav by Patrick Fuhlert
                    SoundPlayer sound = new SoundPlayer(Properties.Resources.FEIERABEND);
                    sound.Play();
                    soundHasBeenPlayed = true;
                }

                // Indeterminate state (flashing) when you should call it a day
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
            }
            else
            {
                // Default state
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            }
        }

        /// <summary>
        /// Set startup time using the enter key
        /// </summary>
        private void ArbeitsbeginnTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetArbeitsbeginn();
            }
        }

        /// <summary>
        /// Clear textbox when focused
        /// </summary>
        private void ArbeitsbeginnTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ArbeitsbeginnTextBox.Clear();
        }

        /// <summary>
        /// Set startup time when losing focus
        /// </summary>
        private void ArbeitsbeginnTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SetArbeitsbeginn();
        }

        /// <summary>
        /// Change startup time using the interface
        /// </summary>
        private void SetArbeitsbeginn()
        {
            if (ArbeitsbeginnTextBox.Text.Length <= 0)
            {
                ArbeitsbeginnTextBox.Text = Arbeitsbeginn.ToShortTimeString();
            }
            else
            {
                if (DateTime.TryParse(ArbeitsbeginnTextBox.Text, out Arbeitsbeginn))
                {
                    // Clear textbox background
                    ArbeitsbeginnTextBox.ClearValue(BackgroundProperty);

                    // Refresh display times
                    RefreshDisplayTimes();
                    timer.Start();
                }
                else
                {
                    // Error state
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
                    ArbeitsbeginnTextBox.Background = Brushes.IndianRed;
                    timer.Stop();
                }
            }
        }

        /// <summary>
        /// Refresh display times
        /// </summary>
        private void RefreshDisplayTimes()
        {
            ArbeitsbeginnTextBox.Text = Arbeitsbeginn.ToShortTimeString();
            SechsStundenPauseTextBox.Text = SechsStundenPause.ToShortTimeString();
            FeierabendTextBox.Text = Feierabend.ToShortTimeString();
            NeunStundenPauseTextBox.Text = NeunStundenPause.ToShortTimeString();
        }
    }
}