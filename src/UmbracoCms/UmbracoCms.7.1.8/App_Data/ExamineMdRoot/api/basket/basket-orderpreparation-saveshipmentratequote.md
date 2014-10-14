---
layout: default
title: Basket.OrderPreparation.SaveShipmentRateQuote
menu: Basket.OrderPreparation.SaveShipmentRateQuote
section: API
level: 2
---
##Description##
This routine applies the provided shipping method to the shipment in the basket.  Once the shipment is has been updated to the correct shipping method, the appropriate shipping charge is added to the basket.

Note that the shipping method is stored as a line item in the OrderPreparation.ItemCache collection.  It is not persisted to the original basket line items.

##Dependencies##
- [IShipmentRateQuote](../interfaces/ishipmentratequote/)

##Usage##
	// build a list of available shipping methods and rates
    var shipRateQuotes = shipment.ShipmentRateQuotes().ToArray();

	// Grab the first available shipping rate
    var approvedShipRateQuote = shipRateQuotes.FirstOrDefault();

	// assign the shipping method/rate to the basket
    CurrentCustomer.Basket().OrderPreparation().SaveShipmentRateQuote(approvedShipRateQuote);

