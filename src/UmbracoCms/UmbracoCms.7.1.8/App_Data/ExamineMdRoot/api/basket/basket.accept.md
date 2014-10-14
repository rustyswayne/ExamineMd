---
layout: default
title: Basket.Accept
menu: Basket.Accept
section: API
level: 2
---
##Description##
Allows the developer to supply a custom class that filters line items in a custom way. 

##Dependencies##
- [ILineItemVisitor](../interfaces/ilineitemvisitor/)
 
##Usage##
Basket.Accept([ILineItemVisitor](../interfaces/ilineitemvisitor/) visitor)

##Returns##
void

##Example##
    // filter basket items for shippable items
    var shippableVisitor = new ShippableProductVisitor();            

    basket.Accept(shippableVisitor);            

    if(!shippableVisitor.ShippableItems.Any()) return new List<IShipment>();

 

