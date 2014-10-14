---
layout: default
title: Basket.AddItem
menu: Basket.AddItem
section: API
level: 2
---
##Description##
Adds either IProduct or IProductVariant to the basket item collection.  Overloads exist for overriding quantity and name.  Optionally you can also supply an ExtendedData collection of key-value pairs.

##Dependencies##
- [IProduct](../interfaces/iproduct/)
- [IProductVariant](../interfaces/iproductvariant/)


##Usage##
	void AddItem(IProduct product);
	void AddItem(IProduct product, int quantity);
	void AddItem(IProduct product, string name, int quantity);
	void AddItem(IProduct product, string name, int quantity, ExtendedDataCollection extendedData);
	void AddItem(IProductVariant productVariant);
	void AddItem(IProductVariant productVariant, int quantity);
	void AddItem(IProductVariant productVariant, string name, int quantity);
	void AddItem(IProductVariant productVariant, string name, int quantity, ExtendedDataCollection extendedData);

##Returns##
void

##Example##
    _basket.AddItem(product, product.Name, model.Quantity);
    _basket.Save();

 

