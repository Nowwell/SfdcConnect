# SfdcConnect

This is the continuation of the original version I put up awhile ago on an account related to a previous employer: https://github.com/seanfifepep/SfdcConnect

![.NET Build workflow](https://github.com/Nowwell/SfdcConnect/actions/workflows/dotnet.yml/badge.svg)
![.NET Framework Build workflow](https://github.com/Nowwell/SfdcConnect/actions/workflows/dotnetframework.yml/badge.svg)

This is a collection of wrapper classes for the Salesforce Apis.  Currently implemented are:

- SOAP API
- Metadata API
- Apex API
- Bulk API
- Tooling API

Partially Implemented:

- REST API

Written in C#.  Some of the code for the Bulk API implementation came from another source, I don't remember where.

There are two version of this dll in the project, one for the .NET Framework (versions 4.XX) and for the updated .NET version 5+ and .NET Core. You can change the target framework in each respective project to target which framework you need.

# Usage (.NET Framework 4.X Versions)
![.NET Framework Build workflow](https://github.com/Nowwell/SfdcConnect/actions/workflows/dotnetframework.yml/badge.svg)

The SfdcConnection object is not intended to be used by itself, it is the base class for the other API objects that handle login and logout.


## Login Examples

### Login in specifying if the environment is test or production and the api version
```C#
  SfdcConnection conn = new SfdcConnection(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;

  conn.Open();

  conn.Close();
```

### Login in specifying the Url the connection should use
```C#
  SfdcConnection conn = new SfdcConnection(string.Format("https://test.salesforce.com/services/Soap/u/{0}.0/", 36));

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;

  conn.Open();

  conn.Close();
```

### Login in asynchronouosly
Both constructors are valid for async login
```C#
  SfdcConnection conn = new SfdcConnection(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;

  conn.OpenAsync();

  int i = 0;
  while (conn.State == ConnectionState.Connecting && i < 60)
  {
      Thread.Sleep(1000);
      i++;
  }
```

### Login in asynchronouosly with Custom Callback
The custom login completed function runs after the internal login completed function so you can ensure the connection state is open in your function
```C#
  SfdcConnection conn = new SfdcConnection(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;

  conn.customLoginCompleted += YourCustomLoginCompletedFunction;

  conn.OpenAsync();
```


### Login in asynchronouosly using await
```C#
  SfdcConnection conn = new SfdcConnection(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;
  
  CancellationToken cancelToken = new CancellationToken();

  await conn.OpenAsync(cancelToken);
```

### Login in with OAuth password flow
```C#
  SfdcConnection conn = new SfdcConnection(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;
  conn.ClientId = client_id;
  conn.ClientSecret = client_secret;

  conn.Open(LoginFlow.OAuthPassword);

  conn.Close();
```

### Login in with OAuth Authorize flow
```C#
  SfdcConnection conn = new SfdcConnection(true, 36);

  conn.ClientId = client_id;
  conn.ClientSecret = client_secret;//Optional

  conn.Open(LoginFlow.OAuthDesktop, optionalState);

  string refreshToken = conn.RefreshToken;

  conn.Close();
 
```

### Login in with a OAuth Refresh Token flow
```C#
  SfdcConnection conn = new SfdcConnection(true, 36, refreshToken);

  conn.ClientId = client_id;
  conn.ClientSecret = client_secret;//Optional

  conn.Open(LoginFlow.OAuthDesktop);

  conn.Close();
 
```

## API Examples
### SOAP API Example
```C#
  SfdcSoapApi conn = new SfdcSoapApi(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;

  conn.Open(LoginFlow.SOAP);//LoginFlow.SOAP is the default value and is not required

  SfdcConnect.SoapObjects.DescribeSObjectResult result = conn.describeSObject("Contact");

  conn.Close();
```

### REST API Example
```C#
  SfdcRestApi conn = new SfdcRestApi(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;
  conn.ClientId = client_id;
  conn.ClientSecret = client_secret;

  conn.Open(LoginFlow.OAuthPassword);

  byte[] data = conn.GetBlobField("Document", "0151K0000049Z6PQAU", "body");

  using(BinaryWriter output = new BinaryWriter(File.OpenWrite(@"document.docx")))
  {
      output.Write(data, 0, data.Length);
      output.Flush();
  }
  
  conn.Close();
```

### REST API Wrapper Example
```C#
  SfdcRestApi api = new SfdcRestApi(true, 36);

  api.Username = username;
  api.Password = password;
  api.Token = token;
  api.ClientId = client_id;
  api.ClientSecret = client_secret;

  api.Open(LoginFlow.OAuthPassword);

  SfdcRestApiWrapper conn = new SfdcRestApiWrapper(api);

  QueryResult qr = conn.Query("SELECT Id, Name FROM Account");
  
  conn.Close();
```

### Metadata API Example
```C#
  SfdcMetadataApi conn = new SfdcMetadataApi(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;

  conn.Open();

  SfdcConnect.MetadataObjects.DescribeMetadataResult dmd = conn.describeMetadata(double.Parse(conn.Version));

  conn.Close();
```

### Apex API Example
```C#
  SfdcApexApi conn = new SfdcApexApi(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;

  conn.Open();

  CompileClassResult[] ccr = conn.compileClasses(new string[] { "public class TestClass12321 { }" });

  conn.Close();
```

###  Bulk API Example
```C#
  SfdcBulkApi conn = new SfdcBulkApi(true, 36);

  conn.Username = username;
  conn.Password = password;
  conn.Token = token;
  
  conn.Open();

  Job job = conn.CreateJob("Contact", ContentType.CSV, Operations.query, ConcurrencyMode.Parallel, "");

  Batch batch = conn.CreateBatch(job, "SELECT Id FROM Contact", "", "");

  conn.CloseJob(job);

  job = conn.GetJob(job);

  //Wait for the job to complete
  while (job.IsDone == false)
  {
    Thread.Sleep(2000);

    job = conn.GetJob(job);
  }

  //If the batch failed, let us know, if it didn't download the batch
  batch = conn.GetBatch(job, batch);

  if (batch.State == "Failed")
  {
    //log it
  }
  else
  {
    //There's no need to download an empty batch
    if (batch.NumberRecordsProcessed > 0)
    {
        //zip file is downloaded to path
        string path = System.IO.Path.Combine("C:\\", "Contact.zip");
        bool success = conn.GetQueryBatchResults(job, batch, path, true);
    }
  }
```

# Usage (All other .NET and .NET Core Versions)
![.NET Build workflow](https://github.com/Nowwell/SfdcConnect/actions/workflows/dotnet.yml/badge.svg)

TODO

## Login Examples

### Login with OAuth Username and Password flow
```C#
  SfdcSession conn = new SfdcSession(Environment.Production, 54);

  loginRequest request = new loginRequest();

  request.username = username;
  request.password = password + token;
  request.client_id = clientid;
  request.client_secret = clientsecret;
  
  conn.Open(LoginFlow.OAuthUsernamePassword, request);

  conn.Close();
```