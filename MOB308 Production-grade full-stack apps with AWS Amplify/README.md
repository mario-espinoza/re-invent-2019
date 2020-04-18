# AWS Amplify @heitor_lessa

Fullstack -> builder

CLI
Libs
Platfor specific
Manage fosting deployment

##CLI
conv over conf
single -> multi env
Local mock
Code generation

Client
interact
native 4 ios and android

Envs:
Ephemeral envs

## UC:severless airline proj

\*\* coolors.co

### UIUX

- Quasar framework
- Vuejs
- Amplify
- Stripe

### Auth

- Cognito
  `amplify init`
  `amplify auth add`

### GraphQL API

AWS app Sync
API Gateway

```js
type Booking
  @model
  @auth(rules: [{allow: owner}]
  @key()
{
  id: ID,
  outbound: Flight! @connection
}
```

@model - CRUD
@connection
@auth: fine Graninde autg
@key: Custom indexes for @model

### Backend

AWS appSync

### Payments

Stipe elems & Stripe js

### Data

GraphQL
DynanoDB

## Operation

### Load Optimization

gatling.io
Endpoint + Memory Optimization

ServerSide Caching

### Custom metrics

- Cloudwatch
  - monitoring: access logs
  - Structured Logging: Standarize logs
    Correlation Id
    Configure HTTP headers x-correlation-id on AWS Configure

@model: Subscriptions: null

### Security Headers

Common security headers
infosec.mozilla.org

## Catalog

Apache Velocity (VTL) to DynamoDb

## Payment

StepFunctions - State Machine

## Loyalty

Amazon SNS

## Summary

Code is liability
Focus on business
Serverless First
Amplify It
Modularize Frontend

AWS X Ray

lambda python powetools
awslabs/aws-lambda-powertools
MOB307
DOP334
