using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0621Watering
{
    class PipeD
    {
        //只需要通水的属性
        public PipeD(int i)//水管的初始化
        {
            this.Num = i;
            switch (i)
            {
                case 1:
                    IsLeft = IsTop = false;
                    break;
                case 2:
                    IsRight = IsTop = false;
                    break;
                case 3:
                    IsRight = IsBottom = false;
                    break;
                case 4:
                    IsLeft = IsBottom = false;
                    break;
                case 5:
                    IsRight = false;
                    break;
                case 6:
                    IsBottom = false;
                    break;
                case 7:
                    IsLeft = false;
                    break;
                case 8:
                    IsTop = false;
                    break;
                case 9:
                    IsTop = IsBottom = false;
                    break;
                case 10:
                    IsLeft = IsRight = false;
                    break;
                case 11:
                    IsBottom = IsRight = IsLeft = IsTop = true;
                    break;

                case 12:
                    IsBottom = IsRight = IsLeft = IsTop = false;
                    break;
                default:
                    break;
            }
        }

        int num;//专门用来存储初始化水管时穿进来的水管编号
        public int Num
        {
            get { return num; }
            set { num = value; }
        }



        bool isRight = true;//字段

        public bool IsRight
        {
            get { return isRight; }
            set { isRight = value; }
        }
        bool isLeft = true;

        public bool IsLeft
        {
            get { return isLeft; }
            set { isLeft = value; }
        }
        bool isTop = true;

        public bool IsTop
        {
            get { return isTop; }
            set { isTop = value; }
        }
        bool isBottom = true;
        /// <summary>
        /// 向下通水
        /// </summary>
        public bool IsBottom //对字段的封装==>>属性
        {
            get { return isBottom; }
            set { isBottom = value; }
        }

    }
}
