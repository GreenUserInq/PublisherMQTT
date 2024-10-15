using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherMQTT.Common.Models
{
    class InternetConfig
    {
        public InternetConfig(string ssid, string pass) 
        { 
            SSID = ssid;
            Password = pass;
        }

        public InternetConfig() { }

        public string SSID { get;  set; }
        public string Password { get; set; }
    }
}
