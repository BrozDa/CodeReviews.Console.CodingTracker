# WORK  IN PROGRESS
Current Plan of Action:
1. Read about Spectre Console
2. Read about Dapper ORM
3. Read about Configuration Manager
4. Create diagram how object should interact between each other
5. Code

- Self imposed challenge: Everything I learn needs to be only from official documentation for respective tools - no StackOverFlow, no blog posts, no videos,... - This is to enforce understanding by reading and getting more used to official documentation style which might be relevant for non-public codebases.

-----------------------------------------------------------------------------------------------------------------------------------------

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
* 
## Lessons Learned
--

## Areas for Improvement
--

## Main Resources Used
--
