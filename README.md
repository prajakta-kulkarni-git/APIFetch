# Propylon Code Test - C# Oireachtas API

This project has a C# file `Program.cs` which defines 3 methods to
load and process a couple of the [Houses of the Oireachtas Open Data APIs][1].

Specifically, they use the data obtained from the `legislation` and `members`
api endpoints to answer the questions:

* Which bills were sponsored by a given member ?
* Which bills were last updated within a specified time period ?

You are tasked with doing one or more of the following in any order you are
comfortable with. Obviously the more you manage to get done, the better.

1. The current implementation loads previously obtained offline copy of the data
   obtained from the endpoints. Update the module to fetch the latest data from
   the api endpoint if the parameter passed is the URL to the endpoint instead
   of a filename.

2. The current implementation of the `filterBillsSponsoredBy` appears to be
   correct. It is also reasonably quick when processing the offline data.
   However, when the complete dataset obtained from the api is loaded, it is
   noticeably slower. Refactor the implementation to be faster than the current
   inefficient implementation.

3. Provide an implementation for the unimplemented function
   `filterBillsByLastUpdated`. The specification for this is documented in the 
   function's doc-string.

4. The `Main` method of the `Program` class is currently unused. The methods are
    only used in the test project. Update the project to allow a user to choose
    a filter message and input arguments through the command line.

5. Improve the code base as you would any professional quality code. This
   includes not just error checking and improving code readability but also
   adding documentation, comments where necessary, additional tests if any ...etc.

This project was designed in Visual Studio with .Net Framework 4.7.2
Microsoft offers a [community version of Visual Studio][2]
Alternatives to Visual Studio include downloading just the .Net Framework and using
the MSBuild command line tool or using [Mono Project's][3] cross platform open source tools

Feel free to ask any questions or clarifications, if required.

Wish you the best of luck !

[1] https://api.oireachtas.ie/
[2] https://visualstudio.microsoft.com/vs/community/
[3] https://www.mono-project.com/
