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
        UIElement displayTopObj;
        UIElement displayBottomObj;
        double old_Top_DisplayTop;
        double old_Top_DisplayBottom;     

        public QuadWin(MediaElement videoM)
        {
            InitializeComponent();

            displayTop.Fill = new VisualBrush(videoM);
            displayLeft.Fill = new VisualBrush(videoM);
            displayRight.Fill = new VisualBrush(videoM);
            displayBottom.Fill = new VisualBrush(videoM);

            double maxComp = Width / 2;
            sliderHT.Maximum = maxComp;
            sliderHT.Value = maxComp / 2;
        }

        private void btnMenosW_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMaisW_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMaisH_Click(object sender, RoutedEventArgs e)
        {
                        
            UIElement displayTopObj = displayTop;
            double old_Left = Canvas.GetLeft(displayTopObj);
            double old_Top = Canvas.GetTop(displayTopObj);

            if(old_Top+10 > 0) { 
                displayTop.Height += 10;
                Canvas.SetTop(displayTopObj, old_Top - 10);
            }
        }

        private void btnMenosH_Click(object sender, RoutedEventArgs e)
        {            
            // displayTop.Width = displayTop.Width - 10;
            displayTop.Height = displayTop.Height - 10;

            UIElement displayTopObj = displayTop;
            double old_Left = Canvas.GetLeft(displayTopObj);
            double old_Top = Canvas.GetTop(displayTopObj);

            //Canvas.SetLeft(displayTopObj, old_Left + 10);
            Canvas.SetTop(displayTopObj, old_Top + 10);
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //----------------< Canvas_SizeChanged() >----------------

            Canvas canvas = sender as Canvas;


            SizeChangedEventArgs canvas_Changed_Args = e;

            //< check >

            //*if size=0 then initial

            if (canvas_Changed_Args.PreviousSize.Width == 0) return;

            //</ check >

            //< init >

            double old_Height = canvas_Changed_Args.PreviousSize.Height;
            double new_Height = canvas_Changed_Args.NewSize.Height;
            double old_Width = canvas_Changed_Args.PreviousSize.Width;
            double new_Width = canvas_Changed_Args.NewSize.Width;

            double scale_Width = new_Width / old_Width;
            double scale_Height = new_Height / old_Height;

            Title = "Height = "+new_Height+"Width = "+new_Width;

            //</ init >
            //----< adapt all children >----

            foreach (FrameworkElement element in canvas.Children)
            {
                //< get >
                double old_Left = Canvas.GetLeft(element);
                double old_Top = Canvas.GetTop(element);
                //</ get >

                // < set Left-Top>
                Canvas.SetLeft(element, old_Left * scale_Width);
                Canvas.SetTop(element, old_Top * scale_Height);
                // </ set Left-Top >

                //< set Width-Heigth >
                element.Width = element.Width * scale_Width;
                element.Height = element.Height * scale_Height;
                //</ set Width-Heigth >
            }
            //----</ adapt all children >----
            //----------------</ Canvas_SizeChanged() >----------------
        }

        private void btnAfastaH_Click(object sender, RoutedEventArgs e)
        {            
            UIElement displayTopObj = displayTop;
            UIElement displayBottomObj = displayBottom;
            double old_Top_DisplayTop = Canvas.GetTop(displayTopObj);
            double old_Top_DisplayBottom = Canvas.GetTop(displayBottomObj);

            if (old_Top_DisplayTop > 0)
            {
                Canvas.SetTop(displayTopObj, old_Top_DisplayTop - 10);
                Canvas.SetTop(displayBottomObj, old_Top_DisplayBottom + 10);
            }            
        }

        private void btnAproxH_Click(object sender, RoutedEventArgs e)
        {
            UIElement displayTopObj = displayTop;
            UIElement displayBottomObj = displayBottom;
            double old_Top_DisplayTop = Canvas.GetTop(displayTopObj);
            double old_Top_DisplayBottom = Canvas.GetTop(displayBottomObj);

            if (old_Top_DisplayTop > 0)
            {
                Canvas.SetTop(displayTopObj, old_Top_DisplayTop + 1);
                Canvas.SetTop(displayBottomObj, old_Top_DisplayBottom - 1);
            }
        }

        private void sliderHT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Title = sliderHT.Value.ToString();
            displayTopObj = displayTop;
            displayBottomObj = displayBottom;

            old_Top_DisplayTop = Canvas.GetTop(displayTopObj);
            old_Top_DisplayBottom = Canvas.GetTop(displayBottomObj);

            Canvas.SetTop(displayTopObj, sliderHT.Value);
          //  Canvas.SetTop(displayBottomObj, sliderHT.Value - 1);

            /*
            if (sliderHT.Value > 0 && sliderHT.Value < 60)
            {
                Canvas.SetTop(displayTopObj, old_Top_DisplayTop + 1);
                Canvas.SetTop(displayBottomObj, old_Top_DisplayBottom - 1);
            }
            else
            {
                Canvas.SetTop(displayTopObj, old_Top_DisplayTop - 1);
                Canvas.SetTop(displayBottomObj, old_Top_DisplayBottom + 1);
            }
            */
        }

        private void sliderH_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UIElement displayTopObj = displayTop;
            double old_Left = Canvas.GetLeft(displayTopObj);
            double old_Top = Canvas.GetTop(displayTopObj);

            if (old_Top + 10 > 0)
            {
                displayTop.Height += 10;
                Canvas.SetTop(displayTopObj, old_Top - 10);
            }
        }
    }
}
