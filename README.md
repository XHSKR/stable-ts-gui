# stable-ts-gui
Simple gui version of [stable-ts](https://github.com/jianfch/stable-ts)

![](screenshot.png)
# Prerequisites
1. [Python 3.10.x](https://www.python.org/downloads/release/python-31010/)

2. Chocolatey (throw it at powershell.exe)
```
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
```
3. FFmpeg
```
choco install ffmpeg
```
# Setup
```
pip install -U stable-ts
```

To install the latest commit:
```
pip install -U git+https://github.com/jianfch/stable-ts.git
```
