# ESDC Maternity Benefit Rules Engine

This is a prototype for a Rules-as-Code (RaC) engine for the EI Maternity Benefits entitlement calculation. The encoded rules have been coded based on research and guidance by various maternity benefits policy experts. The functionality is exposed via a Web API, and it allows consumers to access various pieces of the entitlments calculation in a flexible way. This repo can serve as a template for future RaC engines as well, since it makes heavy use of abstract OOP concepts such as interfaces and generics. The Rules engine can be used by any application that needs a coded version of the maternity benefits rules, including the Policy Difference Engine (PDE).

## Maternity Benefits

The Maternity Benefit Calculation can be thought of as having 3 parameters: The percentage of average weekly income, the maximum weekly amount, and the number of eligible weeks. Suppose we have an applicant that has an average weekly income (AWI) of $1000. The entitlement calculation proceeds as follows:
- Take 55% of the AWI (55% of 1000 is 550)
- Compare it with the maximum weekly amount of $595 (550 < 595, so keep 550)
- Multiply by 15 weeks (550 x 15 = 8250)
- So this applicant is eligible for $8250

Let's take another example that uses different values. Suppose we have someone with an AWI of $700, and we use different parameters for the rule.
- Take 60% of the AWI (60% of 700 is 420)
- Compare it with a maximum weekly amount of $400 (420 > 400, so use 400)
- Multiply by 16 weeks (400 x 16 = 6400)
- This applicant is eligible for $6400

### Entitlement Calculation
The values that are input into this calculation include the AWI, which is a property of an applicant, and the 3 rule parameters. We can break up many rules into this framework of the entity (the applicant) and the rule specifications. So a rule calculation must take in a set of parameters corresponding to the rule values, as well as the properties of the individual that the rule applies to. In this case, the rule parameters are the percentage, the max weekly amount, and the number of weeks. The only individual property needed is AWI. These properties all become part of the API contract. If you want to know how much an individual who has an AWI of $700 would be entitled to under a certain ruleset (e.g. 60%, max $400 weekly, 16 weeks), then that is the data that must be passed into the API call. The specific values can be found in the Open API Spec.

### Average Weekly Income Calculation
While this entitlement calculation may seem fairly straightforward, the real complexity comes when trying to calculate the AWI. This value is calculated using data from the EI Application, the corresponding record(s) of employment (RoE), and a number of variable best weeks (see below). So before we calculate the entitlement, we will first need to calculate the Average Weekly Income. This is the most logically complex piece in the entire PDE flow. We want the rules engine to be as precise as possible, so this calculation has been coded based on lots of research and guidance from policy experts. Ideally this is an ongoing process, where policy experts are able to help come up with tests that validate and ensure the accuracy of this calculation. 

### Best Weeks Calculation
Required in the Average Weekly Income calculation is the number of best weeks the applicant is entitled to. This value is based on the unemployment rate in the applicants economic region. If the unemployment rate is high, then this number will be quite low, allowing them to take fuller advantage of the weeks where they earned the most. If the unemployment is low, then this number is higher, so the AWI is spread out among many weeks, potentially including some where they did not reach their highest earnings.

If we have raw RoE and application data from an individual, then the flow through the Rules Engine will proceed as follows:
- Make a request to the /BestWeeks endpoint. The request includes the postal code. The result is the number of best weeks
- Make a request to the /AverageIncome endpoint. The request includes the number of best weeks from the previous calculation, as well as a large set of application and RoE data 
- Make a request to the /MaternityBenefits endpoint. The request will include the AverageIncome from the previous step, as well as the desired rule parameters explained in the introduction

For specifics on the request objects and responses, see the Open API Specification, found at `/swagger/v1/swagger.json`


### Use case

Since it is an API, a rules engine may not typically be directly used by end-users. The goal of a rules engine is that it can be used under the hood by many different user-facing applications as an authoritative source of the government rule. It is carefully designed with the input of policy experts and written with tests to ensure accuracy. This project is a prototype for such a system to showcase the flow that would be used in a RaC scenario and give an example of a technology infrastructure that could be used.

The rules engine does not attempt to prescribe any particular use-case. An example for a user-facing web application that we are experimenting with is a Policy Difference Engine. This is an application that measures the impact of potential changes to a rule. In this case the rule is the maternity benefit calculation. While there are many changes that could be proposed to this rule, we are narrowing our focus to simple changes, such as tweaking the 3 rule parameters. This is the type of rule change that this rules engine facilitates. If you want to measure the impact of such a change on a set of eligible maternity applicants, then you can call the maternity benefits endpoint twice for each applicant: Once with the rule parameters set at their existing values, and again with the rule parameters set at some new proposed values. You can then store, aggregate, and display those results as needed. The Rules Engine doesn't handle any storage or aggregation logic - it only acts as a calculator for the etitlement.


## Projects
The Rules engine is made up of a number of different projects.

### esdc-rules-api
This is the executable project that is run as a web API. It contains the handlers and logic for the three different calculations (BestWeeks, AverageIncome, MaternityBenefits). The code here makes fairly heavy use of some OOP principles such as interfaces and generics. This is done to show how the architecture could be extended to other government rules. There is also a corresponding Tests project.

