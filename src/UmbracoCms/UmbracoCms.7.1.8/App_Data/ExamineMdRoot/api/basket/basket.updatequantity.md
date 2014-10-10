---
layout: default
title: Basket.UpdateQuantity
menu: Basket.UpdateQuantity
section: API
level: 2
---
##Description##
Changes the quantity value of an existing line item in the basket item collection.  You can identify which basket item to change by providing the product GUID key, the product SKU or an IProductVariant object.

##Dependencies##
- [IProductVariant](/api/interfaces/iproductvariant/)

##Usage##
    void UpdateQuantity(Guid key, int quantity);
    void UpdateQuantity(string sku, int quantity);
    void UpdateQuantity(IProductVariant productVariant, int quantity);

##Returns##
void

##Example##
    _basket.UpdateQuantity(item.Key, item.Quantity);
    _basket.Save();

 

