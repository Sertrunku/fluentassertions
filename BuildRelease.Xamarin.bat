CALL "%VS120COMNTOOLS%vsvars32.bat"

msbuild /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild FluentAssertions.Xamarin.sln

tools\GitLink.exe . -u https://github.com/onovotny/fluentassertions -b Xamarin-Support

.nuget\nuget pack package\FluentAssertions.Xamarin.nuspec -o package