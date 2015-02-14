: Sets up the maya environment for tools development
: author: wbondie

SET SCRIPTPATH=%cd%

call %homepath%\envs\sotie\Scripts\activate.bat
python env_modify.py "%~dp0\..\..\.."
PAUSE