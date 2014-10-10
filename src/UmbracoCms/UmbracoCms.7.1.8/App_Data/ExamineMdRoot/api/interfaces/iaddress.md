---
layout: default
title: IAddress
menu: IAddress
section: API
level: 2
---
##Description##
IAddress is an interface used to declare the object to be treated as a address.     

##Dependencies##
none
 
##Usage##
	public IAddress Address

##Returns##
void

##Example##
    IAddress destination = new Address()
        {
            Name = "Mindfly Web Design Studio",
            Address1 = "115 W. Magnolia St.",
            Address2 = "Suite 504",
            Locality = "Bellingham",
            Region = "WA",
            PostalCode = "98225",
            CountryCode = "US"
        };

 

