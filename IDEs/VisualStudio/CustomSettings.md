# Visual Studio Custom Settings

Visual Studio settings to change to make it more customised for personal preference.

## Project Locations

To change the default *Projects location* directory go to

> Tools > Options > Projects and Solutions > Locations

and change the path in the Projects locations text box.

## Place `System` directives first

To place the `System` directives first when sorting usings check the following:

> Tools > Options > Text Editor > C# > Formatting > Organize Usings > Place 'System' directives first when sorting usings

The ReSharper extension also contains this setting, which can be found in:

> Extensions > ReSharper > Options > Code Editing > C# > Syntax Style > Reference qualification and 'using' directives > Place 'System.\*' and 'Windows.\*' namespaces first when sorting 'using' directives

## Custom Dictionary

ReSharper has ReSpeller which comes with a built in English (US) dictionary. If you want to change this to English (GB) or another dictionary, then follow the instructions here:
https://www.jetbrains.com/help/resharper/Spell_Checking.html

Where the dictionaries are placed on your local PC is up to you. One suggestion is

> %UserProfile%/Libs/Resharper ReSpell Dictionaries
