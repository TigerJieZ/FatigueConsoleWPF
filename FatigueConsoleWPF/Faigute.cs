using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect.Face;
using Microsoft.Kinect;
using System.Windows;
using System.IO;

namespace Faigute_WPF
{

    class Faigute
    {
        //左/右眼的闭合帧数
        private MyQueue m_nNumeEyesLeftClosed = new MyQueue(30 * 30);
        private MyQueue m_nNumeEyesRightClosed = new MyQueue(30 * 30);

        //嘴部张帧数
        private MyQueue m_nNumeMouthOpen = new MyQueue(30*30);

        //疲劳值
        private double NumeFaigute = 0.0;

        //面部信息的四种情况
        static int YES = 3;
        static int MAYBE = 2;
        static int NO = 1;
        static int UNKNOW = 0;

        //帧计时器，眼部疲劳队列每更新一遍向文本中储存一下
        //int i = 0;


        public Faigute()
        {
        }

        //眼部闭合帧记录
        public void NoteRightEyeClosed(int index)
        {
            //右眼闭合记录
            switch (index)
            {
                case 3:
                    this.m_nNumeEyesRightClosed.Push(YES);
                    break;
                case 2:
                    this.m_nNumeEyesRightClosed.Push(MAYBE);
                    break;
                case 1:
                    this.m_nNumeEyesRightClosed.Push(NO);
                    break;
                case 0:
                    this.m_nNumeEyesRightClosed.Push(UNKNOW);
                    break;
            }
        }

        public void NoteLeftEyeClosed(int index)
        {
            //左眼闭合记录
            switch (index)
            {
                case 3:
                    this.m_nNumeEyesLeftClosed.Push(YES);
                    break;
                case 2:
                    this.m_nNumeEyesLeftClosed.Push(MAYBE);
                    break;
                case 1:
                    this.m_nNumeEyesLeftClosed.Push(NO);
                    break;
                case 0:
                    this.m_nNumeEyesLeftClosed.Push(UNKNOW);
                    break;
            }

        }

        //嘴部闭合记录
        public void NoteMouthOpen(int index)
        {
            switch (index)
            {
                case 3:
                    m_nNumeMouthOpen.Push(YES);
                    break;
                case 2:
                    m_nNumeMouthOpen.Push(MAYBE);
                    break;
                case 1:
                    m_nNumeMouthOpen.Push(NO);
                    break;
                case 0:
                    m_nNumeMouthOpen.Push(UNKNOW);
                    break;
            }
        }

        //嘴部疲劳指数统计
        public double calculateMouthFaigute()
        {
            double faigute = 0.0f;

            int myOpen = this.m_nNumeMouthOpen.CountYes();
            faigute = myOpen / this.m_nNumeMouthOpen.getLength();

            return faigute;
        }

        //眼部疲劳值统计
        public double calculatFaigute()
        {
            ///当前队列中右眼闭眼帧数
            int rClosed = m_nNumeEyesRightClosed.CountYes();

            ///当前队列中左眼闭眼帧数
            int lClosed = m_nNumeEyesLeftClosed.CountYes();

            ///眼部疲劳值
            double faigute = (rClosed + lClosed) / (this.m_nNumeEyesLeftClosed.getLength() * (double)(2));
            //Console.WriteLine(m_nNumeEyesRightClosed.CountYes()+"///////" + m_nNumeEyesLeftClosed.CountYes() +"//////"+ this.m_nNumeEyesLeftClosed.getLength());

            return faigute;
        }

        private double calculateFaigute(double rEye,double lEye,double mMouth)
        {
            double faigute = 0.0f ;

            return faigute;
        }

        /// <summary>
        /// 输出当前队列中右眼的闭合频率
        /// </summary>
        /// <returns></returns>
        public double getRightEyesClosedFrequency()
        {
            int rClosed = m_nNumeEyesRightClosed.CountYes();
            double frequency = rClosed / this.m_nNumeEyesRightClosed.getLength();

            return frequency;
        }

