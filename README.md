# Scm.Oidc

#### 介绍
OIDC(OpenID Connect)：简单、易用的多平台联合登录系统。

![验证登录界面](screenshots/home.png)

OIDC同时支持OAuth1.0、OAuth2.0协议，支持国内外近百家大型网站的登录与授权：
支付宝、阿里云、Apple Inc.、百度、Coding、钉钉、抖音、Facebook、飞书、Gitee、Github、GitLab、Google、华为、京东、快手、领英、美团、小米、微软、OSChina、QQ、微博、StackOverflow、Teambition、微信、WPS、小红书、迅雷、OPPO、Vivo、荣耀、印象笔记、开放原子、魅族、360、Evernote、Amazon、Slack、Yahoo、X、Yandex、VK.ru、Ok.ru、Mail.ru、DCloud、Notion、Figma、Atlassian、Line、Linear、Zoom、Discord、Bitbucket、Adobe、Dropbox、Zoho、Box、Todoist、Tower，更多支持还在持续更新中……

您是否遇到过这样的场景？

1.  由于应用/网站太多，根本记不住每个应用/网站的用户和密码信息？
2.  很多网站使用相同的用户/密码，很不安全，有没有更好的解决方案？
3.  我的应用程序需要使用第三方登录功能，一个个集成太过于麻烦？
4.  某些大的应用/网站的开放平台需要严苛的身份资质，而且注册认证成本过高？
5.  使用三方的密码管理软件，又担心重要信息出现泄露？
6.  其它更多场景……

那有没有一个更简易的解决方案？

这就是OIDC所要解决的问题，我们致力于为用户提供一个简单易用的三方授权认证平台，让用户免去高强度记忆用户和密码的烦恼。

OIDC的特点：
1.  基于OAuth协议；
2.  不记忆用户隐私数据；
3.  支持多平台身份认证/授权；
4.  拿来即用，用完即走，不增加用户负担。

在这里，你可以使用多个平台的身份认证/授权的功能，而不需要花费巨量精力去单独对接各个平台。

#### 软件架构
此项目是OIDC（OpenID Connect）官网的.Net使用案例，包含服务端和客户端的示例代码，用于向使用者演示如何快速调用OIDC的服务，并为终端用户提供一个简单易用的用户登录功能。

**项目结构：**
- `Scm.Oidc.Client` - OIDC客户端SDK核心库
- `Scm.Oidc.Web` - ASP.NET Core Web示例项目
- `Scm.Oidc.Wpf` - WPF桌面应用示例项目
- `Test` - 测试项目

#### 安装说明

**方式一：使用NuGet包（推荐）**
```bash
Install-Package Scm.Oidc.Client
```

**方式二：手动引用**
1. 下载release目录下的最新版本DLL文件
2. 在项目中添加对 `Scm.Oidc.Client.dll` 的引用

#### 使用说明

**1. 初始化客户端**
```csharp
// 使用演示配置
var config = new OidcConfig();
config.UseDemo();

// 或使用自定义配置
var config = new OidcConfig
{
    AppKey = "your_app_key",
    AppSecret = "your_app_secret",
    RedirectUrl = "https://your-domain.com/callback"
};

var client = new OidcClient(config);
```

**2. 获取服务商列表**
```csharp
// 获取所有支持的服务商
var ospList = await client.ListAllOspAsync();

// 获取应用配置的服务商
var appOspList = await client.ListAppOspAsync();
```

**3. OAuth服务端模式登录**
```csharp
// 生成授权URL
string authUrl = client.GetAuthorizeAUrl(state: "custom_state", scope: "user_info");

// 用户授权后，通过回调获取code，换取访问令牌
var tokenResponse = await client.AccessTokenAsync(code);
string accessToken = tokenResponse.AccessToken;
```

**4. OAuth客户端模式登录**
```csharp
// 握手获取ticket
var handshake = await client.HandshakeAsync(state: "custom_state");
string ticket = handshake.Ticket.Code;

// 生成授权URL（在浏览器中打开）
string authUrl = client.GetAuthorizeBUrl(ticket);

// 轮询监听授权状态
var listenResponse = await client.ListenAsync(handshake.Ticket);
```

**5. 验证码登录**
```csharp
// 发送验证码
var smsResponse = await client.SendSmsAsync(OidcSmsEnums.Phone, "13800138000");

// 校验验证码
var verifyResponse = await client.VerifySmsAAsync(smsResponse.Key, "123456");
```

**6. 获取用户信息**
```csharp
var userInfo = await client.GetUserInfoAsync(accessToken);
```

**7. 刷新令牌**
```csharp
var refreshResponse = await client.RefreshTokenAsync(accessToken, refreshToken);
```

#### 服务端效果
请移步这里查看演示：[演示链接](http://demo.oidc.org.cn)

客户端效果：
![验证登录界面](screenshots/vcode.png)
![授权登录界面](screenshots/oauth.png)
![授权成功界面](screenshots/success.png)

#### API参考

| 方法 | 说明 | 参数 | 返回值 |
|------|------|------|--------|
| `ListAllOspAsync` | 获取所有服务商 | 无 | `List<OidcOspInfo>` |
| `ListAppOspAsync` | 获取应用服务商 | 无 | `List<OidcOspInfo>` |
| `GetWebUrl` | 获取标准网页登录地址 | `responseType`, `state` | `string` |
| `GetAuthorizeAUrl` | 服务端模式授权地址 | `state`, `scope` | `string` |
| `AccessTokenAsync` | 换取访问令牌 | `code` | `AccessTokenResponse` |
| `HandshakeAsync` | 握手（客户端模式） | `state` | `HandshakeResponse` |
| `ListenAsync` | 监听授权状态 | `ticket` | `ListenResponse` |
| `SendSmsAsync` | 发送验证码 | `type`, `code`, `requestId` | `SendSmsResponse` |
| `VerifySmsAAsync` | 校验验证码（服务端） | `key`, `sms` | `VerifySmsResponse` |
| `GetUserInfoAsync` | 获取用户信息 | `accessToken` | `UserInfoResponse` |
| `RefreshTokenAsync` | 刷新令牌 | `accessToken`, `refreshToken` | `RefreshTokenResponse` |

#### 配置说明

**OidcConfig配置项：**
- `AppKey` - 应用ID（必填）
- `AppSecret` - 应用密钥（必填）
- `RedirectUrl` - 回调地址（服务端模式必填）
- `Mode` - 应用模式（暂未使用）

#### 参与贡献

1. Fork 本仓库
2. 创建 Feat_xxx 分支
3. 提交代码
4. 创建 Pull Request

#### 许可证
本项目采用 MIT 许可证，详见 LICENSE 文件。

#### 联系方式

QQ沟通群：
![QQ沟通群](qq.jpg)

网站首页：[http://www.oidc.org.cn](http://www.oidc.org.cn)