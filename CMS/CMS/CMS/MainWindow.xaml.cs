﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace CMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IDictionary<string, Image> dictionaryImages;
        private ImageSource _img;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            dictionaryImages = new Dictionary<string, Image>();
            InitializeComponent();
            this.DataContext = this;
            // THE IMAGES DIRECTORY
            DirectoryInfo dirI = new DirectoryInfo("C:\\Users\\Cedric Hermans\\Desktop\\CMS\\images\\");
            FileInfo[] infoI = dirI.GetFiles();
            textBlockI.Text = "All Files from the images folder: \n";
            foreach (FileInfo file in infoI)
            {
                Image = new BitmapImage(new Uri("C:\\Users\\Cedric Hermans\\Desktop\\CMS\\images\\" + file.Name));
                Image image = new Image(file.Name, Image);
                dictionaryImages[file.Name] = image;
            }
            

            // THE VIDEOS DIRECTORY
            DirectoryInfo dirV = new DirectoryInfo("C:\\Users\\Cedric Hermans\\Desktop\\CMS\\videos\\");
            FileInfo[] infoV = dirV.GetFiles();
            textBlockV.Text = "All Files from the videos folder: \n";
            foreach (FileInfo file in infoV)
            {
                textBlockV.Text += "\t" + file.Name + "\n";
            }

            // THE AUDIOS DIRECTORY
            DirectoryInfo dirA = new DirectoryInfo("C:\\Users\\Cedric Hermans\\Desktop\\CMS\\audios\\");
            FileInfo[] infoA = dirA.GetFiles();
            textBlockA.Text = "All Files from the audios folder: \n";
            foreach (FileInfo file in infoA)
            {
                textBlockA.Text += "\t" + file.Name + "\n";
            }
        }

        public ImageSource Image
        { 
            get {
                return _img;
            }
            set
            {
                _img = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }

        public List<Image> ListImages
        {
            get { return dictionaryImages.Values.ToList(); }
        }

        private void Button_Click_Images(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            string fileName = System.IO.Path.GetFileName(file);
            //textBlockI.Text += "\t" + fileName + "\n";
            System.IO.File.Copy(file, "C:\\Users\\Cedric Hermans\\Desktop\\CMS\\images\\" + fileName);
        }

        private void Button_Click_Videos(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            string fileName = System.IO.Path.GetFileName(file);
            textBlockV.Text += "\t" + fileName + "\n";
            System.IO.File.Copy(file, "C:\\Users\\Cedric Hermans\\Desktop\\CMS\\videos\\" + fileName);
        }

        private void Button_Click_Audios(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            string fileName = System.IO.Path.GetFileName(file);
            textBlockA.Text += "\t" + fileName + "\n";
            System.IO.File.Copy(file, "C:\\Users\\Cedric Hermans\\Desktop\\CMS\\audios\\" + fileName);
        }

        private string GetFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            return ofd.FileName;
        }

        private void listbox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // works after 2 clicks
            var item = listbox.SelectedItem as Image;
            if (item != null)
            {
                item = listbox.SelectedItem as Image;
                Image = item.Source;
            }
        }
    }
}
