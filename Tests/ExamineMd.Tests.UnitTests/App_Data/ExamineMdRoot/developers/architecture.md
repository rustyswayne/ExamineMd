---
layout: default
title: Architecture Summary
menu: Architecture Summary
section: Developer
level: 2
sort: 3
---
The Merchello application itself is split into two main areas.  The **high-level API** and the **low-level API**.  This was done to provide logical separation of tasks as well as maintain a close alignment with how Umbraco itself is organized.

The **high-level API** is used throughout your Umbraco views, partial views and controllers.   This API provides quick and easy access to context-specific information such as shopper data and the current basket.  You can easily resolve Umbraco pages to Merchello products using this API.

The **low-level API** handles all of the persistence and caching for every Merchello table.       



###[Source Code Repository](/developers/source-repository/)###
This section offers a detailed look into how the repository is organized.  It can help you quickly find where to look for specific classes or behavior.
  

###[MerchelloKit](/developers/merchello-kit/)###
**MerchelloKit** is provided only with the Starter Kit and includes a collection of classes, controllers and extensions to simplify referencing Merchello.Core.  [MerchelloKit](/developers/merchello-kit/) can be found in the /App_Code/ folder after the Merchello [Starter Kit](/starter-kit/) has been installed.  

We highly recommend you begin with the [Starter Kit](/starter-kit/) if this is your first leap into Merchello.

###[Gateway Providers](/developers/gateway-providers/)###
A gateway provider includes the necessary programming to talk with specific service providers outside of the Merchello application.   A payment provider, shipping provider or tax provider is formally known as a **Gateway Provider**.  

If you want Merchello to talk to your own payment, shipping or tax provider, this is the place to start.  We've constructed and documented specific programming interfaces necessary to implement your own provider gateway.   

###[Config Files](/developers/config-files/)###
There are several configuration settings that can be controlled through the Merchello configuration file.  These settings can have a profound impact on the behavior and stability of your Merchello and Umbraco installation.   It's important to study this chapter in the wiki before making any changes to the Merchello configuration settings.    



