using System;
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

        private void refreshAdjectiveButton_Click(object sender, RoutedEventArgs e)
        {
            adjectiveLabel.Content = GetAdjective((char)alphabetComboBox.SelectedItem);
        }

        private void refreshAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            animalLabel.Content = GetAnimal((char)alphabetComboBox.SelectedItem);
        }

        private void refreshBothButton_Click(object sender, RoutedEventArgs e)
        {
            adjectiveLabel.Content = GetAdjective((char)alphabetComboBox.SelectedItem);
            animalLabel.Content = GetAnimal((char)alphabetComboBox.SelectedItem);
        }
    }
}