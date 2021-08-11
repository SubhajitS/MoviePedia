# MoviePedia
A Movie database with to filter through the best movies

## Web API
Following are the steps to run the API
- Having .NET Core 3.1 SDK is prerequisite
- Run with Visual Studio using IIS Express or using `dotnet run` using Krestal Server
- Current configuration is set for IIS Express and the app is supposed to be available at `https://localhost:44353`
- If the API is started with `dotnet run` then please change the endpoint property of [Enviroment.ts](MoviepediaUI/src/environments/environment.ts) to `https://localhost:5001` 
## SPA UI App
Following are the steps to follow to run the SPA app
- NPM Install with `npm ci`
- Run the App with `npm run start` or `ng s`
- Make sure you have port 4200 free
- Open url `http://localhost:4200` in your browser and Happy browsing 

