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

namespace _0621Watering
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 600;
            this.Background = Brushes.DarkCyan;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            CreateFiled();//创建土地
            CreatePipeV();//创建链接水池的通道
            CreatePool();//创建水池
            CreateReadyPipe();//创建四个预备的水管
        }
        Image[] Pipe = new Image[4];//水管图片数组
        PipeD[] pid = new PipeD[4];//水管属性数组
        Random r = new Random();


        private void CreateReadyPipe()
        {

            for (int i = 0; i < Pipe.Length; i++)
            {
                pid[i] = new PipeD(r.Next(1, 12));//水管对象


                Pipe[i] = new Image();
                Pipe[i].Source = new BitmapImage(new Uri("images/Ready/" + pid[i].Num + ".jpg", UriKind.Relative));
                if (i < 3)
                {
                    Pipe[i].Width = Pipe[i].Height = 50;
                }
                else
                {
                    Pipe[i].Width = Pipe[i].Height = 60;
                }

                Canvas.SetLeft(Pipe[i], 50 + i * (50 + 10));
                Canvas.SetTop(Pipe[i], 50);
                back.Children.Add(Pipe[i]);
            }
        }

        private void CreatePool()
        {
            Image Pool = new Image();
            Pool.Width = 80;
            Pool.Height = 50;
            Pool.Source = new BitmapImage(new Uri("images/Water.jpg", UriKind.Relative));
            Canvas.SetTop(Pool, 50);
            Canvas.SetLeft(Pool, 435);
            back.Children.Add(Pool);

            Pool.MouseLeftButtonDown += Pool_MouseLeftButtonDown;
        }
        //递归算法
        void Pool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            imgV.Source = new BitmapImage(new Uri("images/Water/H10.jpg", UriKind.Relative));

          
       

      
        }


        Image imgV = new Image();
        private void CreatePipeV()
        {

            imgV.Width = imgV.Height = 50;
            imgV.Source = new BitmapImage(new Uri("images/V.jpg", UriKind.Relative));
            Canvas.SetLeft(imgV, 450);
            Canvas.SetTop(imgV, 100);
            back.Children.Add(imgV);
        }
      
        private void CreateFiled()
        {
            for (int i = 0; i < 49; i++)
            {
               
                Image img = new Image();
                img.Tag = i;
                img.Width = img.Height = 50;
                img.MouseLeftButtonDown += img_MouseLeftButtonDown;
                img.Source = new BitmapImage(new Uri("images/field.jpg", UriKind.Relative));
                Canvas.SetLeft(img, 300 + i % 7 * img.Width);
                Canvas.SetTop(img, 150 + i / 7 * img.Height);
                back.Children.Add(img);
            }
        }

        void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.MouseLeftButtonDown -= img_MouseLeftButtonDown;

            
            img.Source = Pipe[3].Source;



            for (int i = 3; i > 0; i--)
            {
                Pipe[i].Source = Pipe[i - 1].Source;
                pid[i] = pid[i - 1];
            }
            pid[0] = new PipeD(r.Next(1, 12));
            Pipe[0].Source = new BitmapImage(new Uri("images/Ready/" + pid[0].Num + ".jpg", UriKind.Relative));
        }
    }
}
