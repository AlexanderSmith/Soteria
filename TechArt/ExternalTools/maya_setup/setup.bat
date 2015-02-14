: Sets up the maya environment for tools development
: using the custom module.
: author: wbondie

call %homepath%\envs\sotie\Scripts\activate.bat
python env_modify.py "%~dp0\..\..\.."
PAUSE
