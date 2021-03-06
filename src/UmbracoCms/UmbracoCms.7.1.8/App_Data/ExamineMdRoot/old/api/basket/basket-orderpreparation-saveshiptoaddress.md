---
layout: default
title: Basket.OrderPreparation.SaveShipToAddress
menu: Basket.OrderPreparation.SaveShipToAddress
section: API
level: 2
---
###Description###
This method accepts an IAddress object and persists the address values to the current basket object.  The address values are stored in the ExtendedData property of the current customer object.

###Dependencies###
- [IAddress](/api/interfaces/iaddress/)

###Usage###
    // Customer enters the Shipping address
    var destination = new Address()
        {
            Name = "Mindfly Web Design Studio",
            Address1 = "115 W. Magnolia St.",
            Address2 = "Suite 504",
            Locality = "Bellingham",
            Region = "WA",
            PostalCode = "98225",
            CountryCode = "US"
        };

    // Assume this address to the basket Shipping address
    CurrentCustomer.Basket().OrderPreparation().SaveShipToAddress(destination);