#### BestWeeks Code
The BestWeeks calculation is very straightforward at this point - all postal codes return the same value of 14. This will need to be updated as the rules around Maternity Benefits change. We will need to track down a reliable source that can retrieve the number of best weeks based on the postal code. If such a service is not open for connection, then the values may need to be hard-coded and updated.

#### AverageIncome Code
As mentioned, this is the most complex logic in the Rules Engine. More consultation and feedback with policy experts is needed to validate and improve this calculation. There are certain restrictions on the types of values that can be used with the endpoint. For example, it cannot handle applications that have multiple RoEs, and it can only handle RoEs that have a "regular" pay period type (i.e. monthly, semi-monthly, bi-weekly, weekly). 

#### MaternityBenefits Code
This is the main "rule" calculation of the engine, which takes in a custom set of rule parameters.

There are two separate implementations of the maternity benefits calculation. The goal of having two is to demonstrate flexibility as well as demonstrate the use of integration specifically with OpenFisca:
- A default calculator, written entirely within the scope of this project, and uses C#
- A connection to a separate API that uses OpenFisca. This implementation parses out the required parameters, builds the specific OpenFisca request, performs the API call, and processes the response.

The specific implementation of the calculation can be toggled in the Startup.cs file, which handles the dependency injection.


### esdc-rules-classes
This class contains the classes that are used for requests and responses to the web API. It is stored as a separate project so that it can shipped as a Nuget package and imported into apps that consume the Rules Engine (such as the Simulation Engine piece of the PDE). 

If these classes are ever updated, then the Nuget package must be updated appropriately. (See below)


## Development

### Running Locally

- `cd` into the solution folder
- `dotnet run --project esdc-rules-api`

Note: If running this project locally alongside related web APIs, ensure you are specifying the projects to run on separate ports in the launchSettings.json file (if using VS Code)

### Running in Docker

- `docker build -t babel-rules-engine .`
- `docker run -it --rm -p 6000:80 babel-rules-engine`

### Testing

-  `dotnet test`

### Nuget

The esdc-rules-classes library must be re-published when making a change to the API request/response objects. 

Reference: https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli

To update the Nuget package:

- Open the esdc-rules-classes.csproj file
- Update the number inside the tag appropriately, depending on the type of change (semantic versioning)
- Run dotnet build. The project is configured to generate the Nuget package on build
- cd into the bin/debug folder of the esdc-rules-classs project, where the generated .nupkg file is stored
- Replace the relevant values (X.Y.Z and the Api Key) in the following command and run it: dotnet nuget push BabelRulesEngineSample.X.Y.Z.nupkg --api-key [API_KEY_GOES_HERE] --source https://api.nuget.org/v3/index.json. The X.Y.Z should correspond to the version you set in the csproj file.
- You can now go into the consuming applications (e.g. Data Primer) and update the package reference


### Deployment

The Rules engine is currently deployed to Azure as an App Service. Deployments are set up using github actions, based on manually clicking a button. Go to the github actions, choose the 'Deploy' workflow and run it.

## Related Ideas and Future Development

### Division of rules into separate APIs

While we advocate for a large-scale Rules-as-Code system, it may not make sense to put a variety of different rules into one single project. This could cause too much overhead when trying to make changes to the rules, and a lot of the rules may not have anything to do with eachother.

This brings up the difficult question of deciding *how* exactly to split up the code-bases for different RaC APIs. One possibility is to align it with the government documents themselves, which could promote these APIs as a "coded version" of the written rules. In this case, the API would encode precisely the target rule document (policy/regulation/legslation) - nothing more, nothing less. It may be the case that a general approach to this problem could be defined, and then adapting the approach as needed, due to the variety of government rules.

The approach could work well, since the various RaC APIs could be independently maintained and deployed. If changes must be made to a given regulation, for example, then it could be tested and completed without touching unrelated rules.

### API Gateway

If we have a collection of different RaC APIs running as their own service, then they could all be deployed to different endpoints. For applications that want to consume many of these services (think chatbots or service agent tools), this may lead to a confusing experience, since they would have to manage multiple endpoints. Consider an analogy to written rules. Suppose every single government rule was on a different website. And if a service agent was providing help to someone, they would need to have all the different websites bookmarked. Thankfully this is not the case, since all the rules can be accessed at https://laws-lois.justice.gc.ca/. We could to take a similar approach with rules-as-code. 

If we can put all of these rule services behind an API gateway, then they could all be accessed through the same domain, creating a more consistent experience for consuming applications. This gateway could also offer other advantages and be used where we want some common functionality across *all* the rules services. This could include monitoring, logging, load-balancing, analytics, scoping/permissions, etc. Any cross-cutting functionality can be omitted from the individual rule services and added into the common gateway layer. 

This microservices approach combines the decoupled approach with a consistent experience for applications that would consume multiple rules. In fact, it may be possible to use the same pattern used by the justice.gc.ca website for organizing the endpoints of the various rules. 

The Rules Engine is currently sitting behind an APIM Gateway, which is also managed in Microsoft Azure. The gateway is exposed as an API, and is configured to route requests related to maternity benefits through to this Rules Engine. Consuming applications can call either the API Gateway or this API directly to make use of the functionality. 

