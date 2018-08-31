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
using System.Windows.Shapes;

namespace HoloMake
{
    /// <summary>
    /// Lógica interna para QuadWin.xaml
    /// </summary>
    public partial class QuadWin : Window
    {
        public QuadWin(VisualBrush visualBrush)
        {
            InitializeComponent();

            displayTop.Fill = visualBrush;
            displayLeft.Fill = visualBrush;
            displayRight.Fill = visualBrush;
            displayBottom.Fill = visualBrush;
        }

        public QuadWin(MediaElement videoM)
        {
            InitializeComponent();

            displayTop.Fill = new VisualBrush(videoM);
            displayLeft.Fill = new VisualBrush(videoM);
            displayRight.Fill = new VisualBrush(videoM);
            displayBottom.Fill = new VisualBrush(videoM);
        }

        private void btnMenosW_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMaisW_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMaisH_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMenosH_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
