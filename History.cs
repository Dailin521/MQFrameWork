1.快速开始： 
	点击开始->生成10个敌人->依次点击敌人至全部消失->显示游戏胜利界面
2.树结构 与 无架构项目的优缺点
	2.1 难维护，理不清引用关系
	2.2 难管理，团队协作会造成场景文件冲突
	2.3 难拓展，耦合高，牵一发动全身
4.对象之间的交互（低耦合） 和 模块化（高内聚）
  交互一般三种：
	*方法调用，例如：A调用B的Hello方法
	*委托或者回调，例如：界面监听子按钮的点击事件
	*消息或者事件，例如：服务器向客户端发送通知
  模块化一般三种：
	*单例，
	*IOC, 例如Extenject, UFrame的Container, StrangeIOC
	*分层，例如： MVC, 三层架构，领域驱动
	*其他，unity的Entity Component，门面模式等
  耦合是双向引用或循环引用，方法调用需要持有对象，这必然造成一次担心引用，
  所以方法调用达成一个共识，父节点可以调用子节点。
    *用委托来监听事件
  跨模块通信需要通过事件（Event）来通信
5.表现和数据要分离
	*用泛型+继承 提取Event工具类
	*子节点通知父节点可以用事件
	*表示和数据要分离（数据要在空间和时间上共存，数据类型有很多）
	*正确的代码要放在正确的位置（要符合正常逻辑）
6.交互逻辑和表现逻辑
	*交互逻辑：View->Model
	*表现逻辑：Model->View
7.表现逻辑游戏化-引入 BindableProperty（数据+数据变更事件）
	*表现逻辑用委托实现数据驱动
	*表现逻辑也可以用事件来驱动。
	//委托和事件用哪个？
	*如果是单个数值变化用委托更合适，如 金币，分数，等级等等
	*如果是颗粒度较大的更新用事件，比如从服务器拉去一个任务列表，然后任务列表数据（一堆各种类型数据）存到Model，此时Model的任务列表数据变更，向View发送事件合适
	*两个问题：一是Event工具类无法传参，二是每个数据都实现一遍，不聪明
	总结：
	*自底向上的逻辑  用委托和事件
	*自顶向下的逻辑  用方法调用
8.《点点点》使用 BindableProperty
9.交互逻辑优化-引入 Command
	*交互逻辑 会有很多 会让Controller变臃肿
	*Command模式可以让逻辑的调用和执行在空间（不同位置）和时间（不是像方法那样立刻调用）上分离
	*分担Controller的交互逻辑
	*struct 比 calss 有更好的内存管理效率
	*CQRS读写分离
10.CounterApp 编辑器扩展版本
	*表现层 到 底层系统层 用 Command
	*底层系统层 到 表现层 用委托或者时间（通知）
	*表现层是可替换的
11.《点点点》使用 Command
	*事件由 系统层，向表现层发送
	*表现层 只能 用 Command 改变底层系统的状态（数据）
	*表现层可以直接查询数据
12.模块化优化-引入单例
	*除了类之间的复杂引用，模块之间也会出现这种问题，引入单例
	//问题
	*静态类没有访问限制
	*使用static 去扩展模块，模块的识别度不高
	//其他，单例没访问限制
13.模块化优化-引入 IOC 容器
	*简单理解为一个字典，以Type为Key, 以对象Instance为value。
14.《点点点》和 CounterApp 引入 IOC 容器
	*引入IOC可以限制模块的访问
	*IOC模块统一注册，有更好的宏观视角
	*符合SOLD原则，可以增加层级，增加中间件模式，增加事件中心等等
15.IOC 容器的隐藏功能-注册接口模块
	*抽象-实现 这种形式注册和获取对象  符合依赖倒置原则
	//优点
	*接口设计分两步骤，设计和实现都专注与自身，设计时可减少系统设计的干扰
	*实现是可以替换的，只换一行代码
	*容易单元测试
	*实现的细节改变时，接口不需要改变
16.《点点点》和 CounterApp 支持接口模块
	*通过接口注册和获取对象，不用考虑具体细节实现
17. CounterApp 支持数据存储
		*想在获得模块之前进行初始化数据，通过其他存储接口获得，这部分不可以放在构造函数，因为Get方法回调用初始化导致递归
		*注册模块后，单独用一个Init初始化，避免生命周期导致的递归
		*新键一个注册模块的方法，和一个接口, IB, IAchitecture(继承IB)。模块继承并实现这个IB接口，注册模块的同时将接口IA赋值
		给Model自己，因为自己也要继承IA接口，那么通过Model可以直接调用自己接口定义的工具方法（这里面进行注册，获取）
		*新增一个接口IM，定义一个Init，注册完后会统一的调用IM的所有Init。
18.接口的阉割技术
		*接口-抽象类-实现类，接口显示调用
		*接口+扩展
20.实现 System 层
		*有状态，（管理数据）同时提供一些API访问，比如蓝牙，商城系统，服务器请求
		*需要很多硬编码，同时将这些统一管理的，比如成就系统，分数统计系统等
21.表现层的 IController 接口定义与实现
		*Architecture 里实现 static IArchitecture Interface  属性，
		*构造期间不能调用Unity的Api  , 差分两个接口，用函数获得，Get，Set
		*不想每次都实现，定义抽象类继承两接口，在抽象类里实现，同时用一个abstract函数拓展子类初始化

22.IUtiilty 实现 与 ICommand 完善
		*抽象类AbstractCommand继承ICommand，并通过接口显示实现，和abstract方法拓展接口Excute
		*Command类继承AbstractCommand接口，并实现抽象Excute方法
		*调用Excute方法，是要持有Architecture实例，同时IA拓展SendCommand接口函数，通过实例调用SendCommand接口函数，而接口函数内部进行直接调用
		*表现层先实现接口IController，对Architecture进行赋值，通过GetArchitecture得到实例后进行SenCommand（传入对应Command类）
23.架构使用规范实现
		*对IModel，ISystem，ICommand，IController，增加使用规则
24.增加事件的使用规则
		*IController 可以监听事件
		*ICommand 可以发送事件
		*ISystem 可以发送和监听事件
		*IModel 可以发送事件


         