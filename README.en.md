# 🔐 Scm.Oidc - Multi-Platform Federation Login SDK

> A lightweight and easy-to-use multi-platform federated login solution based on OpenID Connect

![Login Interface](screenshots/home.png)

---

## ✨ Features

| Feature | Description |
|---------|-------------|
| **Multi-protocol Support** | Supports both OAuth1.0 and OAuth2.0 protocols |
| **Multi-platform Coverage** | Supports nearly 100 mainstream platforms worldwide |
| **Privacy Protection** | No user privacy data stored |
| **Multi-mode Support** | Supports server-side (Authorization Code) and client-side modes |
| **SMS Verification** | Supports phone and email verification code login |
| **PKCE Security** | Built-in PKCE protocol support for enhanced security |
| **Cross-platform** | Supports .NET Standard 2.0, works with Web, WPF and more |

---

## 🌍 Supported Platforms

### China Platforms
- Alipay · Alibaba Cloud · WeChat · QQ · Weibo · Baidu · Douyin · DingTalk · Feishu
- Huawei · Xiaomi · JD.com · Meituan · Kuaishou · Xiaohongshu · WPS · Evernote
- 360 · Meizu · OPPO · Vivo · Honor · OpenAtom · DCloud

### International Platforms
- Google · Apple · Microsoft · Facebook · X (Twitter) · LinkedIn · GitHub
- GitLab · Gitee · Bitbucket · StackOverflow · Atlassian · Slack
- Amazon · Yahoo · Yandex · VK.ru · Ok.ru · Mail.ru · Line
- Notion · Figma · Linear · Zoom · Discord · Adobe · Dropbox
- Zoho · Box · Todoist · Tower · Evernote · Coding · Teambition

> More platforms coming soon...

---

## 🖼️ Screenshots

| Verification Login | OAuth Login | Success |
|-------------------|-------------|---------|
| ![Verification](screenshots/vcode.png) | ![OAuth](screenshots/oauth.png) | ![Success](screenshots/success.png) |

---

## 📁 Project Structure

```
Scm.Oidc.Net/
├── Scm.Oidc.Client/          # OIDC Client SDK Core Library
│   ├── Enums/               # Enum Definitions
│   ├── Exceptions/          # Exception Handling
│   ├── Models/              # Data Models
│   ├── Response/            # API Response Models
│   ├── OidcClient.cs        # Core Client Class
│   ├── OidcConfig.cs        # Configuration Class
│   └── PkceObject.cs        # PKCE Security Object
├── Scm.Oidc.Web/            # ASP.NET Core Web Sample
├── Scm.Oidc.Wpf/            # WPF Desktop Application Sample
├── Test/                    # Test Project
├── Libs/                    # Dependencies
├── release/                 # Release Packages
└── screenshots/             # Screenshots
```

---

## 📦 Installation

### Method 1: NuGet Package (Recommended)

```bash
Install-Package Scm.Oidc.Client
```

Or using .NET CLI:

```bash
dotnet add package Scm.Oidc.Client
```

### Method 2: Manual Reference

1. Download the latest DLL from the `release` directory
2. Add reference to `Scm.Oidc.Client.dll` in your project

---

## 🚀 Quick Start

### 1. Initialize Client

```csharp
// Use demo configuration (quick start)
var config = new OidcConfig();
config.UseDemo();

// Or use custom configuration
var config = new OidcConfig
{
    AppKey = "your_app_key",           // Application ID
    AppSecret = "your_app_secret",     // Application Secret
    RedirectUrl = "https://your-domain.com/callback"  // Callback URL
};

var client = new OidcClient(config);
```

### 2. Get Service Provider List

```csharp
// Get all supported providers
var allOsp = await client.ListAllOspAsync();

// Get app configured providers
var appOsp = await client.ListAppOspAsync();
```

### 3. OAuth Server-side Login

```csharp
// Generate authorization URL
string authUrl = client.GetAuthorizeAUrl(state: "custom_state", scope: "user_info");

// After user authorization, exchange code for token
var tokenResponse = await client.AccessTokenAsync(code);
string accessToken = tokenResponse.AccessToken;
```

### 4. Get User Info

```csharp
var userInfo = await client.GetUserInfoAsync(accessToken);
```

---

## 📖 API Reference

