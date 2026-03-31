# 御花园声景地图 | Imperial Garden Sound Map

一个基于 Unity 开发的沉浸式御花园声景交互地图，支持四季声景体验与空间音频定位。

An immersive interactive sound map of the Imperial Garden built with Unity, featuring seasonal soundscapes and spatial audio positioning.

---

## 项目简介 | Project Overview

本项目为清华大学建筑学院「设计课-认知迁移」课程研究成果，通过 Unity 构建可交互的御花园声景地图，让用户在虚拟环境中体验御花园的四季声音景观。

This project is a research outcome from the Tsinghua University School of Architecture's "Design Studio — Cognitive Transfer" course. Built with Unity, it offers an interactive sound map of the Imperial Garden, allowing users to experience the seasonal soundscapes of the garden in a virtual environment.

---

## 技术栈 | Tech Stack

| 技术 Technology | 说明 Description |
|----------------|-----------------|
| Unity 2022.3.62f3c1 | 游戏引擎 Game Engine |
| Unity XR Interaction Toolkit | XR 交互工具包 |
| Hear XR | 空间音频与头部追踪 Spatial Audio & Head Tracking |
| TextMesh Pro | 高质量文本渲染 |
| Joystick Pack | 移动端虚拟摇杆 |
| Mapbox / Custom Map | 地图系统 |

---

## 四季声景 | Seasonal Soundscapes

| 季节 Season | 中文描述 | English Description | 代表声源 Representative Sounds |
|-------------|---------|---------------------|------------------------------|
| 春 Spring | 春风拂柳、鸟鸣啁啾 | Spring breeze, birdsong | 风声、鸟鸣 wind, birds |
| 夏 Summer | 荷塘蛙鸣、蝉声阵阵 | Lotus pond frogs, cicadas | 蛙鸣、蝉噪 frogs, cicadas |
| 秋 Autumn | 落叶沙沙、秋风萧瑟 | Rustling leaves, autumn wind | 落叶声、秋风 leaves, wind |
| 冬 Winter | 炭火噼啪、万籁寂静 | Crackling charcoal, deep silence | 炭火燃烧声 charcoal fire |

---

## 目录结构 | Directory Structure

```
map/
├── Assets/
│   ├── AppFlowManager.cs          # 应用流程管理 Application Flow
│   ├── MapManager.cs              # 地图管理器 Map Manager
│   ├── PlayerController.cs        # 玩家控制器 Player Controller
│   ├── SoundPoint.cs               # 声景点组件 Sound Point Component
│   ├── UIManager.cs                # UI 管理
│   ├── SideMenuController.cs      # 侧边菜单 Side Menu
│   ├── TabController.cs           # 标签页控制器 Tab Controller
│   ├── CameraFollow.cs            # 相机跟随 Camera Follow
│   ├── CameraZoom.cs              # 相机缩放 Camera Zoom
│   ├── IconBreathing.cs           # 图标呼吸动画 Icon Breathing Animation
│   ├── LibraryManager.cs          # 声库管理 Sound Library Manager
│   ├── Audio/                     # 音频文件 Audio Files
│   │   ├── History/               # 历史声景（冬）Historical (Winter)
│   │   ├── SpringSummer/          # 春夏声景 Spring & Summer
│   │   └── Winter/                # 冬季声景 Winter
│   ├── Image/                     # 图片资源 Images
│   ├── Fonts/                     # 字体 Fonts（思源宋体 Noto Serif）
│   ├── Hear XR/                   # XR 音频插件 Spatial Audio Plugin
│   ├── Joystick Pack/             # 摇杆控制 Joystick Controls
│   ├── TextMesh Pro/              # 文本渲染 Text Rendering
│   ├── XRI/                       # XR 交互工具包 XR Interaction Toolkit
│   ├── Scenes/                   # 场景文件 Unity Scenes
│   └── StreamingAssets/           # Web 端声景档案
│       └── index.html             # 御花园声景档案（可独立部署）
├── ProjectSettings/               # Unity 项目设置
├── Packages/                      # Unity 包依赖
├── UserSettings/                 # 编辑器用户设置
└── map.sln                       # Unity 项目解决方案文件
```

---

## 构建与运行 | Build & Run

### iOS 构建 | iOS Build

1. 使用 Unity 2022.3.62f3c1 打开项目 / Open project in Unity 2022.3.62f3c1
2. 打开 `Assets/Scenes/SampleScene.unity`
3. File → Build Settings → iOS → Build
4. 部署到 Xcode 进行真机调试 / Deploy to Xcode for device testing

### Web 端声景档案 | Web Sound Archive

`Assets/StreamingAssets/index.html` 为独立的御花园声景档案网页，可独立部署。

`Assets/StreamingAssets/index.html` is a standalone web-based sound archive of the Imperial Garden, deployable independently.

---

## 研究背景 | Research Background

本项目为清华大学建筑学院研究生课题研究成果，探讨：
- 视听联觉认知映射 / Audiovisual Synesthetic Cognitive Mapping
- 声景档案化设计 / Soundscape Archival Design
- 沉浸式交互体验 / Immersive Interactive Experience

---

## 作者 | Author

**张鸿翼 (Elias)** / zhanghongyi25
清华大学建筑学院 | School of Architecture, Tsinghua University

---

## 许可证 | License

本项目仅供学术研究使用。涉及的第三方资源（字体、插件等）遵循其各自的许可证。

This project is for academic research purposes only. Third-party resources (fonts, plugins, etc.) are subject to their respective licenses.
