# Authentication

The authentication project provides the link to whichever service will authenticate and authorise a user.

## Create Project

Go to the *Solution Explorer > src > infrastructure* folder, right clock and go *Add > New Project...*
Find the *Class Library (.NET Core) project template and click Next.

## Naming
Give the project a name along the lines of
> Company.Product.Authentication
The *Compnay.Product* portion should match the solution name.
This name may be made more specific if there will be multiple authentication projects, e.g., Company.Products.Authorisation.Auth0

## Location
Do not use the default location, as this will place the project at the root of the repository.
Instead select the src/infrastructure directory.

## .csproj
If the TargetFramework is specified in the Directory.Build.props then the TargetFramework in the .csproj can be removed.
