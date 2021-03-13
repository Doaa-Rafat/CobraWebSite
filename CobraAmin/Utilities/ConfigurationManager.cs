using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraAmin.Utilities
{
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
