---
layout: default
title: MerchelloContext
menu: MerchelloContext
section: API
level: 2
---
This a singleton class that exposes various services necessary for Merchello interaction.  This includes access to configured payment, shipping and taxation gateways.  Typically this class is referenced in controllers that provide business logic or classes that require direct access to the database.

##Members##
- [Current](/api/merchellocontext/merchellocontext-current/) A property which returns the currently instantiated singleton
   