# GraphQL.NetCore
Using GraphQL in .Net Core app

## GraphiQL
http://facebook.github.io/graphql/October2016/

```text
{
  account(accountId: 11) {
    accountId
    bamAccountId
    cppiConfiguration
    cppiPercentage
    managementStartDate
    lastAssetValueUpdate
    lastProcessedDate
  }
  floorHistory(accountId: 11) {
    cppiFloorHistoryId
    accountCppiConfigId
    cppiFloor
    cashFlow
    changeReason
    changeDateTime
  }
  balance(accountId: 11) {
    capital
    buffer
    bufferPercentage
    reserve
  }
  revenue(accountId: 11) {
    revenuePercentageCurrentYear
    revenuePercentageSinceStartAccount
    currentAssetValue
  }
  quarterlyRevenue(accountId: 11) {
    revenuePercentage
    quarterlyAssetValue
    quarterlyAssetValueDate
    latestAssetValue
    latestAssetValueDate
    reportDrop
    dropBracket
  }
}
```
### Aliases
```text
query
{
  a1: account(accountId: 1) {
    accountId
    bamAccountId
    cppiConfiguration
    cppiPercentage
    managementStartDate
    lastAssetValueUpdate
    lastProcessedDate
  }
  a2: account(accountId: 2) {
    accountId
    bamAccountId
    cppiConfiguration
    cppiPercentage
    managementStartDate
    lastAssetValueUpdate
    lastProcessedDate
  }
}
```
### Fragments
```text
query
{
  a1: account(accountId: 1) {
    ...comparisonFields
  }
  a2: account(accountId: 2) {
    ...comparisonFields
  }
}

fragment comparisonFields on Account {
    accountId
    cppiConfiguration
    cppiPercentage
}
```
### Variables
```text
query q1($accountId: Int!) # or with default value Int = 10
{
  account(accountId: $accountId) {
    accountId
    bamAccountId
    cppiConfiguration
    cppiPercentage
    managementStartDate
    lastAssetValueUpdate
    lastProcessedDate
  }
  floorHistory(accountId: $accountId) {    
    cppiFloorHistoryId
    accountCppiConfigId
    cppiFloor
    cashFlow
    changeReason
    changeDateTime
  }
  balance(accountId: $accountId) {
    capital
    buffer
    bufferPercentage
    reserve
  }
  revenue(accountId: $accountId) {
    revenuePercentageCurrentYear
    revenuePercentageSinceStartAccount
    currentAssetValue
  }
  quarterlyRevenue(accountId: $accountId) {
    revenuePercentage
    quarterlyAssetValue
    quarterlyAssetValueDate
    latestAssetValue
    latestAssetValueDate
    reportDrop
    dropBracket
  }
}
-----------------------
{
  "accountId": 1
}
```
### Directives
```text
query q1($accountId: Int!, $withHistory: Boolean!)
{
  account(accountId: $accountId) {
    accountId
    bamAccountId
    cppiConfiguration
    cppiPercentage
    managementStartDate
    lastAssetValueUpdate
    lastProcessedDate
  }
  # @include(if: Boolean!)
  # @skip(if: Boolean!)
  floorHistory(accountId: $accountId) @include(if: $withHistory) {    
    cppiFloorHistoryId
    accountCppiConfigId
    cppiFloor
    cashFlow
    changeReason
    changeDateTime
  }
  balance(accountId: $accountId) {
    capital
    buffer
    bufferPercentage
    reserve
  }
  revenue(accountId: $accountId) {
    revenuePercentageCurrentYear
    revenuePercentageSinceStartAccount
    currentAssetValue
  }
  quarterlyRevenue(accountId: $accountId) {
    revenuePercentage
    quarterlyAssetValue
    quarterlyAssetValueDate
    latestAssetValue
    latestAssetValueDate
    reportDrop
    dropBracket
  }
}
--------------------
{
  "accountId": 1,
  "withHistory": false
}
```
### Mutation
```text
mutation m1($accountId: Int!, $cashFlow: Float, $changeReason: FloorChangeReason!)
{
  addFloorChange(accountId: $accountId, cashFlow: $cashFlow, changeReason: $changeReason)
  {
    cppiFloorHistoryId
    accountCppiConfigId
    cashFlow
    cppiFloor
    changeReason
    changeDateTime
  }
}
------------------------
{
  "accountId": 1,
  "cashFlow": 987.65,
  "changeReason": "DEPOSIT"
}
```
## Postman
raw, application/json

```json
{"query": "{account(accountId: 1){accountId cppiConfiguration}}"}
```

```json
{"query":"query\n{\n  account(accountId: 1) {\n    accountId\n    bamAccountId\n    cppiConfiguration\n    cppiPercentage\n    managementStartDate\n    lastAssetValueUpdate\n    lastProcessedDate\n  }\n  floorHistory(accountId: 1) {    \n    cppiFloorHistoryId\n    accountCppiConfigId\n    cppiFloor\n    cashFlow\n    changeReason\n    changeDateTime\n  }\n  balance(accountId: 1) {\n    capital\n    buffer\n    bufferPercentage\n    reserve\n  }\n  revenue(accountId: 1) {\n    revenuePercentageCurrentYear\n    revenuePercentageSinceStartAccount\n    currentAssetValue\n  }\n  quarterlyRevenue(accountId: 1) {\n    revenuePercentage\n    quarterlyAssetValue\n    quarterlyAssetValueDate\n    latestAssetValue\n    latestAssetValueDate\n    reportDrop\n    dropBracket\n  }\n}\n","variables":null}
```

```json
{"query": "query q1($accountId: Int!)
{
  account(accountId: $accountId) {
    accountId
    bamAccountId
    cppiConfiguration
    cppiPercentage
    managementStartDate
    lastAssetValueUpdate
    lastProcessedDate
  }
  floorHistory(accountId: $accountId) {
    cppiFloorHistoryId
    accountCppiConfigId
    cppiFloor
    cashFlow
    changeReason
    changeDateTime
  }
  balance(accountId: $accountId) {
    capital
    buffer
    bufferPercentage
    reserve
  }
  revenue(accountId: $accountId) {
    revenuePercentageCurrentYear
    revenuePercentageSinceStartAccount
    currentAssetValue
  }
  quarterlyRevenue(accountId: $accountId) {
    revenuePercentage
    quarterlyAssetValue
    quarterlyAssetValueDate
    latestAssetValue
    latestAssetValueDate
    reportDrop
    dropBracket
  }
}", "variables":{'accountId': 1}
}
```
