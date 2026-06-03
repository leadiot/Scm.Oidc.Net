# 🔐 Scm.Oidc - 多平台联合登录 SDK

> 基于 OpenID Connect 的轻量、易用的多平台联合登录解决方案

![登录界面](screenshots/home.png)

---

## 📋 目录

- [功能特点](#功能特点)
- [支持平台](#支持平台)
- [项目结构](#项目结构)
- [安装方式](#安装方式)
- [快速开始](#快速开始)
- [API 参考](#api-参考)
- [配置说明](#配置说明)
- [使用示例](#使用示例)
- [参与贡献](#参与贡献)
- [许可证](#许可证)

---

## ✨ 功能特点

| 特性 | 说明 |
|------|------|
| **多协议支持** | 同时支持 OAuth1.0、OAuth2.0 协议 |
| **多平台覆盖** | 支持国内外近百家主流平台登录授权 |
| **隐私安全** | 不存储用户隐私数据，保护用户信息安全 |
| **多模式支持** | 支持服务端模式（Authorization Code）和客户端模式 |
| **验证码登录** | 支持手机、邮箱验证码登录 |
| **PKCE 安全** | 内置 PKCE 协议支持，提升授权安全性 |
| **跨平台兼容** | 支持 .NET Standard 2.0，可用于 Web、WPF 等多种项目 |

---

## 🌍 支持平台

### 国内平台
- 支付宝 · 阿里云 · 微信 · QQ · 微博 · 百度 · 抖音 · 钉钉 · 飞书
- 华为 · 小米 · 京东 · 美团 · 快手 · 小红书 · WPS · 印象笔记 · 钉钉
- 360 · 魅族 · OPPO · Vivo · 荣耀 · 开放原子 · DCloud

### 国际平台
- Google · Apple · Microsoft · Facebook · X（Twitter）· LinkedIn · GitHub
- GitLab · Gitee · Bitbucket · StackOverflow · Atlassian · Slack
- Amazon · Yahoo · Yandex · VK.ru · Ok.ru · Mail.ru · Line
- Notion · Figma · Linear · Zoom · Discord · Adobe · Dropbox
- Zoho · Box · Todoist · Tower · Evernote · Coding · Teambition

> 更多平台支持持续更新中...

---

## 📁 项目结构

```
Scm.Oidc.Net/
├── Scm.Oidc.Client/          # OIDC 客户端 SDK 核心库
│   ├── Enums/               # 枚举定义
│   ├── Exceptions/          # 异常处理
│   ├── Models/              # 数据模型
│   ├── Response/            # API 响应模型
│   ├── OidcClient.cs        # 核心客户端类
│   ├── OidcConfig.cs        # 配置类
│   └── PkceObject.cs        # PKCE 安全对象
├── Scm.Oidc.Web/            # ASP.NET Core Web 示例项目
├── Scm.Oidc.Wpf/            # WPF 桌面应用示例项目
├── Test/                    # 测试项目
├── Libs/                    # 依赖库
├── release/                 # 发布包
└── screenshots/             # 截图资源
```

---

## 📦 安装方式

### 方式一：NuGet 安装（推荐）

```bash
Install-Package Scm.Oidc.Client
```

或使用 .NET CLI：

```bash
dotnet add package Scm.Oidc.Client
```

### 方式二：手动引用

1. 下载 `release` 目录下的最新版本 DLL
2. 在项目中添加对 `Scm.Oidc.Client.dll` 的引用

---

## 🚀 快速开始

### 1. 初始化客户端

```csharp
// 使用演示配置（快速体验）
var config = new OidcConfig();
config.UseDemo();

// 或使用自定义配置
var config = new OidcConfig
{
    AppKey = "your_app_key",           // 应用 ID
    AppSecret = "your_app_secret",     // 应用密钥
    RedirectUrl = "https://your-domain.com/callback"  // 回调地址
};

var client = new OidcClient(config);
```

### 2. 获取服务商列表

```csharp
// 获取所有支持的服务商
var allOsp = await client.ListAllOspAsync();

// 获取当前应用配置的服务商
var appOsp = await client.ListAppOspAsync();
```

### 3. OAuth 服务端模式登录

```csharp
// 生成授权 URL，引导用户跳转
string authUrl = client.GetAuthorizeAUrl(state: "custom_state", scope: "user_info");

// 用户授权后，通过回调获取 code，换取访问令牌
var tokenResponse = await client.AccessTokenAsync(code);
string accessToken = tokenResponse.AccessToken;
```

### 4. 获取用户信息

```csharp
var userInfo = await client.GetUserInfoAsync(accessToken);
```

---

## 📖 API 参考

| 方法 | 说明 | 参数 | 返回值 |
|------|------|------|--------|
| `ListAllOspAsync()` | 获取所有服务商列表 | 无 | `List<OidcOspInfo>` |
| `ListAppOspAsync()` | 获取应用配置的服务商 | 无 | `List<OidcOspInfo>` |
| `GetWebUrl(responseType, state)` | 获取标准网页登录地址 | `responseType`, `state` | `string` |
| `GetAuthorizeAUrl(state, scope)` | 服务端模式授权地址 | `state`, `scope` | `string` |
| `AccessTokenAsync(code)` | 换取访问令牌 | `code` | `AccessTokenResponse` |
| `HandshakeAsync(state)` | 握手（客户端模式） | `state` | `HandshakeResponse` |
| `ListenAsync(ticket)` | 监听授权状态 | `ticket` | `ListenResponse` |
| `SendSmsAsync(type, code, requestId)` | 发送验证码 | `type`, `code`, `requestId` | `SendSmsResponse` |
| `VerifySmsAAsync(key, sms)` | 校验验证码（服务端） | `key`, `sms` | `VerifySmsResponse` |
| `VerifySmsBAsync(ticket, key, sms)` | 校验验证码（客户端） | `ticket`, `key`, `sms` | `VerifySmsResponse` |
| `GetUserInfoAsync(accessToken)` | 获取用户信息 | `accessToken` | `UserInfoResponse` |
| `RefreshTokenAsync(accessToken, refreshToken)` | 刷新令牌 | `accessToken`, `refreshToken` | `RefreshTokenResponse` |
| `HeartBeatAsync(accessToken, type, data)` | 心跳检测（客户端） | `accessToken`, `type`, `data` | `HeartBeatResponse` |

---

## ⚙️ 配置说明

### OidcConfig 配置项

| 属性 | 类型 | 必填 | 说明 |
|------|------|------|------|
| `AppKey` | `string` | 是 | 应用 ID，在 OIDC 平台注册获取 |
| `AppSecret` | `string` | 是 | 应用密钥，在 OIDC 平台注册获取 |
| `RedirectUrl` | `string` | 服务端模式必填 | 授权回调地址 |
| `Mode` | `int` | 否 | 应用模式（暂未使用） |

### 快捷配置方法

```csharp
var config = new OidcConfig();

// 使用演示配置（用于测试）
config.UseDemo();

// 使用测试配置
config.UseTest();
```

---

## 💡 使用示例

### 示例 1：完整的服务端模式登录流程

```csharp
// 1. 初始化客户端
var config = new OidcConfig
{
    AppKey = "your_app_key",
    AppSecret = "your_app_secret",
    RedirectUrl = "https://example.com/callback"
};
var client = new OidcClient(config);

// 2. 生成授权 URL
string authUrl = client.GetAuthorizeAUrl(state: "my_state", scope: "user_info");

// 3. 用户访问授权 URL 并授权后，获取 code
// （此步骤由浏览器跳转完成）

// 4. 使用 code 换取令牌
var tokenResponse = await client.AccessTokenAsync(code);

// 5. 使用 accessToken 获取用户信息
var userInfo = await client.GetUserInfoAsync(tokenResponse.AccessToken);

// 6. 令牌过期时刷新
var refreshResponse = await client.RefreshTokenAsync(
    tokenResponse.AccessToken, 
    tokenResponse.RefreshToken
);
```

### 示例 2：客户端模式登录（桌面应用）

```csharp
// 1. 初始化客户端
var config = new OidcConfig();
config.UseDemo();
var client = new OidcClient(config);

// 2. 握手获取 ticket
var handshake = await client.HandshakeAsync(state: "app_state");

// 3. 生成授权 URL（在嵌入式浏览器中打开）
string authUrl = client.GetAuthorizeBUrl(handshake.Ticket.Code);

// 4. 轮询监听授权状态
var listenResponse = await client.ListenAsync(handshake.Ticket);

// 5. 获取用户信息
var userInfo = await client.GetUserInfoAsync(listenResponse.AccessToken);
```

### 示例 3：验证码登录

```csharp
// 发送手机验证码
var smsResponse = await client.SendSmsAsync(
    OidcSmsEnums.Phone, 
    "13800138000"
);

// 校验验证码（服务端模式）
var verifyResponse = await client.VerifySmsAAsync(
    smsResponse.Key, 
    "123456"
);
```

---

## 🖼️ 界面预览

| 验证登录界面 | 授权登录界面 | 授权成功界面 |
|-------------|-------------|-------------|
| ![验证登录](screenshots/vcode.png) | ![授权登录](screenshots/oauth.png) | ![授权成功](screenshots/success.png) |

---

## 🤝 参与贡献

欢迎提交 Issue 和 Pull Request！

### 贡献流程

1. **Fork** 本仓库
2. 创建 `Feat_xxx` 或 `Fix_xxx` 分支
3. 提交代码
4. 创建 Pull Request

### 开发规范

- 代码风格遵循 .NET 官方规范
- 新增功能请添加对应的单元测试
- 提交前请确保代码编译通过

---

## 📄 许可证

本项目采用 **MIT 许可证**，详见 [LICENSE](LICENSE) 文件。

---

## 📮 联系方式

- 官网：[https://www.oidc.org.cn](https://www.oidc.org.cn)
- QQ群：121750370
- 问题反馈：提交 Issue

---

## 🙏 致谢

感谢所有为项目做出贡献的开发者！

---

**版本**: 1.2.4  
**更新时间**: 2024年