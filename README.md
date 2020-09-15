# basket-calculator-source-test

I would query with the users whether the total on the receipt should begin "Total:" or "Total price:".

I would enquire about anticipated future special offers to inform the design of the SpecialOffersPipelineFactory - this could come from a DSL, JSON configuration or data.

I have standardized all dates to UTC and all prices to GBP. In the case where internationalization was required this would made conversion easier.

I have added a few acceptance tests because there are scenarios in any system where all unit tests can pass but the application still fail. A useful addition to the acceptance tests would be to run the console application itself as this would grant test coverage of the dependency injection and configuration loading.

I have used terms from the business domain mentioned on the programming challenge PDF wherever possible and made up my own for the rest. However a better approach would be to talk to users and determine their preferred terminology. Maintenance is far easier when the terms used in the code and the terms used by the users match.

Stephen Aggett 15.09.2020