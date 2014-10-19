---
layout: default
title: Basket.RemoveItem
menu: Basket.RemoveItem
section: API
level: 2
---
##Description##
Removes a specified product from the basket regardless of the quantity set in the line item.

Note that GUID is the key of the line item, not the GUID of a given product.

##Dependencies##
- [IProductVariant](/api/interfaces/iproductvariant/)

##Usage##
    void RemoveItem(Guid itemKey);     
    void RemoveItem(string sku);        
    void RemoveItem(IProductVariant productVariant);

##Returns##
void

##Example##
    _basket.RemoveItem(lineItemKey);
    _basket.Save();

 

