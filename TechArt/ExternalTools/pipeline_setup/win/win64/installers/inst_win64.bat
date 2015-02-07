:Windows cmd line script for installing Python26
:author: wbondie
:size: 41MB

cd /d %~dp0
cd ../bin
msiexec /i python-2.6.6.amd64.msi TARGETDIR=c:\Python26 /qn
cd ../../
copy python.bat C:\Windows\System32\python.bat
set PATH=%PATH%;C:\python26\Scripts
cd ../
python get-pip.py
exit