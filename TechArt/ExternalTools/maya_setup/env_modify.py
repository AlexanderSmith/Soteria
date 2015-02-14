#!/usr/bin/env python
# encoding: utf-8

"""Environment modification to Maya to setup custom modules for Soteria

.. module:: ExternalTools.maya_setup.env_modify
    :platform: Unix, Linux

.. moduleauthor:: wbondie

.. note::
    None
"""

# Built in
import sys
import os
from os.path import expanduser

# Third Party
try:
    import click
except:
    pass


CONTEXT_SETTINGS = dict(help_option_names=['-h','--help'])

@click.command(context_settings=CONTEXT_SETTINGS)
@click.argument('git_repo', type=click.Path(exists=True))
@click.option('--maya-version', type=click.Choice(['2012', '2013', '2014']))
def main(git_repo, maya_version):
    """Description: Maya environment file setup.

    git_repo: The file path location to your git repository on disk.
    """
    script_path = os.path.abspath(os.path.dirname(sys.argv[0]))
    home = expanduser("~")
    env_location = home
    if maya_version is not None:
        env_location += '\\Documents\\Maya\\' + maya_version + '-x64\\Maya.env'
    else:
        env_location += '\\Documents\\Maya\\2014-x64\\Maya.env'
    script_path = os.path.dirname(os.path.dirname(script_path))
    script_path += '\\MayaSoteria'
    mod_file = open('%s\\module_setup.mod' % script_path,'w+')
    mod_file.write('+ soteriaModule 0.1 %s' % script_path)
    env_file = open(env_location, 'w+')
    env_file.write('MAYA_MODULE_PATH = %s' % script_path)
    print '[Process completed.]'

if __name__ == "__main__":
    main()
