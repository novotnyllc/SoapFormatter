SoapFormatter
=============

This is the `SoapFormatter` and related classes taken from [Mono](https://github.com/mono/mono/tree/master/mcs/class/System.Runtime.Serialization.Formatters.Soap) and compiled
for .NET Standard 2.0. Its goal is to enable serializing and deserializing SOAP
XML for applications that have a dependency on that legacy format.

[![Build Status](https://dev.azure.com/clairernovotny/GitBuilds/_apis/build/status/SoapFormatter%20-%20CI?repoName=novotnyllc%2FSoapFormatter&branchName=refs%2Fpull%2F5%2Fmerge)](https://dev.azure.com/clairernovotny/GitBuilds/_build/latest?definitionId=45&repoName=novotnyllc%2FSoapFormatter&branchName=refs%2Fpull%2F5%2Fmerge)
[![NuGet](https://img.shields.io/nuget/v/SoapFormatter.svg)](https://www.nuget.org/packages/SoapFormatter)


The library is a code-compatible replacement, but has a different assembly identity. That means you must recompile
code against this library. Three types were moved into a different namespace to avoid potential conflicts:

`Header` and `HeaderHandler`. Those are now in the `System.Runtime.Remoting.Messaging.Legacy` namespace and `ISoapMessage` is now in
`System.Runtime.Serialization.Formatters.Legacy`.

## Compiling
See the `sln` file in `mcs/class/System.Runtime.Serialization.Formatters.Soap`.

## Limitations
.NET Core does not support serializing all of the same types that .NET Framework does.
Noteably, Delegates, `Type`, and `Assembly` are not supported.

## Disclaimer
Use at your own risk. The formatter may have security implications in your code that you are solely
responsible for.
