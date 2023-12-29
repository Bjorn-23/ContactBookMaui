# Contact Book Project
The goal of this assignment was to create a contactbook that could add, view, update and delete contacts.
The contacts should be written to a file on disk, and should be retrieved upon starting the application.

## Structure
The project was build with a shared class librarary that both the console app and the Maui application has access to.
It implements interfaces, dependancy injection, a few SOLID principles, service pattern and MVVVM for the MAUI part.

## Funtions
The goals have been met and a user can indeed add, view, update and delete contacts to and from a file on the disk.
The location of this file is environment dependant but here are locations where they show up on my system (win 11):
C:\Users\{your username here}\AppData\Local\Contacts - ConsoleApp
C:\Users\{your username here}\AppData\Local\Packages\com.companyname.contactbookmaui_9zz4h110yvjzm\LocalCache\Local\Contacts.json - MauiApp
(NB that contactbookmaui_9zz4h110yvjzm might be named differently on your system)

The Console app and Maui app all share the same logic (PContactServices and FileRepository) and calls upon it with their specific program logic.
Therefore a user can add a common path to file on disk and the 2 programs can work upon the same list, however simultaneous use of the list with both programs has not been tested and is no advisable.
This is however not true for the Android part of the app which will not make use of a direct path to disk such as "C:\Users\{your username here}\AppData\Local\Contacts", but rather creates its own list while the app is runnning.

## Summary
A fun task in new environment ( for me, as of NOV-23) with a steep learning curve. It was highly rewardable in terms of challenge and has granted me a new skill set.
/Björn
