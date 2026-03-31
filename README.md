# 御花园声景地图 Imperial Garden Sound Map

一个基于 Unity 开发的沉浸式御花园声景交互地图，支持四季声景体验与空间音频定位。

## 项目概述

本项目为清华大学建筑学院「设计课-认知迁移」课程研究成果，通过 Unity 构建可交互的御花园声景地图，让用户在虚拟环境中体验御花园的四季声音景观。

## 技术栈

- **引擎**: Unity 2022.3.62f3c1
- **平台**: iOS（构建版本见 Build_iOS1）
- **交互**: Unity XR Interaction Toolkit
- **音频**: Hear XR（头部运动追踪）
- **UI**: TextMesh Pro
- **控制**: Joystick Pack（移动端摇杆）
- **地图**: Mapbox/自定义地图系统

## 目录结构

```
map/
├── Assets/                          # Unity 资源目录
│   ├── AppFlowManager.cs            # 应用流程管理
│   ├── MapManager.cs                # 地图管理器
│   ├── PlayerController.cs          # 玩家控制器
│   ├── SoundPoint.cs                # 声景点组件
│   ├── UIManager.cs                  # UI 管理
│   ├── SideMenuController.cs         # 侧边菜单
│   ├── TabController.cs              # 标签页控制器
│   ├── CameraFollow.cs               # 相机跟随
│   ├── CameraZoom.cs                 # 相机缩放
│   ├── IconManager.cs                # 图标管理
│   ├── IconBreathing.cs              # 图标呼吸动画
│   ├── LibraryManager.cs              # 声库管理
│   ├── Audio/                        # 音频文件
│   │   ├── History/                  # 历史声景（冬）
│   │   ├── SpringSummer/             # 春夏声景
│   │   └── Winter/                   # 冬季声景
│   ├── Image/                        # 图片资源
│   ├── Fonts/                        # 字体（思源宋体）
│   ├── Hear XR/                      # XR 音频插件
│   ├── Joystick Pack/                # 摇杆控制
│   ├── TextMesh Pro/                 # 文本渲染
│   ├── XRI/                          # XR 交互工具包
│   ├── Scenes/                       # 场景文件
│   └── StreamingAssets/              # Web 端声景档案
│       └── index.html                # 御花园声景档案网页
├── ProjectSettings/                   # Unity 项目设置
├── Packages/                         # Unity 包依赖
├── UserSettings/                     # 编辑器用户设置
└── map.sln                           # Unity 项目解决方案文件
```

## 四季声景

| 季节 | 描述 | 代表声源 |
|------|------|---------|
| 春 Spring | 春风拂柳、鸟鸣 | 风声、鸟鸣 |
| 夏 Summer | 荷塘蛙鸣、蝉声 | 蛙鸣、蝉噪 |
| 秋 Autumn | 落叶、秋风 | 落叶声、秋风 |
| 冬 Winter | 炭火、寂静 | 炭火燃烧声 |

## 构建与运行

### iOS 构建

1. 使用 Unity 2022.3.62f3c1 打开项目
2. 打开 `Assets/Scenes/SampleScene.unity`
3. File → Build Settings → iOS → Build
4. 部署到 Xcode 进行真机调试

### Web 端声景档案

`Assets/StreamingAssets/index.html` 为独立的御花园声景档案网页，可独立部署。

## 研究背景

本项目为清华大学建筑学院研究生课题研究成果，探讨：
- 视听联觉认知映射
- 声景档案化设计
- 沉浸式交互体验

## 作者

- 张鸿翼（Elias）/ zhanghongyi25
- 清华大学建筑学院

## 许可证

本项目仅供学术研究使用，涉及的第三方资源（字体、插件等）遵循其各自的许可证。
