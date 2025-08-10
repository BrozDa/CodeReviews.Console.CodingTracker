# Console Coding Tracker Project by C# Academy

Project Link: [https://www.thecsharpacademy.com/project/12/habit-logger](https://www.thecsharpacademy.com/project/13/coding-tracker)


## Project Requirements
* This application has the same requirements as the [Habit tracker](https://github.com/BrozDa/CodeReviews.Console.HabitTracker/tree/master) project, except that now you'll be logging your daily coding time.
* To show the data on the console, you should use the "Spectre.Console" library.
* You're required to have separate classes in different files (ex. UserInput.cs, Validation.cs, CodingController.cs)
* You should tell the user the specific format you want the date and time to be logged and not allow any other format.
* You'll need to create a configuration file that you'll contain your database path and connection strings.
* You'll need to create a "CodingSession" class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration
* The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.
* The user should be able to input the start and end times manually.
* You need to use Dapper ORM for the data access instead of ADO.NET. (This requirement was included in Feb/2024)
* When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.
* Follow the DRY Principle, and avoid code repetition.

## Additional Challenges
* Add the possibility of tracking the coding time via a stopwatch so the user can track the session as it happens.
* Let the users filter their coding records per period (weeks, days, years) and/or order ascending or descending.
* Create reports where the users can see their total and average coding session per period.
* Create the ability to set coding goals and show how far the users are from reaching their goal, along with how many hours a day they would have to code to reach their goal. You can do it via SQL queries or with C#.
  
## Lessons Learned
* Improved my understanding of parsing date and time and how to pass them between methods effectively.
* Learned Spectre.Console, which provided an easy and efficient way to gather and validate user input. The documentation was well-written and approachable, making it easier to learn.
* Learned how to use Dapper for object mapping. Initially, this was tricky, especially when handling time-related data, but it became clearer as I worked through it.
* Gained experience in managing dependencies using a service container. This approach allowed me to refactor classes more easily—whenever I needed to rework a class, I just had to implement the appropriate interface.

## Areas for Improvement
* I aimed to structure this project correctly, but in retrospect, it feels somewhat overengineered. While the service container effectively manages dependencies, I can't shake the feeling that it might be more than what was necessary for this project. Streamlining the architecture in future projects could be an area of focus.

## Main Resources Used
* Spectre console - https://spectreconsole.net/
* Dapper ORM - https://www.learndapper.com/
* MS docs for DateTime

