# FatigueConsoleWPF
> Fatigue monitoring system developed by Kinect for windows V2.

### 程序运行主窗口（Main Windows）
![avatar](/img/mainwindow.png)

### 介绍(Introduction)
<p>&emsp;&emsp;本程序通过使用Kinect for windows SDKv2在VS2015/VS2017平台开发，以PERCLOS算法为原理设计出了通过判断眼睛在单位情况下的闭合情况计算被测者的疲劳值。并且通过Kinect的深度信息计算出距离摄像头最近的人体，以该人体的眼部信息作为检测目标，屏蔽了周围非被测人员的干扰。(This program is developed on VS2015/VS2017 platform by using Kinect for windows SDKv2. Based on PERCLOS algorithm, the fatigue value of the tested person is calculated by judging the closure of the eyes in the unit case. The human body nearest to the camera is calculated by the depth information of Kinect, and the eye information of the human body is taken as the detection target to shield the interference of the non-measured persons around.)</p>
<p>&emsp;&emsp;该程序可以通过串口向下位机发送疲劳情况，供下位机做出缓解疲劳的动作，比如我们的实验中提供不同芬芳、音乐和灯光等刺激已达到缓解疲劳。(The program can send fatigue information to the lower computer through serial port, so that the lower computer can make action to alleviate fatigue, such as providing different fragrance, music and lighting stimulation in our experiment, which has achieved the alleviation of fatigue.)</p>