using System;
using System.Collections.Generic;
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

            // Deserialize wordlists
            try
            {
                adjectives = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Wordlists\Adjectives.txt").ToList();
                animals = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Wordlists\Animals.txt").ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}