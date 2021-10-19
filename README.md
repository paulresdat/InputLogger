# InputLogger

## This application exists to demonstrate the basic steps to creating a .NET console application with a database domain layer.

This application demonstrates separation of concern, dependency injection as well as clean architecture by separating the "Domain" layer from the other layers of the application such as the repository layer.  It also demonstrates separation of concern by separating EF data logic from being manipulated outside of the repository layer.

This project also showcases the use of Automapper to help map entity objects to shared models, or "Data Transfer Objects" so that entities can not be inadvertently changed outside of the repository layer.

An xUnit test suite has also been stood up to demonstrate how to link in your project and directly test against the database and demonstrates some polymorphic programming with an abstract base unit testing class.
