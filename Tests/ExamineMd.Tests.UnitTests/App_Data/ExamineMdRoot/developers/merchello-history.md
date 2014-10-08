---
layout: default
title: Introduction and History
menu: Merchello History
section: Developer
level: 2
sort: 2
---
Merchello has been built to keep the core codebase aligned with the same design concepts used by Umbraco.   If you are familiar with Umbraco source code, you will have no problem finding what you need in Merchello code.

For example, the source code repository folder structure has the same layout as Umbraco.  Much of the core POCO classes implement or inherit from Umbraco core classes.   This was all done to make integration with Umbraco as tight as possible without creating overlapping functionality.

###Design History###
Merchello is an Umbraco V7 project.  The back office is built in AngularJS and becoming quite extensive.   
 
When Merchello is installed, we add 30+ tables to the same database as the Umbraco instance.  This is done in the same manner that Umbraco handles its own database installation.  Because our repositories respect the “SqlSyntax” defined by Umbraco’s SqlSyntaxProvider singleton, we also get the benefit of Merchello working equally as well in Sql Server, Sql CE, Sql Azure and MySql (MySql untested).  
 
The Merchello concept is pretty straight forward – everything that is eCommerce-workflow related will have a database record and a supporting Service API (exactly like Umbraco’s V6 Services except accessed through MerchelloContext singleton).  From a designers perspective, we focused on including the common behaviors a designer would need while building a store.  This eliminated the need for the designer to understand much about the API.  

For example, we define a MerchelloTemplatePage which inherits from UmbracoTemplatePage and is intended to be used in views.  This class establishes a CustomerContext and instantiates our MerchelloHelper.  The designer can simply reference things “CurrentCustomer” and “Merchello” in the respective view. 
 
The original idea for Merchello products was to use Umbraco content with a defined set of aliases (similar to umbracoNaviHide) to define e-commerce workflow required properties such as sku, price, etc.  This concept had to be altered once we started looking at some of the more advanced inventory features we wanted in our feature set.  

We didn't want to lose the flexibility of offering products through content.  Thus we rolled our own product picker.  This product picker allows the user to add as many properties to a piece of content as they need.  The user can then associate the actual Merchello product and have a “Product Page”.  

Speed is always in the forefront of our design.  We serialize the entire product record into a Lucene index and then instantiate a front end object via the MerchelloHelper.   
