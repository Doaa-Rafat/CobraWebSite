using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraWebSite.Utilities
{
    /// <summary>
    /// class to read settings from configuration in appsetting.json
    /// </summary>
    public class ConfigurationManager
    {
        public static SettingKeys settingKeys { get; set; }
    }

    public class SettingKeys
    {
        public string CobraAPIURL { get; set; }
        public string DBConnectionString { get; set; }

        public MailSettings MailSettings { get; set; }

    }
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
