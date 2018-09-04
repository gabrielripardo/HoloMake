using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace HoloMake
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        Image icPlayPause;
        DispatcherTimer timer;
        bool pause;
        QuadWin quadWin;

        public MainWindow()
        {
            InitializeComponent();
            pause = true;
            icPlayPause = new Image();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += new EventHandler(timer_Tick);
        }
        void timer_Tick(object sender, EventArgs e)
        {
            barrinha.Value = videoMain.Position.TotalSeconds;
        }
        private void videoMain_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = videoMain.NaturalDuration.TimeSpan;
            barrinha.Maximum = ts.TotalSeconds;
            timer.Start();
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblDuration.Content = TimeSpan.FromSeconds(barrinha.Value).ToString(@"hh\:mm\:ss");

            if (barrinha.Value == barrinha.Maximum)
            {
                btnPlayPause.Content = "Play";
                icPlayPause.Source = new BitmapImage(new Uri(@"/icons/play_ic.png", UriKind.Relative));
                btnPlayPause.Content = icPlayPause;
                pause = true;
            }
        }
        private void btnLigDes_Click(object sender, RoutedEventArgs e)
        {
            quadWin = new QuadWin(videoMain);
            quadWin.Show();
        }

        private void addVideo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlayPause_Click(object sender, RoutedEventArgs e)
        {            
            if (pause)
            {
                videoMain.Play();
                pause = false;
                icPlayPause.Source = new BitmapImage(new Uri(@"/icons/pause_ic.png", UriKind.Relative));
            }
            else
            {
                videoMain.Pause();
                pause = true;
                icPlayPause.Source = new BitmapImage(new Uri(@"/icons/play_ic.png", UriKind.Relative));
            }
            btnPlayPause.Content = icPlayPause;
            if (barrinha.Value == barrinha.Maximum)
            {
                barrinha.Value = 0;
                videoMain.Stop();                
                videoMain.Play();                
                pause = false;
                icPlayPause.Source = new BitmapImage(new Uri(@"/icons/pause_ic.png", UriKind.Relative));
            }            
        }
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Title = volu.Value.ToString();
            videoMain.Volume = (double)volu.Value;
        }

        private void btnHideShow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSettingsQuad_Click(object sender, RoutedEventArgs e)
        {
            new SettingsQuad().Show();
        }

        private void btnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            quadWin.WindowState = WindowState.Maximized;
            quadWin.WindowStyle = WindowStyle.None;
        }
    }
}
