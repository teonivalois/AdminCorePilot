**Admin[Core]Pilot** is an ongoing result of my studies on vue.js. I simply cloned the [CoPilot](http://github.com/misterGF/CoPilot) project which already has an [AdminLTE](https://almsaeedstudio.com/) version organized as a [vue.js](https://vuejs.org/) project, and added some small features, along with support for an [ASP.Net core](http://dot.net/core) WebAPI backend.

Client Side

 - AdminLTE + CoPilot theme + Vue.js (thanks [@misterGF](http://github.com/misterGF/CoPilot))
 - vuejs-localization plugin (see src/lang folder)
 - DataTable component that plugs directly to an ASP.Net WebAPI controller
	 - Supporting sorting, filtering and pagination
 - Authentication against an ASP.Net core WebAPI backend using JWT
	 - Supports automatic token renewal

Server Side

- ASP.Net core WebAPI
- JSON Web Tokens (JWT) for authentication


How to run
-------------------

Client

> npm install
> 
> npm run dev

Server

> dotnet restore
> 
> dotnet run
