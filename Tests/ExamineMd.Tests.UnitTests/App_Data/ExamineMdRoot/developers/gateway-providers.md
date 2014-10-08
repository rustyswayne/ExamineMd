---
layout: default
title: Gateway Providers Home
menu: Gateway Providers
section: Developer
level: 2
sort: 5
---

Gateway providers are used to provide a standardized way of implementing either internal programming or external service communications.  This functionality is broken down into three specific types of providers.  These provider types are Payment, Shipping and Taxation.

Rolling your own gateway provider can be a very rewarding experience.  When you are finished testing your gateway, **contribute it to the community**.   As the community library of gateways continues to grow, imagine the benefits to you finding a gateway already written to your need.  Everyone wins!

Merchello gateway providers are all written to follow a specific set of design guidelines.   Follow these guidelines in your provider to ensure your gateway has full compatibility throughout the Merchello application.

![](http://i.imgur.com/dtKWzuu.jpg)

From this diagram you can quickly see how a payment gateway provider class is implemented.  Note the interface class is optional, but it's always best to follow the standards.  

As you can tell, everything after [IProvider](/api/interfaces/iprovider/) gets "provider-specific" to the type of gateway being implemented.  So we've broken up the remainder of the gateway documentation into the following sections.  Click the appropriate link for the type of gateway you want to build.

**Don't forget, everyone benefits when you share with the community!**

##Gateway Types
[Payment Gateway Provider](/developers/payment-provider/) handles all necessary transaction processing for authorizing, capturing and refunding payments.

[Shipping Gateway Provider](/developers/shipping-provider/) handles all communication between Merchello and a specific shipping carrier to obtain shipping rate quotes.  A level-based shipping gateway provider has already been included with the default Merchello install. 

[Taxation Gateway Provider](/developers/taxation-provider/) responsible for calculating the correct tax amount.   A level-based tax gateway provider has already been included with the default Merchello install.    