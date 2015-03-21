#! usr/bin/env python
# encoding utf-8

"""Python script to format files in a directory to a standard

.. module:: file_rename.filerename
    :platform: Unix, Linux

.. moduleauthor:: wbondie

.. note::
    None
"""

# Built in
import os

# Third Party
import click


@click.command()
@click.argument('directory', type=click.Path(exists=True))

def cli(directory):
    """FileRename v0.0.4

    File renaming command line tool.

    \b
    This script reads in a directory from the user and renames
    all files in the directory to a standard.

    \b
    Current implementation:
    Lowercases all letters in filenames.
    Removes the character(s) space, -

    \b
    example:
    python filerename.py /Users/Tester/Documents

    """
    rename_files(directory)

def rename_files(directory):
    """Function to lowercase all files in a directory

    :param directory: Existing directory to lowercase files
    :type node: str
    :returns: none

    """
    u_chars = ['\'','-',' ']
    for root, dirnames, filenames in os.walk(directory, topdown=True):
        for f in filenames + dirnames:
            fname = f
            for c in u_chars:
                if c in f:
                    f = f.replace(c, '')
            os.rename(os.path.join(root,fname),
                      os.path.join(root, f).lower())
    print 'success!'

if __name__ == '__main__':
    cli()
