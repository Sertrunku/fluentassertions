CALL "%VS140COMNTOOLS%vsvars32.bat"


.nuget\nuget install gitlink -SolutionDir . -Verbosity quiet -ExcludeVersion -pre
.nuget\nuget restore FluentAssertions.Xamarin.sln
msbuild /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild FluentAssertions.Xamarin.sln

packages\gitlink\lib\net45\GitLink.exe . -u https://github.com/onovotny/fluentassertions -b Xamarin-Support -errorsaswarnings

.nuget\nuget pack src\package\FluentAssertions.Xamarin.nuspec 