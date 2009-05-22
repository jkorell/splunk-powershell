$c = Get-Credential
Get-Splunk -s localhost:8089 -l $c.UserName -p $c.GetNetworkCredential().Password -q "something" -f _raw,_time,sourcetype -verbose