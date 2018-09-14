using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Media;
using System.Threading;

namespace 飞机大战
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        struct direction
        {
            public double x;
            public double y;
            public bool assign;//记录是否已经赋过方向
        }
        struct myplane
        {
            public long score;          //分数
            public int blood;     //血量
            public int power;      //子弹威力
            public int kill;        //绝杀数量

        }
        struct enemyplane2
        {
            public int blood;           //血量
            public double movedirection;   //移动方向
        }
        int bossshow = 45;                  //boss出现时间
        int cheat = 0;          //作弊码
        Image MyPlane = new Image();
        Image boss = new Image();
        Image plus = new Image();
        myplane mp = new myplane();
        enemyplane2 bossattribute = new enemyplane2();
        Image BombPicture = new Image();    //爆炸图片

        List<Image> ammo = new List<Image>();               //自己子弹
        List<Image> killingPlane = new List<Image>();       //绝杀无人机

        List<Image> enemy = new List<Image>();              //敌机1(冲撞)
        List<direction> enemyp = new List<direction>();     //对应敌机的移动方向

        List<Image> enemy2 = new List<Image>();             //敌机2(发射火箭、左右移动)
        List<enemyplane2> enemy2_attribute = new List<enemyplane2>();     //敌机2的血量记录
        List<Image> enemy2_ammo = new List<Image>();         //敌机2的子弹

        List<Image> enemy3 = new List<Image>();             //敌机3(飞入、静止、射击、飞出)
        List<Image> enemy3_ammo = new List<Image>();        //敌机3的子弹
        List<direction> enemy3_ammoDirection = new List<direction>();//子弹的移动方向

        List<Image> enemy4 = new List<Image>();             //敌机4(从上进入，不消失，左右移动并发射子弹)
        List<enemyplane2> enemy4_attribute = new List<enemyplane2>();//敌机4的血量记录
        List<Image> enemy4_ammo = new List<Image>();        //敌机4的子弹

        List<Image> bossrocket = new List<Image>();          //boss火箭
        List<Image> bossammo = new List<Image>();           //boss子弹

        DispatcherTimer Fire = new DispatcherTimer();       //子弹移动
        DispatcherTimer ControlFire = new DispatcherTimer();//控制发射子弹

        DispatcherTimer MakeEnemy = new DispatcherTimer();  //制作敌机1
        DispatcherTimer ControlEnemy = new DispatcherTimer();//控制敌机1的移动

        DispatcherTimer MakeEnemy2 = new DispatcherTimer(); //制作敌机2
        DispatcherTimer ControlEnemy2 = new DispatcherTimer();//敌机2的移动
        DispatcherTimer Enemy2Fire = new DispatcherTimer();     // 敌机2子弹的制作
        DispatcherTimer Enemy2AmmoMove = new DispatcherTimer(); //敌机2子弹的移动

        DispatcherTimer MakeEnemy3 = new DispatcherTimer();     //制作敌机3 
        DispatcherTimer ControlEnemy3 = new DispatcherTimer();  //敌机3的移动
        DispatcherTimer Enemy3AmmoMove = new DispatcherTimer(); //敌机3子弹的移动

        DispatcherTimer MakeEnemy4 = new DispatcherTimer();     //制作敌机4
        DispatcherTimer ControlEnemy4 = new DispatcherTimer();  //敌机4的移动
        DispatcherTimer Enemy4Fire = new DispatcherTimer(); //敌机4子弹的制作
        DispatcherTimer Enemy4AmmoMove = new DispatcherTimer();//敌机4 子弹的移动

        DispatcherTimer JudgeCrash = new DispatcherTimer();  //判断子弹碰撞
        DispatcherTimer JudgeBlood = new DispatcherTimer();  //控制血条的显示

        DispatcherTimer BombEffect = new DispatcherTimer();  //爆炸效果
        DispatcherTimer MyBombEffect = new DispatcherTimer();//自己爆炸效果
        DispatcherTimer Boss = new DispatcherTimer();       //计时boss
        DispatcherTimer BossMove = new DispatcherTimer();   //boss移动
        DispatcherTimer BossRocket = new DispatcherTimer();//boss火箭制作
        DispatcherTimer BossAmmo = new DispatcherTimer();   //boss子弹制作
        DispatcherTimer BossAmmoMove = new DispatcherTimer();//boss子弹的移动
        DispatcherTimer rocketfire = new DispatcherTimer();         //控制火箭的移动
        DispatcherTimer PlusMove = new DispatcherTimer();   //奖励的移动

        DispatcherTimer Remove = new DispatcherTimer();     //删除子弹
        DispatcherTimer ToKilling = new DispatcherTimer();    //绝杀无人机制造
        DispatcherTimer KillingPM = new DispatcherTimer();      //无人机移动
        DispatcherTimer War = new DispatcherTimer();        //提示闪烁
        DispatcherTimer CGdonghua = new DispatcherTimer();  //CG动画
        DispatcherTimer Hitted = new DispatcherTimer();     //被击中

        SoundPlayer EnemyDestroy = new SoundPlayer();       //爆炸音效
        MediaPlayer MyPlaneFire = new MediaPlayer();        //射击音效
        MediaPlayer BgMusic = new MediaPlayer();            //背景音乐
        MediaPlayer MyBomb = new MediaPlayer();             //自己爆炸音效
        MediaPlayer Killing = new MediaPlayer();            //绝杀音效
        MediaPlayer Warning = new MediaPlayer();            //boss提示
        MediaPlayer BossMusic = new MediaPlayer();          //boss音效
        MediaPlayer GameOn = new MediaPlayer();             //进入游戏时音效
        MediaPlayer GameOn2 = new MediaPlayer();
        MediaPlayer Get = new MediaPlayer();                //吃到道具的声音
        MediaPlayer Select = new MediaPlayer();             //选择后的音效
        Random r = new Random();
        Storyboard b = new Storyboard();
        int bosstime = 0;               //boss进入时间
        int AmmoType = 1;               //子弹类型


        Image Ui = new Image();         //进入游戏UI
        Image CG1 = new Image();        //CG
        Image CG2 = new Image();
        Image CG3 = new Image();
        public MainWindow()
        {
            InitializeComponent();
            DoubleAnimation move = new DoubleAnimation(0, 700, new Duration(new TimeSpan(0, 0, 6)));
            move.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(move, back);
            Storyboard.SetTargetProperty(move, new PropertyPath("(Canvas.Top)"));

            DoubleAnimation move1 = new DoubleAnimation(-691, 0, new Duration(new TimeSpan(0, 0, 6)));
            move1.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(move1, back1);
            Storyboard.SetTargetProperty(move1, new PropertyPath("(Canvas.Top)"));

            b.Children.Add(move);
            b.Children.Add(move1);


            MyPlane.Width = 70; MyPlane.Height = 80;
            MyPlane.Stretch = Stretch.Fill;
            Canvas.SetTop(MyPlane, 500);
            Canvas.SetLeft(MyPlane, ca.Width / 2 - MyPlane.Width / 2);
            MyPlane.Source = new BitmapImage(new Uri("Images/plane/plane1.png", UriKind.Relative));
            Canvas.SetZIndex(MyPlane, 5);
            MyPlane.MouseLeftButtonDown += new MouseButtonEventHandler(MyPlane_MouseLeftButtonDown);
            MyPlane.MouseLeftButtonUp += new MouseButtonEventHandler(MyPlane_MouseLeftButtonUp);
            MyPlane.MouseRightButtonDown += new MouseButtonEventHandler(MyPlane_MouseRightButtonDown);
            ca.Children.Add(MyPlane);

            //boss的赋值
            boss.Width = 360;
            boss.Height = 200;
            boss.Source = new BitmapImage(new Uri("Images/Enemy/Boss.png", UriKind.Relative));
            boss.Stretch = Stretch.Fill;
            boss.Tag = "weitingzhi";
            Canvas.SetZIndex(boss, 3);
            bossattribute.blood = 3000;
            bossattribute.movedirection = 1;

            //myplane结构体的赋值
            mp.blood = 100;
            mp.kill = 3;
            mp.power = 10;
            mp.score = 0;

            //附加奖励的赋值
            plus.Width = plus.Height = 40;
            plus.Stretch = Stretch.Fill;
            plus.Tag = 0;
            Canvas.SetZIndex(plus, 4);

            Warn.Opacity = 0;

            BgMusic.Open(new Uri(System.Environment.CurrentDirectory + "\\Sound\\BackgroundMusic.mp3"));
            BgMusic.Volume = 100;
            BgMusic.MediaEnded += new EventHandler(BgMusic_MediaEnded);

            EnemyDestroy.SoundLocation = System.Environment.CurrentDirectory + "\\Sound\\Bomb.wav";
            string str = System.Environment.CurrentDirectory + "\\Sound\\Ammo.mp3";
            MyPlaneFire.Open(new Uri(str));
            MyBomb.Open(new Uri(System.Environment.CurrentDirectory + "\\Sound\\MyBomb.wav"));
            Killing.Open(new Uri(System.Environment.CurrentDirectory + "\\Sound\\Killing.mp3"));
            Warning.Open(new Uri(System.Environment.CurrentDirectory + "\\Sound\\Warning.mp3"));
            BossMusic.Open(new Uri(System.Environment.CurrentDirectory + "\\Sound\\BossMusic.mp3"));
            GameOn.Open(new Uri(System.Environment.CurrentDirectory + "\\Sound\\GameOn.wav"));
            GameOn.Play();
            GameOn2.Open(new Uri(System.Environment.CurrentDirectory + "\\Sound\\GameOn.mp3"));
            GameOn.MediaEnded += new EventHandler(GameOn_MediaEnded);
            Get.Open(new Uri(System.Environment.CurrentDirectory + "\\Sound\\Get.mp3"));
            Get.MediaEnded += new EventHandler(Get_MediaEnded);
            Select.Open(new Uri(System.Environment.CurrentDirectory + "\\Sound\\Select.mp3"));
            Ui.Source = new BitmapImage(new Uri("Images/UI/UI.jpg", UriKind.Relative));
            Ui.Width = back.Width;
            Ui.Height = back.Height;
            Ui.Stretch = Stretch.Fill;
            Canvas.SetZIndex(Ui, 10);
            ca.Children.Add(Ui);

            CG1.Source = new BitmapImage(new Uri("Images/UI/CG1.png", UriKind.Relative));
            CG1.Width = back.Width;
            CG1.Height = back.Height;
            CG1.Stretch = Stretch.Fill;
            Canvas.SetZIndex(CG1, 9);
            ca.Children.Add(CG1);
            CG1.MouseDown += new MouseButtonEventHandler(CG1_MouseDown);
            CG2.Source = new BitmapImage(new Uri("Images/UI/CG2.png", UriKind.Relative));
            CG2.Width = back.Width;
            CG2.Height = back.Height;
            CG2.Stretch = Stretch.Fill;
            Canvas.SetZIndex(CG2, 8);
            ca.Children.Add(CG2);
            CG2.MouseDown += new MouseButtonEventHandler(CG2_MouseDown);
            CG3.Source = new BitmapImage(new Uri("Images/UI/CG3.png", UriKind.Relative));
            CG3.Width = back.Width;
            CG3.Height = back.Height;
            CG3.Stretch = Stretch.Fill;
            Canvas.SetZIndex(CG3, 7);
            ca.Children.Add(CG3);
            CG3.MouseDown += new MouseButtonEventHandler(CG3_MouseDown);
            CGdonghua.Tag = Ui;
            start.MouseEnter += new MouseEventHandler(start_MouseEnter);
            start.MouseDown += new MouseButtonEventHandler(start_MouseDown);
            start.MouseLeave += new MouseEventHandler(start_MouseLeave);
            Fire.Interval = TimeSpan.FromMilliseconds(1);
            Fire.Tick += new EventHandler(Fire_Tick);
            Fire.Start();

            ControlFire.Interval = TimeSpan.FromMilliseconds(100);//子弹发射速度
            ControlFire.Tick += new EventHandler(ControlFire_Tick);
            ControlFire.Start();

            MakeEnemy.Interval = TimeSpan.FromMilliseconds(2000);//敌机1产生
            MakeEnemy.Tick += new EventHandler(MakeEnemy_Tick);


            ControlEnemy.Interval = TimeSpan.FromMilliseconds(1);//敌机1的移动
            ControlEnemy.Tick += new EventHandler(ControlEnemy_Tick);
            ControlEnemy.Start();


            MakeEnemy2.Interval = TimeSpan.FromSeconds(5);//敌机2的产生
            MakeEnemy2.Tick += new EventHandler(MakeEnemy2_Tick);


            ControlEnemy2.Interval = TimeSpan.FromMilliseconds(1);//敌机2的移动
            ControlEnemy2.Tick += new EventHandler(ControlEnemy2_Tick);
            ControlEnemy2.Start();

            Enemy2Fire.Interval = TimeSpan.FromMilliseconds(800);//敌机2子弹的制作
            Enemy2Fire.Tick += new EventHandler(Enemy2Fire_Tick);
            Enemy2Fire.Start();

            Enemy2AmmoMove.Interval = TimeSpan.FromMilliseconds(1);//敌机2子弹的移动
            Enemy2AmmoMove.Tick += new EventHandler(Enemy2AmmoMove_Tick);
            Enemy2AmmoMove.Start();

            MakeEnemy3.Interval = TimeSpan.FromMilliseconds(800);     //敌机3的制作
            MakeEnemy3.Tick += new EventHandler(MakeEnemy3_Tick);

            ControlEnemy3.Interval = TimeSpan.FromMilliseconds(1);  //敌机3的移动
            ControlEnemy3.Tick += new EventHandler(ControlEnemy3_Tick);
            ControlEnemy3.Start();

            Enemy3AmmoMove.Interval = TimeSpan.FromMilliseconds(1);     //敌机3子弹的移动
            Enemy3AmmoMove.Tick += new EventHandler(Enemy3AmmoMove_Tick);
            Enemy3AmmoMove.Start();

            MakeEnemy4.Interval = TimeSpan.FromSeconds(11);      //敌机4的制作
            MakeEnemy4.Tick += new EventHandler(MakeEnemy4_Tick);



            ControlEnemy4.Interval = TimeSpan.FromMilliseconds(1);  //敌机4的移动
            ControlEnemy4.Tick += new EventHandler(ControlEnemy4_Tick);
            ControlEnemy4.Start();

            Enemy4Fire.Interval = TimeSpan.FromMilliseconds(700);      //敌机4子弹的制作
            Enemy4Fire.Tick += new EventHandler(Enemy4Fire_Tick);
            Enemy4Fire.Start();

            Enemy4AmmoMove.Interval = TimeSpan.FromMilliseconds(1);     //敌机4子弹的移动 
            Enemy4AmmoMove.Tick += new EventHandler(Enemy4AmmoMove_Tick);

            Boss.Interval = TimeSpan.FromSeconds(1);            //boss计时
            Boss.Tick += new EventHandler(Boss_Tick);


            BossMove.Interval = TimeSpan.FromMilliseconds(1);   //boss的移动
            BossMove.Tick += new EventHandler(BossMove_Tick);

            BossRocket.Interval = TimeSpan.FromMilliseconds(1700);   //boss火箭
            BossRocket.Tick += new EventHandler(BossRocket_Tick);

            rocketfire.Interval = TimeSpan.FromMilliseconds(1);     //boss火箭的移动
            rocketfire.Tick += new EventHandler(rocketfire_Tick);

            BossAmmo.Interval = TimeSpan.FromMilliseconds(600);     //boss子弹的制作
            BossAmmo.Tick += new EventHandler(BossAmmo_Tick);

            BossAmmoMove.Interval = TimeSpan.FromMilliseconds(1);   //Boss子弹的移动
            BossAmmoMove.Tick += new EventHandler(BossAmmoMove_Tick);

            JudgeCrash.Interval = TimeSpan.FromMilliseconds(1);//子弹的碰撞
            JudgeCrash.Tick += new EventHandler(JudgeCrash_Tick);
            JudgeCrash.Start();

            JudgeBlood.Interval = TimeSpan.FromMilliseconds(1);//血条、分数的控制
            JudgeBlood.Tick += new EventHandler(JudgeBlood_Tick);
            JudgeBlood.Start();

            BombEffect.Interval = TimeSpan.FromMilliseconds(1);  //爆炸效果
            BombEffect.Tick += new EventHandler(BombEffect_Tick);

            MyBombEffect.Interval = TimeSpan.FromMilliseconds(30);//自己飞机消失
            MyBombEffect.Tick += new EventHandler(MyBombEffect_Tick);

            PlusMove.Interval = TimeSpan.FromMilliseconds(1);       //plus的移动
            PlusMove.Tick += new EventHandler(PlusMove_Tick);
            PlusMove.Start();

            Remove.Interval = TimeSpan.FromMilliseconds(1);         //清除
            Remove.Tick += new EventHandler(Remove_Tick);

            ToKilling.Interval = TimeSpan.FromMilliseconds(1);      //绝杀无人机制造
            ToKilling.Tick += new EventHandler(ToKilling_Tick);

            KillingPM.Interval = TimeSpan.FromMilliseconds(1);      //无人机移动
            KillingPM.Tick += new EventHandler(KillingPM_Tick);
            KillingPM.Start();

            War.Interval = TimeSpan.FromMilliseconds(1);            //警告闪烁
            War.Tick += new EventHandler(War_Tick);

            CGdonghua.Interval = TimeSpan.FromMilliseconds(1);      //CG动画
            CGdonghua.Tick += new EventHandler(CGdonghua_Tick);

            Hitted.Interval = TimeSpan.FromMilliseconds(100);       //被击中动画
            Hitted.Tick += new EventHandler(Hitted_Tick);
            Hitted.Start();
        }
        bool ishit = false;
        void Hitted_Tick(object sender, EventArgs e)
        {
            if (ishit == false)
            {
                Canvas.SetZIndex(hitted, -2);
            }
            if (ishit == true)
            {
                Canvas.SetZIndex(hitted, 5);
                ishit = false;
            }
        }

        void Get_MediaEnded(object sender, EventArgs e)
        {
            Get.Position = TimeSpan.Zero;
            Get.Stop();
        }

        void GameOn_MediaEnded(object sender, EventArgs e)      //未点击进入游戏前的重复的音效
        {
            GameOn.Position = TimeSpan.Zero;
            GameOn.Play();
        }

        void start_MouseLeave(object sender, MouseEventArgs e)
        {
            start.Source = new BitmapImage(new Uri("Images/UI/UI_start.png", UriKind.Relative));
        }

        void start_MouseEnter(object sender, MouseEventArgs e)
        {
            start.Source = new BitmapImage(new Uri("Images/UI/UI_start_on.png", UriKind.Relative));
        }

        void CG3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.None;
            CGdonghua.Tag = CG3;
            CGdonghua.Start();
            MakeEnemy.Start();
            MakeEnemy2.Start();
            MakeEnemy3.Start();
            MakeEnemy4.Start();
            Boss.Start();
            b.Begin();
            GameOn2.Stop();
            BgMusic.Play();
            isstart = true;
        }

        void CG2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CGdonghua.Tag = CG2;
            CGdonghua.Start();
        }

        void CG1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CGdonghua.Tag = CG1;
            CGdonghua.Start();
        }

        void CGdonghua_Tick(object sender, EventArgs e)
        {
            DispatcherTimer u = (DispatcherTimer)sender;
            Image ui = (Image)u.Tag;
            ui.Opacity -= 0.02;
            if (ui.Opacity <= 0)
            {
                ca.Children.Remove(ui);
                CGdonghua.Stop();
            }

        }
        void start_MouseDown(object sender, MouseButtonEventArgs e)     //开始游戏
        {
            GameOn.Stop();
            Select.Play();
            ca.Children.Remove(start);
            GameOn.MediaEnded -= new EventHandler(GameOn_MediaEnded);
            GameOn2.Play();
            GameOn2.MediaEnded += new EventHandler(GameOn2_MediaEnded);
            CGdonghua.Start();
        }

        void GameOn2_MediaEnded(object sender, EventArgs e)
        {
            GameOn2.Position = TimeSpan.Zero;
            GameOn2.Play();
        }

        void KillingPM_Tick(object sender, EventArgs e)     //无人机移动
        {
            foreach (Image it in killingPlane)
            {
                Canvas.SetTop(it, Canvas.GetTop(it) - 25);
                if (Canvas.GetTop(it) < -it.Height)
                {
                    ca.Children.Remove(it);
                    killingPlane.Remove(it);
                    break;
                }
                ZhuangJiZidan(it, enemy2_ammo);
                for (int p = 0; p < 100; p++)
                {
                    foreach (Image a in enemy3_ammo)
                    {
                        if (Canvas.GetTop(it) < Canvas.GetTop(a))
                        {
                            ca.Children.Remove(a);
                            enemy3_ammo.Remove(a);
                            for (int i = 0; i < enemy3_ammoDirection.Count; i++)
                            {
                                enemy3_ammoDirection.RemoveAt(i);
                            }
                            break;
                        }
                    }
                }
                ZhuangJiZidan(it, enemy4_ammo);
                ZhuangJiZidan(it, bossammo);
                ZhuangJiZidan(it, bossrocket);

            }

            DijiSiwang(killingPlane, 20, killingPlane);        //无人机当做子弹
        }

        private void ZhuangJiZidan(Image it, List<Image> x)
        {
            foreach (Image a in x)
            {
                if (Canvas.GetTop(it) < Canvas.GetTop(a))
                {
                    ca.Children.Remove(a);
                    x.Remove(a);
                    break;
                }
            }
        }
        //用于判断子弹和无人机的撞击
        private void DijiSiwang(List<Image> x, int power, List<Image> po)
        {
            bool isremove = false;
            foreach (Image bullet in x)  //打到enemy
            {
                foreach (Image ene in enemy)
                {
                    if (Canvas.GetTop(bullet) <= Canvas.GetTop(ene) + ene.Height && Canvas.GetTop(bullet) >= Canvas.GetTop(ene)
                        && (Canvas.GetLeft(bullet) + bullet.Width >= Canvas.GetLeft(ene) && Canvas.GetLeft(bullet) <= Canvas.GetLeft(ene) + ene.Width))
                    {
                        BombE(ene);
                        EnemyDestoryMusic();
                        mp.score += 200;
                        int i = enemy.IndexOf(ene);
                        double eneX = Canvas.GetLeft(ene);
                        double eneY = Canvas.GetTop(ene);
                        ca.Children.Remove(bullet);
                        po.Remove(bullet);
                        enemy.Remove(ene);
                        enemyp.Remove(enemyp[i]);
                        ca.Children.Remove(ene);
                        isremove = true;
                        //奖励的产生
                        int mid = r.Next(1, 8);
                        if (mid == 3)             //产生奖励的概率
                        {
                            isget = false;
                            int mid2 = r.Next(1, 5);
                            if (mid2 == 1)     //1  治疗
                            {
                                plus.Source = new BitmapImage(new Uri("Images/Plus/Cure.png", UriKind.Relative));
                                plus.Tag = 1;
                            }
                            if (mid2 == 2)         //绝杀数量
                            {
                                plus.Source = new BitmapImage(new Uri("Images/Plus/Killing.png", UriKind.Relative));
                                plus.Tag = 2;
                            }
                            if (mid2 == 3)         //最大子弹威力
                            {
                                plus.Source = new BitmapImage(new Uri("Images/Plus/MaxPower.png", UriKind.Relative));
                                plus.Tag = 3;
                            }
                            if (mid2 == 4)         //增加子弹威力
                            {
                                plus.Source = new BitmapImage(new Uri("Images/Plus/PowerUp.png", UriKind.Relative));
                                plus.Tag = 4;
                            }
                            Canvas.SetLeft(plus, eneX);
                            if (eneY < 0)
                                Canvas.SetTop(plus, 5);
                            else
                                Canvas.SetTop(plus, eneY);
                            try
                            {
                                ca.Children.Add(plus);
                            }
                            catch (Exception)
                            {

                            }
                        }
                        break;
                    }
                }
                if (isremove)
                {
                    isremove = false;
                    break;
                }
            }

            bool isremove2 = false;
            foreach (Image a in x)      //打到enemy2
            {
                foreach (Image ene2 in enemy2)
                {
                    if (Canvas.GetTop(a) <= Canvas.GetTop(ene2) + ene2.Height && Canvas.GetTop(a) >= Canvas.GetTop(ene2)
                        && (Canvas.GetLeft(a) + a.Width >= Canvas.GetLeft(ene2) && Canvas.GetLeft(a) <= Canvas.GetLeft(ene2) + ene2.Width))
                    {
                        ca.Children.Remove(a);
                        po.Remove(a);
                        int i = enemy2.IndexOf(ene2);
                        enemyplane2 mid = new enemyplane2();
                        mid.movedirection = enemy2_attribute[i].movedirection;
                        mid.blood = enemy2_attribute[i].blood - power;
                        enemy2_attribute[i] = new enemyplane2();
                        enemy2_attribute[i] = mid;
                        isremove2 = true;
                        break;
                    }
                }
                if (isremove2)
                {
                    isremove2 = false;
                    break;
                }
            }

            foreach (enemyplane2 ene2 in enemy2_attribute)      //敌机2爆炸
            {
                if (ene2.blood <= 0)
                {
                    int i = enemy2_attribute.IndexOf(ene2);
                    BombE(enemy2[i]);
                    ca.Children.Remove(enemy2[i]);
                    enemy2.Remove(enemy2[i]);
                    EnemyDestoryMusic();
                    enemy2_attribute.Remove(ene2);
                    mp.score += 400;
                    break;
                }
            }
            bool isremove3 = false;
            foreach (Image a in x)              //打到enemy3
            {
                foreach (Image ene in enemy3)
                {
                    if (Canvas.GetTop(a) <= Canvas.GetTop(ene) + ene.Height && Canvas.GetTop(a) >= Canvas.GetTop(ene)
                        && (Canvas.GetLeft(a) + a.Width >= Canvas.GetLeft(ene) && Canvas.GetLeft(a) <= Canvas.GetLeft(ene) + ene.Width))
                    {
                        BombE(ene);
                        EnemyDestoryMusic();
                        mp.score += 100;
                        int i = enemy3.IndexOf(ene);
                        ca.Children.Remove(a);
                        po.Remove(a);
                        enemy3.Remove(ene);
                        ca.Children.Remove(ene);
                        isremove3 = true;
                        break;
                    }
                }
                if (isremove3)
                {
                    isremove3 = false;
                    break;
                }
            }

            bool isremove4 = false;
            foreach (Image a in x)      //打到enemy4
            {
                foreach (Image ene4 in enemy4)
                {
                    if (Canvas.GetTop(a) <= Canvas.GetTop(ene4) + 30 && Canvas.GetTop(a) >= Canvas.GetTop(ene4)
                        && (Canvas.GetLeft(a) + a.Width >= Canvas.GetLeft(ene4) && Canvas.GetLeft(a) <= Canvas.GetLeft(ene4) + ene4.Width))
                    {
                        ca.Children.Remove(a);
                        po.Remove(a);
                        int i = enemy4.IndexOf(ene4);
                        enemyplane2 mid = new enemyplane2();
                        mid.movedirection = enemy4_attribute[i].movedirection;
                        mid.blood = enemy4_attribute[i].blood - power;
                        enemy4_attribute[i] = new enemyplane2();
                        enemy4_attribute[i] = mid;
                        isremove4 = true;
                        break;
                    }
                }
                if (isremove4)
                {
                    isremove4 = false;
                    break;
                }
            }

            foreach (enemyplane2 ene4 in enemy4_attribute)      //敌机4爆炸
            {
                if (ene4.blood <= 0)
                {
                    int i = enemy4_attribute.IndexOf(ene4);
                    BombE(enemy4[i]);
                    ca.Children.Remove(enemy4[i]);
                    enemy4.Remove(enemy4[i]);
                    EnemyDestoryMusic();
                    enemy4_attribute.Remove(ene4);
                    mp.score += 1000;
                    break;
                }
            }
            foreach (Image bullet in x)      //打到Boss
            {
                if (boss.Tag.ToString() == "tingzhi" && isbossover == false &&
                    Canvas.GetTop(bullet) <= Canvas.GetTop(boss) + boss.Height / 2 && Canvas.GetTop(bullet) >= Canvas.GetTop(boss)
                    && (Canvas.GetLeft(bullet) >= Canvas.GetLeft(boss) + bullet.Width && Canvas.GetLeft(bullet) <= Canvas.GetLeft(boss) + boss.Width - bullet.Width))
                {
                    ca.Children.Remove(bullet);
                    po.Remove(bullet);
                    bossattribute.blood -= power;
                    if (bossattribute.blood <= 0)
                    {
                        ca.Children.Remove(boss);
                        isbossover = true;
                        MyBomb.Play();
                        BombE(boss);
                        BossAmmo.Stop();
                        BossAmmoMove.Stop();
                        BossRocket.Stop();
                        rocketfire.Stop();
                        BossMove.Stop();
                        Remove.Start();
                        Label succeed = new Label();
                        succeed.Foreground = Brushes.Red;
                        succeed.FontSize = 20;
                        Canvas.SetTop(succeed, ca.Height / 4);
                        Canvas.SetLeft(succeed, 140);
                        succeed.Content = "MISSION ACCOMPLISHED ";
                        ca.Children.Add(succeed);
                        MessageBox.Show("请等待后续情节");

                    }
                    break;
                }
            }
        }
        int times = 0;
        void ToKilling_Tick(object sender, EventArgs e)     //绝杀无人机制造
        {
            Image k = new Image();
            k.Width = 50;
            k.Height = 70;
            k.Source = new BitmapImage(new Uri("Images/Plane/Killing.png", UriKind.Relative));
            k.Stretch = Stretch.Fill;
            ca.Children.Add(k);
            killingPlane.Add(k);
            Canvas.SetTop(k, ca.Height);
            Canvas.SetLeft(k, r.Next((int)(ca.Width - k.Width)));
            Canvas.SetZIndex(k, 4);
            times++;
            if (times >= 25)
                ToKilling.Stop();
        }

        void MyPlane_MouseRightButtonDown(object sender, MouseButtonEventArgs e)            //开启绝杀
        {
            if (mp.kill > 0)
            {
                mp.kill--;
                times = 0;
                ToKilling.Start();
                Killing.Play();
                Killing.MediaEnded += new EventHandler(Killing_MediaEnded);
            }
        }

        void Killing_MediaEnded(object sender, EventArgs e)
        {
            Killing.Position = TimeSpan.Zero;
            Killing.Stop();
        }

        void BossAmmoMove_Tick(object sender, EventArgs e)          //boss子弹的移动
        {
            for (int i = 0; i < bossammo.Count; i++)
            {
                if (i % 6 == 0)     //左数第一颗子弹
                {
                    Canvas.SetLeft(bossammo[i], Canvas.GetLeft(bossammo[i]) - 5);
                }
                if (i % 6 == 1)     //第二颗子弹
                {
                    Canvas.SetLeft(bossammo[i], Canvas.GetLeft(bossammo[i]) - 3);
                }
                if (i % 6 == 2)     //第三颗子弹
                {
                    Canvas.SetLeft(bossammo[i], Canvas.GetLeft(bossammo[i]) - 1);
                }
                if (i % 6 == 3)     //第四颗子弹
                {
                    Canvas.SetLeft(bossammo[i], Canvas.GetLeft(bossammo[i]) + 1);
                }
                if (i % 6 == 4)     //第五颗子弹
                {
                    Canvas.SetLeft(bossammo[i], Canvas.GetLeft(bossammo[i]) + 3);
                }
                if (i % 6 == 5)     //第六颗子弹
                {
                    Canvas.SetLeft(bossammo[i], Canvas.GetLeft(bossammo[i]) + 5);
                }
                Canvas.SetTop(bossammo[i], Canvas.GetTop(bossammo[i]) + 6);

                foreach (Image am in bossammo)           //子弹碰到自己
                {
                    if (Canvas.GetTop(am) > Canvas.GetTop(MyPlane) && Canvas.GetTop(am) < Canvas.GetTop(MyPlane) + MyPlane.Height
                        && Canvas.GetLeft(am) + am.Width > Canvas.GetLeft(MyPlane) && Canvas.GetLeft(am) + am.Width < Canvas.GetLeft(MyPlane) + MyPlane.Width)
                    {
                        Image newammo = new Image();
                        newammo.Width = newammo.Height = am.Height;
                        newammo.Source = am.Source;
                        int x = bossammo.IndexOf(am);
                        ca.Children.Remove(am);
                        bossammo.Remove(am);
                        bossammo.Insert(x, newammo);
                        mp.blood -= 10;
                        ishit = true;
                        break;
                    }
                }
            }
            foreach (Image am in bossammo)
            {
                if (bossammo.IndexOf(am) % 6 == 5 && Canvas.GetTop(am) >= ca.Height)       //子弹超出范围
                {
                    for (int j = 5; j >= 0; j--)
                    {
                        ca.Children.Remove(bossammo[j]);
                        bossammo.Remove(bossammo[j]);
                    }
                    break;
                }
            }
        }

        void BossAmmo_Tick(object sender, EventArgs e)      //boss子弹的制作
        {
            if (boss.Tag.ToString() == "tingzhi")
            {
                for (int i = 0; i < 6; i++)
                {
                    Image am = new Image();
                    am.Width = am.Height = 20;
                    am.Source = new BitmapImage(new Uri("Images/Bullet/BossAmmo.png", UriKind.Relative));
                    am.Stretch = Stretch.Fill;
                    Canvas.SetTop(am, Canvas.GetTop(boss) + boss.Height - 30);
                    Canvas.SetLeft(am, Canvas.GetLeft(boss) + boss.Width / 2);
                    ca.Children.Add(am);
                    bossammo.Add(am);
                    BossAmmoMove.Start();
                }
            }
        }

        void BossRocket_Tick(object sender, EventArgs e)    //boss火箭
        {
            Image ro = new Image();
            ro.Width = 25;
            ro.Height = 100;
            ro.Source = new BitmapImage(new Uri("Images/Bullet/BossAmmo2.png", UriKind.Relative));
            ro.Stretch = Stretch.Fill;
            Canvas.SetTop(ro, Canvas.GetTop(boss) + boss.Height - 40);
            Canvas.SetLeft(ro, Canvas.GetLeft(boss) + boss.Width / 2 - ro.Width / 2);
            bossrocket.Add(ro);
            ca.Children.Add(ro);

        }
        double speed = 0;
        void rocketfire_Tick(object sender, EventArgs e)        //火箭的移动
        {
            for (int i = 0; i < bossrocket.Count; i++)
            {
                Canvas.SetTop(bossrocket[i], Canvas.GetTop(bossrocket[i]) + speed);
                speed += 0.3;

                if (Canvas.GetTop(bossrocket[i]) >= ca.Height + bossrocket[i].Height)
                {
                    ca.Children.Remove(bossrocket[i]);
                    bossrocket.Remove(bossrocket[i]);
                }
            }
            foreach (Image am in bossrocket)
            {
                if (Canvas.GetTop(am) > Canvas.GetTop(MyPlane) && Canvas.GetTop(am) < Canvas.GetTop(MyPlane) + MyPlane.Height
                   && Canvas.GetLeft(am) > Canvas.GetLeft(MyPlane) + am.Width / 3 && Canvas.GetLeft(am) < Canvas.GetLeft(MyPlane) + MyPlane.Width - am.Width / 3)
                {
                    ca.Children.Remove(am);
                    bossrocket.Remove(am);
                    mp.blood -= 20;
                    ishit = true;
                    break;
                }
            }
        }

        void BossMove_Tick(object sender, EventArgs e)      //boss的移动
        {
            if (Canvas.GetTop(boss) < 0)
                Canvas.SetTop(boss, Canvas.GetTop(boss) + 1);
            if (Canvas.GetTop(boss) >= 0)
                boss.Tag = "tingzhi";
            if (boss.Tag.ToString() == "tingzhi")
            {
                BossAmmo.Start();
                Canvas.SetLeft(boss, Canvas.GetLeft(boss) + bossattribute.movedirection);

                if (Canvas.GetLeft(boss) <= -boss.Width / 2)                 //   移动到右边发射火箭        
                {
                    bossattribute.movedirection = 1;
                    speed = 0;
                    BossRocket.Stop();
                    rocketfire.Start();

                }
                if (Canvas.GetLeft(boss) >= ca.Width - boss.Width / 2)          //移动到右边开始制作火箭
                {
                    bossattribute.movedirection = -1;
                    rocketfire.Stop();
                    BossRocket.Start();
                }

            }
        }

        double opa = 0.04;
        void War_Tick(object sender, EventArgs e)               //警告闪烁
        {
            Warn.Opacity += opa;
            if (Warn.Opacity <= 0 || Warn.Opacity >= 0.9)
                opa = -opa;
        }

        void Boss_Tick(object sender, EventArgs e)          //boss的出现计时
        {
            bosstime += 1;
            if (bosstime >= bossshow && isover == false)                             //10秒后出现Boss
            {
                War.Start();
                MakeEnemy.Stop();
                MakeEnemy2.Stop();
                MakeEnemy3.Stop();
                MakeEnemy4.Stop();

                BgMusic.Stop();
                BgMusic.MediaEnded -= new EventHandler(BgMusic_MediaEnded);
                Warning.Play();
                Canvas.SetZIndex(Warn, 5);
                Warning.MediaEnded += new EventHandler(Warning_MediaEnded);
                ca.Children.Add(boss);
                Canvas.SetTop(boss, -boss.Height);
                Canvas.SetLeft(boss, ca.Width / 2 - boss.Width / 2);

                Boss.Stop();
            }
        }

        void Warning_MediaEnded(object sender, EventArgs e)
        {
            Canvas.SetZIndex(Warn, -1);
            BossMove.Start();
            BossMusic.Play();
            BossMusic.MediaEnded += new EventHandler(BossMusic_MediaEnded);
            War.Stop();
        }

        void BossMusic_MediaEnded(object sender, EventArgs e)
        {
            BossMusic.Position = TimeSpan.Zero;
            BossMusic.Play();
        }


        void Enemy4AmmoMove_Tick(object sender, EventArgs e)    //敌机4子弹的移动
        {
            for (int i = 0; i < enemy4_ammo.Count; i++)
            {
                if (i % 4 == 0)     //左数第一颗子弹
                {
                    Canvas.SetLeft(enemy4_ammo[i], Canvas.GetLeft(enemy4_ammo[i]) - 3.5);
                }
                if (i % 4 == 1)     //第二颗子弹
                {
                    Canvas.SetLeft(enemy4_ammo[i], Canvas.GetLeft(enemy4_ammo[i]) - 1.5);
                }
                if (i % 4 == 2)     //第三颗子弹
                {
                    Canvas.SetLeft(enemy4_ammo[i], Canvas.GetLeft(enemy4_ammo[i]) + 1.5);
                }
                if (i % 4 == 3)     //第四颗子弹
                {
                    Canvas.SetLeft(enemy4_ammo[i], Canvas.GetLeft(enemy4_ammo[i]) + 3.5);
                }
                Canvas.SetTop(enemy4_ammo[i], Canvas.GetTop(enemy4_ammo[i]) + 6);
            }

            foreach (Image am in enemy4_ammo)           //子弹碰到自己
            {
                if (Canvas.GetTop(am) > Canvas.GetTop(MyPlane) && Canvas.GetTop(am) < Canvas.GetTop(MyPlane) + MyPlane.Height
                    && Canvas.GetLeft(am) > Canvas.GetLeft(MyPlane) && Canvas.GetLeft(am) < Canvas.GetLeft(MyPlane) + MyPlane.Width)
                {
                    Image newammo = new Image();
                    newammo.Width = newammo.Height = am.Height;
                    newammo.Source = am.Source;
                    int i = enemy4_ammo.IndexOf(am);
                    ca.Children.Remove(am);
                    enemy4_ammo.Remove(am);
                    enemy4_ammo.Insert(i, newammo);
                    mp.blood -= 10;
                    ishit = true;
                    break;
                }
                if (Canvas.GetTop(am) >= ca.Height + am.Height && enemy4_ammo.IndexOf(am) % 4 == 3)       //子弹超出范围
                {
                    for (int i = 3; i >= 0; i--)
                    {
                        ca.Children.Remove(enemy4_ammo[i]);
                        enemy4_ammo.Remove(enemy4_ammo[i]);
                    }
                    break;
                }
            }
        }

        void Enemy4Fire_Tick(object sender, EventArgs e)        //敌机4子弹的制作
        {
            foreach (Image ene4 in enemy4)
            {
                if (ene4.Tag.ToString() == "tingzhi")
                {
                    Image[] am = new Image[4];
                    for (int i = 0; i < 4; i++)
                    {
                        am[i] = new Image();
                        am[i].Width = am[i].Height = 25;
                        am[i].Source = new BitmapImage(new Uri("Images/Bullet/Ammo_enemy5.png", UriKind.Relative));
                        am[i].Stretch = Stretch.Fill;
                        Canvas.SetTop(am[i], Canvas.GetTop(ene4) + ene4.Height - 20);
                        Canvas.SetLeft(am[i], Canvas.GetLeft(ene4) + ene4.Width / 2);
                        enemy4_ammo.Add(am[i]);
                        ca.Children.Add(am[i]);
                        Enemy4AmmoMove.Start();
                    }
                }
            }
        }

        void ControlEnemy4_Tick(object sender, EventArgs e)     //敌机4的移动
        {
            foreach (Image ene in enemy4)       //自己的飞机与敌机4碰撞
            {
                if (Canvas.GetLeft(MyPlane) + MyPlane.Width >= Canvas.GetLeft(ene) && Canvas.GetLeft(MyPlane) <= Canvas.GetLeft(ene) + ene.Width &&
                    Canvas.GetTop(MyPlane) + MyPlane.Height >= Canvas.GetTop(ene) && Canvas.GetTop(MyPlane) <= Canvas.GetTop(ene) + ene.Height)
                {
                    EnemyDestoryMusic();
                    BombE(ene);
                    ca.Children.Remove(ene);
                    enemy4.Remove(ene);
                    mp.blood -= 20;
                    mp.score += 1000;
                    ishit = true;
                    break;
                }
            }

            for (int i = 0; i < enemy4.Count; i++)
            {
                if (Canvas.GetTop(enemy4[i]) <= 150)
                {
                    Canvas.SetTop(enemy4[i], Canvas.GetTop(enemy4[i]) + 3);
                }

                if (Canvas.GetTop(enemy4[i]) >= 150)
                {
                    enemy4[i].Tag = "tingzhi";
                    Canvas.SetLeft(enemy4[i], Canvas.GetLeft(enemy4[i]) + enemy4_attribute[i].movedirection);
                    if (Canvas.GetLeft(enemy4[i]) <= 0 || Canvas.GetLeft(enemy4[i]) >= ca.Width - enemy4[i].Width)
                    {
                        enemyplane2 mid = new enemyplane2();
                        mid.movedirection = -enemy4_attribute[i].movedirection;
                        mid.blood = enemy4_attribute[i].blood;
                        enemy4_attribute[i] = mid;
                        break;
                    }
                }
            }
        }

        void MakeEnemy4_Tick(object sender, EventArgs e)        //敌机4的制作
        {
            Image ene4 = new Image();
            ene4.Width = 200;
            ene4.Height = 100;
            ene4.Tag = "weitingzhi";
            ene4.Stretch = Stretch.Fill;
            Canvas.SetZIndex(ene4, 4);
            ene4.Source = new BitmapImage(new Uri("Images/Enemy/Enemy3_" + r.Next(1, 4) + ".png", UriKind.Relative));
            Canvas.SetLeft(ene4, r.Next(0, (int)(ca.Width - ene4.Width)));
            Canvas.SetTop(ene4, -ene4.Height);
            enemyplane2 ene4_att = new enemyplane2();
            ene4_att.blood = 200;
            ene4_att.movedirection = 2;
            enemy4.Add(ene4);
            enemy4_attribute.Add(ene4_att);
            ca.Children.Add(ene4);
        }


        void Enemy3AmmoMove_Tick(object sender, EventArgs e)        //敌机3子弹的移动        
        {
            foreach (Image am in enemy3_ammo)
            {
                int i = enemy3_ammo.IndexOf(am);
                Canvas.SetLeft(am, Canvas.GetLeft(am) + enemy3_ammoDirection[i].x);
                Canvas.SetTop(am, Canvas.GetTop(am) + enemy3_ammoDirection[i].y);
                if (Canvas.GetTop(am) >= ca.Height + am.Height || Canvas.GetLeft(am) <= -am.Width || Canvas.GetLeft(am) >= ca.Width - am.Width)
                {
                    enemy3_ammo.Remove(am);
                    ca.Children.Remove(am);
                    enemy3_ammoDirection.Remove(enemy3_ammoDirection[i]);
                    break;
                }

                //子弹碰到自己的飞机
                if (Canvas.GetTop(am) > Canvas.GetTop(MyPlane) && Canvas.GetTop(am) < Canvas.GetTop(MyPlane) + MyPlane.Height &&
                    Canvas.GetLeft(am) + am.Width > Canvas.GetLeft(MyPlane) + am.Width && Canvas.GetLeft(am) + am.Width < Canvas.GetLeft(MyPlane) + MyPlane.Width)
                {
                    ca.Children.Remove(am);
                    enemy3_ammo.Remove(am);
                    enemy3_ammoDirection.Remove(enemy3_ammoDirection[i]);
                    mp.blood -= 10;
                    ishit = true;
                    break;
                }
            }
        }

        void ControlEnemy3_Tick(object sender, EventArgs e)     //敌机3的移动
        {
            foreach (Image ene in enemy3)       //自己的飞机与敌机3碰撞
            {

                if (Canvas.GetLeft(ene) > Canvas.GetLeft(MyPlane) && Canvas.GetLeft(ene) < Canvas.GetLeft(MyPlane) + MyPlane.Width
                    && Canvas.GetTop(ene) > Canvas.GetTop(MyPlane) && Canvas.GetTop(ene) < Canvas.GetTop(MyPlane) + MyPlane.Height)
                {
                    EnemyDestoryMusic();
                    BombE(ene);
                    ca.Children.Remove(ene);
                    enemy3.Remove(ene);
                    mp.blood -= 20;
                    mp.score += 100;
                    ishit = true;
                    break;
                }
            }
            for (int i = 0; i < enemy3.Count; i++)          //敌机3的移动
            {
                if ((int)enemy3[i].Tag == 0)
                {
                    Canvas.SetTop(enemy3[i], Canvas.GetTop(enemy3[i]) + 7);
                    if (Canvas.GetTop(enemy3[i]) >= ca.Height / 3)
                    {
                        enemy3[i].Tag = 1;
                        Image am = new Image();
                        am.Width = am.Height = 20;
                        am.Stretch = Stretch.Fill;
                        am.Source = new BitmapImage(new Uri("Images/Bullet/Ammo_enemy4.png", UriKind.Relative));
                        Canvas.SetLeft(am, Canvas.GetLeft(enemy3[i]) + enemy3[i].Width / 2 - am.Width / 2);
                        Canvas.SetTop(am, Canvas.GetTop(enemy3[i]) + enemy3[i].Height - am.Height);
                        enemy3_ammo.Add(am);
                        ca.Children.Add(am);
                        direction ammod = new direction();
                        int speed = 8;
                        ammod.x = Canvas.GetLeft(MyPlane) + MyPlane.Width / 2 - Canvas.GetLeft(am) - (am.Width / 2);
                        ammod.y = Canvas.GetTop(MyPlane) + MyPlane.Height / 2 - Canvas.GetTop(am) - (am.Height / 2);
                        double h = Math.Sqrt(ammod.x * ammod.x + ammod.y * ammod.y);
                        ammod.x = ammod.x * speed / h;
                        ammod.y = ammod.y * speed / h;
                        ammod.assign = true;
                        enemy3_ammoDirection.Add(ammod);
                    }

                }
                if ((int)enemy3[i].Tag == 1)
                {
                    if (Canvas.GetLeft(enemy3[i]) > ca.Width / 2)
                        Canvas.SetLeft(enemy3[i], Canvas.GetLeft(enemy3[i]) + 3);
                    else
                        Canvas.SetLeft(enemy3[i], Canvas.GetLeft(enemy3[i]) - 3);
                    Canvas.SetTop(enemy3[i], Canvas.GetTop(enemy3[i]) - 7);
                }
                if (Canvas.GetTop(enemy3[i]) >= ca.Height + enemy3[i].Height || Canvas.GetLeft(enemy3[i]) <= -enemy3[i].Width || Canvas.GetLeft(enemy3[i]) > ca.Width + enemy3[i].Width)
                {
                    ca.Children.Remove(enemy3[i]);
                    enemy3.Remove(enemy3[i]);
                }
            }
        }


        void MakeEnemy3_Tick(object sender, EventArgs e)        //敌机3的制作
        {
            Image ene3 = new Image();
            ene3.Width = ene3.Height = 60;
            ene3.Tag = 0;
            ene3.Stretch = Stretch.Fill;
            ene3.Source = new BitmapImage(new Uri("Images/Enemy/Enemy4.png", UriKind.Relative));
            Canvas.SetTop(ene3, -ene3.Height);
            Canvas.SetLeft(ene3, r.Next(0, (int)(ca.Width - ene3.Width)));
            ca.Children.Add(ene3);
            enemy3.Add(ene3);
            ControlEnemy3.Start();
        }

        void Enemy2AmmoMove_Tick(object sender, EventArgs e)    //敌机2子弹的移动
        {
            foreach (Image am in enemy2_ammo)
            {
                Canvas.SetTop(am, Canvas.GetTop(am) + 15);

                if (Canvas.GetTop(am) > Canvas.GetTop(MyPlane) && Canvas.GetTop(am) < Canvas.GetTop(MyPlane) + MyPlane.Height
                    && Canvas.GetLeft(am) > Canvas.GetLeft(MyPlane) && Canvas.GetLeft(am) < Canvas.GetLeft(MyPlane) + MyPlane.Width)
                {
                    ca.Children.Remove(am);
                    enemy2_ammo.Remove(am);
                    mp.blood -= 20;
                    ishit = true;
                    break;
                }

                if (Canvas.GetTop(am) >= ca.Height)
                {
                    enemy2_ammo.Remove(am);
                    break;
                }
            }
        }

        void Enemy2Fire_Tick(object sender, EventArgs e)        //敌机2子弹的制作
        {
            foreach (Image en in enemy2)
            {
                Image ammo1 = new Image();
                Image ammo2 = new Image();
                ammo1.Width = ammo2.Width = 13;
                ammo1.Height = ammo2.Height = 90;
                ammo1.Stretch = Stretch.Fill;
                ammo2.Stretch = Stretch.Fill;
                Canvas.SetTop(ammo1, Canvas.GetTop(en) + en.Height / 2);
                Canvas.SetTop(ammo2, Canvas.GetTop(en) + en.Height / 2);
                Canvas.SetLeft(ammo1, Canvas.GetLeft(en) + en.Width / 3);
                Canvas.SetLeft(ammo2, Canvas.GetLeft(en) + en.Width * 2 / 3);
                ammo1.Source = ammo2.Source = new BitmapImage(new Uri("Images/Bullet/Ammo_enemy3.png", UriKind.Relative));
                ca.Children.Add(ammo1);
                ca.Children.Add(ammo2);
                enemy2_ammo.Add(ammo1);
                enemy2_ammo.Add(ammo2);

            }
        }
        void ControlEnemy2_Tick(object sender, EventArgs e)     //敌机2的移动
        {
            for (int i = 0; i < enemy2.Count; i++)
            {
                Canvas.SetLeft(enemy2[i], Canvas.GetLeft(enemy2[i]) + enemy2_attribute[i].movedirection);

                if ((Canvas.GetTop(enemy2[i]) > (Canvas.GetTop(MyPlane) - enemy2[i].Height + enemy2[i].Height / 3)//敌机2与自己相撞
                        && (Canvas.GetTop(enemy2[i]) < Canvas.GetTop(MyPlane) + MyPlane.Height - enemy2[i].Height / 3)
                        && ((Canvas.GetLeft(enemy2[i]) > Canvas.GetLeft(MyPlane) - enemy2[i].Width * 2 / 3)
                        && (Canvas.GetLeft(enemy2[i]) < Canvas.GetLeft(MyPlane) + (MyPlane.Width - enemy2[i].Width / 3)))))
                {

                    ishit = true;
                    EnemyDestoryMusic();
                    BombE(enemy2[i]);
                    mp.blood -= 20;
                    mp.score += 400;
                    ca.Children.Remove(enemy2[i]);
                    enemy2.Remove(enemy2[i]);
                }
            }
        }

        void MakeEnemy2_Tick(object sender, EventArgs e)    //敌机2的之制作
        {

            clear.Stop();       //Again后关闭对boss子弹清除


            int i = r.Next(2);
            Image en = new Image();
            enemyplane2 en_attribute = new enemyplane2();
            en_attribute.blood = 60;
            en.Width = 90;
            en.Height = 100;
            en.Source = new BitmapImage(new Uri("Images/Enemy/Enemy2.png", UriKind.Relative));
            en.Stretch = Stretch.Fill;
            Canvas.SetTop(en, 100);
            if (i == 0)
            {
                Canvas.SetLeft(en, -en.Width);
                en_attribute.movedirection = 2.5;
            }
            if (i == 1)
            {
                Canvas.SetLeft(en, ca.Width + en.Width);
                en_attribute.movedirection = -2.5;
            }
            enemy2.Add(en);
            enemy2_attribute.Add(en_attribute);
            ca.Children.Add(en);
        }

        void ControlEnemy_Tick(object sender, EventArgs e)//敌机1的移动
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                if (enemy[i].Tag.ToString() == "weitingzhi")
                {
                    Canvas.SetTop(enemy[i], Canvas.GetTop(enemy[i]) + 5);

                    if (Canvas.GetTop(enemy[i]) >= ca.Height / 4 - enemy[i].Height)
                    {
                        enemy[i].Tag = "tingzhi";
                        for (int j = 0; j < enemy.Count; j++)
                        {
                            if (enemy[j].Tag.ToString() == "tingzhi" && enemyp[j].assign == false)          //敌机1的冲撞
                            {
                                double speed = 12;
                                direction mid = new direction();
                                mid.x = Canvas.GetLeft(MyPlane) + MyPlane.Width / 2 - Canvas.GetLeft(enemy[j]) - (enemy[j].Width / 2);
                                mid.y = Canvas.GetTop(MyPlane) + MyPlane.Height / 2 - Canvas.GetTop(enemy[j]) - (enemy[j].Height / 2);
                                double h = Math.Sqrt(mid.x * mid.x + mid.y * mid.y);
                                mid.x = mid.x * speed / h;
                                mid.y = mid.y * speed / h;
                                mid.assign = true;
                                enemyp[j] = mid;
                                ControlEnemy.Start();
                                continue;
                            }
                        }
                    }
                }
                if (enemy[i].Tag.ToString() == "tingzhi")
                {
                    Canvas.SetLeft(enemy[i], Canvas.GetLeft(enemy[i]) + enemyp[i].x);
                    Canvas.SetTop(enemy[i], Canvas.GetTop(enemy[i]) + enemyp[i].y);
                }
            }

            foreach (Image en in enemy) //敌机与自己飞机相撞   
            {
                if ((Canvas.GetTop(en) > (Canvas.GetTop(MyPlane) - en.Height + en.Height / 3)
                        && (Canvas.GetTop(en) < Canvas.GetTop(MyPlane) + MyPlane.Height - en.Height / 3)
                        && ((Canvas.GetLeft(en) > Canvas.GetLeft(MyPlane) - en.Width * 2 / 3)
                        && (Canvas.GetLeft(en) < Canvas.GetLeft(MyPlane) + (MyPlane.Width - en.Width / 3)))))
                {
                    BombE(en);
                    EnemyDestoryMusic();
                    mp.blood -= 20;
                    mp.score += 200;
                    int i = enemy.IndexOf(en);
                    enemy.Remove(en);
                    enemyp.Remove(enemyp[i]);
                    ca.Children.Remove(en);
                    ishit = true;
                    break;
                }
            }
        }

        void MakeEnemy_Tick(object sender, EventArgs e)//敌机1的制作
        {
            int x = r.Next(0, (int)ca.Width - 50);
            Image ene = new Image();
            direction enep = new direction();
            enep.assign = false;
            ene.Width = 50;
            ene.Height = 60;
            ene.Tag = "weitingzhi";
            ene.Stretch = Stretch.Fill;
            ene.Source = new BitmapImage(new Uri("Images/Enemy/Enemy1.png", UriKind.Relative));
            Canvas.SetLeft(ene, x);
            Canvas.SetTop(ene, -ene.Height);
            ca.Children.Add(ene);
            enemy.Add(ene);
            enemyp.Add(enep);

        }

        void JudgeCrash_Tick(object sender, EventArgs e)//子弹的撞击
        {
            DijiSiwang(ammo, mp.power, ammo);
        }

        void Remove_Tick(object sender, EventArgs e)            //遗留boss子弹的清除
        {
            for (int i = 0; i < bossammo.Count; i++)
            {
                ca.Children.Remove(bossammo[i]);
                bossammo.Remove(bossammo[i]);
            }
            for (int u = 0; u < bossrocket.Count; u++)
            {
                ca.Children.Remove(bossrocket[u]);
                bossrocket.Remove(bossrocket[u]);
            }
        }

        bool isbossover = false;

        int xm = 2, ym = 2;
        void PlusMove_Tick(object sender, EventArgs e)               //plus的移动
        {
            Canvas.SetLeft(plus, Canvas.GetLeft(plus) + xm);
            Canvas.SetTop(plus, Canvas.GetTop(plus) + ym);
            if (Canvas.GetTop(plus) <= 0 || Canvas.GetTop(plus) >= ca.Height - plus.Height)
            {
                ym = -ym;
            }
            if (Canvas.GetLeft(plus) <= 0 || Canvas.GetLeft(plus) >= ca.Width - plus.Width)
            {
                xm = -xm;
            }
        }

        void BgMusic_MediaEnded(object sender, EventArgs e)     //控制背景音乐重复播放
        {
            BgMusic.Stop();
            BgMusic.Position = TimeSpan.Zero;
            BgMusic.Play();
        }

        private void BombE(Image i)                         //设置爆炸
        {
            BombPicture.Width = i.Width;
            BombPicture.Height = i.Height;
            Canvas.SetTop(BombPicture, Canvas.GetTop(i));
            Canvas.SetLeft(BombPicture, Canvas.GetLeft(i));
            t = 0;
            try
            {
                ca.Children.Add(BombPicture);
            }
            catch
            {
                ca.Children.Remove(BombPicture);
                ca.Children.Add(BombPicture);
            }
            BombEffect.Start();
        }

        private void EnemyDestoryMusic()                //控制爆炸音效
        {
            EnemyDestroy.Load();
            EnemyDestroy.Play();
        }

        bool fire = false;
        void ControlFire_Tick(object sender, EventArgs e)//控制开火
        {
            if (fire == true)
            {
                Image bu = new Image();
                bu.Width = 8;
                bu.Height = 60;
                bu.Stretch = Stretch.Fill;
                switch (AmmoType)
                {
                    case 1:
                        bu.Source = new BitmapImage(new Uri("Images/Bullet/Ammo1.png", UriKind.Relative));
                        break;
                    case 2:
                        bu.Width = 16;
                        bu.Source = new BitmapImage(new Uri("Images/Bullet/Ammo2.png", UriKind.Relative));
                        break;
                    case 3:
                        bu.Width = 20;
                        bu.Source = new BitmapImage(new Uri("Images/Bullet/Ammo3.png", UriKind.Relative));
                        break;
                    case 4:
                        bu.Width = 30;
                        bu.Height += 10;
                        bu.Source = new BitmapImage(new Uri("Images/Bullet/Ammo4.png", UriKind.Relative));
                        break;
                }
                Canvas.SetLeft(bu, (Canvas.GetLeft(MyPlane) + MyPlane.Width / 2 - bu.Width / 2));
                Canvas.SetTop(bu, Canvas.GetTop(MyPlane) - bu.Height + 20);
                ammo.Add(bu);
                ca.Children.Add(bu);
            }
        }

        void MyPlaneFire_MediaEnded(object sender, EventArgs e)
        {
            MyPlaneFire.Stop();
            MyPlaneFire.Position = TimeSpan.Zero;
            MyPlaneFire.Play();
        }

        void MyPlane_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyPlaneFire.Play();
            MyPlaneFire.MediaEnded += new EventHandler(MyPlaneFire_MediaEnded);
            fire = true;
        }

        void MyPlane_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            fire = false;
            MyPlaneFire.Stop();
            MyPlaneFire.Position = TimeSpan.Zero;
        }

        void Fire_Tick(object sender, EventArgs e)//自己子弹的移动
        {
            foreach (Image i in ammo)
            {
                Canvas.SetTop(i, Canvas.GetTop(i) - 20);
                if (Canvas.GetTop(i) <= -i.Height)
                {
                    ammo.Remove(i);
                    ca.Children.Remove(i);
                    break;
                }
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isover == false && isstart == true)
                Mouse.OverrideCursor = Cursors.None;
        }

        bool isover = false;
        bool isget = false;
        bool isstart = false;
        private void Window_MouseMove(object sender, MouseEventArgs e)//自己飞机的移动
        {
            if (isover == false && isstart == true)
            {
                Point p = e.GetPosition(ca);
                Canvas.SetTop(MyPlane, p.Y - MyPlane.Height / 2);
                Canvas.SetLeft(MyPlane, p.X - MyPlane.Width / 2);
                if (Canvas.GetLeft(MyPlane) <= 0)
                    Canvas.SetLeft(MyPlane, 0);
                if (Canvas.GetLeft(MyPlane) >= ca.Width - MyPlane.Width)
                    Canvas.SetLeft(MyPlane, ca.Width - MyPlane.Width);
                if (Canvas.GetTop(MyPlane) <= 0)
                    Canvas.SetTop(MyPlane, 0);
                if (Canvas.GetTop(MyPlane) >= ca.Height - MyPlane.Height)
                    Canvas.SetTop(MyPlane, ca.Height - MyPlane.Height);
                //碰到boss
                if (Canvas.GetTop(boss) + boss.Height >= Canvas.GetTop(MyPlane) + 50 &&
                    Canvas.GetLeft(boss) + boss.Width >= Canvas.GetLeft(MyPlane) && Canvas.GetLeft(boss) <= Canvas.GetLeft(MyPlane) + MyPlane.Width)
                {
                    mp.blood = 0;
                }
                if ((isget == false) && Canvas.GetLeft(plus) > Canvas.GetLeft(MyPlane) && Canvas.GetLeft(plus) < Canvas.GetLeft(MyPlane) + MyPlane.Width &&
                    Canvas.GetTop(plus) > Canvas.GetTop(MyPlane) && Canvas.GetTop(plus) < Canvas.GetTop(MyPlane) + MyPlane.Height)
                {
                    ca.Children.Remove(plus);
                    Get.Play();
                    isget = true;
                    switch ((int)plus.Tag)
                    {
                        case 1:                 //治疗
                            mp.blood = 100;
                            break;
                        case 2:                 //绝杀
                            if (mp.kill < 3)
                            {
                                isget = true;
                                mp.kill++;
                            }
                            break;
                        case 3:                 //最大威力
                            AmmoType = 4;
                            mp.power = 25;
                            break;
                        case 4:                 //威力增加
                            if (mp.power < 25)
                            {
                                mp.power += 5;
                                AmmoType += 1;
                                return;
                            }
                            break;
                    }
                }
            }
        }
        Border failed = new Border();
        Label failedShow = new Label();
        Label Again = new Label();
        DispatcherTimer failedDH = new DispatcherTimer();
        void JudgeBlood_Tick(object sender, EventArgs e)//血条、分数的控制
        {
            killcount.Width = mp.kill * 31;
            score.Content = mp.score.ToString();
            if (mp.blood <= 0 && isover == false)                  //游戏结束
            {
                BgMusic.Stop();
                Warning.Stop();
                Canvas.SetZIndex(Warn, -1);
                BossMusic.Stop();
                MyPlaneFire.Stop();
                MyBomb.Play();
                blood.Width = 0;
                isover = true;
                Mouse.OverrideCursor = Cursors.Arrow;
                MakeEnemy.Stop();
                MakeEnemy2.Stop();
                MakeEnemy3.Stop();
                MakeEnemy4.Stop();
                fire = false;
                MyBombEffect.Start();
                MyPlane.MouseLeftButtonDown -= new MouseButtonEventHandler(MyPlane_MouseLeftButtonDown);
                MyPlane.MouseLeftButtonUp -= new MouseButtonEventHandler(MyPlane_MouseLeftButtonUp);

                ca.Children.Add(failed);
                failed.Width = back.Width;
                failed.Height = back.Height;
                failed.Background = Brushes.Black;
                failed.Opacity = 0;
                Canvas.SetZIndex(failed, 10);

                ca.Children.Add(failedShow);
                failedShow.Content = "MISSION FAILED";
                failedShow.FontSize = 30;
                failedShow.Foreground = Brushes.Red;
                Canvas.SetLeft(failedShow, 150);
                Canvas.SetTop(failedShow, ca.Height / 2 - 100);
                Canvas.SetZIndex(failedShow, 11);

                ca.Children.Add(Again);
                Again.Content = "AGAIN";
                Again.Foreground = Brushes.Red;
                Again.FontSize = 20;
                Canvas.SetLeft(Again, 230);
                Canvas.SetTop(Again, ca.Height / 2 - 40);
                Canvas.SetZIndex(Again, 11);
                Again.MouseDown += new MouseButtonEventHandler(Again_MouseDown);
                Again.MouseEnter += new MouseEventHandler(Again_MouseEnter);
                Again.MouseLeave += new MouseEventHandler(Again_MouseLeave);

                failedDH.Interval = TimeSpan.FromMilliseconds(1);
                failedDH.Tick += new EventHandler(failedDH_Tick);
                failedDH.Start();
                return;
            }
            if (mp.blood >= 0)
                blood.Width = mp.blood * 2;
        }

        void Again_MouseLeave(object sender, MouseEventArgs e)
        {
            Again.Foreground = Brushes.Red;
        }
        void Again_MouseEnter(object sender, MouseEventArgs e)
        {
            Again.Foreground = Brushes.Blue;
        }
        DispatcherTimer clear = new DispatcherTimer();
        void Again_MouseDown(object sender, MouseButtonEventArgs e)         //again   
        {
            if (bosstime >= bossshow)
            {
                ca.Children.Remove(boss);
                Canvas.SetTop(boss, -boss.Height);
                BossAmmoMove.Stop();
                rocketfire.Stop();
                BossMove.Stop();
                BossAmmo.Stop();
                BossRocket.Stop();
                boss.Tag = "weitingzhi";

                bossattribute.blood = 3000;
                BossMusic.Stop();
                Warning.Stop();
                Boss.Start();
                clear.Interval = TimeSpan.FromMilliseconds(1);
                clear.Tick += new EventHandler(clear_Tick);
                clear.Start();
            }
            War.Stop();
            isover = false;
            bosstime = 0;
            CGdonghua.Start();
            ca.Children.Remove(failed);
            ca.Children.Remove(failedShow);
            ca.Children.Remove(Again);
            ca.Children.Remove(plus);
            MyPlane.Source = new BitmapImage(new Uri("Images/plane/plane1.png", UriKind.Relative));
            mp.blood = 100;
            mp.kill = 3;
            mp.score = 0;
            mp.power = 10;
            AmmoType = 1;
            Mouse.OverrideCursor = Cursors.None;
            BgMusic.Play();
            MakeEnemy.Start();
            MakeEnemy2.Start();
            MakeEnemy3.Start();
            MakeEnemy4.Start();
            fire = false;
            MyBombEffect.Stop();
            MyBomb.Stop();
            m = 0;
            MyPlane.MouseLeftButtonDown += new MouseButtonEventHandler(MyPlane_MouseLeftButtonDown);
            MyPlane.MouseLeftButtonUp += new MouseButtonEventHandler(MyPlane_MouseLeftButtonUp);

        }

        void clear_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < bossammo.Count; i++)
            {
                ca.Children.Remove(bossammo[i]);
                bossammo.Remove(bossammo[i]);
            }
            for (int i = 0; i < bossrocket.Count; i++)
            {
                ca.Children.Remove(bossrocket[i]);
                bossrocket.Remove(bossrocket[i]);
            }
        }

        void failedDH_Tick(object sender, EventArgs e)
        {
            failed.Opacity += 0.02;
        }

        int t = 0;
        void BombEffect_Tick(object sender, EventArgs e)
        {
            if (t < 32)
            {
                if (t <= 9)
                    BombPicture.Source = new BitmapImage(new Uri("Images/Bomb/1000" + t + ".png", UriKind.Relative));
                else
                    BombPicture.Source = new BitmapImage(new Uri("Images/Bomb/100" + t + ".png", UriKind.Relative));
                t += 2;
            }
            if (t >= 32)
            {
                ca.Children.Remove(BombPicture);
                BombEffect.Stop();
            }
        }
        int m = 0;
        void MyBombEffect_Tick(object sender, EventArgs e)
        {
            if (m < 32)
            {
                if (m <= 9)
                    MyPlane.Source = new BitmapImage(new Uri("Images/Bomb/1000" + m + ".png", UriKind.Relative));
                else
                    MyPlane.Source = new BitmapImage(new Uri("Images/Bomb/100" + m + ".png", UriKind.Relative));
                m++;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }

            if (e.Key == Key.W)
            {
                cheat = 1;
            }
            if (e.Key == Key.E)
            {
                cheat = 2;
            }
            if (e.Key == Key.Q)
            {
                cheat = 3;
            }
            if (e.Key == Key.Space)
            {
                if (cheat == 1)
                {
                    mp.blood = 100;

                }
                if (cheat == 2)
                {
                    mp.power = 25;
                    AmmoType = 4;
                }
                if (cheat == 3)
                {
                    mp.kill = 3;
                }
            }

        }


    }
}
