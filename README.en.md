# OIDC SDK

#### Description
A lightweight and easy-to-use multi-platform federated login system based on OpenID Connect (OIDC).

![Login Interface](screenshots/home.png)

OIDC supports both OAuth1.0 and OAuth2.0 protocols, enabling login and authorization for nearly a hundred major websites worldwide:
Alipay, Alibaba Cloud, Apple Inc., Baidu, Coding, DingTalk, Douyin, Facebook, Feishu, Gitee, Github, GitLab, Google, Huawei, JD.com, Kuaishou, LinkedIn, Meituan, Xiaomi, Microsoft, OSChina, QQ, Weibo, StackOverflow, Teambition, WeChat, WPS, Xiaohongshu, Xunlei, OPPO, Vivo, Honor, Evernote, OpenAtom, Meizu, 360, Amazon, Slack, Yahoo, X, Yandex, VK.ru, Ok.ru, Mail.ru, DCloud, Notion, Figma, Atlassian, Line, Linear, Zoom, Discord, Bitbucket, Adobe, Dropbox, Zoho, Box, Todoist, Tower, and more coming soon...

#### Software Architecture
This project is a .Net implementation of the OIDC (OpenID Connect) official website, containing both server and client example code. It demonstrates how to quickly integrate OIDC services and provides users with an easy-to-use login functionality.

**Project Structure:**
- `Scm.Oidc.Client` - OIDC client SDK core library
- `Scm.Oidc.Web` - ASP.NET Core Web sample project
- `Scm.Oidc.Wpf` - WPF desktop application sample project
- `Test` - Test project

#### Features
1. Built on OAuth protocol
2. No user privacy data stored
3. Multi-platform authentication/authorization support
4. Ready to use, no additional burden

#### Installation

**Method 1: Using NuGet (Recommended)**
```bash
Install-Package Scm.Oidc.Client
```

**Method 2: Manual Reference**
1. Download the latest DLL from the release directory
2. Add reference to `Scm.Oidc.Client.dll` in your project

#### Usage

**1. Initialize Client**
```csharp
// Use demo configuration
var config = new OidcConfig();
config.UseDemo();

// Or use custom configuration
var config = new OidcConfig
{
    AppKey = "your_app_key",
    AppSecret = "your_app_secret",
    RedirectUrl = "https://your-domain.com/callback"
};

var client = new OidcClient(config);
```

**2. Get Service Provider List**
```csharp
// Get all supported providers
var ospList = await client.ListAllOspAsync();

// Get app configured providers
var appOspList = await client.ListAppOspAsync();
```

**3. OAuth Server-Side Login**
```csharp
// Generate authorization URL
string authUrl = client.GetAuthorizeAUrl(state: "custom_state", scope: "user_info");

// After user authorization, exchange code for access token
var tokenResponse = await client.AccessTokenAsync(code);
string accessToken = tokenResponse.AccessToken;
```

**4. OAuth Client-Side Login**
```csharp
// Handshake to get ticket
var handshake = await client.HandshakeAsync(state: "custom_state");
string ticket = handshake.Ticket.Code;

// Generate authorization URL (open in browser)
string authUrl = client.GetAuthorizeBUrl(ticket);

// Poll for authorization status
var listenResponse = await client.ListenAsync(handshake.Ticket);
```

**5. SMS Verification Login**
```csharp
// Send verification code
var smsResponse = await client.SendSmsAsync(OidcSmsEnums.Phone, "13800138000");

// Verify verification code
var verifyResponse = await client.VerifySmsAAsync(smsResponse.Key, "123456");
```

**6. Get User Info**
```csharp
var userInfo = await client.GetUserInfoAsync(accessToken);
```

**7. Refresh Token**
```csharp
var refreshResponse = await client.RefreshTokenAsync(accessToken, refreshToken);
```

#### API Reference

| Method | Description | Parameters | Return Value |
|--------|-------------|------------|--------------|
| `ListAllOspAsync` | Get all service providers | None | `List<OidcOspInfo>` |
| `ListAppOspAsync` | Get app configured providers | None | `List<OidcOspInfo>` |
| `GetWebUrl` | Get standard web login URL | `responseType`, `state` | `string` |
| `GetAuthorizeAUrl` | Server-side authorization URL | `state`, `scope` | `string` |
| `AccessTokenAsync` | Exchange code for access token | `code` | `AccessTokenResponse` |
| `HandshakeAsync` | Handshake (client-side) | `state` | `HandshakeResponse` |
| `ListenAsync` | Listen for authorization status | `ticket` | `ListenResponse` |
| `SendSmsAsync` | Send verification code | `type`, `code`, `requestId` | `SendSmsResponse` |
| `VerifySmsAAsync` | Verify code (server-side) | `key`, `sms` | `VerifySmsResponse` |
| `GetUserInfoAsync` | Get user information | `accessToken` | `UserInfoResponse` |
| `RefreshTokenAsync` | Refresh access token | `accessToken`, `refreshToken` | `RefreshTokenResponse` |

#### Configuration

**OidcConfig Options:**
- `AppKey` - Application ID (required)
- `AppSecret` - Application secret (required)
- `RedirectUrl` - Callback URL (required for server-side mode)
- `Mode` - Application mode (not used yet)

#### Screenshots
![Verification Code Login](screenshots/vcode.png)
![OAuth Login](screenshots/oauth.png)
![Success](screenshots/success.png)

#### Demo
Server-side demo: [Demo Link](http://demo.oidc.org.cn)

#### Contribution

1. Fork the repository
2. Create Feat_xxx branch
3. Commit your code
4. Create Pull Request

#### License
This project is licensed under the MIT License - see the LICENSE file for details.

#### Contact
Official Website: [http://www.oidc.org.cn](http://www.oidc.org.cn)