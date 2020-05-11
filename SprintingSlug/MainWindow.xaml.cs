using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

namespace SprintingSlug
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> adjectives;
        public List<string> animals;
        Random rnd = new Random();

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetAdjective(char c)
        {
            // Check if input is an allowed letter
            if (char.IsLetter(c))
            {
                // Filter adjectives wordlist for leading char (parameter)
                List<string> filteredAdjectives = adjectives.FindAll(a => a.ToCharArray()[0] == char.ToUpper(c));

                if (filteredAdjectives.Count != 0)
                {
                    // Return one random adjective from the filtered wordlist
                    return filteredAdjectives[rnd.Next(0, filteredAdjectives.Count())];
                }
                else
                {
                    return "Nothing found.";
                }
            }
            else
            {
                return "Unallowed input.";
            }
        }

        public string GetAnimal(char c)
        {
            // Filter animals wordlist for leading char (parameter)
            List<string> filteredAnimals = animals.FindAll(a => a.ToCharArray()[0] == char.ToUpper(c));

            // Return one random animal from the filtered wordlist
            return filteredAnimals[rnd.Next(0, filteredAnimals.Count())];
        }
    }
}