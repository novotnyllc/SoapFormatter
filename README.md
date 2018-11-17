SoapFormatter
=============

This is the `SoapFormatter` and related classes taken from [Mono](https://github.com/mono/mono/tree/master/mcs/class/System.Runtime.Serialization.Formatters.Soap) and compiled
for .NET Standard 2.0. It's goal is to enable serializing and deserializing SOAP
XML for applications that have a dependency on that legacy format.

[![Build Status](https://dev.azure.com/onovotny/GitBuilds/_apis/build/status/SoapFormatter%20-%20CI?branchName=master)](https://dev.azure.com/onovotny/GitBuilds/_build/latest?definitionId=45)
[![NuGet](https://img.shields.io/nuget/v/SoapFormatter.svg)](https://www.nuget.org/packages/SoapFormatter)

## Compiling
See the `sln` file in `mcs/class/System.Runtime.Serialization.Formatters.Soap`.

## Limitations
.NET Core does not support serialzing all of the same types that .NET Framework does.
Noteably, Delegates, `Type`, and `Assembly` are not supported.