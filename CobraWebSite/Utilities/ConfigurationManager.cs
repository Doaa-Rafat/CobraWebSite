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
    }
}
