# VolvoTestWebApp 

VolvoTestWebApp is an app developed using .NET 6 (for training purposes only)

## Requirements

Since the code for this was created using .NET 6, it's required for any user to have the .NET 6 SDK installed.
Download the latest version [here.](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
The application can also be built and run from Visual Studio - given that the IDE has the ASP.NET 6 Web
Development workload.

## Installation

Perform a checkout of the branch or download the code to a folder of your choice.
Open a terminal inside the project folder - the VolvoTestWebApp project folder, contained below the solution folder/file - and run:

```bash
# to build the app
dotnet build

# it may be necessary to trust the development certificate. 
# Run the command below in order to do that
dotnet dev-certs https --trust

# to run the application
dotnet run
```

## Usage

