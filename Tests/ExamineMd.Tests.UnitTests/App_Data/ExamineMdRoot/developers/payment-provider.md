---
layout: default
title: Payment Provider
menu: Gateway Providers
section: Developer
level: 3
---
Payment providers are used to process monetary transactions.  In most situations the provider will be used to communicate with an outside service in order to authorize, capture and refund electronic payments.  However a payment provider can be used for any purpose so long as it conforms to the class interfaces and abstractions.

We have included a Cash payment provider in Merchello.   This provider offers no external communication.  But it serves as a good example of how a payment provider is implemented using the Merchello architecture.

![](http://i.imgur.com/gpB3MXp.jpg)

