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
        private IDictionary<string, Image> dictionaryVideos;
        private IDictionary<string, Image> dictionaryAudio;
        private BitmapImage _img;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            dictionaryImages = new Dictionary<string, Image>();
            dictionaryVideos = new Dictionary<string, Image>();
            dictionaryAudio = new Dictionary<string, Image>();

            InitializeComponent();
            this.DataContext = this;

            // THE IMAGES DIRECTORY
            // First this, others will follow
            textBlockI.Text = "All Files from the images folder: \n";
            textBlockV.Text = "All Files from the videos folder: \n";
            textBlockA.Text = "All Files from the audios folder: \n";

            TextReader tr = new StreamReader("E:\\CMS\\CMS.txt");
            string len;
            char[] split = new char[] { ',' };
            while ((len = tr.ReadLine()) != null)
            {
                Image image = null;
                string[] array = len.Split(split);
                string code = array[0];
                string type = array[2];
                bool typeBool = false;
                if (type.Equals("3D"))
                    typeBool = true;

                if (array[1].Split('.').Last().Equals("mp4"))
                {
                    Image = new BitmapImage(new Uri("E:\\CMS\\images\\video_placeholder.jpg"));
                    image = new CMS.Image(array[0], Image, typeBool);
                    image.Name = array[1].Replace("file:///E:/CMS/videos/", "");
                } else if (array[1].Split('.').Last().Equals("wav"))
                {
                    Image = new BitmapImage(new Uri("E:\\CMS\\images\\audio_placeholder.jpg"));
                    image = new CMS.Image(array[0], Image, typeBool);
                    image.Name = array[1].Replace("file:///E:/CMS/audios/", "");
                }
                else
                {
                    Image = new BitmapImage(new Uri(array[1]));
                    image = new CMS.Image(array[0], Image, typeBool);
                }
                if (image.Name.Split('.').Last().Equals("jpg"))
                {
                    dictionaryImages[code] = image;
                } else if (image.Name.Split('.').Last().Equals("mp4"))
                {
                    dictionaryVideos[code] = image;
                } else if (image.Name.Split('.').Last().Equals("wav"))
                {
                    dictionaryAudio[code] = image;
                }
            }
            tr.Close();
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

        public List<Image> ListVideos
        {
            get { return dictionaryVideos.Values.ToList(); }
        }

        public List<Image> ListAudios
        {
            get { return dictionaryAudio.Values.ToList(); }
        }

        private void Button_Click_Images(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            Debug.WriteLine(file);
            if (!file.Equals(""))
            {
                string fileName = System.IO.Path.GetFileName(file);
                if (!File.Exists("E:\\CMS\\images\\" + fileName))
                {
                    System.IO.File.Copy(file, "E:\\CMS\\images\\" + fileName);
                    Image = new BitmapImage(new Uri("E:\\CMS\\images\\" + fileName));
                    Image image = new Image(fileName, Image, true);
                    dictionaryImages[fileName] = image;
                    ListImages.Add(image);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListImages)));
                }
                else
                {
                    errorImages.Text = "This file already exists in CMS";
                }
            }
        }

        private void Button_Click_Videos(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            string fileName = System.IO.Path.GetFileName(file);
            System.IO.File.Copy(file, "E:\\CMS\\videos\\" + fileName);
            Image = new BitmapImage(new Uri("E:\\CMS\\images\\video_placeholder.jpg"));      
            Image image = new Image(fileName, Image, true);
            image.Name = fileName;
            dictionaryVideos[fileName] = image;
            ListVideos.Add(image);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListVideos)));
        }

        private void Button_Click_Audios(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            string fileName = System.IO.Path.GetFileName(file);
            System.IO.File.Copy(file, "E:\\CMS\\audios\\" + fileName);
            Image = new BitmapImage(new Uri("E:\\CMS\\images\\audio_placeholder.jpg"));
            Image image = new Image(fileName, Image, true);
            image.Name = fileName;
            dictionaryAudio[fileName] = image;
            ListAudios.Add(image);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListAudios)));
        }

        private string GetFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            return ofd.FileName;
        }

        private void listbox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var listbox = e.Source as ListBox;
            var item = listbox.SelectedItem as Image;
            if (item != null)
            {
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
            if (name.Split('.').Last().Equals("mp4"))
            {
                var key = dictionaryVideos.FirstOrDefault(x => x.Value.Name.Equals(name)).Key;
                var video = dictionaryVideos[key];
                video.Code = code;
                video.Type = type;
                logs.Text += "\n\tFile " + video.Name + " is saved with code " + video.Code;
            }
            else if (name.Split('.').Last().Equals("wav"))
            {
                var key = dictionaryAudio.FirstOrDefault(x => x.Value.Name.Equals(name)).Key;
                var sound = dictionaryAudio[key];
                sound.Code = code;
                sound.Type = type;
                logs.Text += "\n\tFile " + sound.Name + " is saved with code " + sound.Code;
            }
            else
            { 
                var key = dictionaryImages.FirstOrDefault(x => x.Value.Name.Equals(name)).Key;
                var image = dictionaryImages[key];
                image.Code = code;
                image.Type = type;
                logs.Text += "\n\tFile " + image.Name + " is saved with code " + image.Code;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageCode)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListImages)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListVideos)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListAudios)));
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
            foreach (Image img in ListVideos)
            {
                string type = "3D";
                if (!img.Type)
                    type = "2D";
                tw.WriteLine(img.Code + ",file:///E:/CMS/videos/" + img.Name + "," + type);
            }
            foreach (Image img in ListAudios)
            {
                string type = "3D";
                if (!img.Type)
                    type = "2D";
                tw.WriteLine(img.Code + ",file:///E:/CMS/audios/" + img.Name + "," + type);
            }
            tw.Close();
            logs.Text += "\n\tWriting to file complete";
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            // todo: needs some rework
            String path = dictionaryImages[ImageCode].URI.LocalPath;
            ListImages.Remove(dictionaryImages[ImageCode]);
            dictionaryImages.Remove(ImageCode);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListImages)));
            // Maybe shouldn't erase the file entirely
            System.IO.File.Delete(path);
        }
    }
}
