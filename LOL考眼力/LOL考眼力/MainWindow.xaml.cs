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

namespace LOL考眼力
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Top = this.Left = 0;
            this.Width = SystemParameters.FullPrimaryScreenWidth;
            this.Height = SystemParameters.FullPrimaryScreenHeight;            

            Loaded += MainWindow_Loaded;
        }
        Random ra = new Random();
        int[] face = new int[4];
        Image[] imClick;
        DispatcherTimer dt = new DispatcherTimer();
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            DeFen();

            ShowImage();

            MuBiao();

            MuBiaoTime();

            dt.Tick += dt_Tick;
            dt.Interval = new TimeSpan(0, 0, 0, 6);
            dt.Start();
        }
        Label df;
        Label sw;
        Label cg;
        int dfjl = 0;
        int swjl = 0;
        int cgjl = 0;
        private void DeFen()
        {
            df = new Label();
            df.Height = 30;
            df.Width = 60;
            df.Content = "得分：";
            //fs.FontStretch = FontStretches.s;
            Canvas.SetLeft(df, 10);
            Canvas.SetTop(df, 0);
            back.Children.Add(df);

            sw = new Label();
            sw.Height = 30;
            sw.Width = 60;
            sw.Content = "失误：";
            Canvas.SetLeft(sw, 80);
            Canvas.SetTop(sw, 0);
            back.Children.Add(sw);

            cg = new Label();
            cg.Height = 30;
            cg.Width = 60;
            cg.Content = "错过：";
            Canvas.SetLeft(cg, 150);
            Canvas.SetTop(cg, 0);
            back.Children.Add(cg);
        }

        private void ShowImage()
        {
            imClick = new Image[2];
            for (int i = 0; i < face.Length; i++)
            {

                face[i] = ra.Next(12, 51);
                for (int j = 0; j < i; j++)
                {
                    if (face[i] == face[j])
                    {
                        i--;
                        break;
                    }
                }
            }
            for (int i = 0; i < face.Length; i++)
            {
                Image im = new Image();
                im.Height = im.Width = 100;
                im.Source = new BitmapImage(new Uri("images/" + face[i] + ".png", UriKind.Relative));
                Canvas.SetLeft(im, this.Width / 2 - (int)Math.Sqrt(face.Length) * im.Width/2 + i % (int)Math.Sqrt(face.Length) * im.Width);
                Canvas.SetTop(im, this.Height / 2 - (int)Math.Sqrt(face.Length) * im.Height/2 + i / (int)Math.Sqrt(face.Length) * im.Height);
                im.Tag = face[i];
                //imClick[i] = im;
                back.Children.Add(im);

                im.MouseEnter += im_MouseEnter;
                im.MouseLeave += im_MouseLeave;
                im.MouseLeftButtonDown += im_MouseLeftButtonDown;
            }
        }
        int num = 1;
        bool isTrue = true;
        int temp = 0;
        int temp2 = 0;
        void im_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image imm = (Image)sender;
            imClick[num] = imm;
            num++;
            if (num==2)
            {
                for (int i = 1; i < imClick.Length; i++)
                {
                    num = 1;
                    isTrue = true;
                    for (int j = 0; j < i; j++)
                    {
                        if (imClick[j].Tag.ToString()!=imClick[i].Tag.ToString())
                        {
                            isTrue = false;
                            swjl++;
                            temp2++;
                            sw.Content = "失误：" + swjl.ToString();
                            if (temp2==3)
                            {
                                temp2 = 0;
                                MessageBox.Show("输了");
                                dt.Stop();
                                this.Close();
                                //imm.MouseLeftButtonDown -= im_MouseLeftButtonDown;
                                break;
                            }
                            //ShowImage();

                            //MuBiao();

                            //MuBiaoTime();
                        }
                    }
                }

                if (isTrue==true)
                {
                    dfjl++;
                    df.Content = "得分：" + dfjl.ToString();

                    temp++;
                    if (temp==3)
                    {
                        temp = 0;
                        face = new int[(int)Math.Pow((int)Math.Sqrt(face.Length)+1, 2)]; 
                    }

                    ShowImage();

                    MuBiao();

                    MuBiaoTime();
                }
            }
            dt.Stop();
            dt.Start();
        }
        Storyboard sbd2 = new Storyboard();
        private void MuBiaoTime()
        {
            Border mt = new Border();
            mt.Width = 20;
            mt.Height = 600;
            mt.Background = Brushes.Red;
            mt.CornerRadius = new CornerRadius(10);
            Canvas.SetLeft(mt, this.Width - 100);
            Canvas.SetTop(mt, 50);
            back.Children.Add(mt);


            Border mtFace = new Border();
            mtFace.Width = 20;
            mtFace.Height = 600;
            mtFace.Background = Brushes.White;
            Canvas.SetLeft(mtFace, this.Width - 100);
            Canvas.SetTop(mtFace, -550);
            back.Children.Add(mtFace);
            
            DoubleAnimation da2 = new DoubleAnimation(-550,50,new Duration(TimeSpan.FromSeconds(6)));
            Storyboard.SetTarget(da2, mtFace);
            Storyboard.SetTargetProperty(da2, new PropertyPath("(Canvas.Top)"));
            //da2.RepeatBehavior = RepeatBehavior.Forever;
            sbd2.Children.Add(da2);
            sbd2.Begin();



        }
        Border bd;
        void im_MouseLeave(object sender, MouseEventArgs e)
        {
            back.Children.Remove(bd);
            Image im = (Image)sender;
            Canvas.SetLeft(im, Canvas.GetLeft(im) - 1);
            Canvas.SetTop(im, Canvas.GetTop(im) - 1);
        }

        void im_MouseEnter(object sender, MouseEventArgs e)
        {
            Image im = (Image)sender;
            Canvas.SetLeft(im, Canvas.GetLeft(im) + 1);
            Canvas.SetTop(im, Canvas.GetTop(im) + 1);
            //im.MouseEnter -= im_MouseEnter;
            bd = new Border();
            bd.Width = bd.Height = im.Width;
            bd.BorderBrush = Brushes.Yellow;
            bd.BorderThickness = new Thickness(1);
            Canvas.SetLeft(bd, Canvas.GetLeft(im));
            Canvas.SetTop(bd, Canvas.GetTop(im));
            back.Children.Add(bd);
        }
        Storyboard sbd = new Storyboard();
        Image im1;
        private void MuBiao()
        {
            int jl = ra.Next(face.Length);
            im1 = new Image();
            im1.Height = 150;
            im1.Width = 150;
            im1.Source = new BitmapImage(new Uri("Images/" + face[jl] + ".png", UriKind.Relative));
            im1.Stretch = Stretch.UniformToFill;
            im1.Tag = face[jl];
            Canvas.SetLeft(im1, 50);
            Canvas.SetTop(im1, 40);
            imClick[0] = im1;
            back.Children.Add(im1);

            //imfa = new Border();
            //imfa.Width = imfa.Height = 150;
            ////imfa.Source = new BitmapImage(new Uri("images/12.png", UriKind.Relative));
            //imfa.Background = Brushes.White;
            //Canvas.SetLeft(imfa, 50);
            //Canvas.SetTop(imfa, 40);
            //back.Children.Add(imfa);

            //DoubleAnimation da = new DoubleAnimation(40, 190, new Duration(TimeSpan.FromSeconds(3)));
            ////da.RepeatBehavior = RepeatBehavior.Forever;
            //da.AutoReverse = true;
            //Storyboard.SetTarget(da, imfa);
            //Storyboard.SetTargetProperty(da, new PropertyPath("(Canvas.Top)"));
            //sbd.Children.Add(da);
            //sbd.Begin();

            DoubleAnimation da = new DoubleAnimation(0, im1.Height, new Duration(TimeSpan.FromSeconds(3)));
            //da.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(da, im1);
            Storyboard.SetTargetProperty(da, new PropertyPath("Height"));
            da.AutoReverse = true;
            sbd.Children.Add(da);
            sbd.Begin();
        }
        Border imfa;
        
        void dt_Tick(object sender, EventArgs e)
        {
            ShowImage();

            MuBiao();

            MuBiaoTime();

            cgjl++;
            cg.Content = "错过：" + cgjl.ToString();
        }
    }
}
