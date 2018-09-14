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
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;
namespace SnakeGame
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

        Random r = new Random();
        Border but = new Border();//摇杆按钮
        Border touch = new Border();//摇杆外部圈
        double tx, ty;///touch中心点坐标
        bool isOn = false;
        double x, y;///touch按钮坐标

        List<Border> snake = new List<Border>();///蛇的泛型
        List<Border> snakeposition = new List<Border>();///坐标泛型
        List<Border> snList = new List<Border>();//机械小蛇
        List<Border> snPosition = new List<Border>();//机械小蛇位置
        int snakeLength = 5;///蛇初始长度
        double snakesize = 15;///蛇初始的大小

        DispatcherTimer moveTimer = new DispatcherTimer();

        DispatcherTimer diedTimer = new DispatcherTimer();

        DispatcherTimer roundTimer = new DispatcherTimer();

        DispatcherTimer walkTimer = new DispatcherTimer();
        DispatcherTimer diedSTimer = new DispatcherTimer();
        Storyboard stb = new Storyboard();
        Storyboard stb2 = new Storyboard();
        double moveX, moveY;///蛇两个方向移动的速度
        bool begin = false;///是否开始
        Border thick = new Border();
        MediaPlayer SnakeMusic= new MediaPlayer();
        MediaPlayer SnakeDieMusic = new MediaPlayer();
        //MediaElement ss = new MediaElement();

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

            #region///边框

            thick.Tag = "bk";
            thick.Width = 800;
            thick.Height = 600;
            Canvas.SetLeft(thick, 0);
            Canvas.SetTop(thick, 0);
            thick.BorderThickness = new Thickness(5);
            thick.BorderBrush = Brushes.Brown;
            back.Children.Add(thick);
            #endregion
            #region///地图中小食物
            for (int i = 0; i < 300; i++)
            {
                Border small = new Border();
                small.Tag = "food";
                back.Children.Add(small);
                small.Width = small.Height = 6;
                small.Background = new SolidColorBrush(Color.FromRgb((byte)r.Next(256), (byte)r.Next(256), (byte)r.Next(256)));
                small.CornerRadius = new CornerRadius(4);
                Canvas.SetTop(small, r.Next(600));
                Canvas.SetLeft(small, r.Next(800));
            }
            #endregion
            #region///虚拟摇杆作用

            touch.Tag = "t";
            touch.Width = touch.Height = 100;
            touch.BorderThickness = new Thickness(4);
            touch.CornerRadius = new CornerRadius(100);
            touch.BorderBrush = Brushes.DeepSkyBlue;
            Canvas.SetTop(touch, 480);
            Canvas.SetLeft(touch, 20);
            back.Children.Add(touch);

            but.Tag = "t";
            Canvas.SetZIndex(but, 10);
            but.Width = but.Height = 40;
            tx = Canvas.GetLeft(touch) + touch.Width / 2;//touch外部的圈的中心点坐标
            ty = Canvas.GetTop(touch) + touch.Height / 2;
            x = tx - but.Width / 2;//touch中间按钮的坐标
            y = ty - but.Height / 2;
            but.Background = Brushes.OrangeRed;
            but.CornerRadius = new CornerRadius(20);
            Canvas.SetTop(but, y);
            Canvas.SetLeft(but, x);
            but.MouseLeftButtonDown += but_MouseLeftButtonDown;
            but.MouseLeftButtonUp += but_MouseLeftButtonUp;
            MouseMove += MainWindow_MouseMove;
            back.Children.Add(but);
            #endregion
            #region///制造初始小蛇
            for (int i = 0; i < snakeLength; i++)
            {
                Border br = new Border();
                br.Tag = "snake";
                br.BorderThickness = new Thickness(2);
                br.BorderBrush = Brushes.Red;
                br.Width = br.Height = snakesize;
                br.CornerRadius = new CornerRadius(20);
                Canvas.SetTop(br, 300);
                Canvas.SetLeft(br, 300 - i * snakesize);
                if (i == 0)
                {
                    br.Background = Brushes.DeepPink;
                }
                else
                {
                    br.Background = Brushes.LightCoral;
                }
                back.Children.Add(br);
                snake.Add(br);

                Border position = new Border();
                position.Tag = "local";
                Canvas.SetTop(position, 300);
                Canvas.SetLeft(position, 300 - i * snakesize);
                back.Children.Add(position);
                snakeposition.Add(position);
            }
            #endregion
            #region///控制蛇的移动
            moveTimer.Interval = TimeSpan.FromMilliseconds(100);
            moveTimer.Tick += moveTimer_Tick;
            moveTimer.Start();
            #endregion
            #region///判断蛇的死亡
            diedTimer.Interval = TimeSpan.FromMilliseconds(300);
            diedTimer.Tick += diedTimer_Tick;
            diedTimer.Start();
            diedSTimer.Interval = TimeSpan.FromMilliseconds(300);
            diedSTimer.Tick += diedSTimer_Tick;
            diedSTimer.Start();

            #endregion
            #region///制造自由小蛇
            MakeSnake();

            #endregion
            #region
            roundTimer.Interval = TimeSpan.FromMilliseconds(1000);
            roundTimer.Tick += roundTimer_Tick;
            roundTimer.Start();

            centerx = 0.5;
            centery = 0.5;

            walkTimer.Interval = TimeSpan.FromMilliseconds(100);
            walkTimer.Tick += walkTimer_Tick;
            walkTimer.Start();
            SnakeMusic.Open(new Uri("7895.wav", UriKind.Relative));
            SnakeMusic.Volume = 60;  //音量
            SnakeMusic.Play();
            #endregion
        }
        private void MakeSnake()
        {
             #region
            double top, left;
            top = r.Next(100, 560);
            left = r.Next(100, 700);
            for (int i = 0; i < snakeLength; i++)
            {
                Border br = new Border();
                br.BorderThickness = new Thickness(2);
                br.BorderBrush = Brushes.Red;
                br.Width = br.Height = snakesize;
                br.Tag = "small";
                br.CornerRadius = new CornerRadius(20);
                Canvas.SetTop(br, top);
                Canvas.SetLeft(br, left - i * snakesize);
                if (i == 0)
                {
                    br.Background = Brushes.DarkOrchid;
                }
                else
                {
                    br.Background = Brushes.Peru;
                }
                back.Children.Add(br);
                snList.Add(br);
                Border position = new Border();
                position.Tag = "smallp";
                Canvas.SetTop(position, top);
                Canvas.SetLeft(position, left - i * snakesize);
                back.Children.Add(position);
                snPosition.Add(position);
            }
            begin = true;
            isSnakeLife = true;
            #endregion
        }
        double centerx, centery;
        double forx, fory;
        double runx, runy;
        void roundTimer_Tick(object sender, EventArgs e)
        {
            #region///单机蛇的随机方向

            if (isSnakeLife)
            {
                forx = r.NextDouble();
                fory = r.NextDouble();
                double distance = Math.Sqrt((centerx - forx) * (centerx - forx) + (centery - fory) * (centery - fory));
                runx = (centerx - forx) / distance;
                runy = (centery - fory) / distance;

                if (Canvas.GetLeft(snList[0]) + snList[0].Width >= thick.Width - 100 ||
                    Canvas.GetTop(snList[0]) + snList[0].Height >= thick.Height - 100 ||
                    Canvas.GetLeft(snList[0]) <= 100 || Canvas.GetTop(snList[0]) <= 100)
                {
                    runx = -runx;
                    runy = -runy;
                }
            }

            #endregion
        }
        double sx, sy, ssx, ssy;
        void diedTimer_Tick(object sender, EventArgs e)
        {
            #region///玩家蛇的死亡判断
            if (Canvas.GetLeft(snake[0]) + snake[0].Width >= thick.Width - snake[0].Width || Canvas.GetTop(snake[0]) + snake[0].Height >= thick.Height - snake[0].Height || Canvas.GetLeft(snake[0]) <= 0 || Canvas.GetTop(snake[0]) <= 0)
            {
                begin = false;
                moveTimer.Stop();
                SnakeDied(true, snake, snakeposition);
                diedTimer.Stop();
                return;
            }

            sx = Canvas.GetLeft(snake[0]);
            sy = Canvas.GetTop(snake[0]);
            for (int i = snList.Count - 1; i >= 0; i--)
            {
                ssx = Canvas.GetLeft(snList[i]);
                ssy = Canvas.GetTop(snList[i]);
                double disantce = Math.Sqrt((sx - ssx) * (sx - ssx) + (sy - ssy) * (sy - ssy));
                if (disantce < snList[i].Width / 2 + snake[0].Width / 2)
                {
                    diedTimer.Stop();
                    moveTimer.Stop();
                    SnakeDied(true, snake, snakeposition);
                    return;
                }
            }

            #endregion
        }
        bool isSnakeLife = false;
        void diedSTimer_Tick(object sender, EventArgs e)
        {
            #region///单机蛇的死亡

            if (isSnakeLife)
            {
                if (Canvas.GetLeft(snList[0]) + snList[0].Width >= thick.Width - snList[0].Width ||
                    Canvas.GetTop(snList[0]) + snList[0].Height >= thick.Height - snList[0].Height ||
                    Canvas.GetLeft(snList[0]) <= 5 || Canvas.GetTop(snList[0]) <= 5)
                {
                    begin = false;
                    isSnakeLife = false;
                    //walkTimer.Stop();
                    SnakeDied(true, snList, snPosition);
                    //diedSTimer.Stop();
                    //MakeSnake();
                    return;
                }
                ssx = Canvas.GetLeft(snList[0]);
                ssy = Canvas.GetTop(snList[0]);
                for (int i = snake.Count - 1; i >= 0; i--)
                {
                    sx = Canvas.GetLeft(snake[i]);
                    sy = Canvas.GetTop(snake[i]);
                    double disantce = Math.Sqrt((sx - ssx) * (sx - ssx) + (sy - ssy) * (sy - ssy));
                    if (disantce < snList[0].Width / 2 + snake[i].Width / 2)
                    {
                        //diedSTimer.Stop();
                        //walkTimer.Stop();
                        SnakeDied(true, snList, snPosition);
                        isSnakeLife = false;

                        return;
                    }
                }
            }
            else if (!isSnakeLife)
            {
                MakeSnake();
            }

            #endregion
        }
        int count = 0, fcount = 0;///两条蛇吃食物的数量
        int snakecount = 0, snlistcount = 0;///两条蛇的长度
        void moveTimer_Tick(object sender, EventArgs e)///蛇的移动
        {
            #region///玩家蛇的移动


            if (begin)
            {

                Canvas.SetLeft(snake[0], Canvas.GetLeft(snake[0]) + moveX * 10);
                Canvas.SetTop(snake[0], Canvas.GetTop(snake[0]) + moveY * 10);
                for (int i = 1; i < snake.Count; i++)
                {
                    Canvas.SetLeft(snake[i], Canvas.GetLeft(snakeposition[i - 1]));
                    Canvas.SetTop(snake[i], Canvas.GetTop(snakeposition[i - 1]));
                }
                for (int i = 0; i < snakeposition.Count; i++)
                {
                    Canvas.SetLeft(snakeposition[i], Canvas.GetLeft(snake[i]));
                    Canvas.SetTop(snakeposition[i], Canvas.GetTop(snake[i]));
                }

            #endregion

                #region///玩家蛇吃到食物

                foreach (Border food in back.Children)
                {
                    if (food.Tag.ToString() == "food")
                    {
                        double sx = Canvas.GetLeft(snake[0]) + snake[0].Width / 2;
                        double sy = Canvas.GetTop(snake[0]) + snake[0].Height / 2;
                        double fx = Canvas.GetLeft(food) + food.Width / 2;
                        double fy = Canvas.GetTop(food) + food.Height / 2;
                        if (Math.Sqrt((fx - sx) * (fx - sx) + (fy - sy) * (fy - sy)) <= snake[0].Width / 2 + 10)
                        {
                            DoubleAnimation leftAnimation = new DoubleAnimation(Canvas.GetLeft(food),
                                Canvas.GetLeft(snake[0]), new Duration(TimeSpan.FromMilliseconds(100)));
                            Storyboard.SetTarget(leftAnimation, food);
                            Storyboard.SetTargetProperty(leftAnimation, new PropertyPath("(Canvas.Left)"));
                            DoubleAnimation topAnimation = new DoubleAnimation(Canvas.GetTop(food),
                                Canvas.GetTop(snake[0]),
                                new Duration(TimeSpan.FromMilliseconds(100)));
                            Storyboard.SetTarget(topAnimation, food);
                            Storyboard.SetTargetProperty(topAnimation, new PropertyPath("(Canvas.Left)"));
                            stb.Children.Add(leftAnimation);
                            stb.Children.Add(leftAnimation);
                            stb.Begin();
                            food.Tag = "aover";
                            stb.Completed += stb_Completed;
                        }
                    }
                    if (food.Tag.ToString() == "isdie")
                    {
                        double sx = Canvas.GetLeft(snake[0]) + snake[0].Width / 2;
                        double sy = Canvas.GetTop(snake[0]) + snake[0].Height / 2;
                        double fx = Canvas.GetLeft(food) + food.Width / 2;
                        double fy = Canvas.GetTop(food) + food.Height / 2;
                        if (Math.Sqrt((fx - sx) * (fx - sx) + (fy - sy) * (fy - sy)) <= snake[0].Width / 2 + 10)
                        {
                            DoubleAnimation leftAnimation = new DoubleAnimation(Canvas.GetLeft(food),
                                Canvas.GetLeft(snake[0]), new Duration(TimeSpan.FromMilliseconds(100)));
                            Storyboard.SetTarget(leftAnimation, food);
                            Storyboard.SetTargetProperty(leftAnimation, new PropertyPath("(Canvas.Left)"));
                            DoubleAnimation topAnimation = new DoubleAnimation(Canvas.GetTop(food),
                                Canvas.GetTop(snake[0]),
                                new Duration(TimeSpan.FromMilliseconds(100)));
                            Storyboard.SetTarget(topAnimation, food);
                            Storyboard.SetTargetProperty(topAnimation, new PropertyPath("(Canvas.Left)"));
                            stb2.Children.Add(leftAnimation);
                            stb2.Children.Add(leftAnimation);
                            stb2.Begin();
                            food.Tag = "clear";
                            stb2.Completed += stb2_Completed;
                        }
                    }
                }

                #endregion

                #region///玩家蛇吃到10个小食物增长

                if (snake.Count < 15 && count >= 10)
                {
                    count = 0;
                    snakecount++;
                    AddSnakeBody(snake, snakeposition, "snake", "local", Colors.LightCoral);
                }
                if (snake.Count >= 15 && snake.Count < 30 && count >= 15)
                {
                    count = 0;
                    snakecount++;
                    AddSnakeBody(snake, snakeposition, "snake", "local", Colors.LightCoral);
                }
                if (snake.Count >= 30 && count >= 20)
                {
                    count = 0;
                    snakecount++;
                    AddSnakeBody(snake, snakeposition, "snake", "local", Colors.LightCoral);
                }

                #endregion

                #region///每增加5节身体变粗一个像素

                if (snakecount >= 5)
                {
                    snakecount = 0;
                    snakesize++;
                    for (int i = 0; i < snake.Count; i++)
                    {
                        snake[i].Width = snake[i].Height = snakesize;
                        snake[i].CornerRadius = new CornerRadius(snakesize);
                    }
                }
            }

                #endregion

        }
        void walkTimer_Tick(object sender, EventArgs e)
        {
            #region///单机蛇的移动

            if (isSnakeLife)
            {
                if (begin)
                {
                    Canvas.SetLeft(snList[0], Canvas.GetLeft(snList[0]) + runx * 10);
                    Canvas.SetTop(snList[0], Canvas.GetTop(snList[0]) + runy * 10);
                    for (int i = 1; i < snList.Count; i++)
                    {
                        Canvas.SetLeft(snList[i], Canvas.GetLeft(snPosition[i - 1]));
                        Canvas.SetTop(snList[i], Canvas.GetTop(snPosition[i - 1]));
                    }
                    for (int i = 0; i < snPosition.Count; i++)
                    {
                        Canvas.SetLeft(snPosition[i], Canvas.GetLeft(snList[i]));
                        Canvas.SetTop(snPosition[i], Canvas.GetTop(snList[i]));
                    }
                }

            #endregion

                #region///单机蛇吃食物

                foreach (Border food in back.Children)
                {
                    if (food.Tag.ToString() == "food")
                    {
                        double fx = Canvas.GetLeft(food) + food.Width / 2;
                        double fy = Canvas.GetTop(food) + food.Height / 2;

                        double snx = Canvas.GetLeft(snList[0]) + snList[0].Width / 2;
                        double sny = Canvas.GetTop(snList[0]) + snList[0].Height / 2;

                        if (Math.Sqrt((fx - snx) * (fx - snx) + (fy - sny) * (fy - sny)) <= snList[0].Width / 2 + 10)
                        {
                            DoubleAnimation leftAnimation = new DoubleAnimation(Canvas.GetLeft(food),
                                Canvas.GetLeft(snList[0]), new Duration(TimeSpan.FromMilliseconds(100)));
                            Storyboard.SetTarget(leftAnimation, food);
                            Storyboard.SetTargetProperty(leftAnimation, new PropertyPath("(Canvas.Left)"));
                            DoubleAnimation topAnimation = new DoubleAnimation(Canvas.GetTop(food),
                                Canvas.GetTop(snList[0]), new Duration(TimeSpan.FromMilliseconds(100)));
                            Storyboard.SetTarget(topAnimation, food);
                            Storyboard.SetTargetProperty(topAnimation, new PropertyPath("(Canvas.Left)"));
                            stb.Children.Add(leftAnimation);
                            stb.Children.Add(leftAnimation);
                            stb.Begin();
                            food.Tag = "bover";
                            stb.Completed += stb_Completed;
                        }
                    }
                    if (food.Tag.ToString() == "isdie")
                    {
                        double sx = Canvas.GetLeft(snList[0]) + snList[0].Width / 2;
                        double sy = Canvas.GetTop(snList[0]) + snList[0].Height / 2;
                        double fx = Canvas.GetLeft(food) + food.Width / 2;
                        double fy = Canvas.GetTop(food) + food.Height / 2;
                        if (Math.Sqrt((fx - sx) * (fx - sx) + (fy - sy) * (fy - sy)) <= snList[0].Width / 2 + 10)
                        {
                            DoubleAnimation leftAnimation = new DoubleAnimation(Canvas.GetLeft(food),
                                Canvas.GetLeft(snList[0]), new Duration(TimeSpan.FromMilliseconds(100)));
                            Storyboard.SetTarget(leftAnimation, food);
                            Storyboard.SetTargetProperty(leftAnimation, new PropertyPath("(Canvas.Left)"));
                            DoubleAnimation topAnimation = new DoubleAnimation(Canvas.GetTop(food),
                                Canvas.GetTop(snList[0]),
                                new Duration(TimeSpan.FromMilliseconds(100)));
                            Storyboard.SetTarget(topAnimation, food);
                            Storyboard.SetTargetProperty(topAnimation, new PropertyPath("(Canvas.Left)"));
                            stb2.Children.Add(leftAnimation);
                            stb2.Children.Add(leftAnimation);
                            stb2.Begin();
                            food.Tag = "clear";
                            stb2.Completed += stb2_Completed;
                        }
                    }
                }

                #endregion

                #region///单机蛇吃到食物增长

                if (snList.Count < 15 && fcount >= 10)
                {
                    fcount = 0;
                    snlistcount++;
                    AddSnakeBody(snList, snPosition, "small", "smallp", Colors.Peru);
                }
                if (snList.Count >= 15 && snList.Count < 30 && fcount >= 15)
                {
                    fcount = 0;
                    snlistcount++;
                    AddSnakeBody(snList, snPosition, "small", "smallp", Colors.Peru);
                }
                if (snList.Count >= 30 && fcount >= 20)
                {
                    fcount = 0;
                    snlistcount++;
                    AddSnakeBody(snList, snPosition, "small", "smallp", Colors.Peru);
                }

                #endregion

                #region///单击蛇每增加5节身体变粗一个像素

                if (snlistcount >= 5)
                {
                    snlistcount = 0;
                    snList[0].Width++;
                    for (int i = 0; i < snList.Count; i++)
                    {
                        snList[i].Width = snList[i].Height = snList[0].Width;
                        snList[i].CornerRadius = new CornerRadius(snList[0].Width);
                    }
                }
            }

                #endregion
        }
        private void AddSnakeBody(List<Border> esnake, List<Border> epoint, string tagname, string pointTag, Color col)///增加蛇身体长度
        {
            #region
            Border br = new Border();
            br.Tag = tagname;
            br.BorderThickness = new Thickness(2);
            br.BorderBrush = Brushes.Red;
            br.Width = br.Height = snakesize;
            br.CornerRadius = new CornerRadius(20);
            Canvas.SetTop(br, Canvas.GetTop(esnake[esnake.Count - 1]));
            Canvas.SetLeft(br, Canvas.GetLeft(esnake[esnake.Count - 1]));
            br.Background = new SolidColorBrush(col);
            back.Children.Add(br);
            esnake.Add(br);

            Border position = new Border();
            position.Tag = pointTag;
            Canvas.SetTop(position, Canvas.GetTop(esnake[esnake.Count - 1]));
            Canvas.SetLeft(position, Canvas.GetLeft(esnake[esnake.Count - 1]));
            back.Children.Add(position);
            epoint.Add(position);
            #endregion
        }
        void stb_Completed(object sender, EventArgs e)
        {
            #region///判断食物是被那条蛇吃了并添加相对应的数量
            stb.Children.Clear();
            foreach (Border food in back.Children)
            {
                if (food.Tag.ToString() == "aover")
                {
                    count++;
                    food.Tag = "food";
                    Canvas.SetTop(food, r.Next(600));
                    Canvas.SetLeft(food, r.Next(800));
                }
                if (food.Tag.ToString() == "bover")
                {
                    fcount++;
                    food.Tag = "food";
                    Canvas.SetTop(food, r.Next(600));
                    Canvas.SetLeft(food, r.Next(800));
                }
            }
            #endregion
        }
        void stb2_Completed(object sender, EventArgs e)
        {
            #region///判断死亡实体被另外的蛇吃掉
            stb2.Children.Clear();
            foreach (Border food in back.Children)
            {
                if (food.Tag.ToString() == "clear")
                {
                    food.Tag = "dis";
                    food.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            #endregion
        }
        private void SnakeDied(bool isDied, List<Border> brs, List<Border> brPosi)
        {
            #region///蛇的死亡效果
            if (isDied)
            {
                //SnakeDieMusic.Open(new Uri("GRAY2WEA.WAV", UriKind.Relative));//死亡音效
                //SnakeDieMusic.Play();
                for (int i = 0; i < brs.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Border br = new Border();
                        br.Width = br.Height = 8;
                        br.Tag = "isdie";
                        br.CornerRadius = new CornerRadius(10);
                        br.Background = Brushes.Red;
                        Canvas.SetLeft(br, r.Next(((int)Canvas.GetLeft(brs[i]) - 3), (int)Canvas.GetLeft(brs[i]) + (int)brs[i].Width + 3));
                        Canvas.SetTop(br, r.Next(((int)Canvas.GetTop(brs[i]) - 3), (int)Canvas.GetTop(brs[i]) + (int)brs[i].Width + 3));
                        back.Children.Add(br);
                    }
                }
                int snakeNum = brs.Count;
                for (int i = snakeNum - 1; i >= 0; i--)
                {
                    back.Children.Remove(brs[i]);
                    brs.Remove(brs[i]);
                    back.Children.Remove(brPosi[i]);
                    brPosi.Remove(brPosi[i]);
                }
            }
            #endregion
        }
        #region///实现虚拟摇杆
        double taggetx, taggety;///摇杆按钮应该到达的位置
        void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //获取鼠标的位置
            taggetx = e.GetPosition(back).X;
            taggety = e.GetPosition(back).Y;
            #region///大致限制touch按钮的范围
            if (taggetx > Canvas.GetLeft(touch) + touch.Width)
            {
                taggetx = Canvas.GetLeft(touch) + touch.Width;
            }
            if (taggetx < Canvas.GetLeft(touch))
            {
                taggetx = Canvas.GetLeft(touch);
            }
            if (taggety > Canvas.GetTop(touch) + touch.Width)
            {
                taggety = Canvas.GetTop(touch) + touch.Width;
            }
            if (taggety < Canvas.GetTop(touch))
            {
                taggety = Canvas.GetTop(touch);
            }
            if (taggetx < Canvas.GetLeft(touch) + touch.Width / 6 && taggety < Canvas.GetTop(touch) + touch.Width / 6)
            {
                taggetx = Canvas.GetLeft(touch) + touch.Width / 6;
                taggety = Canvas.GetTop(touch) + touch.Width / 6;
            }
            if (taggetx < Canvas.GetLeft(touch) + touch.Width / 6 && taggety > Canvas.GetTop(touch) + touch.Width - touch.Width / 6)
            {
                taggetx = Canvas.GetLeft(touch) + touch.Width / 6;
                taggety = Canvas.GetTop(touch) + touch.Width - touch.Width / 6;
            }
            if (taggetx > Canvas.GetLeft(touch) + touch.Width - touch.Width / 6 && taggety > Canvas.GetTop(touch) + touch.Width - touch.Width / 6)
            {
                taggetx = Canvas.GetLeft(touch) + touch.Width - touch.Width / 6;
                taggety = Canvas.GetTop(touch) + touch.Width - touch.Width / 6;
            }
            if (taggetx > Canvas.GetLeft(touch) + touch.Width - touch.Width / 6 && taggety < Canvas.GetTop(touch) + touch.Width / 6)
            {
                taggetx = Canvas.GetLeft(touch) + touch.Width - touch.Width / 6;
                taggety = Canvas.GetTop(touch) + touch.Width / 6;
            }
            #endregion
            #region///虚拟摇杆按钮跟随鼠标移动
            //判断如果是鼠标按下则可以拖动
            if (isOn)
            {
                Canvas.SetLeft(but, taggetx - but.Width / 2);
                Canvas.SetTop(but, taggety - but.Height / 2);
                begin = true;
                isSnakeLife = true;
                //计算按钮应该到达的位置
                double distance = Math.Sqrt((taggetx - tx) * (taggetx - tx) + (taggety - ty) * (taggety - ty));
                moveX = (taggetx - tx) / distance;
                moveY = (taggety - ty) / distance;

            }
            else
            {
                Canvas.SetTop(but, y);
                Canvas.SetLeft(but, x);
            }
            #endregion
        }
        void but_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isOn = false;
            //ss.Source = new Uri("E:\\GRAY2WEA.WAV",UriKind.Absolute);
            //ss.Play();
        }
        void but_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isOn = true;
        }
        #endregion

       

    }
}
