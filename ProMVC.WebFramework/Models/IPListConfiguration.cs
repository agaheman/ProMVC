using System;
using System.Collections.Generic;
using System.Text;

namespace ProMVC.WebFramework.Models
{
    public class IPListConfiguration:IIPListConfiguration
    {
        public List<string> AuthorizedIPs { get; set; }
        public List<string> BlockedIPs { get; set; }
    }
}
