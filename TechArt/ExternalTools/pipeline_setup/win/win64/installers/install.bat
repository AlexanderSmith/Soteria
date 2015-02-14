:Windows cmd line script for installing Python26 essentials
:author: wbondie

cd ../../../
set PATH=%PATH%;C:\python26\Scripts
pip install -r requirements.txt
cd %HOMEPATH%
virtualenv sotie
cd sotie/Scripts
activate
echo Installation finished.
PAUSE
