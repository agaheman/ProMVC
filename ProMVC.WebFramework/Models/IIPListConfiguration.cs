using System;
using System.Collections.Generic;
using System.Text;

namespace ProMVC.WebFramework.Models
{
    public interface IIPListConfiguration
    {
        List<string> BlockedIPs { get; }
        List<string> AuthorizedIPs { get; }
    }
}
