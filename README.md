# Motion-Capture-System-Based-on-Python-and-Unity-Engine

项目说明文档

一、项目概述

采用了Python语言处理通过摄像头采集的用户身体动作数据，调用MediaPipe框架进行人体关键点检测；使用Unity引擎作为搭建虚拟场景和虚拟人物模型的平台，通过Socket通信把数据从Python端实时传输到Unity端从而实现角色驱动。
1. 通过 PC 摄像头 / 本地视频实现人体姿态关键点检测；
2. 将检测到的关键点数据传输至 Unity 端，驱动角色模型运动；
3. 为 Unity 端关键点（Sphere）添加重力物理效果，防止角色模型异常上飘

二、环境依赖（都在master文件夹下）
1. Python 环境（姿态检测端） Python3.12+PyCharm2024.3 UDO07.py实现  project1文件夹（除了gradution文件夹下的其他文件）导入Pycharm
2. Unity 环境（角色驱动端）  C＃＋Unity 2021.3.34f1c1 graduation.sln实现  graduation文件夹导入unity
   
