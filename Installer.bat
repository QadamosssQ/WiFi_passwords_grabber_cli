@echo off

set "repoURL=https://github.com/QadamosssQ/WiFi_passwords_grabber.git"

set "repoFolder=WiFi_passwords_grabber"

set "exePath=%cd%\%repoFolder%\release\WiFi_passwords_grabber.exe"

if not exist "%repoFolder%" (
    git clone "%repoURL%" "%repoFolder%" || (
        echo This repository cannot be used. Append that Git is installed.
        pause
        exit /b
    )
) else (
    cd "%repoFolder%"
    git pull
    cd ..
)


if exist "%exePath%" (
    start "" "%exePath%"
) else (
    echo Couldn 't find file.
    pause
    exit /b
)

exit
