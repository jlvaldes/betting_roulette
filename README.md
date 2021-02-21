# Roulette betting
Services API for betting on roulette games

## Previous requirements
- Have an IDE installed for editing, compiling and executing C # / Net Core code
- Have installed or have access to a MongoDb database

## Important note

This solution is designed to work with two types of storages: Azure Redis and MongoDb, however only the MongoDb version is really functional.
On the other hand, it was also designed to work as a monolithic solution or as a distributed solution, using Kubernetes PODs. However, this last way was not developed due to time and complexity issues to deploy POD for each roulette created

## Postman Collection

They can access the Postman collection where the tests were created for each of the API endpoints
The collection can be downloaded from this URL: https://www.getpostman.com/collections/44ffaa5b3a2bc4ebc59d 

