using System.Collections.Generic;

namespace ProMVC.WebFramework.Models
{
    public interface IIPListConfiguration
    {
        List<string> BlockedIPs { get; }
        List<string> AuthorizedIPs { get; }
    }
    public class IPListConfiguration : IIPListConfiguration
    {
        public List<string> AuthorizedIPs { get; set; }
        public List<string> BlockedIPs { get; set; }
    }
}
