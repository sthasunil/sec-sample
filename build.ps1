SonarScanner.MSBuild.exe begin /k:"Studio" /d:sonar.host.url="http://localhost:9000"
MsBuild.exe /t:Rebuild
SonarScanner.MSBuild.exe end