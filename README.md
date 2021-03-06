# Screech API

**Functionality supported:**

 * Simple token based authentication.Authentication rules are supported.
 * Input validations for all the endpoints are added as per requirements.
 * Possible to retrieve a profile by its key
 * Possible to update a profile picture by itself
 * Possible to update an entire profile,
 * Possible to return a paged list of screeches
	* Default sort order is creation date in descending order
	* Can be requested in ascending order by creation date
	* Can be filtered to return only screeches created by a specific user
	* Default page size is 50, maximum is 500
 * Possible to return an individual screech by its key
 * Possible to create a new screech
 * Possible to update the text of a screech

**Functionality not supported:**

* Authorization rules are not implemented.

## Local Endpoints

 ### Screech CRUD

* Create (Needs to be Authenticated) : http://localhost:8080/api/screech/add/{userId)   
* Update (Needs to be Authenticated)  : http://localhost:8080/api/screech/edit-screech/{userId}/{screechId}
* Get (Needs to be Authenticated)  : http://localhost:8080/api/screech/{ScreechId}
* Filter : http://localhost:8080/api/screech/filter

### User CRUD 

* Create : http://localhost:8080/api/user/add/
* Update (Needs to be Authenticated) : http://localhost:8080/api/user/update{userId}
* Get (Needs to be Authenticated) : http://localhost:8080/api/user/profile/{userId}
* Upodate Profile   (Needs to be Authenticated) : http://localhost:8080/api/user/update-profile-picture/{userId}

## Prerequisites

- [.NET Core 5.0 or higher](https://dotnet.microsoft.com/download/dotnet-core)
- [VisualStudio 16.8 or higher] (https://devblogs.microsoft.com/dotnet/announcing-net-5-0/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Solution file](https://github.com/azirkande/ScreechApi/blob/main/App/Screechr.sln) is located under the App folder to open the project in Visual studio.


## Run the Application locally 
To run the api locally execute [build.sh](https://github.com/azirkande/ScreechApi/blob/main/build.sh) file located in the root of folder which spins up the docker container.
You can make sure if the api running successfully by using url http://localhost:8080/HealthCheck

## Testing 

There are end to end tests wriiten for Controller endpoints
Unit tests are written for AuthManager functionality.

## Authentication

Read Getting Base64 auth tokens section to know more about how to generate the auth token.

Once you have token set the Auth header of the request as Bearer {tokenValue}. E.g. **Bearer YW1yaXRhejpwYXNzMTIz**

If using Postman just use Bearer token option for authentication.

## How to build the project locally

You can use VisualStudio to build the project 
or execute following commands by navidating to the App directory

```sh

dotnet restore   # install packages.
dotnet build	 # builds projects.
dotnet test		 # runs tests. 

```


## Getting Base64 auth tokens

Api is using simple Base64 encoded token for authentication. To get the token use this website to base64 your credential at the moment

https://www.base64encode.net/

Format used to create base64 token is as following

{userName}:{secret}

so for exampple if user name is **amritaz** and secret is **demo123** then string to be encoded should be the format of **amritaz:demo123**

* For quick testing you can use following token with userName as 'amritaz' and secret as 'pass123'
* Test encoded token : YW1yaXRhejpwYXNzMTIz

**NOTE**: This means while creating a new user set the user name to 'amritaz' and secret to 'pass123'

