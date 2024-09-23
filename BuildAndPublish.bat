@echo off

:: build the project
dotnet build

:: copy to mods directory
set "source=C:\dev\personal\trwm\bin\Debug\netstandard2.1\trwm.dll"
set "destination=C:\Program Files (x86)\Steam\steamapps\common\The Farmer Was Replaced\Mods\trwm.dll"
set "game_dir=C:\Program Files (x86)\Steam\steamapps\common\The Farmer Was Replaced"
set "target_exe=%game_dir%\TheFarmerWasReplaced.exe"

if exist "%destination%" (
    del "%destination%"
    echo removed old file
)

copy "%source%" "%destination%"
echo file copied

echo Changing directory to game folder...
pushd "%game_dir%"
echo starting game...
"%target_exe%"