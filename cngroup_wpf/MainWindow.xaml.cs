using cngroup_wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Instruction = System.Collections.Generic.KeyValuePair<string, int>;

namespace CNGroup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
        public string fileContent = string.Empty;
        Calculations Calculations;
        const string applyWord = "apply";

        public MainWindow()
        {
            InitializeComponent();
            Calculations = new Calculations();
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {   
                fileContent = File.ReadAllText(openFileDialog.FileName);
                txtFileContent.Text = fileContent;
            }  
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            Calculations.Init();          
            Calculations.ParseContent(fileContent, applyWord);
            Calculations.Calculate();
            txtOutput.Text = Calculations.PrintResults();
        }

        private void helpWindow_Click(object sender, RoutedEventArgs e)
        {
            Help helpWindow = new Help();
            helpWindow.ShowDialog();
        }

        private void aboutWindow_Click(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.ShowDialog();
        }
    }
}
