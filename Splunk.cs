using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.Collections;
using Splunk;
using Splunk.Net;
using Splunk.Search;
using System.Diagnostics;
using System.Data;
using System.Collections.Specialized;

namespace SplunkPowerShell
{
    [Cmdlet(VerbsCommon.Get, "Splunk", SupportsShouldProcess = true)]
    public class Splunk : PSCmdlet
    {
        private string serverUrl;
        private string login = "admin";
        private string pass = "changeme";
        private string query;
        private string[] fields;

        #region Parameters
        
        [Parameter(Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = false,
            HelpMessage = "Splunk server url")]
        [ValidateNotNullOrEmpty]
        public string Server
        {
            get { return serverUrl; }
            set { serverUrl = value; }
        }
 
        [Parameter(
            ValueFromPipelineByPropertyName = false,
            HelpMessage = "login")]
        [ValidateNotNullOrEmpty]
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        [Parameter(
            ValueFromPipelineByPropertyName = false,
            HelpMessage = "password")]
        [ValidateNotNullOrEmpty]
        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = false,
            HelpMessage = "search query")]
        [ValidateNotNullOrEmpty]
        public string Query
        {
            get { return query; }
            set {
                if (!value.StartsWith("search "))
                    query = "search " + value;
                else
                    query = value;
                
                }
        }

        [Parameter(
            ValueFromPipelineByPropertyName = false,
            HelpMessage = "List of fields to return")]
        [ValidateNotNullOrEmpty]
        public string[] Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        #endregion

        protected override void ProcessRecord()
        {
            try
            {
                WriteVerbose(pass);
                WriteVerbose("Query: " + query);

                SplunkConnection con = new SplunkConnection(serverUrl);
                if(login.StartsWith("\\"))
                {
                    login = login.Substring(1);
                }
                string sessionKey = con.Authenticate(login, pass);
                
                WriteVerbose("SessionKey: " + sessionKey);

                SearchManager searchManager = new SearchManager(con);
//                searchManager.ControlAllJobs(JobAction.CANCEL);

                SearchJob job = searchManager.SyncSearch(query, null);

                WriteVerbose("Job ID: " + job.Id);

                EventParameters ep = new EventParameters();

                if (fields != null && fields.Length > 0)
                    ep.FieldList = fields;

                DataTable dt = job.GetEventsTable(ep);

                WriteObject(dt);

                job.Cancel();
            }
            catch (Exception e)
            {
                WriteObject(e.Message);
            }
        }
    }
}
