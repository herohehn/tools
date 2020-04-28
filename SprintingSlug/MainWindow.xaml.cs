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
        List<string> adjectives;
        List<string> animals;

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
    }
}