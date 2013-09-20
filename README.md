## THIS PROJECT IS NO LONGER IN DEVELOPMENT

*CharImport* is no longer in development; However, project *[NamCore Studio](https://github.com/megasus/Namcore-Studio)* will feature similar functionality.

##  ===============================================================
##  |                                                             |
##  |                    CharImport Universal                     |
##  |                                                             |
##  |               Developed by megasus/Alcanmage                |
##  |                                                             |
##  ===============================================================

About the Software:

CharImport Universal, is a professional tool to manage accounts of your own 
World of Warcraft private server. This tool provides you in a very compact and
simple environment to load, save, import, export and convert certain accounts 
and characters.
The application also allows you to transfer character and account information to
your database either from the official WoW Armory or from another database.


##################################################################################
//Notes and system requirements:

• Tested with WoW WotLK Patch 3.3.5
• Tested with Microsoft © Windows 7 x64 & x86, Windows XP x86
• Integrated update system
• Programming Language: VB.NET
• requires database access
• requires .Net Framework 2.0
• requires Microsoft Windows ©

##################################################################################
//Changelog

Update 0.11 (Beta)

 > Improved support for Trinity and MaNGOS + forks
 > Full support for ArcEmu + forks added
 > Advanced filter options should now work for Trinity, ArcEmu and MaNGOS (forks)
 > When loading characters from WoW Armory following additional options should be transferable now:
   > Appearance of the character
   > Reputation
   > Finished quests
   > Gained achievements
 > "MySqlException: Data truncated for column x" error should no longer occur
 > Fixed an issue that could prevent item icons from loading
 > Fixed an issue that prevented that the character race and class are displayed correctly
 > Fixed an issue that prevented showing the item tooltip on the 'character overview' interface when an item contains "-"
 > Users will now be informed when a character or an account can not be found
 > Localization issues fixed:
  > Items and spells should now be in English
  > Fixed an issue that prevented the application from displaying the correct language when loading a template file
 > UI changes:
  > The 'armory interface' has been changed and is now more user friendly
  > The 'connect interface" has been changed and is now more user friendly
  > It is no longer necessary to specify the core of the destination/source server

Update 0.10.2b (Beta) 

> "Eazfuscator" error message should no longer appear Update 0.10.2a (Beta) > Critical errors fixed Update 0.10.2 (Beta) > Support for 5.x Armory added > The application will no longer stuck when loading characters from the armory > The character gender will now get parsed from the armory > Fixed loading certain enchantments from armory > Enchantment labels should now always be visible when active > Some minnor changes Update 0.10 (Beta) > Users have now 3 additional filter options when loading accounts from a database > 20 possible crash fixes > Template files should now always contain the correct character information > Improved support for MaNGOS 2.4.3 clients > New Localizationsystem > Redirection to Wowhead > Manually set realm and character database names will now be saved > The application should no longer crash during transfers > The MySQL connection should no longer be interrupted during transmissions > False error messages should no longer appear > Critical errors fixed > New logging System

Update 0.9.10 (Beta)

> Support for ArcEmu prepared
> Update system has been improved
> Transfers between different emulation should now work without restrictions
> Converted items will now get the proper enchantments
> It is now possible to specify realm and character databases manually
> Connection interface has been adapted
> New Localization System
> 19.545 possible conversion errors fixed
> Code cleanup
> More bug fixes

Update 0.9 (Beta)

> Support for Mangos is now active
> Advanced Database Verification
> Realm / auth database name can now be changed manually
> Display error in the "Overview" interface fixed
>. NET Framework 2.0 instead of 3.0 is now required
> Glyphs should now be properly loaded from the database

Update 0.8.10 (Beta)

> By clicking the "Yes" button of the update interface the download will now correctly start
> Glyphs interface:
   > Special characters in glyph names will now properly be encoded
   > When you click on an icon you will now be directed to the database of WoWHead
> The creation of template files should now work properly
> The "Welcome" interface should only be displayed while first start
> The color of some labels in the "Character List" interface has been adapted
   > The color of some labels were adjusted
   > After applying the filter function the "Wait-courser" will disappear
