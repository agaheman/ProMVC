using System;
using System.Collections.Generic;
using System.Text;

namespace ProMVC.WebFramework.Models
{
    public class ConnectionString
    {
        public ConnectionString()
        {
        }
        public string DeveloperConnectionString { get; set; }
        public string ProductionConnectionString { get; set; }
    }
}
