---
layout: default
title: IShipmentRateQuote
menu: IShipmentRateQuote
section: API
level: 2
---
##Description##
This class represents a single shipping rate quote generated by a shipping provider. 

##Dependencies##
- IShipment
- IShipMethod


##Properties##

###IShipment Shipment###
readonly access to the shipment that was used to calculate and generate this shipping method

###IShipMethod ShipMethod###
readonly access to the specific shipping method quoted

###decimal Rate###
The calculated shipping cost for the given shipment using the given shipping method 
