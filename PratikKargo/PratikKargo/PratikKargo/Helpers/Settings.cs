using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PratikKargo.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>("Username", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>("Username", value);
            }
        }
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>("Password", value);
            }
        }
        public static string Uid
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>("Uid", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>("Uid", value);
            }
        }

    

    }
}
