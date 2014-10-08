---
layout: default
title: Developer Home
menu: For Devs
section: Developer
level: 1
sort: 1
---
As one developer to another, welcome!  Here you will find the inside scoop on what makes Merchello tick.  We've compiled a tremendous amount of technical information to help you through your development cycle.  First we will offer some background to this project as well as answer some common questions.  Scroll down if you want to go straight to the technical links.

##Design Concept##
The Merchello team goal was to build Merchello not as a full blown website, but rather an **eCommerce API** that can be implemented as an extension to an existing CMS framework.  This would allow everyone to leverage both the power and speed of the CMS framework.  Then we could focus more on eCommerce features, less on driving content to browsers.

Merchello provides all the underlying components necessary to build an effective online store using Umbraco.  This means that there are no pages, no controllers and no views in the base Merchello build.   You as the developer are able to decide how Merchello is implemented within your Umbraco site.

But don't worry.   We certainly won't leave you completely on your own.  The [Starter Kit](/starter-kit/) is provided to offer a fully functional example of how to implement Merchello into Umbraco as a real online store.  It includes the basic product, category, basket and checkout pages common to most every eCommerce site.  Within these pages and views you will find a large variety of working examples to demonstrate Merchello implementation.

##Why just an eCommerce API?##
The Merchello team strongly believes in a community-driven approach to software development.  History has proven that regardless of the need, an application will die when the supply of accurate information evaporates.  Application users simply cannot risk a commitment to software of which the community at-large has little interest.

So we decided to build something that is strong enough to be useful, yet lightweight enough to permit extensive flexibility.   Rather than bind everyone to the same static set of pages with the same UI constraints, we simply provide the core behaviors required of any eCommerce implementation.   This leaves you the freedom to leverage all the benefits of Umbraco for rendering your store content.   

By approaching eCommerce from this perspective, we believe the community will keep up the momentum through ongoing contributions to the API.  When that happens, Merchello is no longer impacted by profit margins or acquisitions that so often sound the death toll for other software products today.  

##Where Should I Go Next?##
We have created a series of documents that explain the various aspects of the Merchello systems.   These documents are always a work-in-progress, so please check back for new content and material.  

Using the menu to the right to navigate to our developer areas.

## Resources ##
###Source Repository###
The source repository for Merchello can be found on [GitHub]({{ site.url }})

As a developer, you are responsible for creating a fork of the master repository using your own GitHub account.   As per the standard GIT repository procedure, you can submit your code changes to the master repository via a **pull request**.  The option to submit a pull request is found on the GitHub home page for the Merchello master copy.

The owner of the Merchello repository will either approve or decline your pull request.  In the event your pull request is declined, a clear and responsible reason will be supplied with the notice.  

**We are community-driven - don't be afraid to contribute often!** 


###Applications###
* [Microsoft Visual Studio](www.visualstudio.com)
* [**SourceTree** - Git client for Windows](http://www.sourcetreeapp.com/)
* [**MarkdownPad** - Nice Markdown editor with preview window](http://markdownpad.com/)
* [**JetBrains - ReSharper** - Code inspections, automated code refactorings, blazing fast navigation, and coding assistance](http://www.jetbrains.com/resharper/) - This Visual Studio plugin that really helps us to keep our code consistent.   
* [**NUnit** - Unit testing](http://nunit.org/index.php?p=home) - NUnit is a unit-testing framework for all .Net languages.

###Media###

* [**VIDEO** - Umbraco 7 (Belle) and AngularJS at CodeGarden](http://stream.umbraco.org/video/8315406/angularjs-in-umbraco-7-belle)
* [**VIDEO SERIES** - AngularJS tutorials](http://www.egghead.io) - These are worth watching a few times!

###Technology###

* [**AngularJS**](http://angularjs.org/)
* [**Node.js**](http://nodejs.org/) - You may need this for JavaScript Unit testing via [GruntJS](http://gruntjs.com/)
