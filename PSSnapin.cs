using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.ComponentModel;

namespace SplunkPowerShell
{
    [RunInstaller(true)]
    public class SplunkPSSnapIn : PSSnapIn
    {
        public override string Name
        {
            get { return "splunk_test"; }
        }
        public override string Vendor
        {
            get { return "Splunk"; }
        }
        public override string VendorResource
        {
            get { return "Splunk"; }
        }
        public override string Description
        {
            get { return "Auths at Splunk"; }
        }
        public override string DescriptionResource
        {
            get { return ""; }
        }
    }
}
