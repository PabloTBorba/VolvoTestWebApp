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

In a browser of your choice, access https://localhost:7287/
In the landing page, go to the menu option "Trucks Editor".
When the page loads, you'll see the list of trucks registered - if there are any - and the option to create
a new truck on the link "New Truck". To create a new one, select this option.
Register the necessary information and click on "Create". If the truck was successfully created,
you'll be redirected to the Trucks Editor page, and the new record will appear on the list, with
two options at the right side - "Edit" and "Delete". Choose the first to change the information
of the related record and the second one to erase that truck from the database.