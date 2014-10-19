---
layout: default
title: Taxation Provider
menu: Gateway Providers
section: Developer
level: 3
---
Taxation providers are used to calculate taxes.  In most situations the provider will be used to communicate with an outside service in order to calculate the appropriate tax rate.  However a taxation provider can also perform the calculations internally.  

You can see this in action with the included Fixed Rate shipping gateway.  This gateway provider has no external communication.  But it serves as a good example of how a shipping provider is implemented using the Merchello architecture.

![](http://i.imgur.com/ahxOYro.jpg)