        /// <summary>
        /// 输出当前队列中左眼的闭合频率
        /// </summary>
        /// <returns></returns>
        public double getLeftEyesClosedFrequency()
        {
            int lClosed = m_nNumeEyesRightClosed.CountYes();
            double frequency = lClosed / this.m_nNumeEyesLeftClosed.getLength();

            return frequency;
        }

        //疲劳主调度器
        public double Scheduler(FaceFrameResult faceFrameResult)
        {
            //记录异常帧
            Note(faceFrameResult);

            this.NumeFaigute = calculatFaigute();

            return this.NumeFaigute;
        }

        public void Note(FaceFrameResult faceFrameResult)
        {
            foreach (var item in faceFrameResult.FaceProperties)
            {
                String s = item.Key.ToString();
                if (s.Equals("RightEyeClosed"))
                {
                    switch (item.Value)
                    {
                        case DetectionResult.Yes:
                            NoteRightEyeClosed(YES);
                            break;
                        case DetectionResult.Maybe:
                            NoteRightEyeClosed(MAYBE);
                            break;
                        case DetectionResult.No:
                            NoteRightEyeClosed(NO);
                            break;
                        case DetectionResult.Unknown:
                            NoteRightEyeClosed(UNKNOW);
                            break;
                    }
                }
                if (s.Equals("LeftEyeClosed"))
                {
                    switch (item.Value)
                    {
                        case DetectionResult.Yes:
                            NoteLeftEyeClosed(YES);
                            break;
                        case DetectionResult.Maybe:
                            NoteLeftEyeClosed(MAYBE);
                            break;
                        case DetectionResult.No:
                            NoteLeftEyeClosed(NO);
                            break;
                        case DetectionResult.Unknown:
                            NoteLeftEyeClosed(UNKNOW);
                            break;
                    }
                }
                /*if (s.Equals("MouthOpen"))
                {
                    switch (item.Value)
                    {
                        case DetectionResult.Yes:
                            NoteMouthOpen(YES);
                            break;
                        case DetectionResult.Maybe:
                            NoteMouthOpen(MAYBE);
                            break;
                        case DetectionResult.No:
                            NoteMouthOpen(NO);
                            break;
                        case DetectionResult.Unknown:
                            NoteMouthOpen(UNKNOW);
                            break;
                    }
                }*/
            }
            //i++;
            /*if (i % 900 == 0)
            {
                StreamWriter swL = new StreamWriter("m_nNumeEyesLeftClosed.txt",true);
                int q = 0;
                foreach (int data in m_nNumeEyesLeftClosed.getData())
                {
                    q++;
                    swL.Write(data+" ");
                    if (q % 10 == 0)
                        swL.WriteLine();
                }
                swL.Close();

                q = 0;
                StreamWriter swR = new StreamWriter("m_nNumeEyesRightClosed.txt", true);
                foreach (int data in m_nNumeEyesRightClosed.getData())
                {
                    q++;
                    swR.Write(data+" ");
                    if (q % 10 == 0)
                        swR.WriteLine();
                }
                swR.Close();

                q = 0;
                StreamWriter swM = new StreamWriter("m_nNumeMouthOpen.txt", true);
                foreach (int data in m_nNumeMouthOpen.getData())
                {
                    q++;
                    swM.Write(data + " ");
                    if (q % 10 == 0)
                        swM.WriteLine();
                }
                swM.Close();
            }*/
                

        }

        //绘制提示图
        //public void DrawingFaigute()
        //{
        //    using (DrawingContext Ddc = this.drawingGroupNew.Open())
        //    {
        //        Ddc.DrawRectangle(Brushes.Black, null, new Rect(new Point(0, 0), new Point(100, 263)));
        //        Ddc.Close();
        //    }
        //}

        // 返回左眼闭合数
        public int getLeftEyeCloseLength()
        {
            return m_nNumeEyesLeftClosed.CountYes();
        }

        // 返回右眼闭合数
        public int getRightEyeCloseLength()
        {
            return m_nNumeEyesRightClosed.CountYes();
        }

        // 返回眼睛队列总长度
        public int getEyeLength()
        {
            return this.m_nNumeEyesLeftClosed.getLength();
        }
    }
}
