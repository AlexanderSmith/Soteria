#!/usr/bin/env python
# encoding: utf-8

"""Unit testing file for environment modification

.. module:: ExternalTools.maya_setup.tests.env_test
    :platform: Unix, Linux

.. moduleauthor:: wbondie

.. note::
    None
"""

# Built in
import unittest

# Third Party
from mock import patch

# Custom
import maya_setup.env_modify as env

class Tests(unittest.TestCase):

    def setUp(self):
        pass

    def tearDown(self):
        pass

    @patch('maya_setup.env_modify.click')
    @patch('maya_setup.env_modify.os.path', create=True)
    def test_env_modify_pathing(self, mock_path, mock_click):
        pass

    def test_env_modify_pathing_fail(self):
        pass

    def test_home_location(self):
        pass

    def test_script_path(self):
        pass

    def test_mod_file_open(self):
        pass

    def test_mod_file_write(self):
        pass

    def test_env_file_open(self):
        pass

    def test_env_file_write(self):
        pass

    def test_env_location_pathing(self):
        pass
