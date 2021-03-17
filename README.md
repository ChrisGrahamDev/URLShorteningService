------------------------------------
I. Folder / File structure list
------------------------------------
URLShorteningService2Test
	Controllers
		UrlServiceController.cs
	DataAccess
		Interfaces
			IDataAccess.cs
		DatabaseDataAccess.cs
		FileSystemDataAccess.cs
	Models
		UrlModel.cs
	Services
		Interfaces
			IFileReaderService.cs
			IFileUpdaterService.cs
			ITokenGeneratorService.cs			
		FileReaderService.cs
		FileUpdaterService.cs
		TokenGeneratorService.cs
	UrlFile
		Urls.json
	Utils	
		IJsonWrapper.cs
		JsonWrapper.cs
	appsettings.json
	Constants.cs
	Program.cs
	Startup.cs
URLShorteningService2Test
	ServiceTests
		FileSystemDataAccessTests.cs
------------------------------------		
II. Process Flow
------------------------------------
ShortenUrl:
When a user wants to shorten the url, the will post the long url to the api in the body of a request. There will first be a check to ensure the value passed is not null before validating the URL. 
Next the process will call to retrieve the shortened url. This call wirll first check the data store for an existing shrt url. If it exists, it will simply return that short url. 
If not, The process will continue to creating a token and storing the long url in storage. 
The token is used as an identifier for the long url and is also returned to the user after being concated with the applications address.

Navigate to shortened url:
When a user tries to navigate to the shortened url, the api will recieve the request with the token as a parameter. After a validation check, there is a check to see if the token exists in storage.
If the token exists, the long url is extracted and returned in the form of a 307 temporary redirect which auto navigates to the full url.
If the token doesnt exist, a bad request is returned to the user with a retry message.

------------------------------------		
III. Design ideas
------------------------------------
Encapsulation
   Where possible, fields were encapsulated to prevent any invalid access to a classes data. 

Dependency Injection
   Where possible, classes and code was broke down to interfaces to allow dependency injection to be performed and have loosely coupled classes while aiding with unit testing

Single responsibility
   while implementing, Single responsibility was enacted to ensure classes only performed the one action

Factory Pattern - IDataAccess, DatabaseDataAccess, FileSystemDataAccess. Startup.cs, appsettings.json
   To make the application open to enhancement, I have included a 'switch' in the startup.cs class that allows the data source to be configurable via the appsettings. A switch statement will read the appsettings 
   and will add the relevant service. This can be done by having both DatabaseDataAccess and FileSystemDataAccess implement the IDataAccess interface.
	
JsonWrapper
   In an effort to help with unit testing, a wrapper was created for newtonsoft.Json as these methods were static and so difficult to mock out. Having a wrapper allows for easy mocking.

FileReaderService, appsettings.json
   The file reader service also reads the filepath from the appsettings.josn file. Again this was to allow for easy placement of the Urls file in any filepath or location. 

Singleton - FileUpdaterService
   The FileUpdaterService was designed as a singleton to ensure only one instance is created and to ensure any writing to the file was thread safe.
   
TemporaryRedirect
	to prevent incoerrect cacheing of the urls, i implemented RedirectPreserveMethod which returns a 307 'temporary redirect' which tells the browser not to store the redirct.
------------------------------------		
IV. Issues
------------------------------------
Testing
The tesing is limited on the solution mainly due to personal Time constraints and a few reasons:

  1 Inability to mock the IConfiguration interface.
	When creating the code, IConfiguration is passed in via pubic constructors in multiple classes. When trying to mock this out in the unit tests, I was aware that it is possible to use the 'ConfigurationBuilder' and pass in a json file to use.
	For example:
	
		private IConfiguration _config;
	
		public void Setup()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();            
        }
	
	However, the correct assembly for the IConfiguration is: Microsoft.Extensions.Configuration.IConfiguration but when attempting to use this, visual studio could not find the reference.
	Instead it tried to reference: Castle.Core.Configuration. On investigating, some solutions suggested creating a wrapper, but this looked like it would create more issues than resolve.
	
Design Issues
	Ideally i would also have liked to implement a cache service so that the most frequently accessed urls could be simply read from cache. Time constraints restricted me from doing so. This would also invlove updating the Model class. 

	
	
























