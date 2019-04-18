using Microsoft.Win32;
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
        private BitmapImage _img;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            dictionaryImages = new Dictionary<string, Image>();
            InitializeComponent();
            this.DataContext = this;

            // THE IMAGES DIRECTORY
            // First this, others will follow
            DirectoryInfo dirI = new DirectoryInfo("E:\\CMS\\images\\");
            FileInfo[] infoI = dirI.GetFiles();
            textBlockI.Text = "All Files from the images folder: \n";

            TextReader tr = new StreamReader("E:\\CMS\\CMS.txt");
            string len;
            char[] split = new char[] { ',' };
            while ((len = tr.ReadLine()) != null)
            {
                string[] array = len.Split(split);
                string code = array[0];
                Image = new BitmapImage(new Uri(array[1]));
                string type = array[2];
                bool typeBool = false;
                if (type.Equals("3D"))
                    typeBool = true;
                Image image = new CMS.Image(array[0], Image, typeBool);
                dictionaryImages[code] = image;
            }
            tr.Close();


            // THE VIDEOS DIRECTORY
            DirectoryInfo dirV = new DirectoryInfo("E:\\CMS\\videos\\");
            FileInfo[] infoV = dirV.GetFiles();
            textBlockV.Text = "All Files from the videos folder: \n";
            foreach (FileInfo file in infoV)
            {
                textBlockV.Text += "\t" + file.Name + "\n";
            }

            // THE AUDIOS DIRECTORY
            DirectoryInfo dirA = new DirectoryInfo("E:\\CMS\\audios\\");
            FileInfo[] infoA = dirA.GetFiles();
            textBlockA.Text = "All Files from the audios folder: \n";
            foreach (FileInfo file in infoA)
            {
                textBlockA.Text += "\t" + file.Name + "\n";
            }
        }

        public BitmapImage Image
        {
            get
            {
                return _img;
            }
            set
            {
                _img = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }

        public string ImageName { get; set; }

        public string ImageCode { get; set; }

        public bool ImageType { get; set; }

        public List<Image> ListImages
        {
            get { return dictionaryImages.Values.ToList(); }
        }

        private void Button_Click_Images(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            string fileName = System.IO.Path.GetFileName(file);
            System.IO.File.Copy(file, "E:\\CMS\\images\\" + fileName);
            Image = new BitmapImage(new Uri("E:\\CMS\\images\\" + fileName));
            Image image = new Image(fileName, Image, true);
            dictionaryImages[fileName] = image;
            ListImages.Add(image);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListImages)));
        }

        private void Button_Click_Videos(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            string fileName = System.IO.Path.GetFileName(file);
            textBlockV.Text += "\t" + fileName + "\n";
            System.IO.File.Copy(file, "E:\\CMS\\videos\\" + fileName);
        }

        private void Button_Click_Audios(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            string fileName = System.IO.Path.GetFileName(file);
            textBlockA.Text += "\t" + fileName + "\n";
            System.IO.File.Copy(file, "E:\\CMS\\audios\\" + fileName);
            
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
                Image = new BitmapImage(item.URI);
                ImageName = item.Name;
                ImageCode = item.Code;
                ImageType = item.Type;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageName)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageCode)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageType)));
            }
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            string name = ImageName;
            string code = ImageCode;
            bool type = ImageType;
            var key = dictionaryImages.FirstOrDefault(x => x.Value.Name.Equals(name)).Key;
            dictionaryImages[key].Code = code;
            dictionaryImages[key].Type = type;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageCode)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListImages)));
        }

        private void Button_Click_SaveToFile(object sender, RoutedEventArgs e)
        {
            // writing to txt file. VR app can look up code in this file and use the absolute path.
            // later to be changed to .meta file (or other)
            TextWriter tw = new StreamWriter("E:\\CMS\\CMS.txt");
            foreach (Image img in ListImages)
            {
                string type = "3D";
                if (!img.Type)
                    type = "2D";
                tw.WriteLine(img.Code + "," + img.URI + "," + type);
            }
            tw.Close();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            String path = dictionaryImages[ImageCode].URI.LocalPath;
            ListImages.Remove(dictionaryImages[ImageCode]);
            dictionaryImages.Remove(ImageCode);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListImages)));
            // Maybe shouldn't erase the file entirely
            //System.IO.File.Delete(path);
        }
    }
}
