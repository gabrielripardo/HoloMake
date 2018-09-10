using Microsoft.Win32;
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
        Image icHideShow;
        Image icFullScreen;

        DispatcherTimer timer;        
        QuadWin quadWin;
        bool pause;
        bool fullscreen;
        bool hide;
        bool ligado;

        String[] files, paths;

        public MainWindow()
        {
            InitializeComponent();            
            icPlayPause = new Image();
            icHideShow = new Image();
            icFullScreen = new Image();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += new EventHandler(timer_Tick);

            pause = true;
            fullscreen = false;
            hide = false;
            ligado = false;
            
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
            if (!ligado)
            {
                quadWin = new QuadWin();
                quadWin.Show();
                quadWin.espelharMidias(videoMain);
                ligado = true;
            }
            else
            {
                quadWin.Close();
                ligado = false;
            }            
        }

        private void addVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            if(openFileDialog1.ShowDialog() == true)
            {
                files = openFileDialog1.SafeFileNames;

                if(paths == null)
                {
                    paths = openFileDialog1.FileNames;
                }
                else
                {
                    String[] pathsLasts = openFileDialog1.FileNames;
                    String[] auxPaths = new string[paths.Length+pathsLasts.Length];                    
                    int lastIndexs = paths.Length;
                    
                    //Inseri itens anteriores
                    for (int iOlds = 0; iOlds < paths.Length; iOlds++) 
                    {
                        auxPaths[iOlds] = paths[iOlds];
                    }
                    //Inseri últimos itens
                    for (int iLasts = 0; iLasts < pathsLasts.Length; iLasts++)
                    {
                        auxPaths[lastIndexs] = pathsLasts[iLasts];
                        lastIndexs++;
                    }
                    paths = auxPaths;
                }   
                
            }
            for (int i = 0; i < files.Length; i++)
            {
                playlistBox.Items.Add(files[i]);
            }
        }
        private void playlistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            videoMain.Source = new Uri(paths[playlistBox.SelectedIndex]);
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

        private void btnHideShow_Click(object sender, RoutedEventArgs e)
        {
            if (!hide)
            {
                quadWin.espelharMidias(null);
                icHideShow.Source = new BitmapImage(new Uri(@"/icons/ic_hide_on.ico", UriKind.Relative));
                hide = true;
            }
            else
            {
                quadWin.espelharMidias(videoMain);
                quadWin.displayTop.Opacity = 100; 
                hide = false;
                icHideShow.Source = new BitmapImage(new Uri(@"/icons/hide_ic.png", UriKind.Relative));
            }
            btnHideShow.Content = icHideShow;
        }

        private void btnSettingsQuad_Click(object sender, RoutedEventArgs e)
        {
            new SettingsQuad().Show();
        }        

        private void btnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (!fullscreen) {
                quadWin.WindowState = WindowState.Maximized;
                quadWin.WindowStyle = WindowStyle.None;
                icFullScreen.Source = new BitmapImage(new Uri(@"/icons/ic_reduce.ico", UriKind.Relative));
                fullscreen = true;
            }
            else
            {
                quadWin.WindowState = WindowState.Normal;
                quadWin.WindowStyle = WindowStyle.SingleBorderWindow;
                icFullScreen.Source = new BitmapImage(new Uri(@"/icons/ic_fullscreen_on.ico", UriKind.Relative));
                fullscreen = false;
            }
            btnFullScreen.Content = icFullScreen;
        }
    }
}
