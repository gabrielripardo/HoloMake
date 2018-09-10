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
        UIElement displayLeftObj;
        UIElement displayRightObj;            

        public QuadWin()
        {
            InitializeComponent();
                  
            //Seta o valor máximo das barras
            double maxComp = Height / 4;
            sliderHT.Maximum = maxComp;

            maxComp = Width / 3;
            sliderWT.Maximum = maxComp;

            sliderH.Maximum = 500;
            sliderW.Maximum = 500;

            //Seta o valor atual das barras
            UIElement displayTopObj = displayTop;            
            double old_Top = Canvas.GetTop(displayTopObj);
            sliderHT.Value = old_Top;

            UIElement displayLeftObj = displayLeft;
            double old_Left = Canvas.GetLeft(displayLeftObj);
            sliderWT.Value = old_Left;

            sliderH.Value = displayTop.Height;
            sliderW.Value = displayTop.Width;
        }
        public void espelharMidias(MediaElement videoM)
        {            
            displayTop.Fill = new VisualBrush(videoM);
            displayLeft.Fill = new VisualBrush(videoM);
            displayRight.Fill = new VisualBrush(videoM);
            displayBottom.Fill = new VisualBrush(videoM);
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

            //Settings Slider
            sliderHT.Maximum = new_Height/4;
            sliderWT.Maximum = new_Width/3;

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
     
        
        private void sliderW_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            displayTop.Width = sliderW.Value;
            displayLeft.Width = sliderW.Value;
            displayRight.Width = sliderW.Value;
            displayBottom.Width = sliderW.Value;
        }

        private void sliderH_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UIElement displayTopObj = displayTop;         
            double old_Top = Canvas.GetTop(displayTopObj);
            
            displayTop.Height = sliderH.Value;
            displayLeft.Height = sliderH.Value;
            displayRight.Height = sliderH.Value;
            displayBottom.Height = sliderH.Value;
            // Canvas.SetTop(displayTopObj, old_Top - sliderH.Value);

        }

        private void sliderHT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Title = sliderHT.Value.ToString();
            displayTopObj = displayTop;
            displayBottomObj = displayBottom;            

            Canvas.SetTop(displayTopObj, sliderHT.Value);
            Canvas.SetTop(displayBottomObj, sliderHT.Maximum * 3 - sliderHT.Value);
        }

        private void sliderWT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Title = sliderHT.Value.ToString();
            displayLeftObj = displayLeft;
            displayRightObj = displayRight;            

            Canvas.SetLeft(displayLeftObj, sliderWT.Value);
            Canvas.SetLeft(displayRightObj, sliderWT.Maximum * 2.5 - sliderWT.Value);
        }        
    }
}
