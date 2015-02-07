#! /usr/bin/env bash

#Install file for the python virtual environment
#    path: python_installer.osx.osx_install
#    author: wbondie

cd `dirname ${BASH_SOURCE}`
cd ../bin
sudo installer -pkg ./Python.mpkg -target /
cd ../../
python get-pip.py

pip install -r requirements.txt

cd
mkdir envs
cd envs/
virtualenv sotie
source sotie/bin/activate

echo Thank you for installing.
