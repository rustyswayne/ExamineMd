---
layout: default
title: MerchellContext.Current.Services.CustomerService
menu: Services.CustomerService
section: API
level: 2

---
These routines provide a way to generate new customer records.  In Merchello, we have two types of customers.  Anonymous customers are everyday shoppers who have not authenticated the site and use the AnonymousCustomer class.   Authenticated shoppers (members in the Umbraco world) use the Customer class.


##Dependencies##
- IAnonymousCustomer
- ICustomer

##Members##

###CreateAnonymousCustomerWithKey()###

Generates a new anonymous customer record and returns the complete anonymous customer record object.

returns IAnonymousCustomer


###CreateCustomerWithKey()###

Generates a new customer record and returns the complete customer record object

returns ICustomer

