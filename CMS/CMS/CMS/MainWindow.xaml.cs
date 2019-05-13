using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
        private string _computer = Environment.UserName;
        private IDictionary<string, Image> dictionaryImages;
        private IDictionary<string, Image> dictionaryVideos;
        private IDictionary<string, Image> dictionaryAudio;
        private BitmapImage _img;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool locked = false;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            dictionaryImages = new Dictionary<string, Image>();
            dictionaryVideos = new Dictionary<string, Image>();
            dictionaryAudio = new Dictionary<string, Image>();

            TextReader tr = new StreamReader("C:\\Users\\" + _computer + "\\Desktop\\CMS\\CMS.txt");
            string len;
            char[] split = new char[] { ',' };
            while ((len = tr.ReadLine()) != null && !len.Equals(""))
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
                    Image = new BitmapImage(new Uri("C:\\Users\\" + _computer + "\\Desktop\\CMS\\images\\video_placeholder.jpg"));
                    image = new CMS.Image(array[0], Image, typeBool);
                    image.Name = array[1].Replace("file:///C:/Users/" + _computer + "/Desktop/CMS/videos/", "");
                } else if (array[1].Split('.').Last().Equals("wav"))
                {
                    Image = new BitmapImage(new Uri("C:\\Users\\" + _computer + "\\Desktop\\CMS\\images\\audio_placeholder.jpg"));
                    image = new CMS.Image(array[0], Image, typeBool);
                    image.Name = array[1].Replace("file:///C:/Users/" + _computer + "/Desktop/CMS/audios/", "");
                }
                else
                {
                    Image = new BitmapImage();
                    Image.BeginInit();
                    Image.UriSource = new Uri(array[1]);
                    Image.CacheOption = BitmapCacheOption.OnLoad;
                    Image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    Image.EndInit();
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
            if (!file.Equals(""))
            {
                string fileName = System.IO.Path.GetFileName(file);
                if (!File.Exists("C:\\Users\\" + _computer + "\\Desktop\\CMS\\images\\" + fileName))
                {
                    logs.Text = "Copying " + fileName + " to folder ";
                    WebClient wc = new WebClient();
                    wc.DownloadProgressChanged += DownloadProgress;
                    wc.DownloadFileAsync(new Uri(file), "C:\\Users\\" + _computer + "\\Desktop\\CMS\\images\\" + fileName);
                    while (wc.IsBusy) { }
                    wc.Dispose();
                    Image = new BitmapImage(new Uri("C:\\Users\\" + _computer + "\\Desktop\\CMS\\images\\" + fileName));
                    Image image = new Image(fileName, Image, true);
                    dictionaryImages[fileName] = image;
                    ListImages.Add(image);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListImages)));
                }
                else
                {
                    logs.Text = "This file already exists in CMS";
                }
            }
        }

        private void Button_Click_Videos(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            if (!file.Equals(""))
            {
                string fileName = System.IO.Path.GetFileName(file);
                if (!File.Exists("C:\\Users\\" + _computer + "\\Desktop\\CMS\\videos\\" + fileName))
                {
                    logs.Text = "Copying " + fileName + " to folder ";
                    WebClient wc = new WebClient();
                    wc.DownloadProgressChanged += DownloadProgress;
                    wc.DownloadFileAsync(new Uri(file), "C:\\Users\\" + _computer + "\\Desktop\\CMS\\videos\\" + fileName);
                    wc.Dispose();
                    Image = new BitmapImage(new Uri("C:\\Users\\" + _computer + "\\Desktop\\CMS\\images\\video_placeholder.jpg"));
                    Image image = new Image(fileName, Image, true);
                    image.Name = fileName;
                    dictionaryVideos[fileName] = image;
                    ListVideos.Add(image);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListVideos)));
                }
                else
                {
                    logs.Text = "This file already exists in CMS";
                }
            }
        }

        private void Button_Click_Audios(object sender, RoutedEventArgs e)
        {
            string file = GetFile();
            if (!file.Equals(""))
            {
                string fileName = System.IO.Path.GetFileName(file);
                if (!File.Exists("C:\\Users\\" + _computer + "\\Desktop\\CMS\\videos\\" + fileName))
                {
                    logs.Text = "Copying " + fileName + " to folder ";
                    WebClient wc = new WebClient();
                    wc.DownloadProgressChanged += DownloadProgress;
                    wc.DownloadFileAsync(new Uri(file), "C:\\Users\\" + _computer + "\\Desktop\\CMS\\audios\\" + fileName);
                    wc.Dispose();
                    Image = new BitmapImage(new Uri("C:\\Users\\" + _computer + "\\Desktop\\CMS\\images\\audio_placeholder.jpg"));
                    Image image = new Image(fileName, Image, true);
                    image.Name = fileName;
                    dictionaryAudio[fileName] = image;
                    ListAudios.Add(image);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListAudios)));
                }
                else
                {
                    logs.Text = "This file already exists in CMS";
                }
            }
        }

        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
            if (progress.Value == 100)
            {
                logs.Text = "Copying done";
                progress.Value = 0;
            }
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
                logs.Text = "File " + video.Name + " is saved with code " + video.Code;
            }
            else if (name.Split('.').Last().Equals("wav"))
            {
                var key = dictionaryAudio.FirstOrDefault(x => x.Value.Name.Equals(name)).Key;
                var sound = dictionaryAudio[key];
                sound.Code = code;
                sound.Type = type;
                logs.Text = "File " + sound.Name + " is saved with code " + sound.Code;
            }
            else
            { 
                var key = dictionaryImages.FirstOrDefault(x => x.Value.Name.Equals(name)).Key;
                var image = dictionaryImages[key];
                image.Code = code;
                image.Type = type;
                logs.Text = "File " + image.Name + " is saved with code " + image.Code;
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
            TextWriter tw = new StreamWriter("C:\\Users\\" + _computer + "\\Desktop\\CMS\\CMS.txt");
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
                tw.WriteLine(img.Code + ",file:///C:/Users/" + _computer + "/Desktop/CMS/videos/" + img.Name + "," + type);
            }
            foreach (Image img in ListAudios)
            {
                string type = "3D";
                if (!img.Type)
                    type = "2D";
                tw.WriteLine(img.Code + ",file:///C:/Users/" + _computer + "/Desktop/CMS/audios/" + img.Name + "," + type);
            }
            tw.Close();
            logs.Text = "Writing to file complete";
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            string name = ImageName;
            string code = ImageCode;
            if (name.Split('.').Last().Equals("mp4"))
            {
                ListVideos.Remove(dictionaryVideos[code]);
                dictionaryVideos.Remove(code);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListVideos)));
                System.IO.File.Delete("C:\\Users\\" + _computer + "\\Desktop\\CMS\\videos\\" + name);
                logs.Text = "Video deleted";
            }
            else if (name.Split('.').Last().Equals("wav"))
            {
                ListAudios.Remove(dictionaryAudio[code]);
                dictionaryAudio.Remove(code);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListAudios)));
                System.IO.File.Delete("C:\\Users\\" + _computer + "\\Desktop\\CMS\\audios\\" + name);
                logs.Text = "Audio deleted";
            }
            else
            {
                ListImages.Remove(dictionaryImages[code]);
                dictionaryImages.Remove(code);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListImages)));
                System.IO.File.Delete("C:\\Users\\" + _computer + "\\Desktop\\CMS\\images\\" + name);
                logs.Text = "Image deleted";
            }
        }
    }
}
