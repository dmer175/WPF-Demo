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

namespace 连连看游戏
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        // 定义数组的长度4  存放第一关的4张图片的编号
        int []face=new int[4];

        // 随机数
        Random ra = new Random();
        // 定义一个变量  用来表示给数组里添加元素的时候  ,两两比较的对数
        int count = 0;
        // 定义一个3秒后  变换图片的计时器
        DispatcherTimer dt;

        // 定义一个image类型数组 ,它是用来存储每一关里连着的点击所对应的个数(第一关里面是连着的两个)的image组件
        Image[] click;


        // 定义一个故事版  来存储点击大眼睛图片旋转360度的动画
        Storyboard story = new Storyboard();
        public MainWindow()
        {
            InitializeComponent();
            // 坐标点
            this.Left = 0;
            this.Top = 0;
            // 大小
            this.Width = SystemParameters.FullPrimaryScreenWidth;
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            // 显示私有图片的方法    
            ShowImage();
        }
        private void ShowImage()
        {

          //  imgs=new Image[(int)Math.Sqrt(face.Length)];
            // 获取第一关的两组数据  
            // 给数组里添加数据  (赋值)
            for (int i = 0; i < face.Length; i++)
            {
                // 每循环一次  就给数据里添加一个元素 这个元素的特点是:两两相同(就是4个长度 每两个相同)
                count = 0;// 记录比较时的对数(就是说有几对)

                //给数据添加数据(赋值)  特点:只可以有 两种值(0,1)
                face[i] = ra.Next(0,(int)Math.Sqrt(face.Length));

                // 问题:  存的是对数(2对) 有可能会随机出现 3对一的情况
                // 解决方案  使用for循环进行循环比较
                for (int j = 0; j <i; j++)
                {
                    if (face[i]==face[j])// 只能比较一次相等
                    {
                        count++;  //对数+1
                        if (count==Math.Sqrt(face.Length))// 如果第一关比较相等的次数为2时,  就要重新随机图片i--
                        {
                            i--;
                            break;
                        }
                    }
                }



            }
            for (int i = 0; i < face.Length; i++)
            {
                
                // 实例化  一个Image 对象
                Image img = new Image();
                img.Width = img.Height = 100;
                // 设置图片是否完全填充  Image
                img.Stretch = Stretch.Fill;
                // 附上图片
                img.Source = new BitmapImage(new Uri("Images/"+face[i]+".png",UriKind.Relative));

                img.Tag=face[i];
                //Canvas.SetLeft(img,this.Width/2-img.Width+(i%2*img.Width));
                //Canvas.SetTop(img,this.Height/2-img.Height+(i/2*img.Height));
                Canvas.SetLeft(img,this.Width/2-Math.Sqrt(face.Length)*img.Width/2+(i%(int)Math.Sqrt(face.Length)*img.Width));

                Canvas.SetTop(img,this.Height/2-Math.Sqrt(face.Length)*img.Height/2+(i/(int)Math.Sqrt(face.Length)*img.Height));
                back.Children.Add(img);
            }

            // 动画部分  ,解决第一个问题:获取大眼睛图片,将图片放到Image组件中去
            // 在这实例化  一个一个新的组件
            click=new Image[2]; // 第一关里面是连着的两个  所以长度为2
            // 间隔3秒之后来播放,实现动画效果
            // 实例化计时器
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(1000);
            dt.Tick += dt_Tick;
            dt.Start();


        }


        // 生成  动画效果的计时器
        void dt_Tick(object sender, EventArgs e)
        {
            foreach (Image img in back.Children)
            {
               // 给每一个原图生面附上  大眼睛图
                img.Source = new BitmapImage(new Uri("Images/Face/"+ra.Next(1,9)+".png",UriKind.Relative));
                // 让该图片左右摆动  实现动画效果
                // 实例化一个二维坐标  按我们指定的顺时针旋转对象
                RotateTransform rt = new RotateTransform();
                img.RenderTransform = rt;
                // 之后需要获取对象的旋转信息
                img.RenderTransformOrigin = new Point(0.5,0.5);


                // 进行动画处理 ,实例化一个对象
                DoubleAnimationUsingKeyFrames daukf = new DoubleAnimationUsingKeyFrames();

                // 设置动画的效果的重复执行行为
                daukf.RepeatBehavior = RepeatBehavior.Forever;

                //// 是否在时间间隔之后按照相反的方向继续旋转
                //daukf.AutoReverse = true;

                // 关联动画
                //  EasingByteKeyFrame ebkf1 = new EasingByteKeyFrame(-30,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)));
                EasingDoubleKeyFrame edkf1 = new EasingDoubleKeyFrame(-30, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)));
                EasingDoubleKeyFrame edkf2 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000)));
                EasingDoubleKeyFrame edkf3 = new EasingDoubleKeyFrame(30, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1500)));
                // 添加到方向的对象中
                daukf.KeyFrames.Add(edkf1);
                daukf.KeyFrames.Add(edkf2);
                daukf.KeyFrames.Add(edkf3);

                Storyboard.SetTarget(daukf, img);
                Storyboard.SetTargetProperty(daukf, new PropertyPath("RenderTransform.Angle"));
                story.Children.Add(daukf);

                img.MouseLeftButtonDown += img_MouseLeftButtonDown;
            }
            dt.Stop();
            story.Begin();
        }


        Storyboard ss = new Storyboard();// 定义一个故事版

        int s = 0;

        string st = "";
        // 定义一个数组  来存储 点击的过的图片 
       // Image[] imgs;
        void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ss.Children.Clear();
            // (Image)
            Image img = (Image)sender;
            img.Source = new BitmapImage(new Uri("Images/" + img.Tag.ToString() + ".png", UriKind.Relative));

            RotateTransform rtf = new RotateTransform();
            img.RenderTransform = rtf;
            img.RenderTransformOrigin = new Point(0.5, 0.5);

            DoubleAnimation da = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromMilliseconds(100)));
            Storyboard.SetTarget(da, img);
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.Angle"));

            ss.Children.Add(da);

        }
    }
}
