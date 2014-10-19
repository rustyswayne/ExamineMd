---
layout: default
title: MerchelloContext.Current.Gateways
menu: MerchelloContext.Current.Gateways
section: API
level: 2
---
##Description##
This is a container class that exposes singleton instances of all configured payment, shipment and taxation gateways in the Merchello store.  When you need to access a specific gateway for any reason, do it via this class.  This saves you the time and effort of declaring your own instance.

##Sub Classes##
- [Payment]()
- [Shipping]()
- [Taxation]()