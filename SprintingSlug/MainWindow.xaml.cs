﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;

namespace SprintingSlug
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> adjectives;
        public List<string> animals;
        public char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        readonly Random rnd = new Random();

        public readonly string nothingFound = "Nothing found.";
        public readonly string unallowedInput = "Unallowed input.";

        /// <summary>
        /// Initial setup
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // Deserialize wordlists
                adjectives = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Wordlists\Adjectives.txt").ToList();
                animals = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Wordlists\Animals.txt").ToList();

                // Format wordlists to title case
                CultureInfo currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                adjectives = adjectives.ConvertAll(adjective => currentCulture.TextInfo.ToTitleCase(adjective.ToLower()));
                animals = animals.ConvertAll(animal => currentCulture.TextInfo.ToTitleCase(animal.ToLower()));

                // Fill combobox with allowed input
                alphabetComboBox.ItemsSource = alphabet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Returns a random adjective out of the according wordlist where the first letter matches the given parameter
        /// </summary>
        public string GetAdjective(char c)
        {
            // Check if input is an allowed letter
            if (char.IsLetter(c))
            {
                // Filter adjectives wordlist for leading char (parameter)
                List<string> filteredAdjectives = adjectives.FindAll(a => a.ToCharArray()[0] == char.ToUpper(c));

                if (filteredAdjectives.Count > 0)
                {
                    // Return one random adjective from the filtered wordlist
                    return filteredAdjectives[rnd.Next(0, filteredAdjectives.Count())];
                }
                else
                {
                    return nothingFound;
                }
            }
            else
            {
                return unallowedInput;
            }
        }

        /// <summary>
        /// Returns a random animal out of the according wordlist where the first letter matches the given parameter
        /// </summary>
        public string GetAnimal(char c)
        {
            // Check if input is an allowed letter
            if (char.IsLetter(c))
            {
                // Filter animals wordlist for leading char (parameter)
                List<string> filteredAnimals = animals.FindAll(a => a.ToCharArray()[0] == char.ToUpper(c));

                if (openCompoundCheckBox.IsChecked == false)
                {
                    // Remove open compound names like "Galapagos Tortoise"
                    filteredAnimals = filteredAnimals.Where(a => !a.Contains(" ")).ToList();
                }

                if (filteredAnimals.Count > 0)
                {
                    // Return one random animal from the filtered wordlist
                    return filteredAnimals[rnd.Next(0, filteredAnimals.Count())];
                }
                else
                {
                    return nothingFound;
                }
            }
            else
            {
                return unallowedInput;
            }
        }

        /// <summary>
        /// Navigates to the given hyperlink using the system default browser
        /// </summary>
        private void BrowserLookup(string hyperlink)
        {
            try
            {
                System.Diagnostics.Process.Start(hyperlink.ToLower());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshAdjectiveButton_Click(object sender, RoutedEventArgs e)
        {
            adjectiveLabel.Content = GetAdjective((char)alphabetComboBox.SelectedItem);
        }

        private void RefreshAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            animalLabel.Content = GetAnimal((char)alphabetComboBox.SelectedItem);
        }

        private void RefreshBothButton_Click(object sender, RoutedEventArgs e)
        {
            adjectiveLabel.Content = GetAdjective((char)alphabetComboBox.SelectedItem);
            animalLabel.Content = GetAnimal((char)alphabetComboBox.SelectedItem);
        }

        private void LookupAdjectiveButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserLookup("https://dict.leo.org/englisch-deutsch/" + adjectiveLabel.Content);
        }

        private void LookupAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserLookup("https://www.google.de/search?q=" + animalLabel.Content + "&tbm=isch");
        }
    }
}