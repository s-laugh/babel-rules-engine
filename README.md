# ESDC Maternity Benefit Rules Engine

This is a prototype for a Rules-as-Code (RaC) engine for the EI Maternity Benefits calculation. 

It can serve as a template for future RaC engines as well, since it makes heavy use of abstract OOP concepts such as interfaces and generics. 

It contains code specifically for maternity benefits and also for a "sample scenario", which can be a placeholder or template for other rules. 

It is exposed as a web API and defines a contract for the different rules that are coded.


## Development

### Running Locally

- `cd` into the solution folder
- `dotnet run --project esdc-rules-api`

Note: If running this project locally alongside related web APIs, ensure you are specifying the projects to run on separate ports in the launchSettings.json file

### Running in Docker

- `docker build -t babel-rules-engine .`
- `docker run -it --rm -p 6000:80 babel-rules-engine`

### Testing

-  `dotnet test`


## Maternity Benefits

In general, a rule has two important properties:
- It applies to an entity (individual, couple, family, etc)
- It contains parameters for the calculation

So the Maternity Benefit calculation endpoint is called when you have an eligible maternity benefit applicant and you want to find out how much they are eligible for under a certain set of rule parameters. So you must pass in relevant information about the individual (e.g. weekly incomes) as well as the rule parameters that you want applied (percentage of average income, maximum weekly amount, etc). The result will be the amount that the individual is entitled to. 

### Use cases

Since it is an API, a rules engine may not typically be directly used by end-users. The goal of a rules engine is that it can be used under the hood by many different user-facing applications as an authoritative source of the government rule. It is carefully designed with the input of policy experts and written with tests to ensure accuracy. This project is a prototype for such a system to showcase the flow that would be used in a RaC scenario and give an example of a technology infrastructure that could be used.

The rules engine does not attempt to prescribe any particular use-case 

The example for a user-facing web application that we are experimenting with is a Policy Difference Engine. This is an application that measures the impact of potential changes to a rule. In this case the rule is the maternity benefit calculation. While there are many changes that could be proposed to this rule, we are narrowing our focus to simple changes, such as tweaking some of the rule parameters. This is the type of rule change that this rules engine facilitates. If you want to measure the impact of such a change on a set of eligible maternity applicants, then you can call the maternity benefits endpoint twice for each applicant: Once with the rule parameters set at their existing values, and again with the rule parameters set at some new proposed values. You can then store, aggregate, and display those results as needed. 


### Flow

Here is a sample request:

```
POST /MaternityBenefits
{
    "Person": {
        "UnemploymentRegionId": "ae876c51-3c4d-4ec3-ba0b-846eaa9e2c7b",
        "weeklyIncome": [
        {
            "startDate": "2020-03-06T13:28:17.6871771-05:00",
            "income": 990
        },
        {
            "startDate": "2020-03-13T13:28:17.6874259-04:00",
            "income": 1050
        },
        ...
        {
            "startDate": "2021-08-06T13:28:17.6875289-04:00",
            "income": 948
        }
    ]
    },
    "Rule": {
        "MaxWeeklyAmount": 595,
        "NumWeeks": 15,
        "Percentage": 55,
        "BestWeeksDict": {
            "ae876c51-3c4d-4ec3-ba0b-846eaa9e2c7b": 14
        }
    }
}

```

This request contains the required data about the individual (Person), and the desired rule parameters (Rule). This approach may also be used in the general case for other rules.

These 'Person' and 'Rule' classes are implementations of interfaces, the goal being that other rule scenarios could reuse much of the existing code, and only need to fill in specific calculation details. The rules engine makes heavy use of generics and interfaces to demonstrate how this approach may be used flexibly with other rules. A generic RequestHandler class is set up to handle requests and must be populated with a calculator class. This calculator class is where the logic for the specific rule will sit.

There are two separate implementations of the maternity benefits calculation. The goal of having two is to demonstrate flexibility as well as demonstrate the use of integration specifically with OpenFisca:
- A default calculator, written entirely within the scope of this project, and uses C#
- A connection to a separate API that uses OpenFisca. This implementation parses out the required parameters, builds the specific OpenFisca request, performs the call, and processes the response.

## Long-term goals

The higher-level goal of this project is to build momentum towards further Rules-as-Code adoption and provide some ideas for technical infrastructure. This type of system may align well with other technologies and practices:

### Division of rules into separate APIs

While we advocate for a large-scale Rules-as-Code system, it may not make sense to put a variety of different rules into one single project. This could cause too much overhead when trying to make changes to the rules, and a lot of the rules may not have anything to do with eachother.

This brings up the difficult question of deciding *how* exactly to split up the code-bases for different RaC APIs. One possibility is to align it with the government documents themselves, which could promote these APIs as a "coded version" of the written rules. In this case, the API would encode precisely the target rule document (policy/regulation/legslation) - nothing more, nothing less. It may be the case that a general approach to this problem could be defined, and then adapting the approach as needed, due to the variety of government rules.

The approach could work well, since the various RaC APIs could be independently maintained and deployed. If changes must be made to a given regulation, for example, then it could be tested and completed without touching unrelated rules.

### API Gateway

If we have a collection of different RaC APIs running as their own service, then they could all be deployed to different endpoints. For applications that want to consume many of these services (think chatbots or service agent tools), this may lead to a confusing experience, since they would have to manage multiple endpoints. Consider an analogy to written rules. Suppose every single government rule was on a different website. And if a service agent was providing help to someone, they would need to have all the different websites bookmarked. Thankfully this is not the case, since all the rules can be accessed at https://laws-lois.justice.gc.ca/. We want to take a similar approach with rules-as-code. 

If we can put all of these rule services behind an API gateway, then they could all be accessed through the same domain, creating a more consistent experience for consuming applications. This gateway could also offer other advantages and be used where we want some common functionality across *all* the rules services. This could include monitoring, logging, load-balancing, analytics, scoping/permissions, etc. Any cross-cutting functionality can be omitted from the individual rule services and added into the common gateway layer. 

This microservices approach combines the decoupled approach with a consistent experience for applications that would consume multiple rules. In fact, it may be possible to use the same pattern used by the justice.gc.ca website for organizing the endpoints of the various rules. 


## Next Steps

This is still in early stages of prototype development, and may undergo many changes as our project develops. Some potential work on this repo in the future may include:
- Integrating further devops processes, such as automated testing, CI/CD, monitoring/alerting
- Adjusting the information that is passed in for the rule calculation. It is debatable whether the rules engine should take in th
- Converting this project into a template that could set up some scaffolding for a generic rules API. The developer would then be responsible only for adding in the specific calculation. 
- Expanding to accommodate more diverse types of rules, for example rules that entities other than an individual (families, couples, etc.)
- Accept batch requests for rule calculations, rather than just one at a time
