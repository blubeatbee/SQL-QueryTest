# README

Hello! This is a simple server application for query testing a relational database.
It utilises ASP.NET Entity Framework Core to map database objects, and a console menu I've programmed myself.

The database is already data seeded and doesn't require any additional scripting. Though you may find a script to recreate and seed the database, as well as other miscellaneous scripts, in the `Script` folder.

## Current Features

### Navigable Console Menu

Displays database rows via a navigable console menu. Because I worked with React when building it, the syntaxes are similar to React JS. Though it is lacking many features that makes building web pages a breeze in React.

- `App.cs`
    - The menu's entry point. Contains essential things which allows the menu to navigate between routes, determine what happens after a button is selected, setting initial configs, etc.
- `Core`
    - Contain service and data access objects. Without it, the menu wouldn't be able to interact with the database.
- `Components`
    - Represents contents on a menu. Every component class inherits `BaseComponent` so the `MenuReader` can display them on the console.
    - Selectable components (e.g. buttons and links) also has to inherit `BaseSelectable` which is what the menu uses to tell non-interactive and interactive components apart from each other.
- `Pages`
    - Contain every project defined page.
    - Each page inherits `BasePage` and contain a `BaseComponent` list that the `MenuReader` uses to display contents on console each time menu navigates to a `Route`.
    - Each `Page` class are highly individualistic and implements their own methods that accesses `Core` objects.
- `Routing`
    - Contains code that allows the menu to identify and display pages on `MenuReader`.
    - Each `Route` represents a `Page` and an identification. The latter is used by `App.cs` to navigate to its associated `Page`, allowing the `MenuReader` to display it on console.
    - These `Route` structs are stored in `Router` object, which serves as a navigation tree, and contains methods to navigate to each `Route` that `App.cs` uses.
- `UI`
    - Contains `MenuReader` which allows the page contents to be displayed on console.
    - Contains `Input` which acts as a submenu for user input validation.

### Data Access Layer

To access database, the server uses the Repository Pattern, specifically [Mosh Hamedani's Repository and UnitOfWork Pattern](https://www.youtube.com/watch?v=rtXpYpZdOzM). This variant wants a generic repository in addition to the regular repositories to avoid repetitive code. It also wants every repository to be fully encapsulated within, and be accessible through, a single `UnitOfWork` object, making it an efficient way to access multiple repositories via a single dependency injection.

![](Docs/Diagrams/ER_Repository_&_UnitOfWork.svg)

As the above diagram shows, these two patterns compliments each other.

1. **The Repository Pattern**: Which can be divided into one generic part and one Model specific part.
    1. The `IRepository` and its `Repository` implementation contain CRUD (with the exception of _Update_) and other basic queries that is shared and usable by all specific repositories. This part **must** be generic, hence the usage of the generic `<T>`.
    2. A specific `I*Repository` interface and its class implementation which queries against a specific Model class. The interface inherits `IRepository`, while the implementation inherits `IRepository` **and** `Repository`.
2. **The UnitOfWork Pattern**: A project specific object which stores every repository interface as properties. The `UnitOfWork` constructor must pass a `DbContext` as parameter and uses that parameter to instance each repository object.
    - The `UnitOfWork` class also implements a method to invoke `DbContext.SaveChanges`.
    - My `UnitOfWork` implementation also has a `BeginTransactionAsync()` that returns a object to be used in SQL transactions. This might not be a technically correct way to do transactions with this pattern, but due to lack of time and knowledge, it'll do for now.

#### Data Query Methods with LINQ Expressions

Previous data query methods returned an `IQueryable` and then chained `Where` clauses (e.g. `x => x.Age > age`) to filter out data rows at the service layer. While this allowed for customisable queries, it technically exposed the database structure to outside repository code, breaking the Repository Pattern's encapsulation as mentioned earlier.

Now the service layer only passes an Expression (e.g. `x => x.Age > age`) as a parameter and then recieves an already filtered data, allowing for similar levels of customisability without exposing the repository contents to the service layer. In effect, the `Where` clauses occurs at the data access layer rather than outside of it.

### Transaction

Currently, only the `Source.Services.StudentService.AddGradingToStudent` method currently uses transaction. The current implementation is a standard transaction within a `try catch` statement: if no exception is catched, the transaction will be commited, and if any exception is found, the transaction will run a rollback before the `throw`.

The transaction is implemented via `UnitOfWork` object's context rather than instancing `SqlConnection.BeginTransaction()`.

## Changes

This dev branch contains numerous changes made to the server code. Due to my own misunderstanding on how to structure a _"Database First"_-database, my previous database caused annoying technical debts.

For example, instead of having separate _Employee_ and _Student_ tables, both linked to a shared _Human_ table using their own primary key as the foreign key.
This was one of many reason that made the database:

1. a pain to fetch data from, as the server had to enter the _Human_ table first before accessing the others.
2. confusing to navigate, as no identification existed to differentiate between which _Human_ was a _Student_, or an _Employee_, or both.
3. not very robust, as adding a new _Student_ row necessitated adding an equivalent _Human_ row as well.

Therefore I've simplified the database by removing unecessary tables which should've already been columns.

- Compare the old ER diagram... ![](Docs/Diagrams/db_er_1.svg)
- ... With the current ER diagram ![](Docs/Diagrams/db_er_2.svg)
