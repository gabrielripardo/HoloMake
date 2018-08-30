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

namespace CanvasResize
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        double old_Width;
        double new_Width;

        public MainWindow()
        {
            InitializeComponent();
            new_Width = old_Width;
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
            old_Width = canvas_Changed_Args.PreviousSize.Width;
            double new_Width = canvas_Changed_Args.NewSize.Width;

            double scale_Width = new_Width / old_Width;
            double scale_Height = new_Height / old_Height;
            
            txtLog.Text = "rectYellow.Width = " + rectYellow.Width + "oldWidth: "+ old_Width+" / newWidth: "+ new_Width;

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
 
        private void btnMaisH_Click(object sender, RoutedEventArgs e)
        {
            /*Afastamento de objetos
            UIElement rectWhiteObj = rectWhite;
            Canvas.SetLeft(rectWhiteObj, 200);
            */
            double old_largura = 783;
            double new_largura = 900;
            double old_comprim = 800;
            double new_comprim = 1500;

            double scale_larg = new_largura / old_largura;
            double scale_comp = new_comprim / old_comprim;

            //Elementos do Canvas
            //1
            UIElement rectWhiteObj = rectWhite;
            double old_Left = Canvas.GetLeft(rectWhiteObj);
            double old_Top = Canvas.GetTop(rectWhiteObj);

            Canvas.SetLeft(rectWhiteObj, old_Left * scale_larg);
            Canvas.SetTop(rectWhiteObj, old_Top * scale_comp);

            rectWhite.Width = rectWhite.Width * scale_larg;
            rectWhite.Height = rectWhite.Height * scale_comp;

            //2
            UIElement rectRedObj = rectRed;
            old_Left = Canvas.GetLeft(rectRedObj);
            old_Top = Canvas.GetTop(rectRedObj);

            Canvas.SetLeft(rectRedObj, old_Left * scale_larg);
            Canvas.SetTop(rectRedObj, old_Top * scale_comp);

            rectRed.Width = rectRed.Width * scale_larg;
            rectRed.Height = rectRed.Height * scale_comp;

            txtLog.Text = "old_width: "+old_largura;
        }
    }
}
