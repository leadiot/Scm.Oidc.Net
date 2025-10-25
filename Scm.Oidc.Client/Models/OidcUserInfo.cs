using Newtonsoft.Json;

namespace Com.Scm.Oidc.Response
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class OidcUserInfo
    {
        /// <summary>
        /// 服务代码
        /// </summary>
        [JsonProperty("osp_code")]
        public string OspCode { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        [JsonProperty("osp_name")]
        public string OspName { get; set; }
        /// <summary>
        /// 用户代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 展示姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 获取绝对头像路径
        /// </summary>
        /// <returns></returns>
        public string GetAvatarUrl()
        {
            return OidcClient.BASE_URL + Avatar;
        }
    }
}
