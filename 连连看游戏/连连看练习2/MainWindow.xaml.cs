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
using System.Windows.Media.Animation;

namespace 连连看练习2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }


        // 定义一个数组  用来存储第一关四张图片的标号
        int[] face = new int[4];

        // 创建计时器  来控制动画效果
        DispatcherTimer dt = new DispatcherTimer();

        // 创建一个显示大眼睛左右摇摆的故事版
        Storyboard story = new Storyboard();
        // 创建一个显示旋转360度出现动物图片的故事版
        Storyboard storyFanZhuan = new Storyboard();

        // 随机添加
        Random ra = new Random();
        // 定义一个  变量用来记录  当中两两相等时的对数
        int count = 0;
        // 创建一个数组  用来存储  点击过的图片
        Image[] imgclick;
        // 定义一个int变量  用来存储点击的次数
        int click = 0;
        // 用来存放  点击的第一章图片的编号
        string str = "";

        // 判断是否点击正确
        bool isSuc = true;

        // 记录消除了几组图片
        int temp = 0;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            this.Left = 0;
            this.Top = 0;
            this.Width = SystemParameters.FullPrimaryScreenWidth;
            this.Height = SystemParameters.FullPrimaryScreenHeight;


            ShowImage();
        }


        void ShowImage()
        {
            imgclick = new Image[(int)Math.Sqrt(face.Length)];
            // 给数组中添加值  两对值  (数组中存储的形式)
            for (int i = 0; i < face.Length; i++)
            {
                count = 0;
                face[i] = ra.Next(0, (int)Math.Sqrt(face.Length));
                for (int j = 0; j < i; j++)
                {
                    if (face[i] == face[j])
                    {
                        count++;
                        if (count == Math.Sqrt(face.Length))
                        {
                            i--;
                            break;
                        }
                    }
                }

            }


            //  创建Image组件,并添加进画布中,同时附上图片
            for (int i = 0; i < face.Length; i++)
            {
                Image img = new Image();
                img.Width = img.Height = 100;
                img.Stretch = Stretch.Fill;
                img.Source = new BitmapImage(new Uri("Images/" + face[i] + ".png", UriKind.Relative));
                img.Tag = face[i];
                Canvas.SetLeft(img, this.Width / 2 - Math.Sqrt(face.Length) * img.Width / 2 + (i % (int)Math.Sqrt(face.Length) * img.Width));
                Canvas.SetTop(img, this.Height / 2 - Math.Sqrt(face.Length) * img.Height / 2 + (i / (int)Math.Sqrt(face.Length) * img.Height));
                back.Children.Add(img);
            }

            dt.Interval = TimeSpan.FromMilliseconds(2000);
            dt.Tick -= dt_Tick;
            dt.Tick += dt_Tick;
            //  dt.Interval = new TimeSpan();
            dt.Start();
        }

        void dt_Tick(object sender, EventArgs e)
        {
            fun();
            dt.Stop();

        }

        private void fun()
        {
            foreach (Image item in back.Children)
            {

                item.Source = new BitmapImage(new Uri("Images/Face/" + ra.Next(1, 9) + ".png", UriKind.Relative));
               
                // 处理动画效果
                RotateTransform rtf = new RotateTransform();
                item.RenderTransform = rtf;
                item.RenderTransformOrigin = new Point(0.5, 0.5);

                // 设置动画  效果

                DoubleAnimation da = new DoubleAnimation(-20, 20, new Duration(TimeSpan.FromMilliseconds(600)));
                Storyboard.SetTarget(da, item);
                Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.Angle"));

                da.AutoReverse = true;
                da.RepeatBehavior = new RepeatBehavior(3);
               // da.RepeatBehavior = RepeatBehavior.Forever;
                story.Children.Add(da);
                
                story.Begin();

                // 添加鼠标点击事件
                item.MouseDown -= item_MouseDown;
                item.MouseDown+=item_MouseDown;

            }

        }

        void item_MouseDown(object sender, MouseButtonEventArgs e)
        {

            storyFanZhuan.Children.Clear();
            Image img = (Image)sender;
            img.MouseDown -= item_MouseDown;
            img.Source = new BitmapImage(new Uri("Images/" + img.Tag.ToString() + ".png", UriKind.Relative));
                
            RotateTransform rt = new RotateTransform();
            img.RenderTransform = rt;
            img.RenderTransformOrigin = new Point(0.5, 0.5);

            DoubleAnimation da = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromMilliseconds(100)));
            Storyboard.SetTarget(da, img);
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.Angle"));
            da.RepeatBehavior = new RepeatBehavior(3);
          
            storyFanZhuan.Children.Add(da);

            storyFanZhuan.Begin();

            // 判断消失
            imgclick[click] = img;
            click++;
            if (click==1)
            {
                
                // 要记录  第一次点击的图片   作为标记
                str = img.Tag.ToString();
            }
            else if (click==Math.Sqrt(face.Length))
            {
                click = 0;
                isSuc = true;
                foreach (Image item in imgclick)
                {
                    if (item.Tag.ToString()!=str)
                    {
                        isSuc = false;
                       // break;
                        fun();
                    }
                   
                }
                if (isSuc==true)
                {
                    for (int i = 0; i < imgclick.Length; i++)
                    {
                        back.Children.Remove(imgclick[i]);
                    }
                }
            }

        }


    }



}
