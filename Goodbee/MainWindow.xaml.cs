﻿using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Goodbee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Current configuration:
        // - Net workday duration is 7 hours and 24 minutes (7.4)
        // - Forced 15 minutes of break after 4 hours and 30 minutes of work
        // - Forced 30 minutes of break after 6 hours of work and 15 minutes of break (as mentioned above)
        // - Hard cut at upper limit of 7 hours and 24 minutes of work, 45 minutes of break (workday) and 2 hours of additional work

        private DateTime startTime;
        private DateTime sixHourBreak => startTime.AddHours(6).AddMinutes(15);
        private DateTime endTime => startTime.AddHours(7.4).AddMinutes(15).AddMinutes(30);
        private DateTime maxEndTime => endTime.AddHours(2);

        private bool playSound = false;
        private bool flashTaskbar = true;
        private readonly DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Initial setup
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            startTime = Process.GetCurrentProcess().StartTime;

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

            if (startTime <= currentTime)
            {
                // Normal use case
                TaskbarManager.Instance.SetProgressValue((int)(currentTime - startTime).TotalMinutes, (int)(endTime - startTime).TotalMinutes);
            }
            else
            {
                // Look into the future case
                TaskbarManager.Instance.SetProgressValue(0, (int)(endTime - startTime).TotalMinutes);
            }

            // Taskbar color handling
            if (currentTime > sixHourBreak && currentTime < sixHourBreak.AddMinutes(30))
            {
                // Pause state (color change) during 6h break
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
            }
            else if (currentTime > endTime)
            {
                if (playSound)
                {
                    // Play soundfile FEIERABEND.wav by Padrig Foolert
                    SoundPlayer sound = new SoundPlayer(Properties.Resources.FEIERABEND);
                    sound.Play();
                    playSound = false;
                }

                if (flashTaskbar)
                {
                    // Indeterminate state (flashing) when you should call it a day
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
                }
            }
            else
            {
                // Default state
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            }
        }

        /// <summary>
        /// Set start time using the enter key
        /// </summary>
        private void StartTimeTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetStartTime();
            }
        }

        /// <summary>
        /// Clear textbox when focused
        /// </summary>
        private void StartTimeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            startTimeTextBox.Clear();
        }

        /// <summary>
        /// Set start time when losing focus
        /// </summary>
        private void StartTimeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SetStartTime();
        }

        /// <summary>
        /// Change start time using the interface
        /// </summary>
        private void SetStartTime()
        {
            if (startTimeTextBox.Text.Length <= 0)
            {
                startTimeTextBox.Text = startTime.ToShortTimeString();
            }
            else if (DateTime.TryParse(startTimeTextBox.Text, out startTime))
            {
                // Clear textbox background
                startTimeTextBox.ClearValue(BackgroundProperty);

                // Refresh display times
                RefreshDisplayTimes();
                timer.Start();
            }
            else
            {
                // Error state
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
                startTimeTextBox.Background = Brushes.IndianRed;
                timer.Stop();
            }
        }

        /// <summary>
        /// Refresh display times
        /// </summary>
        private void RefreshDisplayTimes()
        {
            startTimeTextBox.Text = startTime.ToShortTimeString();
            sixHourBreakTextBox.Text = sixHourBreak.ToShortTimeString();
            endTimeTextBox.Text = endTime.ToShortTimeString();
            maxEndTimeTextBox.Text = maxEndTime.ToShortTimeString();
        }

        /// <summary>
        /// Toggle sound notification on/off
        /// </summary>
        private void ToggleSoundButton_Click(object sender, RoutedEventArgs e)
        {
            if (playSound)
            {
                playSound = false;
                toggleSoundButton.Content = "Deaktiviert";
            }
            else
            {
                playSound = true;
                toggleSoundButton.Content = "Aktiviert";
            }
        }

        /// <summary>
        /// Toggle taskbar flashing on/off
        /// </summary>
        private void ToggleFlashingButton_Click(object sender, RoutedEventArgs e)
        {
            if (flashTaskbar)
            {
                flashTaskbar = false;
                toggleFlashingButton.Content = "Deaktiviert";
            }
            else
            {
                flashTaskbar = true;
                toggleFlashingButton.Content = "Aktiviert";
            }
        }
    }
}