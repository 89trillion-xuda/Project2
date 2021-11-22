# Project2
题目2
# SecondProject

1、整体框架

- 层级视图中，每个小块都用空的游戏对象封装，方便禁用和启用
- 脚本代码以MVC层级结构分层



2、资源目录结构

| 目录名称        | 目录内容                                              | 父目录     | 其他说明           |
| --------------- | ----------------------------------------------------- | ---------- | ------------------ |
| Resources       | 存放动态加载需要的资源                                |            | 处于根目录Assets下 |
| 01.Scenes       | 存放场景                                              |            | 处于根目录Assets下 |
| 02.Scripts      | 存放脚本                                              |            | 处于根目录Assets下 |
| 03.Prefabs      | 存放预设体                                            |            | 处于根目录Assets下 |
| 04.Plugs        | 存放插件                                              |            | 处于根目录Assets下 |
| 05.Animation    | 存放弓箭飞出的动画和控制器，以及己方士兵1的动画控制器 |            | 处于根目录Assets下 |
| ReadMeImg       | 存放技术文档中用到的流程图图片                        |            | 处于根目录Assets下 |
| ParseCsv        | 存放提供的解析方法                                    |            | 处于根目录Assets下 |
| WW4_Effects_Sub | 存放提供的所有愿资源的文件夹                          |            | 处于根目录Assets下 |
| Controller      | 存放控制层脚本代码                                    | 02.Scripts |                    |
| Model           | 存放实体类                                            | 02.Scripts |                    |
| View            | 存放视图层控制代码                                    | 02.Scripts |                    |


3、界面对象结构拆分

| 结构                     | 结构对象说明                                                 | 父界面对象   | 其他说明 |
| ------------------------ | ------------------------------------------------------------ | ------------ | -------- |
| SampleScene              | 主场景                                                       |              |          |
| Plane                    | 用作地面模型                                                 | SampleScene  |          |
| FrostArcher1             | 友方士兵模型的预制体                                         | SampleScene  |          |
| FrostArcher2             | 敌方士兵模型的预制体                                         | SampleScene  |          |
| FrostArcherManagerObject | 用于挂载FrostArcherManager.cs脚本，好让别的脚本可以获得这个管理类的实体，来实例化 | SampleScene  |          |
| BloudTxt                 | 显示敌方士兵FrostArcher2血量的3D文本框                         | FrostArcher2 |          |


4、代码逻辑分层

| 类                                | 主要职责                                                     | 其他说明         |
| --------------------------------- | ------------------------------------------------------------ | ---------------- |
| 实体类：FrostArcherModel.cs       | 对应冰霜弓箭手实体类型的映射                                 | 位于Model下      |
| 控制层：FrostArcherManager        | 冰霜弓箭手管理类，用于获取csv中具体的数据                    | 位于Controller下 |
| 控制层：FrostArcher1Controller.cs | 己方士兵的控制类，管理士兵的动作动画等操作                   | 位于Controller下 |
| 控制层：FrostArcher2Controller.cs | 敌方士兵的控制类，通过提供的解析方法得到csv中的数据，然后管理士兵的血量等参数 | 位于Controller下 |



5、存储设计

冰霜弓箭手的血量等参数数据从csv中读取，使用已经提供的解析方法



6、部分功能的实现思想

- 一、如何实现己方士兵根据键盘按键展示不同动画？：
- 1、初步思想：制作三个动画运行时控制器，分别为AttackController、IdleController、RunController，位于项目目录的Animator文件夹下。在己方士兵控制类脚本ForstArcher1Controller.cs中监听键盘按键，然后根据按下的不同按键，为场景中己方士兵预制体更换不同的动画运行时控制器，来实现播放不同的动画。
- 2、由于上述方法存在问题，当动画过多时要挂载的动画参数也会很多，所以更换方法：将所有的动画放到一个动画控制器FrostArcher1Control中，通过布尔参数和过度设置各个动画间的变更过度。

- 二、如何控制攻击速度为每秒只能射出一只弓箭？
- 设置一个布尔类型的标志锁nextArrow，初始值为true。按下攻击键A键时，若nextArrow为true，则先设置nextArrow为false，然后放人物攻击动画，然后使用Incoke（）延迟0.5秒后调用实现弓箭飞出动画的函数IceArrow（）；该函数使用DOTween的DOMove（）方法控制弓箭飞行时间为0.5秒，通过OnComplete方法实现飞行结束后的扣血和将标志锁nextArrow解开设置为true。这样总共用时就是1秒，也就控制了每秒间无论按多少下攻击按键，只会飞出一只箭



7、关键代码逻辑的流程图

- 控制己方士兵FrostArcher1的控制层的FrostArcherController.cs脚本代码逻辑：

<img src="https://github.com/89trillion-xuda/Project2/blob/master/Assets/ReadMeImg/FrostArcher1Controller.png" style="zoom:80%;" />




