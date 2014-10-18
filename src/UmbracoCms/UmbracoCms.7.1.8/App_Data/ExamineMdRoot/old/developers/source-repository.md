---
layout: default
title: Source Repository Home
menu: Source Repository
section: Developer
level: 2
sort: 6
---

The Merchello repository folder layout consists of the following:

###/build/###
This contains the MSBUILD script file for command-line compiling of the entire Merchello solution.  The build file is designed to completely build the entire project, generate the artifacts (DLL files, zip files etc) and execute all unit tests.

To initiate a build, you must have MSBUILD installed.  Without it you cannot recompile the solution.

The Merchello project includes a DOS batch file to launch the necessary build command.  It is found in the root of the project /build/ folder.  **Build.bat** gives you the ability to recompile the entire project with a single command while also firing all unit tests within the solution.  The build batch file will report a successful or unsuccessful build upon completion.



###Building the Merchello Project###

Below you will find a listing of the various applications and technologies involved with Merchello.   You do not necessarily have to know all of them.  


The ultimate goal of any build is to produce *artifacts*, or files.  In the case of Merchello, the build.bat file produces three separate artifacts found in the [project dir]/build/TEMP/Artifacts/ folder.  These files represent the following:

- **Merchello.AllBinaries.version.zip** contains only the compiled DLL files.  Useful when you want to update just the DLL files of an existing install.
- **Merchello.Core.version.nupkg** is a NuGet package.  NuGet packages make it easy to install Merchello into an existing Umbraco development application using the NuGet Package Manager.
- **Merchello_version.zip** is an Umbraco install package that includes the complete Merchello installation.  Use this to install a fresh copy of Merchello using the standard Umbraco 7 package installer.

###/src/###

The /src/ folder houses all of the Merchello programming.   It is broken up in separate folders based on namespace.  

###Merchello.Core###
Drives all of the persistence and 1st level caching.  Here you will find POCO models, factories, services etc for all core classes.   This is thick stuff.  You probably will never need to change things here.  But you can if you want to.  

###Merchello.Web###
Drives all of the Admin-side pages and functionality.

###Merchello.Examine###
Contains Merchello Examine indexers and data providers for Lucene indexing.

###Merchello.Web.UI###
**TBD**


###/test/###
The /test/ folder contains all of the unit tests and integration tests executed during a build of the Merchello solution.   Example/Mock class data can also be found here.

We've created dozens of unit tests to validate core code and behavior during the build process.   You should make it a habit of using unit tests with your code as well.

###/tools/###
The /tools/ folder houses various development tools and utilities necessary for the Merchello project.   Most of these tools are used when a build is executed from the command line.

###/Umbraco.Web.UI/###

**TBD**