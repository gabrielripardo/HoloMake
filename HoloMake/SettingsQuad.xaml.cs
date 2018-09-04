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
    /// Lógica interna para SettingsQuad.xaml
    /// </summary>
    public partial class SettingsQuad : Window
    {
        public SettingsQuad()
        {
            InitializeComponent();
        }

        private void sliderComprim_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void CanvasPreview_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Title = "janelaResize";
            
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

            txtLog.Text = "rectYellow.Width = "; 

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

        }
}