| Method | Description | Parameters | Return Value |
|--------|-------------|------------|--------------|
| `ListAllOspAsync()` | Get all service providers | None | `List<OidcOspInfo>` |
| `ListAppOspAsync()` | Get app configured providers | None | `List<OidcOspInfo>` |
| `GetWebUrl(responseType, state)` | Get standard web login URL | `responseType`, `state` | `string` |
| `GetAuthorizeAUrl(state, scope)` | Server-side authorization URL | `state`, `scope` | `string` |
| `AccessTokenAsync(code)` | Exchange code for access token | `code` | `AccessTokenResponse` |
| `HandshakeAsync(state)` | Handshake (client-side mode) | `state` | `HandshakeResponse` |
| `ListenAsync(ticket)` | Listen for authorization status | `ticket` | `ListenResponse` |
| `SendSmsAsync(type, code, requestId)` | Send verification code | `type`, `code`, `requestId` | `SendSmsResponse` |
| `VerifySmsAAsync(key, sms)` | Verify code (server-side) | `key`, `sms` | `VerifySmsResponse` |
| `VerifySmsBAsync(ticket, key, sms)` | Verify code (client-side) | `ticket`, `key`, `sms` | `VerifySmsResponse` |
| `GetUserInfoAsync(accessToken)` | Get user information | `accessToken` | `UserInfoResponse` |
| `RefreshTokenAsync(accessToken, refreshToken)` | Refresh access token | `accessToken`, `refreshToken` | `RefreshTokenResponse` |
| `HeartBeatAsync(accessToken, type, data)` | Heartbeat check | `accessToken`, `type`, `data` | `HeartBeatResponse` |

---

## ⚙️ Configuration

### OidcConfig Options

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `AppKey` | `string` | Yes | Application ID, obtained from OIDC platform |
| `AppSecret` | `string` | Yes | Application secret, obtained from OIDC platform |
| `RedirectUrl` | `string` | Yes (Server-mode) | Authorization callback URL |
| `Mode` | `int` | No | Application mode (not used yet) |

### Quick Configuration

```csharp
var config = new OidcConfig();

// Use demo configuration (for testing)
config.UseDemo();

// Use test configuration
config.UseTest();
```

---

## 💡 Usage Examples

### Example 1: Complete Server-side Login Flow

```csharp
// 1. Initialize client
var config = new OidcConfig
{
    AppKey = "your_app_key",
    AppSecret = "your_app_secret",
    RedirectUrl = "https://example.com/callback"
};
var client = new OidcClient(config);

// 2. Generate authorization URL
string authUrl = client.GetAuthorizeAUrl(state: "my_state", scope: "user_info");

// 3. User visits auth URL and authorizes (browser redirect)

// 4. Exchange code for token
var tokenResponse = await client.AccessTokenAsync(code);

// 5. Get user info
var userInfo = await client.GetUserInfoAsync(tokenResponse.AccessToken);

// 6. Refresh token when expired
var refreshResponse = await client.RefreshTokenAsync(
    tokenResponse.AccessToken, 
    tokenResponse.RefreshToken
);
```

### Example 2: Client-side Login (Desktop App)

```csharp
// 1. Initialize client
var config = new OidcConfig();
config.UseDemo();
var client = new OidcClient(config);

// 2. Handshake to get ticket
var handshake = await client.HandshakeAsync(state: "app_state");

// 3. Generate authorization URL (open in embedded browser)
string authUrl = client.GetAuthorizeBUrl(handshake.Ticket.Code);

// 4. Poll for authorization status
var listenResponse = await client.ListenAsync(handshake.Ticket);

// 5. Get user info
var userInfo = await client.GetUserInfoAsync(listenResponse.AccessToken);
```

### Example 3: SMS Verification Login

```csharp
// Send SMS verification code
var smsResponse = await client.SendSmsAsync(
    OidcSmsEnums.Phone, 
    "13800138000"
);

// Verify code (server-side)
var verifyResponse = await client.VerifySmsAAsync(
    smsResponse.Key, 
    "123456"
);
```

---

## 🤝 Contributing

Contributions are welcome!

### Contribution Workflow

1. **Fork** this repository
2. Create `Feat_xxx` or `Fix_xxx` branch
3. Commit your code
4. Create Pull Request

### Development Guidelines

- Follow .NET official coding standards
- Add unit tests for new features
- Ensure code compiles before submitting

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## 📮 Contact

- Website: [https://www.oidc.org.cn](https://www.oidc.org.cn)
- QQ Group: 121750370
- Issues: Submit Issue

---

## 🙏 Acknowledgments

Thanks to all contributors!

---

**Version**: 1.2.4  
**Updated**: 2024