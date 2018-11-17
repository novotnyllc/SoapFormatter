SoapFormatter
=============

This is the `SoapFormatter` and related classes taken from [Mono](https://github.com/mono/mono/tree/master/mcs/class/System.Runtime.Serialization.Formatters.Soap) and compiled
for .NET Standard 2.0. It's goal is to enable serializing and deserializing SOAP
XML for applications that have a dependency on that legacy format.

## Compiling
See the `sln` file in `mcs/class/System.Runtime.Serialization.Formatters.Soap`.

## Limitations
.NET Core does not support serialzing all of the same types that .NET Framework does.
Noteably, Delegates, `Type`, and `Assembly` are not supported.