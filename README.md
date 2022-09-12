# IDCH.CloudSDK
This is collection of SDK for accessing IDCH Cloud with .NET Platform by Gravicode

## Example Project

Refer to the ```TestStorage``` project for exercising the library.

## Getting Started - IDCH Object Storage
```csharp
using IDCH.Storage;

IDCHSettings settings = new IDCHSettings(
	endpoint,      // https://is3.cloudhost.id/
	true,          // enable or disable SSL
	accessKey, 
	secretKey, 
	bucket,
	baseUrl        // i.e. https://is3.cloudhost.id/{bucket}/{key}
	);

Blobs blobs = new Blobs(settings); 
```

## Getting Started - Disk
```csharp
using IDCH.Storage;

DiskSettings settings = new DiskSettings("blobs"); 

Blobs blobs = new Blobs(settings);
```

## Getting Started (Byte Arrays for Smaller Objects)
```csharp
await blobs.Write("test", "text/plain", "This is some data");  // throws IOException
byte[] data = await blobs.Get("test");                         // throws IOException
bool exists = await blobs.Exists("test");
await blobs.Delete("test");
```

## Getting Started (Streams for Larger Objects)
```csharp
// Writing a file using a stream
FileInfo fi = new FileInfo(inputFile);
long contentLength = fi.Length;

using (FileStream fs = new FileStream(inputFile, FileMode.Open))
{
    await _Blobs.Write("key", "content-type", contentLength, fs);  // throws IOException
}

// Downloading to a stream
BlobData blob = await _Blobs.GetStream(key);
// read blob.ContentLength bytes from blob.Data
```

## Metadata and Enumeration
```csharp
// Get BLOB metadata
BlobMetadata md = await _Blobs.GetMetadata("key");

// Enumerate BLOBs
EnumerationResult result = await _Blobs.Enumerate();
// list of BlobMetadata contained in result.Blobs
// continuation token in result.NextContinuationToken
```

## Copying BLOBs from Repository to Repository

If you have multiple storage repositories and wish to move BLOBs from one repository to another, use the ```BlobCopy``` class (refer to the ```Test.Copy``` project for a full working example).


```csharp
Blobs from    = new Blobs(new DiskSettings("./disk1/")); // assume some files are here
Blobs to      = new Blobs(new DiskSettings("./disk2/"));
BlobCopy copy = new BlobCopy(from, to);
CopyStatistics stats = copy.Start();
/*
	{
	  "Success": true,
	  "Time": {
	    "Start": "2021-12-22T18:44:42.9098249Z",
	    "End": "2021-12-22T18:44:42.9379215Z",
	    "TotalMs": 28.1
	  },
	  "ContinuationTokens": 0,
	  "BlobsEnumerated": 12,
	  "BytesEnumerated": 1371041,
	  "BlobsRead": 12,
	  "BytesRead": 1371041,
	  "BlobsWritten": 12,
	  "BytesWritten": 1371041,
	  "Keys": [
	    "filename.txt",
	    ...
	  ]
	}
 */
```

