A small example of how you can use Splunk .NET SDK to create a powershell snap-in.

# Getting started #

  * Download and install powershell and .Net framework
> http://go.microsoft.com/fwlink/?LinkId=79444

> _how-to_:
> http://msdn.microsoft.com/en-us/library/bb204630(VS.85).aspx

  * Extract contents of the attached package in some temp directory
> c:\temp

  * Open powershell and register the Splunk snap-in by executing this command:
```
  c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\installutil.exe c:\temp\SplunkPowerShell.dll
```

  * Check that registration was successful. In powershell, type:
```
  get-pssnapin -registered
```

> make sure 'splunk\_test' is in there

  * Tell powershell that it's ok to run our unsigned script:
```
  Set-ExecutionPolicy RemoteSigned
```

  * To add the snapin to the powershell type:
```
  add-pssnapin splunk_test
```

  * Test it. run.ps1 is a sample batch file that makes a search for 'something' on localhost:
```
  ./run.ps1
```

> or type:
```
  Get-Splunk -s localhost:8089 -l admin -p changeme -q "searchString" -f _raw,_time,sourcetype -verbose
```

This should return 3 fields from the search results that you can pipe
anywhere else using powershell.

Enjoy