---
layout: default
title: IProvider
menu: IProvider
section: API
level: 2
---
##Description##
All gateway providers, regardless of type, must inherit from IProvider.  IProvider exposes two important properties that are necessary for Merchello to instantiate your gateway class when the time comes.

##Dependencies##
none

##Members##
        Guid Key { get; }
        string Name { get; }

##Usage##
    public interface IPaymentGatewayProvider : IProvider
    {
     
