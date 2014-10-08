---
layout: default
title: MerchelloKit Home
menu: MerchelloKit
section: Developer
level: 2
sort: 4
---

##Introduction##
The MerchelloKit collection of files are provided only with the [Starter Kit](/starter-kit/).  These files provide simple and fast access to the most important Merchello API commands.  We have built this files to offer real world examples of how the core Merchello routines can be accessed.

##BasketController##
A basket object is used to organize specific basket items and associate those items with a specific shopper.   A shopper can have only 1 basket.  To work with the basket object, we have provided a controller class called **BasketController**.  The basket controller class inherits from **SurfaceController** and thus exposes the usual Umbraco references.  This controller includes the following methods:

###AddtoBasket###

	[HttpPost]
	public ActionResult AddToBasket(Guid productKey, int quantity)

This routine is used for your ADD-TO-CART functionality.  Must be referenced within a _BeginUmbracoForm_ instance.

**Parameter productKey:**  This represents the unique Merchello product ID value of the product to be added to the basket.

**Parameter quantity:**  Numeric value representing how many of the specified product should be added to the shopper basket.

####Notes####
productKey must exist in merchProductVariants or no item will be added to the basket.

Quantity can be positive or negative allowing for representation of both purchases and returns.


###RemoveItemFromBasket###
    [HttpGet]
    public ActionResult RemoveItemFromBasket(string sku, int redirectId)

This routine will search the current shopper basket for the specified SKU value.  If any line item matches the SKU, it is removed from the basket item collection.

**Parameter string sku:** Represents the sku value to be removed.
  
**Parameter int redirectId:**  specifies the node ID of the Umbraco content page to which the shopper browser will be redirected upon completion. 

##NavigationController##
This controller provides a simple and effective way to generate main navigation links directly from the Umbraco content tree.

##NavigationHelper##
This helper class contains routines that help build a nested collection of **ILinkTier** objects based on how the content is organized in the Umbraco content tree.  A single property is exposed that provides a strongly-typed collection of **ILink** objects to represent individual content nodes. 

###BuildLinkTier###
        public ILinkTier BuildLinkTier(IPublishedContent tierItem,
          IPublishedContent current,
          string[] excludeDocumentTypes = null,
          int tierLevel = 0,
          int maxLevel = 0,
          bool includeContentWithoutTemplate = false)
 
 